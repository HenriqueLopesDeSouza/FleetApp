using FleetApp.Api.Dtos.Validators;
using FleetApp.Application.Abstractions;
using FleetApp.Application.Services;
using FleetApp.Infrastructure.Repositories;
using FluentValidation;

namespace FleetApp.Api.Composition;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFleetApp(this IServiceCollection services)
    {
        services.AddSingleton<IVehicleRepository, InMemoryVehicleRepository>();
        services.AddSingleton<IVehicleService, VehicleService>();

        services.AddValidatorsFromAssemblyContaining<CreateVehicleRequestValidator>();

        return services;
    }
}