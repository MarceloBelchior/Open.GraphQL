using System;
using System.Collections.Generic;
using System.Text;

namespace Open.GraphQL.Mongo.Users.Documents
{
    public class Stock
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string id { get; set; }
        public string code { get; set; }
        public int quantity { get; set; }
        public decimal pay { get; set; }
        public decimal tax { get; set; }
        public DateTime buywhen { get; set; }
        public DateTime? sellwhen { get; set; }
        public decimal profit { get; set; }

        public string information { get; set; }

       

    }
}
