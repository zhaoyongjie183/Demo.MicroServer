
using GraphQL.Types;

using System.Xml.Linq;
/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/28 9:04:40                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： GraphQLWebApi                              
*│　类    名： CustomerGraphType                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace GraphQLWebApi
{
    public class CustomerGraphType : ObjectGraphType<Customer>
    {
        public CustomerGraphType()
        {
            Name = "Customer";
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Customer Id");
            Field(x => x.FirstName).Description("Customer's First Name");
            Field(x => x.LastName).Description("Customer's Last Name");
            Field(x => x.Contact).Description("Customer's Contact");
            Field(x => x.Email).Description("Customer's Email");
        }
    }
}
