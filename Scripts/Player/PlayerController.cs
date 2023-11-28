using Godot;
using System;

namespace Survival
{

    public partial class PlayerController : CharacterBody2D
    {
        [Export]
        public float MaxSpeed = 400; // How fast the player will move (pixels/sec).

        public float RushSpeed = 800;

        public float Acceleration = 0.5f;

        public float DeAcceleration = 1f;

        public float RushCD = 3;
        public float RushDuration = 0.2f;

        public float turnSpeed = 0.03f;

        public Vector2 ScreenSize; // Size of the game window.

        private float velocity = 0;

        private bool isRushing = false;

        private bool isRushOk = true;

        private int currentLine;

        public override void _Ready()
        {
            //GetNode<Camera2D>("Camera2D").MakeCurrent();
            //GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();
            //PointCreatePoint = GetNode<Node2D>("PointCreatePoint");
            //GameEntry.Entity.CreateLine("line");
            Timer rushTimer = GetNode<Timer>("RushTimer");
            rushTimer.Timeout += RushFinish;
            rushTimer.WaitTime = RushCD;
            Timer rushingTimer = GetNode<Timer>("RushingTimer");
            rushingTimer.Timeout += RushingFinish;
            rushingTimer.WaitTime = RushDuration;
            currentLine = GameEntry.Face.GenerateLineId();
        }

        private void RushFinish()
        {
            isRushOk = true;
        }

        private void RushingFinish()
        {
            isRushing = false;
        }

        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;


            if (Input.IsActionPressed("rush") && isRushOk)
            {
                isRushing = true;
                isRushOk = false;
                GetNode<Timer>("RushTimer").Start();
                GetNode<Timer>("RushingTimer").Start();
            }

            if (isRushing)
            {
                Vector2 vector = Vector2.Zero;

                vector.Y = -RushSpeed * MathF.Cos(Rotation);
                vector.X = RushSpeed * MathF.Sin(Rotation);

                Velocity = vector;

                // Vector2 distance = vector * (float)delta;

                // for (int i = 0; i < 5; i++)
                // {
                //     Vector2 position = distance / 5 * i + Position;
                //     GameEntry.Entity.AddLinePoint(position);
                //     GameEntry.Entity.CreatePoint("points", position);
                // }

                MoveAndSlide();
                AddPointToLine(Position);

                return;
            }

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
            // GameEntry.Entity.AddLinePoint(Position);
            // GameEntry.Entity.CreatePoint("points", Position);

            //MoveAndCollide(Velocity * (float)delta);
            MoveAndSlide();
            AddPointToLine(Position);

            //Position += Velocity * (float)delta;


            //CallDeferred("AddPointToLine", Position);
        }

        private void AddPointToLine(Vector2 position)
        {
            // GameEntry.Entity.AddLinePoint(position);
            // GameEntry.Entity.CreatePoint("points", position);
            GameEntry.Face.AddPoint(position, (int)FaceId.PlayerFace, currentLine);
        }
    }
}
