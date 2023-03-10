using Demo.MicroService.Core.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/10 10:36:35                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessModel.Model.Mange                              
*│　类    名： MangeTenant                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessModel.Model.Mange
{
    [Table("MangeTenant")]
    public class MangeTenant:BaseEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int Id { get; set; }
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
        /// 租户ID
        /// </summary>

        public Guid MTenantID { get; set; }
        /// <summary>
        /// 租户编码
        /// </summary>
        public string TenantCode { get; set; }
        /// <summary>
        /// 租户DB类型
        /// </summary>

        public string TenantDBType { get; set;}
    }
}
