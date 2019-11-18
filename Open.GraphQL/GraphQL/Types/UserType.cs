using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open.GraphQL.GraphQL.Types
{
    public class UserType : ObjectGraphType<Service.Interface.User.Model.User>
    {
        public UserType()//ProductReviewRepository reviewRepository, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t => t.active);
            Field(t => t.name);
            Field(t => t.email);
            Field(t => t.active).Description("If this user has active at database");
            Field(t => t.birth).Description("birth date of the user");
            Field(t => t.lastaccess).Description("Date of last access to the page");
            //Field(t => t.Price);
            //Field(t => t.Rating).Description("The (max 5) star customer rating");
            //Field(t => t.Stock);
            //Field<ProductTypeEnumType>("Type", "The type of product");

            //Field<ListGraphType<ProductReviewType>>(
            //    "reviews",
            //    resolve: context =>
            //    {
            //        var user = (ClaimsPrincipal)context.UserContext;
            //        var loader =
            //            dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, ProductReview>(
            //                "GetReviewsByProductId", reviewRepository.GetForProducts);
            //        return loader.LoadAsync(context.Source.Id);
            //    });
        }
    }
}
