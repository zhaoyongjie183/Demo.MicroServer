using SqlSugar;
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
*│　创建时间：2023/3/16 9:10:28                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Model                              
*│　类    名： SqlConneModel                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Model
{
    public class SqlConneModel
    {
        public DbType DbType { get; set; }

        public string SqlConnectionString { get; set; }
    }
}
