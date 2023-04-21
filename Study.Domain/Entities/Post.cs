namespace Study.Domain.Entities;

public class Post
{
    public int userId { get; set; }
    public long Id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}