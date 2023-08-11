namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

public class PoisonedState : State
{
    public PoisonedState(Role role) : base(role)
    {
    }

    public override string Name => "中毒狀態";
    protected override int Timeliness { get; set; } = 3;

    protected internal override void PreRoundAction()
    {
        Role.Hp -= 15;

        if (Role.IsDead())
        {
            Role.Map.RemoveMapObject(Role);
        }
    }
}