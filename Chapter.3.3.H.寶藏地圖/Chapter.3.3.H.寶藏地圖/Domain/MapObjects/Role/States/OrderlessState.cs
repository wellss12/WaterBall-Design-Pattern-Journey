﻿namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role.States;

public class OrderlessState : State
{
    public OrderlessState(Role role) : base(role)
    {
        throw new NotImplementedException();
    }

    public override string Name => "混亂狀態";
    protected override int Timeliness { get; set; } = 3;

    protected override void RoundAction()
    {
        Console.WriteLine($"{Name}:只能上下或左右移動");
        Role.Move();
    }

    protected override IEnumerable<Direction> GetCanMoveDirections()
    {
        var canMoveDirections = base.GetCanMoveDirections();
        var random = new Random();
        var next = random.Next(0, 1);
        var allowMoveDirections = next switch
        {
            0 => new List<Direction> {Direction.Up, Direction.Down},
            1 => new List<Direction> {Direction.Left, Direction.Right}
        };

        return canMoveDirections.Intersect(allowMoveDirections);
    }
}