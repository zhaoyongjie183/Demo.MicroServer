using Demo.MicroService.BusinessModel.Model.Tenant.Project;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Repository.IRepository.ITenantRepository.IPoject;
using Demo.MicroService.Repository.IRepository.ITenantRepository.ISystem;
using SqlSugar;
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
*│　创建时间：2023/3/14 16:31:24                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Repository.Repository.TenantRepository.Project                              
*│　类    名： ProjectRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Repository.Repository.TenantRepository.Project
{
    public class ProjectRepository : TenantBaseRepository<ProjectMange>, IProjectRepository
    {
        public ProjectRepository(SqlSugarClient dbContext) : base(dbContext)
        {
        }
    }
}
