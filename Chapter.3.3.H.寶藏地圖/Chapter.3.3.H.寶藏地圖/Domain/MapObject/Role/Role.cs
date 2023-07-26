using Chapter._3._3.H.寶藏地圖.Domain.MapObject.Treasures;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObject.Role;

public abstract class Role : MapObject
{
    protected Role(Position position, Map.Map map) : base(position, map)
    {
    }

    public int StatusStartTurn { get; set; }
    public abstract int Hp { get; protected set; }
    public State State { get; set; } = State.Normal;

    protected abstract void Move();

    protected abstract void Attack();

    protected void Touch(MapObject mapObject)
    {
        if (mapObject is Treasure treasure)
        {
            Map.RemoveMapObjectAt(treasure.Position);
            if (treasure is SuperStar)
            {
                SetState(State.Invincible);
            }
            else if (treasure is Poison)
            {
                SetState(State.Poisoned);
            }
            else if (treasure is AcceleratingPotion)
            {
                SetState(State.Accelerated);
            }
            else if (treasure is HealingPotion)
            {
                SetState(State.Healing);
            }
            else if (treasure is DevilFruit)
            {
                SetState(State.Orderless);
            }
            else if (treasure is KingRock)
            {
                SetState(State.Stockpile);
            }
            else if (treasure is DokodemoDoor)
            {
                SetState(State.Teleport);
            }
        }

        Console.WriteLine($"{Symbol}待在 [{Position.Row},{Position.Column}] 吧你，前方有其他東西");
    }

    public abstract void TakeTurn();
    public abstract void Damage(int hp);

    protected void SetState(State state)
    {
        State = state;
    }
}