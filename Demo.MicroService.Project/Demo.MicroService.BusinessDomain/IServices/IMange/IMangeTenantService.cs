using Demo.MicroService.BusinessModel.Model.Mange;
using Demo.MicroService.Core.Model;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/13 13:37:03                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessDomain.IServices.IMange                              
*│　类    名： IMangeTenantService                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessDomain.IServices.IMange
{
    public interface IMangeTenantService:IBaseServices
    {
        /// <summary>
        /// 新增客户
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult> AddCustomer(MangeTenant mangeTenant);

        /// <summary>
        /// 更新客户
        /// </summary>
        /// <param name="mangeTenant"></param>
        /// <returns></returns>
        Task<ResponseResult> UpdateCustomer(MangeTenant mangeTenant);
    }
}
