public class FireBall : IChannelSubscriber
{
    public string Name => "火球";

    public void ReceiveNewVideoNotification(Video video)
    {
        if (video.Length.TotalMinutes <= 1)
        {
            video.Channel.UnSubscribe(this);
        }
    }
}