﻿using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role.States;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;
using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role;

public abstract class Role : MapObject
{
    protected Role(Position position, Map map) : base(position, map)
    {
        State = new NormalState(this);
        Hp = FullHp;
    }

    public int StatusStartTurn { get; set; }

    // TODO: <=0 remove from map
    protected abstract int FullHp { get; }
    public int Hp { get; set; }
    public abstract int AttackPower { get; }
    public StateEnum StateEnum { get; set; } = StateEnum.Normal;
    public State State { get; set; }

    protected internal void Move()
    {
        while (true)
        {
            var canMoveDirections = GetCanMoveDirections();
            var targetDirection = ChooseMoveDirection(canMoveDirections);
            var targetPosition = Position.GetMovePosition(targetDirection);

            if (IsValidMove(canMoveDirections, targetDirection))
            {
                var mapObject = Map.GetMapObjectAt(targetPosition);
                if (mapObject is not null)
                {
                    Touch(mapObject);
                }
                else
                {
                    Map.Move(this, targetPosition);
                }
            }
            else
            {
                Console.WriteLine("移動方向無效，請往別的方向移動");
                continue;
            }

            break;
        }
    }

    protected internal abstract void Attack();

    protected void Touch(MapObject mapObject)
    {
        if (mapObject is Treasure treasure)
        {
            Map.RemoveMapObjectAt(treasure.Position);
            if (treasure is SuperStar)
            {
                SetState(StateEnum.Invincible);
            }
            else if (treasure is Poison)
            {
                SetState(StateEnum.Poisoned);
            }
            else if (treasure is AcceleratingPotion)
            {
                SetState(StateEnum.Accelerated);
            }
            else if (treasure is HealingPotion)
            {
                SetState(StateEnum.Healing);
            }
            else if (treasure is DevilFruit)
            {
                SetState(StateEnum.Orderless);
            }
            else if (treasure is KingRock)
            {
                SetState(StateEnum.Stockpile);
            }
            else if (treasure is DokodemoDoor)
            {
                SetState(StateEnum.Teleport);
            }
        }

        Console.WriteLine($"{Symbol}待在 [{Position.Row},{Position.Column}] 吧你，前方有其他東西");
    }

    public abstract void RoundAction();
    public abstract void OnDamaged(int hp);

    protected void SetState(StateEnum stateEnum)
    {
        StateEnum = stateEnum;
    }

    public bool IsDead()
    {
        return Hp <= 0;
    }

    public bool IsFullHp()
    {
        return Hp >= FullHp;
    }

    public void SetState(State state)
    {
        State = state;
    }

    public IEnumerable<Direction> GetCanMoveDirections()
    {
        if (Position.IsValid(new Position(Position.Row - 1, Position.Column)))
        {
            yield return Direction.Up;
        }

        if (Position.IsValid(new Position(Position.Row, Position.Column + 1)))
        {
            yield return Direction.Right;
        }

        if (Position.IsValid(new Position(Position.Row + 1, Position.Column)))
        {
            yield return Direction.Down;
        }

        if (Position.IsValid(new Position(Position.Row, Position.Column - 1)))
        {
            yield return Direction.Left;
        }
    }

    private bool IsValidMove(IEnumerable<Direction> canMoveDirections, Direction targetDirection)
    {
        return canMoveDirections.Any(direction => direction == targetDirection);
    }

    protected abstract Direction ChooseMoveDirection(IEnumerable<Direction> canMoveDirections);
}