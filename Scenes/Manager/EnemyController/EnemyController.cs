using Godot;
using System;

public partial class EnemyController : Node
{
    [Export]
    PackedScene EnemyScene;

    const float SPAWN_RADIUS = 330f;

    public override void _Ready()
    {
        Timer timer = GetNode<Timer>("Timer");
        timer.Timeout += SpawnEnemy;
    }

    public void SpawnEnemy()
    {
        Node2D player = GetTree().GetFirstNodeInGroup("Player") as Node2D;
        GD.Print("this be a test");

        Node2D enemy = EnemyScene.Instantiate<Node2D>();
        Vector2 randomDirection = Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau));
        Vector2 spawnPosition = player.GlobalPosition + (randomDirection * SPAWN_RADIUS);

        GetParent().AddChild(enemy);
        enemy.GlobalPosition = spawnPosition;
    }
}
