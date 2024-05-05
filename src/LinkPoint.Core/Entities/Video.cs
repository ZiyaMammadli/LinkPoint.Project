namespace LinkPoint.Core.Entities;

public class Video:BaseEntity
{
    public string VideoUrl { get; set; }
    public List<Post> posts { get; set; }
}
