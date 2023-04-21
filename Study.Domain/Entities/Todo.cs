namespace Study.Domain.Entities;

public class Todo
{
    public int userId { get; set; }
    public long Id { get; set; }
    public string title { get; set; }
    public bool completed { get; set; }
}