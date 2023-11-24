using Godot;
using System;

namespace Survival
{
    public partial class PointLife : Area2D
    {
        public float Lifetime = 3f;

        private double time = 0;

        [Signal]
        public delegate void pointDestroyEventHandler();

        [Signal]
        public delegate void playerEnterEventHandler();

        public override void _Ready()
        {
            Connect("pointDestroy", new Callable(GameEntry.Entity.line, "PointDestroy"));
            Connect("playerEnter", new Callable(GameEntry.Entity.line, "OnPlayerEnter"));
            BodyEntered += OnPlayerEnter;
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", false);
        }

        public void OnPlayerEnter(Node2D body)
        {
            GD.Print(body.Name + GetIndex());
            GameEntry.Entity.CreateFace("face", GetIndex());
            GameEntry.Entity.ClearPoints();
            EmitSignal("playerEnter");
        }

        public override void _Process(double delta)
        {
            time += delta;
            if (time > Lifetime)
            {
                QueueFree();
                EmitSignal("pointDestroy");
            }
        }


    }
}
