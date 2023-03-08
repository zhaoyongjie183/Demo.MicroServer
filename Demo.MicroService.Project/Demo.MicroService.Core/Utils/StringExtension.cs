/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 11:05:29                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Utils                              
*│　类    名： StringExtension                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Utils
{
    public static class StringExtension
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(this string value)
        {
            return value == null || value == "" || value == string.Empty || value == " " || value.Length == 0;
        }
    }
}
