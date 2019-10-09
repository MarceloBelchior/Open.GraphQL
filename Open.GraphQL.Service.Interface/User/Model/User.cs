using System;
using System.Collections.Generic;
using System.Text;

namespace Open.GraphQL.Service.Interface.User.Model
{
    public class User
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime birth { get; set; }
        public Guid salt { get; set; }
        public DateTime lastaccess { get; set; }
        public DateTime created { get; set; }
        public bool active { get; set; }
        public bool visible { get; set; }
        public bool payment { get; set; }
    }
}
