using Application.Services.BackgroundWorkers;
using Application.Services.ExchangeService;
using Application.Services.ImageService;
using Application.Services.Stroage;
using Infrastructure.Adapters.BackgroundWorkers.Hangfire;
using Infrastructure.Adapters.ExchangeService;
using Infrastructure.Adapters.ImageService;
using Infrastructure.Adapters.Stroage.AWS;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();
        services.AddScoped<IStroageService, AwsStroage>();
        services.AddScoped<ExchangeServiceBase, ExchangeRateAPI>();
        services.AddScoped<BackgroundWorkerBase, HangfireJobs>();

        return services;
    }
}
