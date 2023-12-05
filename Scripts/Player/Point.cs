using Godot;
using System;

namespace Survival
{
    public partial class Point : Area2D
    {
        public float Lifetime = 3f;

        public int lineId;

        private double time = 0;

        private float disabledTime = 0.5f;

        [Signal]
        public delegate void pointDestroyEventHandler();

        [Signal]
        public delegate void playerEnterEventHandler();

        public override void _Ready()
        {
            Connect("pointDestroy", new Callable(GameEntry.Entity.GetLine(lineId), "PointDestroy"));
            // Connect("playerEnter", new Callable(GameEntry.Entity.GetLine(lineId), "OnPlayerEnter"));
            BodyEntered += OnPlayerEnter;
            BodyExited += OnPlayerExit;
        }

        public void OnPlayerEnter(Node2D body)
        {
            PlayerController player = body as PlayerController;
            // GD.Print(body.Name + GetIndex());
            // GameEntry.Entity.CreateFace("face", GetIndex());
            if (!player.IsLinking && !player.isRushing)
            {
                player.IsLinking = true;
                Line line = GameEntry.Entity.GetLine(player.currentLine);
                GameEntry.Face.LinkLines(player.currentLine, lineId, new LinkPoint(this, line.GetChild<Point>(line.GetChildCount() - 4)));
                // EmitSignal("playerEnter");
            }
        }

        public void OnPlayerExit(Node2D body)
        {
            (body as PlayerController).IsLinking = false;
        }


        public override void _Process(double delta)
        {
            time += delta;
            if (time > Lifetime)
            {
                QueueFree();
                EmitSignal("pointDestroy", this);
            }
            else if (time > disabledTime && GetNode<CollisionShape2D>("CollisionShape2D").Disabled)
            {
                GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", false);
            }

        }


    }
}
