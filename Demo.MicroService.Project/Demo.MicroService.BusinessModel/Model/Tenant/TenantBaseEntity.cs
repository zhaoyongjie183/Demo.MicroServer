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
*│　创建时间：2023/3/8 11:15:21                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessModel.Model.Tenant                              
*│　类    名： TenantBaseEntity                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessModel.Model.Tenant
{

    public class TenantBaseEntity: BaseEntity
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid MTenantID { get; set; }
    }
}
