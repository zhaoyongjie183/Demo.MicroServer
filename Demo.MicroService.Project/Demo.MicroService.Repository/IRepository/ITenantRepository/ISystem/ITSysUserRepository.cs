using Demo.MicroService.BusinessModel.Model.Tenant.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 15:50:40                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Repository.IRepository.ITenantRepository                              
*│　类    名： ITSysUserRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Repository.IRepository.ITenantRepository.ISystem
{
    public interface ITSysUserRepository : ITenantBaseRepository<TSysUser>
    {
    }
}
