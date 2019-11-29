using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Open.GraphQL.Service.Interface.User.Interface
{
   public interface IUserService
    {
        Task<dynamic> CreatedUser(Model.User user);
        Task<dynamic> UpdateUser(Model.User user);
        Task<dynamic> DeleteUser(Model.User user);
        Task<dynamic> GetUserByEmail(Model.User user);
    }
}
