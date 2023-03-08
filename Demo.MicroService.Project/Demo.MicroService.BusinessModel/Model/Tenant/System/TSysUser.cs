using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.MicroService.BusinessModel.Enum;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 11:16:45                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessModel.Model.Tenant.System                              
*│　类    名： TSysUser                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessModel.Model.Tenant.System
{
    public class TSysUser: TenantBaseEntity
    {
        /// <summary>
        /// 系统用户ID
        /// </summary>
        [Key]
        public Guid TSysUserID { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 登录密码
        /// </summary>
        public string UserPassword { get; set; } = string.Empty;

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Mail { get; set; } = string.Empty;

        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatusCode UserStatus { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; } = string.Empty;

        /// <summary>
        /// 联系手机号
        /// </summary>
        public string Mobile { get; set; } = string.Empty;


        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; } = string.Empty;

      

        /// <summary>
        /// 用户登录状态
        /// </summary>
        public LoginStatusCode LoginStatus { get; set; } = LoginStatusCode.Invalid;
    }
}
