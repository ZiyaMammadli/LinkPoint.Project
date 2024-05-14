using LinkPoint.Business.Services.Implementations;
using LinkPoint.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using LinkPoint.Business.Validators.UserAboutValidators;
using LinkPoint.Business.MappingProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;


namespace LinkPoint.Business.ServiceRegistirations;

public static class ServiceRegistiration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountSettingsService, AccountSettingsService>();
        services.AddScoped<IFriendShipService, FriendShipService>();
        services.AddScoped<IPostService, PostService>();
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
        services.AddAutoMapper(typeof(UserEducationMapProfile));
    }
}
