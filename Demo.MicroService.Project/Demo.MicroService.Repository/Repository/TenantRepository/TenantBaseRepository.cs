using Demo.MicroService.Core.Model;
using Demo.MicroService.Repository.IRepository.ITenantRepository;
using SqlSugar;

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

        }
    }
}
