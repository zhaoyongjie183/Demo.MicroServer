using Demo.MicroService.BusinessDomain.IServices.ITenant.IProject;
using Demo.MicroService.BusinessModel.DTO.Tenant.Project;
using Demo.MicroService.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MicroService.ProjectMicroService.Controllers.v1
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1")]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;
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
        [Authorize]
        public async Task<ResponseResult> AddProject([FromBody] ProjectDTO projectDTO)
        { 
             return await _projectService.AddProject(projectDTO);
        }
    }
}
