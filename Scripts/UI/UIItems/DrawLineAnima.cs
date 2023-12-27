using Godot;
using System;

public partial class DrawLineAnima : Node2D
{
    private Sprite2D point;
    private Line2D line;

    private AnimationPlayer animation;

    public override void _Ready()
    {
        point = GetNode<Sprite2D>("Sprite2D");
        line = GetNode<Line2D>("Line2D");
        animation = GetNode<AnimationPlayer>("AnimationPlayer");
        //animation.CurrentAnimation = "DrawLine";

    }

    public override void _Process(double delta)
    {
        line.AddPoint(point.Position);
    }

    public void SetAnimation(string AnimaName)
    {
        animation.CurrentAnimation = AnimaName;
    }

    public void Stop()
    {
        animation.Stop();
    }
}
