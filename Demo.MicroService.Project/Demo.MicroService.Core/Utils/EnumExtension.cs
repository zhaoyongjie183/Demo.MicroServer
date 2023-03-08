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
*│　创建时间：2023/3/8 14:07:05                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Utils                              
*│　类    名： EnumExtension                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Utils
{
    public static class EnumExtension
    {
        /// <summary>
        /// 转换类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string obj)
        {
            return (T)Enum.Parse(typeof(T), obj);
        }
    }
}
