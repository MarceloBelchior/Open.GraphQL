using Autofac.Features.AttributeFilters;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Open.GraphQL.Mongo.Users.Mappers;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Open.GraphQL.Mongo.Users.Repository
{
    public class UserRepository : Domain.Users.Interface.IUserRepository
    {
        private readonly string urlMongo;
        private readonly IMongoDatabase mongoDatabase;
        private readonly IConfigurationSection _appSettings;
        private IAsyncPolicy _circuitBreaker;

        private const string nomeCollection = "UserRepository";

        public UserRepository(IConfigurationSection appSettings, [KeyFilter("MongoUser")]IAsyncPolicy circuitBreaker)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _circuitBreaker = circuitBreaker;
            urlMongo = appSettings.GetConnectionString("mongoUrl");

            mongoDatabase = GenericMongo.Create(urlMongo);
        }

        public async Task<bool> Adicionar(Open.GraphQL.Domain.Users.Model.User user)
        {
            return await _circuitBreaker.ExecuteAsync<dynamic>(async () =>
            {
                var collection = mongoDatabase.GetCollection<Documents.User>(nomeCollection);
                var query = Builders<Documents.User>.Filter.And(Builders<Documents.User>.Filter.Eq(eq => eq.Id, user.Id));
                var result = await collection.ReplaceOneAsync(query, user.Map(),
                                                    new UpdateOptions()
                                                    {
                                                        IsUpsert = true
                                                    });
                return result.MatchedCount > 0;
            });
        }
        public async Task<bool> Excluir(Domain.Users.Model.User user)
        {
            return await _circuitBreaker.ExecuteAsync<dynamic>(async () =>
            {
                var collection = mongoDatabase.GetCollection<Documents.User>(nomeCollection);
                var filter = Builders<Documents.User>.Filter.Eq(s => s.Id, user.Id);
                var actionResult = await collection.DeleteOneAsync(filter);
                return actionResult.IsAcknowledged;
            });
        }
        public async Task<Domain.Users.Model.User> GetByemailAddress(string email)
        {
            return await _circuitBreaker.ExecuteAsync<dynamic>(async () =>
            {
                var builder = Builders<Documents.User>.Filter;
                var condition = builder.Eq(p => p.Email, email);

                var collection = mongoDatabase.GetCollection<Documents.User>(nomeCollection);
                var result = await collection.FindAsync(condition);
                return result.FirstOrDefault().Map();

            });
        }
    }
}
