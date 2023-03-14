/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/14 16:27:27                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessModel.DTO.Tenant.Project                              
*│　类    名： ProjectDTO                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessModel.DTO.Tenant.Project
{
    public class ProjectDTO
    {

        public string ProjectName { get; set; }

        public string ProjectCode { get; set; }
        public string Description { get; set; }
    }
}
