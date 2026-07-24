using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using Scrutor;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups;
using AEspejo.FreightQuotes.CarrierApiClient.Http;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces.ICarriers;
using AEspejo.FreightQuotes.CarrierApiClient.Rate;
using AEspejo.FreightQuotes.Shared.Constants;

namespace AEspejo.FreightQuotes.CarrierApiClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<ICarrierRateClient>()
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Client")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // Keyed rate clients, resolved per carrier by SCAC (or the mock key). Each real client is wrapped
            // in RateClientExceptionDecorator so exception handling stays a single cross-cutting concern.
            // The convention scan above cannot register keyed services, so map them explicitly here.
            services.AddKeyedScoped<ICarrierRateClient>(CarrierScacConstant.FXFE,
                (sp, _) => Decorated(sp, ActivatorUtilities.CreateInstance<FedexRateClient>(sp)));
            services.AddKeyedScoped<ICarrierRateClient>(CarrierScacConstant.UPS,
                (sp, _) => Decorated(sp, ActivatorUtilities.CreateInstance<UpsRateClient>(sp)));
            services.AddKeyedScoped<ICarrierRateClient>(CarrierScacConstant.MOCK,
                (sp, _) => Decorated(sp, ActivatorUtilities.CreateInstance<MockRateClient>(sp)));

            // Shared HTTP transport used by every carrier service.
            services.AddScoped<IApiCaller, ApiCaller>();

            // Carrier HTTP transport services (not matched by the "*Client" convention above).
            services.AddScoped<IFedexService, FedexService>();
            services.AddHttpClient(FedexService.HttpClientName);

            services.AddScoped<IUpsService, UpsService>();
            services.AddHttpClient(UpsService.HttpClientName);

            return services;
        }

        private static ICarrierRateClient Decorated(IServiceProvider sp, ICarrierRateClient inner)
            => new RateClientExceptionDecorator(inner, sp.GetRequiredService<ILogger<RateClientExceptionDecorator>>());
    }
}
