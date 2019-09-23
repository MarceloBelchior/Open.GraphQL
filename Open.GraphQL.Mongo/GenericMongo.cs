using MongoDB.Driver;
using System;

namespace Open.GraphQL.Mongo
{
    internal static class GenericMongo
    {
        internal static IMongoDatabase Create(string mongoUrl)
        {
            if (string.IsNullOrEmpty(mongoUrl))
                throw new Exception("Url de conexação com o mongo não informada!");

            var url = MongoUrl.Create(mongoUrl);
            var client = new MongoClient(url);

            return client.GetDatabase(url.DatabaseName);
        }
    }
}
