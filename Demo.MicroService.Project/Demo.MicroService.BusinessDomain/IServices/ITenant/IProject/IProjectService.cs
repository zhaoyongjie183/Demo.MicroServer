using Demo.MicroService.BusinessModel.DTO.Tenant.Project;
using Demo.MicroService.Core.Model;
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
*│　创建时间：2023/3/14 16:37:37                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessDomain.IServices.ITenant.IProject                              
*│　类    名： IProjectService                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessDomain.IServices.ITenant.IProject
{
    public interface IProjectService : IBaseServices
    {
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult> AddProject(ProjectDTO projectDTO);

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="projectDTO"></param>
        /// <returns></returns>
        Task<ResponseResult> UpdateProject(ProjectDTO projectDTO);
    }
}
