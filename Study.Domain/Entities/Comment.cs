namespace Study.Domain.Entities;

public class Comment
{
    public int postId { get; set; }
    public long Id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string body { get; set; }
}