using FluentValidation.AspNetCore;
using LinkPoint.Business.MappingProfiles;
using LinkPoint.Business.Services.Implementations;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Validators.UserAboutValidators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;


namespace LinkPoint.Business.ServiceRegistirations;

public static class ServiceRegistiration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountSettingsService, AccountSettingsService>();
        services.AddScoped<IFriendShipService, FriendShipService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<IConversationService, ConversationService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAdminAuthService, AdminAuthService>();
        services.AddScoped<IAdminUserService, AdminUserService>();
        services.AddScoped<IAdminPostService, AdminPostService>();
        services.AddScoped<IContactMessageService, ContactMessageService>();
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
        services.AddSignalR();
        //services.AddCors(options =>
        //{
        //    options.AddPolicy("AllowAll",
        //        builder =>
        //        {
        //            builder
        //                .AllowAnyOrigin()
        //                .AllowAnyMethod()
        //                .AllowAnyHeader();
        //        });
        //    options.AddPolicy("signalr",
        //            builder => builder
        //            .AllowAnyMethod()
        //            .AllowAnyHeader()

        //            .AllowCredentials()
        //            .SetIsOriginAllowed(hostName => true));
        //});
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(policy =>
            {
                policy.AllowCredentials();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.SetIsOriginAllowed(x => true);
            });
        });
    }
}
