using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Repository.IRepository.ITenantRepository.ISystem;
using SqlSugar;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 15:51:36                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Repository.Repository.TenantRepository                              
*│　类    名： TSysUserRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Repository.Repository.TenantRepository.System
{
    public class TSysUserRepository : TenantBaseRepository<TSysUser>, ITSysUserRepository
    {
        public TSysUserRepository(SqlSugarClient dbContext) : base(dbContext)
        {
        }
    }
}
