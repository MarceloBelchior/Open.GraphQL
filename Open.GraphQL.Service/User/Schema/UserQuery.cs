using GraphQL.Types;
using Open.GraphQL.Service.Interface.User.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open.GraphQL.GraphQL
{
    public class UserQuery : ObjectGraphType
    {
        private readonly IUserService _userService;
        public UserQuery(IUserService userService)
        {
            _userService = userService;
            Field<ListGraphType<Service.User.GraphQLTypes.UserType>>("user",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "email" }),
                    resolve: content =>
                {
                    var email = content.GetArgument<string>("email");
                    return _userService.GetUserByEmail(new Service.Interface.User.Model.User() { email = email });
                });


        }
    }
}
