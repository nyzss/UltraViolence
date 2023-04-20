using Godot;
using System;

public partial class Main : Node
{
    PackedScene player = GD.Load<PackedScene>("res://Scenes/Player/Player.tscn");

    // public Vector2 currentPos;
    public Node2D playerPos;

    public override void _Ready()
    {
        Node2D playerNode = player.Instantiate() as Node2D;
        playerNode.GlobalPosition = new Vector2(200, 500);
        playerPos = playerNode;
        AddChild(playerNode);
    }

    public override void _Process(double delta)
    {
        // GD.Print("---------------------------");
        // GD.Print("Current Position: " + playerPos.GlobalPosition);
    }
}
