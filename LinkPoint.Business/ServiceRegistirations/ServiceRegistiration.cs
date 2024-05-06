using LinkPoint.Business.Services.Implementations;
using LinkPoint.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LinkPoint.Business.ServiceRegistirations;

public static class ServiceRegistiration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
    }
}
