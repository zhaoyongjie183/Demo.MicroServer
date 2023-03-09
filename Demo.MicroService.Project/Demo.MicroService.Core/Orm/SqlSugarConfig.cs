using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Utils;
using Microsoft.Extensions.Configuration;
using Nacos.V2;
using Newtonsoft.Json;
using SqlSugar;


/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 14:04:55                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Orm                              
*│　类    名： SqlSugarConfig                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Orm
{
    public static class SqlSugarConfig
    {
        public static List<SqlFuncExternal> GetLambda()
        {
            //Lambda自定义解析
            var expMethods = new List<SqlFuncExternal>
                        {
                            new SqlFuncExternal()
                            {
                                UniqueMethodName = "ToDateFormat",
                                MethodValue = (expInfo, dbType, expContext) =>
                                {
                                    switch (dbType)
                                    {
                                        case DbType.SqlServer:
                                            return $"CONVERT (VARCHAR (10), {expInfo.Args[0].MemberName}, 121 )";

                                        case DbType.MySql:
                                            return $"DATE_FORMAT( {expInfo.Args[0].MemberName}, '%Y-%m-%d' ) ";

                                        case DbType.Sqlite:
                                            return $"date({expInfo.Args[0].MemberName})";

                                        case DbType.PostgreSQL:
                                        case DbType.Oracle:
                                            return $"to_date({expInfo.Args[0].MemberName},yyyy-MM-dd)";

                                        default:
                                            throw new Exception("未实现");
                                    }
                                }
                            },
                        };
            return expMethods;
        }

        /// <summary>
        /// 默认是SqlServer
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <returns></returns>
        public static (DbType, string) GetConnectionString(IConfiguration nacosConfigService)
        {
            var sqlConnectionOptions = new SqlConnectionOptionsModel();
            nacosConfigService.GetSection(SqlConnectionOptionsModel.SqlSugar).Bind(sqlConnectionOptions);
            if (sqlConnectionOptions.IsNullT())
                throw new Exception("配置不能为空");
            string type = sqlConnectionOptions.DbType;
            var dbType = type.ToEnum<DbType>();
            switch (dbType)
            {
                case DbType.MySql:
                    return (DbType.MySql, sqlConnectionOptions.SqlConnectionString);

                case DbType.SqlServer:
                    return (DbType.SqlServer, sqlConnectionOptions.SqlConnectionString);

                case DbType.Sqlite:
                    return (DbType.Sqlite, sqlConnectionOptions.SqlConnectionString);

                case DbType.Oracle:
                    return (DbType.Oracle, sqlConnectionOptions.SqlConnectionString);

                case DbType.PostgreSQL:
                    return (DbType.PostgreSQL, sqlConnectionOptions.SqlConnectionString);

                default:
                    return (DbType.SqlServer, sqlConnectionOptions.SqlConnectionString);
            }
        }

     
    }
}
