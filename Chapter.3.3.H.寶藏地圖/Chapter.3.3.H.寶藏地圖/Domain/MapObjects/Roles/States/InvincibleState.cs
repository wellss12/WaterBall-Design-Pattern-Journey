namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

public class InvincibleState : State
{
    public InvincibleState(Role role) : base(role)
    {
    }

    public override string Name => "無敵狀態";
    protected override int Timeliness { get; set; } = 2;

    internal override void OnDamaged(int damage)
    {
        Console.WriteLine($"{Name}不會受到傷害");
    }
}