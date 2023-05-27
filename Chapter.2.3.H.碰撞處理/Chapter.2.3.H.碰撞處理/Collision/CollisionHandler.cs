public abstract class CollisionHandler
{
    private readonly CollisionHandler? _next;

    protected CollisionHandler(CollisionHandler? next)
    {
        _next = next;
    }

    public void Handle(Sprite sprite1, Sprite sprite2)
    {
        if (IsMatch(sprite1, sprite2))
        {
            DoHandling(sprite1, sprite2);
        }
        else
        {
            _next?.Handle(sprite1, sprite2);
        }
    }

    protected abstract bool IsMatch(Sprite sprite1, Sprite sprite2);

    protected abstract void DoHandling(Sprite sprite1, Sprite sprite2);
}