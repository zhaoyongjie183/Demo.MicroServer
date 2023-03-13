using Omu.ValueInjecter.Injections;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/13 14:27:16                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.ValInject                              
*│　类    名： CustomValueInjection                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.ValInject
{
    public class CustomValueInjection : LoopInjection
    {
        public CustomValueInjection() : base()
        {

        }
        public CustomValueInjection(string[] ignoredProps) : base(ignoredProps)
        {

        }
        protected override bool MatchTypes(Type source, Type target)
        {
            if (source == typeof(Guid?) && target == typeof(Guid))
            {
                return true;
            }
            if (source == typeof(Guid) && target == typeof(Guid?))
            {
                return true;
            }
            return base.MatchTypes(source, target);
        }

    }
}
