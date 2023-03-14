using Demo.MicroService.BusinessDomain.IServices.ITenant;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.Application;
using Demo.MicroService.Core.HttpApiExtend;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Utils;
using Demo.MicroService.Repository.IRepository.ITenantRepository.ISystem;
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
        private IHttpAPIInvoker _httpAPIInvoker;
        public TSysUserService(ITSysUserRepository tSysUserRepository, IHttpAPIInvoker httpAPIInvoker)
        {
            this._sysUserRepository = tSysUserRepository;
            this._httpAPIInvoker = httpAPIInvoker;
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
        /// 查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="tenantCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult> QuerySysUser(string name, string password, string tenantCode)
        {
            var result=this._httpAPIInvoker.InvokeApi("http://localhost:5010/api/Tenant/QueryTenant?tenantCode="+tenantCode);
            var tenant = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseResult<Guid>>(result);
            if (tenant.IsNullT()||!tenant.IsSuccess)
                return new ResponseResult() { IsSuccess = false, Message = "租户不存在" };
          

            var sysUser = await this._sysUserRepository.Queryable().FirstAsync(x => x.UserName == name && x.UserPassword == password&&x.MTenantID==tenant.DataResult);
            return new ResponseResult<TSysUser>() { IsSuccess=!sysUser.IsNullT(), DataResult=sysUser};
        }

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ResponseResult<TSysUser>> QueryUserByName(string  name)
        {
            var sysUser = await this._sysUserRepository.Queryable().FirstAsync(x => x.UserName == name && x.IsDeleted);

            return new ResponseResult<TSysUser> { IsSuccess = true, DataResult = sysUser };
        }

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult<List<TSysUser>>> QueryUser()
        {
            var sysUser = await this._sysUserRepository.GetListAsync(x => x.MTenantID == ApplicationContext.User.MTenantId && !x.IsDeleted);
          
            return new ResponseResult<List<TSysUser>> { IsSuccess = true, DataResult = sysUser };
        }

        /// <summary>
        /// 查询客户ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseResult<TSysUser>> QueryUserById(Guid id)
        {
            var sysUser = await this._sysUserRepository.Queryable().FirstAsync(x => x.TSysUserID == id);

            return new ResponseResult<TSysUser> { IsSuccess = true, DataResult = sysUser };
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseResult> RegisterUser(TSysUser model,string tenantCode)
        {
            var result = this._httpAPIInvoker.InvokeApi("http://localhost:5010/api/Tenant/QueryTenant?tenantCode=" + tenantCode);
            var tenant = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseResult<Guid>>(result);
            if (tenant.IsNullT() || !tenant.IsSuccess)
                return new ResponseResult() { IsSuccess = false, Message = "租户不存在" };
            model.MTenantID= tenant.DataResult;
            model.TSysUserID=Guid.NewGuid();
            model.CreatedTime = DateTime.Now;
            model.CreatedUser=Guid.NewGuid();
            var data = await this._sysUserRepository.InsertReturnEntityAsync(model);

            return new ResponseResult<TSysUser>() { IsSuccess=true,Message="新增成功",DataResult = data }; ;
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

            model.UpdatedTime = DateTime.Now;
            model.UpdatedUser=Guid.NewGuid();
            var result=await this._sysUserRepository.UpdateAsync(model);

            return new ResponseResult() { IsSuccess = result, Message = result ? "修改成功" : "修改失败" };
        }
    }
}
