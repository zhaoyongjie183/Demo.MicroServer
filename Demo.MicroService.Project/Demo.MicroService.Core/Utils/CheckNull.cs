/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 11:04:40                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Utils                              
*│　类    名： CheckNull                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Utils
{
    public static class CheckNull
    {
        public static void ArgumentIsNullException<TArgument>(TArgument argument, string argumentName = "不能为空")
          where TArgument : class
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }

        public static void ArgumentIsNullException(string argument, string argumentName = "不能为空")
        {
            if (argument.IsNull())
                throw new ArgumentException(argumentName);
        }

        public static void ArgumentIsNullException(string argumentName = "不能为空")
        {
            throw new ArgumentException(argumentName);
        }

        public static void ThrowException(string argumentName = "未实现")
        {
            throw new Exception(argumentName);
        }
    }
}
