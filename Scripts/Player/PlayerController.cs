using Godot;
using System;

namespace Survival
{

    public partial class PlayerController : CharacterBody2D
    {
        public float MaxSpeed = 400; // How fast the player will move (pixels/sec).

        public float RushSpeed = 800;

        public bool IsLinking = false;

        public float Acceleration = 0.5f;

        public float DeAcceleration = 1f;

        public float RushCD = 3;
        public float RushDuration = 0.2f;

        public float turnSpeed = 0.03f;

        public Vector2 ScreenSize; // Size of the game window.

        private float velocity = 0;

        public bool isRushing = false;

        private bool isRushOk = true;

        public int currentLine;

        [Signal]
        public delegate void playerExpChangedEventHandler();

        [Signal]
        public delegate void playerHPChangedEventHandler();

        public override void _Ready()
        {
            Timer rushTimer = GetNode<Timer>("RushTimer");
            rushTimer.Timeout += RushFinish;
            rushTimer.WaitTime = RushCD;
            Timer rushingTimer = GetNode<Timer>("RushingTimer");
            rushingTimer.Timeout += RushingFinish;
            rushingTimer.WaitTime = RushDuration;
            currentLine = GameEntry.Entity.CreatePlayerLine();
        }

        private void RushFinish()
        {
            isRushOk = true;
        }

        private void RushingFinish()
        {
            isRushing = false;
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", false);
        }

        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;

            // if (Input.IsActionPressed("rush") && isRushOk)
            // {
            //     isRushing = true;
            //     isRushOk = false;
            //     currentLine = GameEntry.Entity.CreatePlayerLine();
            //     GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
            //     GetNode<Timer>("RushTimer").Start();
            //     GetNode<Timer>("RushingTimer").Start();
            // }

            // if (isRushing)
            // {
            //     Vector2 vector = Vector2.Zero;

            //     vector.Y = -RushSpeed * MathF.Cos(Rotation);
            //     vector.X = RushSpeed * MathF.Sin(Rotation);

            //     Velocity = vector;

            //     MoveAndSlide();

            //     return;
            // }

            if (Input.IsActionPressed("speed_up"))
            {
                if (velocity < MaxSpeed)
                    velocity += Acceleration;
            }

            if (Input.IsActionPressed("speed_down"))
            {
                if (velocity > 0)
                    velocity -= DeAcceleration;
            }

            if (Input.IsActionPressed("turn_left"))
            {
                Rotate(-turnSpeed);
            }

            if (Input.IsActionPressed("turn_right"))
            {
                Rotate(turnSpeed);
            }


            Vector2 vector2 = Vector2.Zero;

            vector2.Y = -velocity * MathF.Cos(Rotation);
            vector2.X = velocity * MathF.Sin(Rotation);

            Velocity = vector2;

            MoveAndSlide();
            GameEntry.Entity.AddPlayerLinePoint(Position, currentLine);
            //GetNode<Camera2D>("Camera2D").Position = Position;
        }
    }
}
