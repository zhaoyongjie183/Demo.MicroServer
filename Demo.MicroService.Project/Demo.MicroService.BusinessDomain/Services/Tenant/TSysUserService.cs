using Demo.MicroService.BusinessDomain.IServices.ITenant;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Utils;
using Demo.MicroService.Repository.IRepository.ITenantRepository.ISystem;

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
        public TSysUserService(ITSysUserRepository tSysUserRepository)
        {
            this._sysUserRepository = tSysUserRepository;
        }

        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult> DeleteUser(Guid id)
        {
            var sysUser = await this._sysUserRepository.Queryable().FirstAsync(x => x.TSysUserID == id && x.IsDeleted == false);
            if (sysUser.IsNullT())
                return new ResponseResult() { IsSuccess = false, Message = "客户信息不存在" };

            sysUser.IsDeleted = true;
            var result = await this._sysUserRepository.UpdateAsync(sysUser);
            return new ResponseResult() { IsSuccess = result, Message = result ? "删除成功" : "删除失败" };
        }

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult<TSysUser>> QueryUser(Guid id)
        {
            var sysUser = await this._sysUserRepository.Queryable().FirstAsync(x => x.TSysUserID == id && x.IsDeleted);
            if (sysUser.IsNullT())
                return new ResponseResult<TSysUser>() { IsSuccess = false, Message = "客户信息不存在" };

            return new ResponseResult<TSysUser> { IsSuccess = true, DataResult = sysUser };
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseResult> RegisterUser(TSysUser model)
        {
            var data = await this._sysUserRepository.InsertReturnEntityAsync(model);

            return new ResponseResult<TSysUser>() { DataResult = data }; ;
        }

        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult> UpdateUser(TSysUser model)
        {
            var sysUser = await this._sysUserRepository.Queryable().FirstAsync(x => x.TSysUserID == model.TSysUserID && x.IsDeleted);
            if (sysUser.IsNullT())
                return new ResponseResult<TSysUser>() { IsSuccess = false, Message = "客户信息不存在" };

            var result=await this._sysUserRepository.UpdateAsync(model);

            return new ResponseResult() { IsSuccess = result, Message = result ? "修改成功" : "修改失败" };
        }
    }
}
