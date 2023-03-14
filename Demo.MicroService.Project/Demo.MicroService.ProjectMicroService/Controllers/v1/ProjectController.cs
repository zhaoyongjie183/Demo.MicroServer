using Demo.MicroService.BusinessDomain.IServices.ITenant.IProject;
using Demo.MicroService.BusinessModel.DTO.Tenant.Project;
using Demo.MicroService.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MicroService.ProjectMicroService.Controllers.v1
{
    /// <summary>
    /// 项目管理API
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;
        
        /// <summary>
        /// IOC
        /// </summary>
        /// <param name="projectService"></param>
        public ProjectController(IProjectService projectService)
        { 
            this._projectService = projectService;
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="projectDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> AddProject([FromBody] ProjectDTO projectDTO)
        { 
             return await _projectService.AddProject(projectDTO);
        }

        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="projectDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> UpdateProject([FromBody] ProjectDTO projectDTO)
        {
            return await _projectService.AddProject(projectDTO);
        }
    }
}
