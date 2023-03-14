using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 14:44:33                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Orm                              
*│　类    名： SqlSugarServiceCollectionExt                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Orm
{
    public static class SqlSugarServiceCollectionExt
    {
        public static IServiceCollection AddSqlSugarDao(this IServiceCollection services, Action<ConnectionConfig> configAction, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var config = new ConnectionConfig();
            configAction.Invoke(config);
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(new SqlSugarDao(config));
                    break;

                case ServiceLifetime.Scoped:
                    services.AddScoped(serviceProvider =>
                    {
                        return new SqlSugarDao(config);
                    });
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient(serviceProvider =>
                    {
                        return new SqlSugarDao(config);
                    });
                    break;

                default:
                    services.AddScoped(serviceProvider =>
                    {
                        return new SqlSugarDao(config);
                    });
                    break;
            }

            return services;
        }

        public static IServiceCollection AddSqlSugarClient<T>(this IServiceCollection services, Action<ConnectionConfig> configAction, ServiceLifetime lifetime = ServiceLifetime.Scoped) where T : SqlSugarClient
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:

                    services.AddSingleton(serviceProvider =>
                    {
                        var config = new ConnectionConfig()
                        {
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
                        configAction.Invoke(config);
                        var log = serviceProvider.GetRequiredService<ILogger<T>>();
                        var db = new SqlSugarClient(config);
                        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                        string flag = configuration["Log:sqllog"];
                        if (string.IsNullOrWhiteSpace(flag))
                        {
                            flag = "false";
                        }
                        if (flag.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            db.Ado.IsEnableLogEvent = true;
                            //SQL执行前事件
                            db.Aop.OnLogExecuting = (sql, pars) =>
                            {
                                foreach (var item in pars)
                                {
                                    sql = sql.Replace(item.ParameterName.ToString(), $"'{item.Value?.ToString()}'");
                                }
                                log.LogInformation($"执行前SQL: {sql}");
                                //log.Info(db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                            };
                            //SQL执行完事件
                            db.Aop.OnLogExecuted = (sql, pars) =>
                            {
                                foreach (var item in pars)
                                {
                                    sql = sql.Replace(item.ParameterName.ToString(), $"'{item.Value?.ToString()}'");
                                }
                                log.LogInformation($"执行后SQL: {sql}");
                            };
                            db.Aop.OnError = (exp) =>//执行SQL 错误事件
                            {
                                log.LogDebug(exp, exp.Sql);
                            };
                        }
                        else
                        {
                            db.Ado.IsEnableLogEvent = false;
                        }

                        return (T)db;
                    });
                    break;

                case ServiceLifetime.Scoped:

                    services.AddScoped(serviceProvider =>
                    {
                        var configS = new ConnectionConfig()
                        {
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
                        configAction.Invoke(configS);
                        var db = new SqlSugarClient(configS);
                        var log = serviceProvider.GetRequiredService<ILogger<T>>();
                        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                        string flag = configuration["Log:sqllog"];
                        if (string.IsNullOrWhiteSpace(flag))
                        {
                            flag = "false";
                        }
                        if (flag.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            db.Ado.IsEnableLogEvent = true;
                            //SQL执行前事件
                            db.Aop.OnLogExecuting = (sql, pars) =>
                            {
                                foreach (var item in pars)
                                {
                                    sql = sql.Replace(item.ParameterName.ToString(), $"'{item.Value?.ToString()}'");
                                }
                                log.LogInformation($"执行前SQL: {sql}");
                                //log.Info(db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                            };
                            //SQL执行完事件
                            db.Aop.OnLogExecuted = (sql, pars) =>
                            {
                                foreach (var item in pars)
                                {
                                    sql = sql.Replace(item.ParameterName.ToString(), $"'{item.Value?.ToString()}'");
                                }
                                log.LogInformation($"执行后SQL: {sql}");
                            };
                            db.Aop.OnError = (exp) =>//执行SQL 错误事件
                            {
                                log.LogDebug(exp, exp.Sql);
                            };
                        }
                        else
                        {
                            db.Ado.IsEnableLogEvent = false;
                        }

                        return (T)db;
                    });
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient(serviceProvider =>
                    {
                        var configT = new ConnectionConfig()
                        {
                            ConfigureExternalServices = new ConfigureExternalServices
                            {
                                SqlFuncServices = SqlSugarConfig.GetLambda(),
                                EntityService = (property, column) =>
                                {
                                    var attributes = property.GetCustomAttributes(true);//get all attributes 

                                    if (attributes.Any(it => it is KeyAttribute))// by attribute set primarykey
                                    {
                                        column.IsPrimarykey = true; //有哪些特性可以看 1.2 特性明细
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
                        configAction.Invoke(configT);
                        var db = new SqlSugarClient(configT);
                        var log = serviceProvider.GetRequiredService<ILogger<T>>();
                        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                        string flag = configuration["Log:sqllog"];
                        if (string.IsNullOrWhiteSpace(flag))
                        {
                            flag = "false";
                        }
                        if (flag.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            db.Ado.IsEnableLogEvent = true;
                            //SQL执行前事件
                            db.Aop.OnLogExecuting = (sql, pars) =>
                            {
                                foreach (var item in pars)
                                {
                                    sql = sql.Replace(item.ParameterName.ToString(), $"'{item.Value?.ToString()}'");
                                }
                                log.LogInformation($"执行前SQL: {sql}");
                                //log.Info(db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                            };
                            //SQL执行完事件
                            db.Aop.OnLogExecuted = (sql, pars) =>
                            {
                                foreach (var item in pars)
                                {
                                    sql = sql.Replace(item.ParameterName.ToString(), $"'{item.Value?.ToString()}'");
                                }
                                log.LogInformation($"执行后SQL: {sql}");
                            };
                            db.Aop.OnError = (exp) =>//执行SQL 错误事件
                            {
                                log.LogDebug(exp, exp.Sql);
                            };
                        }
                        else
                        {
                            db.Ado.IsEnableLogEvent = false;
                        }
                        return (T)db;
                    });
                    break;
            }
            return services;
        }

        private static TService ImplementationFactory<TService>(IServiceProvider serviceProvider, Action<ConnectionConfig> configAction) where TService : SqlSugarClient
        {
            var configT = new ConnectionConfig()
            {
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    SqlFuncServices = SqlSugarConfig.GetLambda(),
                    EntityService = (property, column) =>
                    {
                        var attributes = property.GetCustomAttributes(true);//get all attributes 

                        if (attributes.Any(it => it is KeyAttribute))// by attribute set primarykey
                        {
                            column.IsPrimarykey = true; //有哪些特性可以看 1.2 特性明细
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
            configAction.Invoke(configT);
            var db = new SqlSugarClient(configT);
            var log = serviceProvider.GetRequiredService<ILogger<TService>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            string flag = configuration["Log:sqllog"];
            if (string.IsNullOrWhiteSpace(flag))
            {
                flag = "false";
            }
            if (flag.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                db.Ado.IsEnableLogEvent = true;
                //SQL执行前事件
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    foreach (var item in pars)
                    {
                        sql = sql.Replace(item.ParameterName.ToString(), $"'{item.Value?.ToString()}'");
                    }
                    log.LogInformation($"执行前SQL: {sql}");
                    //log.Info(db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                };
                //SQL执行完事件
                db.Aop.OnLogExecuted = (sql, pars) =>
                {
                    foreach (var item in pars)
                    {
                        sql = sql.Replace(item.ParameterName.ToString(), $"'{item.Value?.ToString()}'");
                    }
                    log.LogInformation($"执行后SQL: {sql}");
                };
                db.Aop.OnError = (exp) =>//执行SQL 错误事件
                {
                    log.LogDebug(exp, exp.Sql);
                };
            }
            else
            {
                db.Ado.IsEnableLogEvent = false;
            }
            return (TService)db;
        }
    }
}
