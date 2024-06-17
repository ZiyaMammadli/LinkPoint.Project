using LinkPoint.Core.Entities;
using LinkPoint.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkPoint.Data.Contexts;

public class LinkPointDbContext:IdentityDbContext<AppUser>
{
    public LinkPointDbContext(DbContextOptions<LinkPointDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
        base.OnModelCreating(builder);
    }

    public DbSet<AppUser> AppUsers {  get; set; }  
    public DbSet<UserAbout> UserAbouts{  get; set; }  
    public DbSet<UserEducation> UserEducations {  get; set; }  
    public DbSet<UserWork> UserWorks {  get; set; }  
    public DbSet<UserInterest> UserInterests {  get; set; }  
    public DbSet<FriendShip> FriendShips {  get; set; }  
    public DbSet<Like> Likes {  get; set; }  
    public DbSet<Comment> Comments {  get; set; }  
    public DbSet<Post> Posts {  get; set; }  
    public DbSet<Video> Videos {  get; set; }  
    public DbSet<Image> Images {  get; set; }  
    public DbSet<Conversation> Conversations {  get; set; }  
    public DbSet<Message> Messages {  get; set; }  
    public DbSet<ContactMessage> ContactMessages {  get; set; }  
}
