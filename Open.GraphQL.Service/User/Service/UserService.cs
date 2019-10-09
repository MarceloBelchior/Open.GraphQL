using Open.GraphQL.Domain.Users.Interface;
using Open.GraphQL.Service.Interface.User.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Open.GraphQL.Service.User.Service
{
    public class UserService : IUserService
    {
        public readonly IUserRepository userRepository;
        public UserService(IUserRepository _userRepository) => userRepository = _userRepository;

        public async Task<dynamic> CreatedUser(Interface.User.Model.User user)
        {
            return await userRepository.Adicionar(new Domain.Users.Model.User() { Email = user.email });
        }
        public async Task<dynamic> UpdateUser(Interface.User.Model.User user)
        {
            return await userRepository.GetByemailAddress(user.email);
        }
        public async Task<dynamic> DeleteUser(Interface.User.Model.User user)
        {
            return await userRepository.Excluir(new Domain.Users.Model.User() { Email = user.email });
        }

    }
}
