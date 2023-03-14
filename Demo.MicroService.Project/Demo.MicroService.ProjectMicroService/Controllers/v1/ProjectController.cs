using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MicroService.ProjectMicroService.Controllers.v1
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1")]
    public class ProjectController : ControllerBase
    {
        public ProjectController()
        { 
            
        }
    }
}
