/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 10:49:55                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Utils                              
*│　类    名： ListExtension                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Utils
{
    public static class ListExtension
    {
        /// <summary>
        /// 判断List<T>是不是为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNullT<T>(this List<T> t) where T : class
        {
            return t == null || t.Count == 0;
        }

        /// <summary>
        /// 判断对象是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNullT<T>(this T t) where T : class
        {
            return t == null;
        }

        /// <summary>
        /// 判断List<T>是不是为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNullLt<T>(this List<T> t)
        {
            return t == null || t.Count == 0;
        }

        public static bool IsNullT<T>(this IEnumerable<T> value)
        {
            if (value == null)
                return true;
            return !value.Any();
        }
    }
}
