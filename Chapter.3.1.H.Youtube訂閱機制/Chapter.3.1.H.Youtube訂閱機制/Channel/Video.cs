public class Video
{
    public Video(string title, string description, TimeSpan length)
    {
        Title = title;
        Description = description;
        Length = length;
    }

    public string Title { get; }
    public string Description { get; }
    public TimeSpan Length { get; }
    public YoutubeChannel Channel { get; set; }
}