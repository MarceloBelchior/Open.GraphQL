﻿using System;
using System.IO;
using System.Threading;
using Autofac;
using Autofac.Features.AttributeFilters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Polly;

namespace Open.GraphQL.DI
{
  
        public class Cliente : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                var ambiente = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var config = new ConfigurationBuilder()
               xx.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .AddJsonFile(string.Format("appsettings.{0}.json", ambiente), optional: true);

                var configuration = config?.Build();
                builder.RegisterInstance(configuration);

                var canSource = new CancellationTokenSource();
                var can = canSource.Token;

                var _appSettings = configuration.GetSection("configuracao");

                builder.RegisterInstance(_appSettings).As<IConfigurationSection>();

            //    builder.Register((c, p) => new LoggerFactory().AddLog4Net("log4net.config").CreateLogger("Cliente")).As(typeof(Microsoft.Extensions.Logging.ILogger)).SingleInstance();

                builder.RegisterInstance(
                    Policy
                    .Handle<Exception>()
                    .CircuitBreakerAsync(
                        exceptionsAllowedBeforeBreaking: 10,
                        durationOfBreak: TimeSpan.FromSeconds(30))
                    ).As<IAsyncPolicy>().Keyed<IAsyncPolicy>("Favorito");

                //builder.RegisterInstance(
                //    Policy
                //    .Handle<Exception>()
                //    .CircuitBreakerAsync(
                //        exceptionsAllowedBeforeBreaking: 10,
                //        durationOfBreak: TimeSpan.FromSeconds(30))
                //    ).As<IAsyncPolicy>().Keyed<IAsyncPolicy>("Pedido");

                //var retryOMS = Policy
                //   .Handle<Exception>()
                //   .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(2))
                //   .WrapAsync(Policy.Handle<Exception>().CircuitBreakerAsync(
                //               exceptionsAllowedBeforeBreaking: 10,
                //               durationOfBreak: TimeSpan.FromSeconds(30)));

                //var retryCliente = Policy
                //   .Handle<Exception>()
                //   .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(2))
                //   .WrapAsync(Policy.Handle<Exception>().CircuitBreakerAsync(
                //               exceptionsAllowedBeforeBreaking: 10,
                //               durationOfBreak: TimeSpan.FromSeconds(30)));
                //var retryEntrega = Policy
                //   .Handle<Exception>()
                //   .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(2))
                //   .WrapAsync(Policy.Handle<Exception>().CircuitBreakerAsync(
                //               exceptionsAllowedBeforeBreaking: 10,
                //               durationOfBreak: TimeSpan.FromSeconds(30)));

                //builder.Register((c, p) => new Framework.Servico.HttpHelper(retryOMS)).As<Framework.Servico.IHttpHelper>().Keyed<Framework.Servico.IHttpHelper>("OMS").SingleInstance();
                //builder.Register((c, p) => new Framework.Servico.HttpHelper(retryCliente)).As<Framework.Servico.IHttpHelper>().Keyed<Framework.Servico.IHttpHelper>("Cliente").SingleInstance();
                //builder.Register((c, p) => new Framework.Servico.HttpHelper(retryEntrega)).As<Framework.Servico.IHttpHelper>().Keyed<Framework.Servico.IHttpHelper>("Entrega").SingleInstance();

                //builder.RegisterType<Application.Services.FavoritoService>().As<Application.Services.IFavoritoService>().SingleInstance().WithAttributeFiltering();
                //builder.RegisterType<MongoDB.Cliente.Repositories.FavoritoRepository>().As<Domain.Repositories.IFavoritoRepository>().SingleInstance().WithAttributeFiltering();

                //builder.RegisterType<Application.Services.CadastroService>().As<Application.Services.ICadastroService>().SingleInstance().WithAttributeFiltering();


                //builder.RegisterType<Application.Reagendamento.Service.ReagendamentoService>().As<Application.Reagendamento.Services.IReagendamentoService>().SingleInstance().WithAttributeFiltering();
                //builder.RegisterType<Http.Entrega.Repositories.TokStokRepository>().As<Domain.Reagendamento.ITokStokRepository>().SingleInstance().WithAttributeFiltering();

                //builder.RegisterType<Application.Services.PedidoService>().As<Application.Services.IPedidoService>().SingleInstance().WithAttributeFiltering();
                //builder.RegisterType<MongoDB.Pedido.Repositories.PedidoRepository>().As<Domain.Repositories.IPedidoRepository>().Keyed<Domain.Repositories.IPedidoRepository>("DB").SingleInstance().WithAttributeFiltering();

                //builder.RegisterType<Http.Repositories.VtexRepository>().As<Domain.Repositories.IPedidoRepository>().Keyed<Domain.Repositories.IPedidoRepository>("HTTP").SingleInstance().WithAttributeFiltering();
                //builder.RegisterType<Http.Repositories.VtexClienteRepository>().As<Domain.Repositories.IClienteRepository>().SingleInstance().WithAttributeFiltering();

                //builder.RegisterType<Application.Cliente.Services.ClienteService>().As<Application.Cliente.Services.IClienteService>().SingleInstance().WithAttributeFiltering();

                //builder.RegisterType<MongoDB.MongoHealthCheck>().As<IHealthCheck>().SingleInstance().WithAttributeFiltering();

            }
        }
    }