using Demo.MicroService.BusinessModel.Model.Mange;
using Demo.MicroService.Repository.IRepository.IMangeRepository;
using SqlSugar;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/13 13:44:16                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Repository.Repository.MangeRepository                              
*│　类    名： MangeTenantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Repository.Repository.MangeRepository
{
    public class MangeTenantRepository : BaseRepository<MangeTenant>, IMangeTenantRepository
    {
        public MangeTenantRepository(SqlSugarClient dbContext) : base(dbContext)
        {
        }
    }
}
