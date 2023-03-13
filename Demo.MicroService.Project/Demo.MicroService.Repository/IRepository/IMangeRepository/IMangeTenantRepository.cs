using Demo.MicroService.BusinessModel.Model.Mange;
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
*│　创建时间：2023/3/13 13:40:30                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Repository.IRepository.IMangeRepository                              
*│　类    名： IMangeTenantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Repository.IRepository.IMangeRepository
{
    public interface IMangeTenantRepository : IBaseRepository<MangeTenant>
    {
    }
}
