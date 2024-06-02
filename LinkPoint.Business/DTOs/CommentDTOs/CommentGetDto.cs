namespace LinkPoint.Business.DTOs.CommentDTOs;

public class CommentGetDto
{
    public string UserId { get; set; }
    public int CommentId { get; set; }
    public string Text { get; set; }
    public string UserName { get; set; }
    public string UserProfileImage { get; set; }
    public string ElapsedTime { get; set; }
}
