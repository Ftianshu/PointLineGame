using Godot;
using System;

namespace Survival
{
    public abstract partial class Point : Area2D
    {
        public float Lifetime = 3f;

        public int lineId;

        private double time = 0;

        public abstract float disabledTime
        {
            get;
            set;
        }

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
            AreaEntered += OnEnemyEnter;
            AreaExited += OnEnemyExit;
        }

        public abstract void OnEnemyEnter(Area2D area);

        public abstract void OnEnemyExit(Area2D area);

        public abstract void OnPlayerEnter(Node2D body);

        public abstract void OnPlayerExit(Node2D body);


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
