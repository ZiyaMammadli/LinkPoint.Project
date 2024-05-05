using LinkPoint.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkPoint.Data.ServiceRegistirations;

public static class DatabaseRegistiration
{
    public static void AddDatabase(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<LinkPointDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("default"));
        });
    }
}
