namespace LinkPoint.Core.Entities;

public class Video:BaseEntity
{
    public string VideoUrl { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
}
