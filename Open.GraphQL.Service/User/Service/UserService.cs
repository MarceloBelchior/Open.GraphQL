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

        public async Task<Interface.User.Model.User> CreatedUser(Interface.User.Model.User user)
        {
            var result = await userRepository.Adicionar(new Domain.Users.Model.User() { Email = user.email });
            if (result == true)
                return user;
            return null;
        }
        public async Task<Interface.User.Model.User> UpdateUser(Interface.User.Model.User user)
        {
            var result = await userRepository.GetByemailAddress(user.email);
            var i = new Interface.User.Model.User();
            i.active = result.Active;
            i.birth = result.Birth;
            i.email = result.Email;
            return i;

        }
        public async Task<bool> DeleteUser(Interface.User.Model.User user)
        {
            return await userRepository.Excluir(new Domain.Users.Model.User() { Email = user.email });
        }
        public async Task<Interface.User.Model.User> GetUserByEmail(Interface.User.Model.User user)
        {
            var result = await userRepository.GetByemailAddress(user.email);
            return new Interface.User.Model.User() { email = result.Email };
        }

    }
}
