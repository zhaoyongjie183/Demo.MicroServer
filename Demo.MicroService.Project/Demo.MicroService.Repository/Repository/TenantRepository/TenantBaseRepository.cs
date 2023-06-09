﻿using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Orm;
using Demo.MicroService.Repository.IRepository.ITenantRepository;
using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Grpc.Core.Logging;
using Microsoft.Extensions.Logging;
using Demo.MicroService.Core.Infrastructure;
using Demo.MicroService.Core.Application;
using NPOI.SS.Formula.Functions;
using Microsoft.Extensions.DependencyInjection;
using Demo.MicroService.Core.HttpApiExtend;
using Microsoft.Extensions.Configuration;
using MathNet.Numerics.RootFinding;
using Demo.MicroService.Core.Utils;
using Demo.MicroService.Core.ConsulExtend.DispatcherExtend;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/10 9:12:43                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Repository.Repository.TenantRepository                              
*│　类    名： TenantBaseRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Repository.Repository.TenantRepository
{
    public class TenantBaseRepository<TEntity> : BaseRepository<TEntity>, ITenantBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {

        public TenantBaseRepository(SqlSugarClient dbContext) : base(dbContext)
        {
            var _logger = EngineContext.ServiceProvider.GetRequiredService<ILogger<SqlSugarClient>>();
            if (ApplicationContext.User.MTenantId == Guid.Empty)
            {
                dbContext = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = "Data Source=192.168.1.6;Initial Catalog=DemoMicroService;User ID=cdms_admin;Password=fZ`glh_m",
                    DbType = SqlSugar.DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    ConfigureExternalServices = new ConfigureExternalServices
                    {
                        SqlFuncServices = SqlSugarConfig.GetLambda(),
                    }
                });
            }
            else
            {
             
                var httpAPIInvoker = EngineContext.ServiceProvider.GetRequiredService<IHttpAPIInvoker>();
                var configuration = EngineContext.ServiceProvider.GetRequiredService<IConfiguration>();
                var _abstractConsulDispatcher= EngineContext.ServiceProvider.GetRequiredService<AbstractConsulDispatcher>();
                var customerUrl = configuration["CustomerServiceUrl"] + "api/Tenant/QueryTenantConneString?tenantId=" + ApplicationContext.User.MTenantId;
                string realUrl = _abstractConsulDispatcher.MapAddress(customerUrl);
                _logger.LogInformation("数据库连接realUrl:【" + realUrl + "】");
                var result = httpAPIInvoker.InvokeApi(realUrl);
                var sqlconne = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseResult<SqlConneModel>>(result);
                if (sqlconne.IsNullT() || !sqlconne.IsSuccess || sqlconne.DataResult.IsNullT() || string.IsNullOrEmpty(sqlconne.DataResult.SqlConnectionString))
                    throw new UserThrowException("数据库链接异常");

                dbContext = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = sqlconne.DataResult.SqlConnectionString,
                    DbType = sqlconne.DataResult.DbType,
                    IsAutoCloseConnection = true,
                    ConfigureExternalServices = new ConfigureExternalServices
                    {
                        SqlFuncServices = SqlSugarConfig.GetLambda(),
                    }
                });
            }
            _logger.LogInformation($"数据库链接: {dbContext.CurrentConnectionConfig.ConnectionString}");
            _db = dbContext;
            _db.Ado.IsEnableLogEvent = true;
            //SQL执行前事件
            _db.Aop.OnLogExecuting = (sql, pars) =>
            {
                foreach (var item in pars)
                {
                    sql = sql.Replace(item.ParameterName.ToString(), $"'{item.Value?.ToString()}'");
                }
                _logger.LogInformation($"执行前SQL: {sql}");
                //log.Info(db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            };
            //SQL执行完事件
            _db.Aop.OnLogExecuted = (sql, pars) =>
            {
                foreach (var item in pars)
                {
                    sql = sql.Replace(item.ParameterName.ToString(), $"'{item.Value?.ToString()}'");
                }
                _logger.LogInformation($"执行后SQL: {sql}");
            };
            _db.Aop.OnError = (exp) =>//执行SQL 错误事件
            {
                _logger.LogDebug(exp, exp.Sql);
            };

        }
    }
}
