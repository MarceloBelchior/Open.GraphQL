using System;
using System.Collections.Generic;
using System.Text;

namespace Open.GraphQL.Service.Interface.User.Model
{
   public class Stock
    {
        public string code { get; set; }
        public int quantity { get; set; }
        public decimal pay { get; set; }
        public decimal tax { get; set; }
        public DateTime buywhen { get; set; }

        public decimal profit { get; set; }
       
        public string information { get; set; }

    }
}
