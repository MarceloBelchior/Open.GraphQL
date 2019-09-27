using System;
using System.Collections.Generic;
using System.Text;

namespace Open.GraphQL.Domain.Users.Model
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
        public bool Active { get; set; }

        public string ExternalAuth { get; set; }
        public DateTime Birth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
