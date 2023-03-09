using log4net.Core;
using log4net.Layout;
using log4net.Util;
using Newtonsoft.Json;
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
*│　创建时间：2023/3/9 10:09:23                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Utils                              
*│　类    名： LocalDiskLogLayout                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Utils
{
    public class LocalDiskLogLayout : PatternLayout
    {
        /// <summary>
        /// 应用id（用于日志标识）
        /// </summary>
        public string AppId { get; set; }


        #region Constructors
        /// <summary>
        /// Constructs a DynamicPatternLayout using the DefaultConversionPattern
        /// </summary>
        /// <remarks>
        /// <para>
        /// The default pattern just produces the application supplied message.
        /// </para>
        /// </remarks>
        public LocalDiskLogLayout()
            : base()
        {
        }

        /// <summary>
        /// Constructs a DynamicPatternLayout using the supplied conversion pattern
        /// </summary>
        /// <param name="pattern">the pattern to use</param>
        /// <remarks>
        /// </remarks>
        public LocalDiskLogLayout(string pattern)
            : base(pattern)
        {
        }
        #endregion

        #region Member Variables
        /// <summary>
        /// The header PatternString
        /// </summary>
        private PatternString m_headerPatternString = new PatternString("");

        /// <summary>
        /// The footer PatternString
        /// </summary>
        private PatternString m_footerPatternString = new PatternString("");

        private PatternConverter m_fooder;
        #endregion

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var json = JsonConvert.SerializeObject(loggingEvent);

            writer.WriteLine(json);
        }

        #region Override implementation of LayoutSkeleton


        /// <summary>
        /// 是否记录服务上下文
        /// </summary>
        public bool IncludeServerContext { get; set; }

        /// <summary>
        /// The header for the layout format.
        /// </summary>
        /// <value>the layout header</value>
        /// <remarks>
        /// <para>
        /// The Header text will be appended before any logging events
        /// are formatted and appended.
        /// </para>
        /// The pattern will be formatted on each get operation.
        /// </remarks>
        public override string Header
        {
            get
            {
                return m_headerPatternString.Format();
            }
            set
            {
                base.Header = value;
                m_headerPatternString = new PatternString(value);
            }
        }       /* property DynamicPatternLayout Header */



        /// <summary>
        /// The footer for the layout format.
        /// </summary>
        /// <value>the layout footer</value>
        /// <remarks>
        /// <para>
        /// The Footer text will be appended after all the logging events
        /// have been formatted and appended.
        /// </para>
        /// The pattern will be formatted on each get operation.
        /// </remarks>
        public override string Footer
        {
            get
            {
                return m_footerPatternString.Format();
            }
            set
            {
                base.Footer = value;
                m_footerPatternString = new PatternString(value);
            }
        }       /* property DynamicPatternLayout Footer */
        #endregion
    }
}
