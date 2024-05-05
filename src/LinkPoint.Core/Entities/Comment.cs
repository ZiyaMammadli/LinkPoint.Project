namespace LinkPoint.Core.Entities;

public class Comment:BaseEntity
{
    public int PostId { get; set; }
    public string UserId { get; set; }
    public string Text { get; set; }
    public AppUser User { get; set; }
    public Post Post { get; set; }  
}
