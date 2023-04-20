using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 200;

    [Export]
    public int accelerationSmoothing = 25;

    public override void _Process(double delta)
    {
        // Velocity = GetVelocity() * Speed;

        Vector2 movementVector = GetVelocity().Normalized();
        Vector2 targetVelocity = movementVector * Speed;
        Velocity = Velocity.Lerp(
            targetVelocity,
            (float)(1 - Mathf.Exp(-delta * accelerationSmoothing))
        );

        // GD.Print("Speed: " + Velocity);
        MoveAndSlide();
    }

    public Vector2 GetVelocity()
    {
        float x_movement = Input.GetActionStrength("Right") - Input.GetActionStrength("Left");
        float y_movement = Input.GetActionStrength("down") - Input.GetActionStrength("up");

        return new Vector2(x_movement, y_movement);

        // Vector2 inputDirection = Input.GetVector("Left", "Right", "up", "down");
        // return inputDirection;
    }
}
