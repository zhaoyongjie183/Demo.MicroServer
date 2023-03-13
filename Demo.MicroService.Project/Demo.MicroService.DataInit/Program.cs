// See https://aka.ms/new-console-template for more information
using Demo.MicroService.Core.Orm;
using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using static Grpc.Core.Metadata;

Console.WriteLine("Hello, World!");
ConnectionConfig connection = new ConnectionConfig()
{
    ConnectionString = "Data Source=192.168.1.6;Initial Catalog=DemoMicroService;User ID=cdms_admin;Password=fZ`glh_m",
    DbType = SqlSugar.DbType.SqlServer,
    IsAutoCloseConnection = true,
    ConfigureExternalServices = new ConfigureExternalServices
    {
        SqlFuncServices = SqlSugarConfig.GetLambda(),
        EntityService = (property, column) =>
        {
            var attributes = property.GetCustomAttributes(true);//get all attributes 

            if (attributes.Any(it => it is KeyAttribute))// by attribute set primarykey
            {
                column.IsPrimarykey = true; 
            }
            if (column.IsPrimarykey == false && new NullabilityInfoContext().Create(property).WriteState is NullabilityState.Nullable)
            {
                column.IsNullable = true;
            }
            if (column.IsPrimarykey && column.PropertyInfo.PropertyType == typeof(int))  //是id并且是int的是自增
            {
                column.IsIdentity = true;
            }
        },
        EntityNameService = (type, entity) =>
        {
            var attributes = type.GetCustomAttributes(true);
            if (attributes.Any(it => it is TableAttribute))
            {
                entity.DbTableName = (attributes.First(it => it is TableAttribute) as TableAttribute).Name;
            }
        }

    }
};
using (SqlSugarClient client = new SqlSugarClient(connection))
{

    client.Aop.OnLogExecuted = (s, p) =>
    {
        Console.WriteLine($"OnLogExecuted:输出Sql语句:{s} || 参数为：{string.Join(",", p.Select(p => p.Value))}");
    };
    client.Aop.OnError = e =>
    {
        Console.WriteLine($"OnError:Sql语句执行异常:{e.Message}");
    };
    //建库：如果不存在创建数据库存在不会重复创建 
    client.DbMaintenance.CreateDatabase();
    Console.WriteLine("创建数据库成功");
    Assembly assembly = Assembly.LoadFile(Path.Combine(AppContext.BaseDirectory, "Demo.MicroService.BusinessModel.dll"));

    Type[] typeArray = assembly.GetTypes().Where(t => !t.Name.Contains("Base") && t.Namespace.Contains("Demo.MicroService.BusinessModel.Model.Mange"))
        .ToArray();
    Console.WriteLine("开始执行创建数据表");
    //创建表：根据实体类CodeFirstTable1  (所有数据库都支持)  
    client.CodeFirst.InitTables(typeArray);
    Console.WriteLine("数据表创建完成");
    Console.ReadLine();
}