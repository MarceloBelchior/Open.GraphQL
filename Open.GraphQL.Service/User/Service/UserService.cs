using Open.GraphQL.Domain.Users.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Open.GraphQL.Service.User.Service
{
   public class UserService
    {
        public readonly IUserRepository userRepository;
        public UserService(IUserRepository _userRepository) => userRepository = _userRepository;

        public async Task<dynamic> CreatedUser(Model.User user)
        {
            return await userRepository.Adicionar(new Domain.Users.Model.User() { Email = user.email });
        }


    }
}
