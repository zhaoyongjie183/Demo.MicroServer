using Demo.MicroService.BusinessDomain.IServices.ITenant.IProject;
using Demo.MicroService.BusinessModel.DTO.Tenant.Project;
using Demo.MicroService.BusinessModel.Model.Tenant.Project;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.Application;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Utils;
using Demo.MicroService.Core.ValInject;
using Demo.MicroService.Repository.IRepository.ITenantRepository.IPoject;
using Omu.ValueInjecter;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/14 16:38:52                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessDomain.Services.Tenant.Project                              
*│　类    名： ProjectService                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessDomain.Services.Tenant.Project
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository) 
        { 
            this._projectRepository = projectRepository;
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="projectDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult> AddProject(ProjectDTO projectDTO)
        {
            ProjectMange projectMange = new ProjectMange();
            projectMange.InjectFrom<CustomValueInjection>(projectDTO);

            projectMange.MTenantID = ApplicationContext.User.MTenantId;
            projectMange.ProjectId = Guid.NewGuid();
            projectMange.CreatedTime = DateTime.Now;
            projectMange.CreatedUser = Guid.NewGuid();
            var data = await _projectRepository.InsertReturnEntityAsync(projectMange);

            return new ResponseResult<ProjectMange>() { IsSuccess = true, Message = "新增成功", DataResult = projectMange }; ;
        }

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="projectDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult> UpdateProject(ProjectDTO projectDTO)
        {
            var projectMange = await _projectRepository.Queryable().FirstAsync(x => x.MTenantID == ApplicationContext.User.MTenantId && !x.IsDeleted&&x.ProjectCode==projectDTO.ProjectCode);
            if (projectMange.IsNullT())
                return new ResponseResult() { IsSuccess = false, Message = "项目信息不存在" };

            projectMange.UpdatedTime = DateTime.Now;
            projectMange.UpdatedUser = Guid.NewGuid();
            projectMange.ProjectName= projectDTO.ProjectName;
            projectMange.Description= projectDTO.Description;
            var result = await _projectRepository.UpdateAsync(projectMange);

            return new ResponseResult() { IsSuccess = result, Message = result ? "修改成功" : "修改失败" };
        }
    }
}
