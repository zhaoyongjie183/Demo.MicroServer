using Demo.MicroService.BusinessModel.Model.Tenant.Project;
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
*│　创建时间：2023/3/14 16:29:50                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Repository.IRepository.ITenantRepository.IPoject                              
*│　类    名： IProjectRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Repository.IRepository.ITenantRepository.IPoject
{
    public interface IProjectRepository: ITenantBaseRepository<ProjectMange>
    {

    }
}
