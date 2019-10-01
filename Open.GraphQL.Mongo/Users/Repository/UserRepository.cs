using Autofac.Features.AttributeFilters;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Open.GraphQL.Mongo.Users.Repository
{
    public class UserRepository //: Domain.Repositories.IFavoritoRepository
    {
        private readonly string urlMongo;
        private readonly IMongoDatabase mongoDatabase;
        private readonly IConfigurationSection _appSettings;
        private IAsyncPolicy _circuitBreaker;

        private const string nomeCollection = "UserRepository";

        public UserRepository(IConfigurationSection appSettings, [KeyFilter("User")]IAsyncPolicy circuitBreaker)
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

                var filter = Builders<Documents.User>.Filter.Eq(s => s.Id, user.Id);
                var update = Builders<Documents.User>.Update
                                .Set(s => s.Active, user.Active)
                                .Set(s => s.Birth, user.Birth);

                UpdateResult actionResult = await collection.UpdateOneAsync(filter, update, new UpdateOptions() { IsUpsert = true });

                return actionResult.IsAcknowledged;
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
        public async Task<Favorito> GetBySKU(string clienteId, int sku)
        {
            var builder = Builders<Documents.FavoritoDocument>.Filter;
            var condition = builder.Eq(p => p.ClienteId, clienteId);
            condition = condition & builder.Eq(p => p.Sku, sku);
            var item = await GetAllByCondition(condition);
            return item.FirstOrDefault();
        }

        public async Task<IEnumerable<int>> GetAllByCliente(string clienteId)
        {
            var builder = Builders<Documents.FavoritoDocument>.Filter;
            var condition = builder.Eq(p => p.ClienteId, clienteId);

            return await _circuitBreaker.ExecuteAsync<dynamic>(async () =>
            {
                var collection = mongoDatabase.GetCollection<Documents.FavoritoDocument>(nomeCollection);
                var result = await collection.FindAsync(condition);
                return result.ToList().Select(s => s.Sku);
            });
        }

        private async Task<IEnumerable<Favorito>> GetAllByCondition(FilterDefinition<Documents.FavoritoDocument> condition)
        {
            return await _circuitBreaker.ExecuteAsync<dynamic>(async () =>
            {
                var collection = mongoDatabase.GetCollection<Documents.FavoritoDocument>(nomeCollection);
                var result = await collection.FindAsync(condition);
                return result.ToList().Map();
            });
        }
    }
}
