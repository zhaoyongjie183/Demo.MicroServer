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
*│　创建时间：2023/3/13 14:02:25                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessModel.DTO.Mange                              
*│　类    名： MangeTenantDTO                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessModel.DTO.Mange
{
    public class MangeTenantDTO
    {
        /// <summary>
        /// 数据库Ip地址
        /// </summary>
        public string DBIP { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// 数据库端口
        /// </summary>

        public string DBPort { get; set; }
        /// <summary>
        /// 数据库密码
        /// </summary>

        public string DBPwd { get; set; }

        /// <summary>
        /// 数据库用户名称
        /// </summary>

        public string DBUser { get; set; }

        /// <summary>
        /// 租户编码
        /// </summary>
        public string TenantCode { get; set; }
        /// <summary>
        /// 租户DB类型
        /// </summary>

        public string TenantDBType { get; set; }
    }
}
