using Demo.MicroService.BusinessDomain.IServices.ITenant;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Repository.IRepository;
using Demo.MicroService.Repository.IRepository.ITenantRepository.ISystem;
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
    public class TSysUserService : ITSysUserService
    {
        private ITSysUserRepository _sysUserRepository;
        public TSysUserService(ITSysUserRepository tSysUserRepository) {
            this._sysUserRepository = tSysUserRepository;
        }    
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseResult> RegisterUser(TSysUser model)
        {
            var data=await this._sysUserRepository.InsertReturnEntityAsync(model);
            ResponseResult responseResult=new ResponseResult<TSysUser>() { DataResult=data};
            return responseResult;
        }
    }
}
