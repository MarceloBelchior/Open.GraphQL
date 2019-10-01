using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Open.GraphQL.Domain.Users.Interface
{
  public  interface IUserRepository
    {
        Task<bool> Adicionar(Open.GraphQL.Domain.Users.Model.User user);
        Task<bool> Excluir(Domain.Users.Model.User user);
        Task<Domain.Users.Model.User> GetByemailAddress(string email);
    }
}
