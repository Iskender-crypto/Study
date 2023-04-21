namespace Study.Domain.Entities;

public class Photo
{
    public int albumId { get; set; }
    public long Id { get; set; }
    public string title { get; set; }
    public string url { get; set; }
    public string thumbnailUrl { get; set; }
}