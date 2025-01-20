using Application.Services.ImageService;
using Application.Services.Stroage;
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

        return services;
    }
}
