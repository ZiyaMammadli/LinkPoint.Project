namespace LinkPoint.Core.Entities;

public class Video:BaseEntity
{
    public int PostId { get; set; }
    public string VideoUrl { get; set; }
    public Post Post { get; set; }
}
