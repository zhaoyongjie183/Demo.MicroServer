
using GraphQL.Types;

using System.Xml.Linq;
/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/28 9:05:06                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： GraphQLWebApi                              
*│　类    名： CustomerQuery                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace GraphQLWebApi
{
    public class CustomerQuery : ObjectGraphType
    {
        //private readonly ApplicationDbContext _appContext;
        //public CustomerQuery(ApplicationDbContext appContext)
        //{
        //    this._appContext = appContext;
        //    Name = "Query";
        //    Field<ListGraphType<CustomerGraphType>>("customers", "Returns a list of Customer", resolve: context => _appContext.Customers.ToList());
        //    Field<CustomerGraphType>("customer", "Returns a Single Customer",
        //        new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Customer Id" }),
        //            context => _appContext.Customers.Single(x => x.Id == context.Arguments["id"].GetPropertyValue<int>()));
        //}
    }
}
