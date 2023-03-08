using System.ComponentModel;
using System.Runtime.Serialization;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 11:19:17                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessModel.Enum                              
*│　类    名： UserStatusCode                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessModel.Enum
{
    public enum UserStatusCode
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        [EnumMember]
        Normal = 1,
        /// <summary>
        /// 锁定
        /// </summary>
        [Description("锁定")]
        [EnumMember]
        Locked = 2,
        /// <summary>
        /// 注销
        /// </summary>
        [Description("注销")]
        [EnumMember]
        Disable = 3
    }
}
