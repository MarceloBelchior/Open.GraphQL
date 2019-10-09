using Autofac;
using Autofac.Features.AttributeFilters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Open.GraphQL.Domain.Users.Interface;
using Open.GraphQL.Mongo.Users.Repository;
using Open.GraphQL.Service.Interface.User.Interface;
using Open.GraphQL.Service.User.Service;
using Polly;
using System;
using System.IO;
using System.Threading;

namespace Open.GraphQL.DI
{

    public class GraphQL : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Variables 
            var _environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .AddJsonFile(string.Format("appsettings.{0}.json", _environment), optional: true);
            #endregion
            #region config
            var configuration = config?.Build();
            builder.RegisterInstance(configuration);

            var canSource = new CancellationTokenSource();
            var can = canSource.Token;

            var _appSettings = configuration.GetSection("configuration");
            builder.RegisterInstance(_appSettings).As<IConfigurationSection>();
            #endregion

            builder.RegisterInstance(
                Policy.Handle<Exception>().CircuitBreakerAsync(exceptionsAllowedBeforeBreaking: 10,
                durationOfBreak: TimeSpan.FromSeconds(30))).As<IAsyncPolicy>().Keyed<IAsyncPolicy>("MongoUser");

            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance().WithAttributeFiltering();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance().WithAttributeFiltering();

            builder.RegisterType<Open.GraphQL.Mongo.MongoHealthCheck>().As<IHealthCheck>().SingleInstance().WithAttributeFiltering();

        }
    }
}
