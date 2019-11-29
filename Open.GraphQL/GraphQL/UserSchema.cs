﻿using GraphQL;
using GraphQL.Types;


namespace Open.GraphQL.GraphQL
{
    public class UserSchema : Schema
    {
        public UserSchema(IDependencyResolver resolver) : base(resolver)
        {

            Query = resolver.Resolve<UserQuery>();
            // Mutation = resolver.Resolve<CarvedRockMutation>();
        }
    }
}