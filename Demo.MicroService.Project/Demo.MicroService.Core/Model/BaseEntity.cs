/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 10:09:15                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Model                              
*│　类    名： BaseEntity                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Model
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// 并发控制时间戳
        /// </summary>
        public DateTime? RowVersion { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

 
        /// <summary>
        /// 创建者ID
        /// </summary>
        public Guid? CreatedUser { get; set; }


        /// <summary>
        /// 上次修改时间
        /// </summary>
       
        public DateTime? UpdatedTime { get; set; }
       

        /// <summary>
        /// 上次修改者ID
        /// </summary>
       
        public Guid? UpdatedUser { get; set; }

     

        /// <summary>
        /// 表操作版本
        /// </summary>
        public long TableOperateVersion { get; set; }
    }
}
