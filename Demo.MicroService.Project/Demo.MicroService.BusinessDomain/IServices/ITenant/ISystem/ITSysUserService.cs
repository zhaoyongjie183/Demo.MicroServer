using Demo.MicroService.BusinessModel.Model.Tenant.System;
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
*│　创建时间：2023/3/8 15:47:18                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessDomain.IServices.ITenant                              
*│　类    名： ITSysUserService                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessDomain.IServices.ITenant.ISystem
{
    public interface ITSysUserService : IBaseServices
    {
        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseResult<List<TSysUser>>> QueryUser();

        /// <summary>
        /// 查询客户根据ID
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<TSysUser>> QueryUserById(Guid id);

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResponseResult> RegisterUser(TSysUser model, string tenantCode);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResponseResult> UpdateUser(TSysUser model);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResponseResult> DeleteUser(Guid id);

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="tenantCode"></param>
        /// <returns></returns>
        Task<ResponseResult> QuerySysUser(string name, string password, string tenantCode);

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ResponseResult<TSysUser>> QueryUserByName(string name);

    }
}
