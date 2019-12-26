using GraphQL;
using GraphQL.Types;


namespace Open.GraphQL.QGL.Schema
{
    public class UserSchema : GraphQL.Types. Schema
    {
        public UserSchema(IDependencyResolver resolver) : base(resolver)
        {

            Query = resolver.Resolve<UserQuery>();
            // Mutation = resolver.Resolve<CarvedRockMutation>();
        }
    }
}