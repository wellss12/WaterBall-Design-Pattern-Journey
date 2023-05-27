public class WaterBall : IChannelSubscriber
{
    public string Name => "水球";

    public void ReceiveNewVideoNotification(Video video)
    {
        if (video.Length.TotalMinutes >= 3)
        {
            Console.WriteLine($"{Name} 對影片 \"{video.Title}\" 按讚。");
        }
    }
}