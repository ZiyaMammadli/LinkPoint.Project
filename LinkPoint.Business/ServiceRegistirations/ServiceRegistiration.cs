using LinkPoint.Business.Services.Implementations;
using LinkPoint.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using LinkPoint.Business.Validators.UserAboutValidators;


namespace LinkPoint.Business.ServiceRegistirations;

public static class ServiceRegistiration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddHttpContextAccessor();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IUrlHelper>(x =>
        {
            var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
            var factory = x.GetRequiredService<IUrlHelperFactory>();
            return factory.GetUrlHelper(actionContext);
        });
        services.AddControllers().AddFluentValidation(opt =>
        {
            opt.RegisterValidatorsFromAssembly(typeof(UserAboutPutValidator).Assembly);
        });       
    }
}
