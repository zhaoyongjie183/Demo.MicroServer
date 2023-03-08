// See https://aka.ms/new-console-template for more information
using SqlSugar;
using System.Data;
using System.Reflection;

Console.WriteLine("Hello, World!");
ConnectionConfig connection = new ConnectionConfig()
{
    ConnectionString = "Data Source=192.168.1.6;Initial Catalog=DemoMicroService;User ID=cdms_admin;Password=fZ`glh_m",
    DbType = SqlSugar.DbType.SqlServer,
    IsAutoCloseConnection = true
};
using (SqlSugarClient client = new SqlSugarClient(connection))
{
    //建库：如果不存在创建数据库存在不会重复创建 
    client.DbMaintenance.CreateDatabase();
    Console.WriteLine("创建数据库成功");
    Assembly assembly = Assembly.LoadFile(Path.Combine(AppContext.BaseDirectory, "Demo.MicroService.BusinessModel.dll"));

    Type[] typeArray = assembly.GetTypes().Where(t => !t.Name.Contains("Base") && t.Namespace.Contains("Demo.MicroService.BusinessModel.Model"))
        .ToArray();
    Console.WriteLine("开始执行创建数据表");
    //创建表：根据实体类CodeFirstTable1  (所有数据库都支持)  
    client.CodeFirst.InitTables(typeArray);
    Console.WriteLine("数据表创建完成");
    Console.ReadLine();
}