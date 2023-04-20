using Godot;
using System;

public partial class GameCamera : Camera2D
{
    Vector2 targetPosition = new Vector2();

    // Called when the node enters the scene tree for the first time.

    public override void _Ready()
    {
        MakeCurrent();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        AquireTarget();
        GlobalPosition = GlobalPosition.Lerp(targetPosition, (float)(1.0 - Mathf.Exp(-delta * 20)));
    }

    public void AquireTarget()
    {
        Godot.Collections.Array<Node> playerNodes = GetTree().GetNodesInGroup("Player");
        if (playerNodes.Count > 0)
        {
            // CharacterBody2D player = (CharacterBody2D)playerNodes[0];
            Node2D player = playerNodes[0] as Node2D;
            targetPosition = player.GlobalPosition;
        }
    }
}
