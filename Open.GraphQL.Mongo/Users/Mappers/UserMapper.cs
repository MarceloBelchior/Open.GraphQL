using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Open.GraphQL.Mongo.Users.Mappers
{
    internal static class UserMapper
    {
        private static IMapper mapper;
        static UserMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Documents.User, Domain.Users.Model.User>().ReverseMap();
            });
            mapper = config.CreateMapper();
        }
        internal static Domain.Users.Model.User Map(this Documents.User user)
        {
            if (user == null) return null;
            var _result = mapper.Map<Documents.User, Domain.Users.Model.User>(user);
            return _result;
        }

        internal static Documents.User Map(this Domain.Users.Model.User user)
        {
            if (user == null) return null;
            var _result = mapper.Map<Domain.Users.Model.User, Documents.User>(user);
            return _result;
        }
    }
}
