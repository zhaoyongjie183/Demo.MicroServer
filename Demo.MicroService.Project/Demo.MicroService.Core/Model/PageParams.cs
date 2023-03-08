/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 10:57:54                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Model                              
*│　类    名： PageParams                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Model
{
    public class PageParams
    {
        /// <summary>
        /// 10*(2-1)
        /// 页码*页面显示行数=offset
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 页面显示行数
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string order { get; set; }

        public string search { get; set; }
        public string _ { get; set; }
        public string datemin { get; set; }
        public string datemax { get; set; }
        public string keyword { get; set; }
    }
}
