namespace NotificationDotNet6SignalR.Models;

public class Article
{
    public string Heading { get; set; }

    public string Content { get; set; }

    public Guid userId { get; set; }
}