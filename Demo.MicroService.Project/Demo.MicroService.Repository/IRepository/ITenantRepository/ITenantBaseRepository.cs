using Demo.MicroService.Core.Model;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
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
*│　创建时间：2023/3/10 9:17:02                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Repository.IRepository.ITenantRepository                              
*│　类    名： ITenantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Repository.IRepository.ITenantRepository
{
    public interface ITenantBaseRepository<T>: IBaseRepository<T> where T : BaseEntity, new()
    {

    }
}
