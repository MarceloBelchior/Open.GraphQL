using Autofac;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Open.GraphQL.Mongo;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace Open.GraphQL
{
    public class Startup
    {
        public IConfiguration _config { get; }
        private readonly IHostingEnvironment _env;
        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new Open.GraphQL.DI.GraphQL());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Acesso", policyUser =>
                {
                    policyUser.RequireClaim("Acesso", "CanRead");
                });
            });
            services.AddHealthChecks()
                  .AddCheck<MongoHealthCheck>("mongodb",
                                               failureStatus: HealthStatus.Unhealthy,
                                               tags: new[] { "elasticsearch" });
          //  services.AddScoped<CarvedRockSchema>();

            services.AddGraphQL(o => { o.ExposeExceptions = _env.IsDevelopment(); })
                .AddGraphTypes(ServiceLifetime.Scoped).AddUserContextBuilder(httpContext => httpContext.User)
                .AddDataLoader();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseGraphQL<GraphQL.UserSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
