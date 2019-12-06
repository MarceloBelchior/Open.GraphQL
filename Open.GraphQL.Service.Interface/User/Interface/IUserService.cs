using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Open.GraphQL.Service.Interface.User.Interface
{
   public interface IUserService
    {
        Task<Model.User> CreatedUser(Model.User user);
        Task<Model.User> UpdateUser(Model.User user);
        Task<bool> DeleteUser(Model.User user);
        Task<Model.User> GetUserByEmail(Model.User user);
    }
}
