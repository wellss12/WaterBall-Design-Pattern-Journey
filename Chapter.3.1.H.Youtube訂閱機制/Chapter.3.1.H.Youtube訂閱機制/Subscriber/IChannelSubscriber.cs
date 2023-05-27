public interface IChannelSubscriber
{
    public string Name { get; }

    public void ReceiveNewVideoNotification(Video video);
}