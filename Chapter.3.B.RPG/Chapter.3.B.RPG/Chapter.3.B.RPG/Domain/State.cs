namespace Chapter._3.B.RPG.Domain;

public abstract class State
{
    protected abstract string Name { get; }
    public override string ToString() => $"State: {Name}";
}

public class NormalState : State
{
    protected override string Name => "正常";
}

public class PetrochemicalState : State
{
    protected override string Name => "石化";
}

public class PoisonedState : State
{
    protected override string Name => "中毒";
}

public class CheerupState : State
{
    protected override string Name => "受到鼓舞";
}