using Demo.MicroService.BusinessDomain.IServices.ITenant;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Repository.IRepository;
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
*│　创建时间：2023/3/8 15:48:17                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessDomain.Services.Tenant                              
*│　类    名： TSysUserService                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessDomain.Services.Tenant
{
    internal class TSysUserService : BaseServices<TSysUser>, ITSysUserService
    {
        public TSysUserService(IBaseRepository<TSysUser> repository) : base(repository)
        {
        }
    }
}
