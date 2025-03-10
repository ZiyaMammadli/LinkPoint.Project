﻿using LinkPoint.Core.Repositories;
using LinkPoint.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LinkPoint.Data.ServiceRegistirations;

public static class RepositoryRegistiration
{
    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IUserAboutRepository, UserAboutRepository>();
        services.AddScoped<IUserEducationRepository, UserEducationRepository>();
        services.AddScoped<IUserWorkRepository, UserWorkRepository>();
        services.AddScoped<IUserInterestRepository, UserInterestRepository>();
        services.AddScoped<IFriendShipRepository, FriendShipRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IVideoRepository, VideoRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
    }
}
