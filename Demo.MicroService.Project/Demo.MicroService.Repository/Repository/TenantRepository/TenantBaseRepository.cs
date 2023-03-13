using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Orm;
using Demo.MicroService.Repository.IRepository.ITenantRepository;
using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    public class TenantBaseRepository<TEntity> : BaseRepository<TEntity>,ITenantBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {

        public TenantBaseRepository(SqlSugarClient dbContext) : base(dbContext)
        {
          
            dbContext = new SqlSugarClient(new ConnectionConfig() {
                ConnectionString = "Data Source=192.168.1.6;Initial Catalog=Tenant_T001;User ID=cdms_admin;Password=fZ`glh_m",
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
            });
            _db = dbContext;
            
        }
    }
}
