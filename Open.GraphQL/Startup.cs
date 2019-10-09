using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace Open.GraphQL
{
    public class Startup
    {


        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;


        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
        public void ConfigureServices(IServiceCollection services)
        {
            
            //services.AddMvc()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //    .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            ////services.AddHealthChecks();

            //services.AddHealthChecks()
            //      .AddCheck<MongoDB.MongoHealthCheck>("mongodb",
            //                                   failureStatus: HealthStatus.Unhealthy,
            //                                   tags: new[] { "elasticsearch" });





            //services.AddSwaggerGen(s => {
            //    s.SwaggerDoc("v1", new Info { Title = "Catalogo.WebApi", Version = "V1" });
            //    s.EnableAnnotations();
            //});

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
