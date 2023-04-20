using System.Diagnostics.Tracing;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using Godot;
using System.Linq;

public partial class HammerAbilityController : Node
{
    [Export]
    PackedScene HammerAbility;

    [Export]
    public float MAX_RANGE = 150f;

    public override void _Ready()
    {
        Timer timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout;
    }

    public void OnTimerTimeout()
    {
        var player = GetTree().GetFirstNodeInGroup("Player") as Node2D;
        if (player == null)
        {
            return;
        }
        var enemies = GetTree().GetNodesInGroup("Enemy").Cast<Node2D>().ToArray<Node2D>();

        if (enemies.Length == 0)
        {
            return;
        }

        enemies = enemies
            .AsQueryable()
            .Where(
                enemy =>
                    enemy.GlobalPosition.DistanceSquaredTo(player.GlobalPosition)
                    < Mathf.Pow(MAX_RANGE, 2)
            )
            .ToArray();
        enemies = enemies
            .OrderBy(enemy =>
            {
                var distance = enemy.GlobalPosition.DistanceSquaredTo(player.GlobalPosition);
                return distance;
            })
            .ToArray();

        // cast c# array into a godot array. if it helps performance use this.
        // var em = new Godot.Collections.Array<Node2D>(enemies);

        GD.Print("Enemies: " + enemies);

        Node2D hammer = HammerAbility.Instantiate<Node2D>();
        if (enemies.Length == 0)
        {
            return;
        }
        hammer.GlobalPosition = enemies[0].GlobalPosition;

        //something about adding a random offset but I don't need it cause I'm smart
        // hammer.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau)) * 4;

        Vector2 enemyDirection = player.GlobalPosition - enemies[0].GlobalPosition;
        hammer.Rotation = enemyDirection.Angle();

        player.GetParent().AddChild(hammer);
    }
}
