using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D
{
    [Export]
    public int MAX_SPEED = 75;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Area2D area = GetNode<Area2D>("Area2D");
        area.AreaEntered += OnAreaEntered;
    }

    public void OnAreaEntered(Area2D otherArea)
    {
        GD.Print("area entered");
        QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Vector2 direction = GetDirectionToPlayer();
        Velocity = direction * MAX_SPEED;
        MoveAndSlide();
    }

    public Vector2 GetDirectionToPlayer()
    {
        Node2D playerNode = GetTree().GetFirstNodeInGroup("Player") as Node2D;
        if (playerNode != null)
        {
            return (playerNode.GlobalPosition - GlobalPosition).Normalized();
        }
        else
        {
            return new Vector2();
        }
    }
}
