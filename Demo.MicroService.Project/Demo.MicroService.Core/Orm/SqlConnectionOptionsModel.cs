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
*│　创建时间：2023/3/8 15:32:56                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Orm                              
*│　类    名： SqlConnectionModel                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Orm
{
    public class SqlConnectionOptionsModel
    {
        public const string SqlSugar = "SqlSugar";
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// 数据库链接
        /// </summary>
        public string SqlConnectionString { get; set; }
    }
}
