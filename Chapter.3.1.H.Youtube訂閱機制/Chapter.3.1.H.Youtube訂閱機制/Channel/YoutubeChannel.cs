public class YoutubeChannel
{
    public YoutubeChannel(string name)
    {
        Name = name;
    }

    public string Name { get; }
    private List<IChannelSubscriber> Subscribers { get; } = new();
    public List<Video> Videos { get; } = new();

    public void Upload(Video video)
    {
        Videos.Add(video);
        video.Channel = this;
        Console.WriteLine($"頻道 {Name} 上架了一則新影片 \"{video.Title}\"。");

        Notify(video);
    }

    private void Notify(Video video)
    {
        Subscribers
            .ToList()
            .ForEach(subscriber => subscriber.ReceiveNewVideoNotification(video));
    }

    public void Subscribe(IChannelSubscriber subscriber)
    {
        Subscribers.Add(subscriber);
        Console.WriteLine($"{subscriber.Name} 訂閱了 {Name}。");
    }

    public void UnSubscribe(IChannelSubscriber subscriber)
    {
        Subscribers.Remove(subscriber);
        Console.WriteLine($"{subscriber.Name} 解除訂閱了 {Name}。");
    }
}