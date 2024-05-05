namespace LinkPoint.Core.Entities;

public class Like:BaseEntity
{
    public string UserId { get; set; } 
    public int PostId { get; set; }
    public AppUser User { get; set; }
    public Post Post { get; set; }
}
