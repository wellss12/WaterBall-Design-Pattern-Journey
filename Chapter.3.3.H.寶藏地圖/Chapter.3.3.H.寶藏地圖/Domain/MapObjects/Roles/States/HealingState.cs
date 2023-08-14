namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

public class HealingState : State
{
    public HealingState(Role role) : base(role)
    {
    }

    public override string Name => "恢復狀態";
    protected override int Timeliness { get; set; } = 5;

    internal override void PreRoundAction()
    {
        Role.Hp += 30;
        Console.WriteLine($"{Role.Symbol} 的 hp 回復 {30}");

        if (Role.IsFullHp())
        {
            Console.WriteLine($"{Role} 已滿血");
            Role.SetState(new NormalState(Role));
        }
    }
}