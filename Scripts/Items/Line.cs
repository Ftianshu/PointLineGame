using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Survival
{
    public abstract partial class Line : Area2D
    {
        public Line2D line;
        public int lineId;
        public int faceId;

        public List<int> linkedLines = new List<int>();
        public List<LinkPoint> linkPoints = new List<LinkPoint>();

        public Node Timers;

        public float PointLifetime = 3f;
        private double time = 0;

        private Vector2 lastPoint;

        private int DisabledPointIndex = 2;


        public override void _Ready()
        {
            line = GetNode<Line2D>("Line2D");
            Timers = GetNode<Node>("Timers");
            linkedLines.Add(lineId);
            BodyEntered += OnPlayerEnter;
            BodyExited += OnPlayerExit;
            AreaEntered += OnEnemyEnter;
            AreaExited += OnEnemyExit;
        }

        public abstract void OnEnemyEnter(Area2D area);

        public abstract void OnEnemyExit(Area2D area);

        public abstract void OnPlayerEnter(Node2D body);

        public abstract void OnPlayerExit(Node2D body);


        public void AddPoint(Vector2 position)
        {
            if (lastPoint.DistanceTo(position) < 2)
                return;

            line.AddPoint(position);
            if (line.Points.Length <= 1)
            {
                lastPoint = position;
                return;
            }

            var collision = new CollisionShape2D();

            AddChild(collision);

            var segment = new SegmentShape2D();
            segment.A = line.Points[line.Points.Length - 2];
            segment.B = line.Points[line.Points.Length - 1];
            collision.Shape = segment;
            lastPoint = position;

            Timer timer = new Timer();
            timer.WaitTime = 8f;
            timer.Autostart = true;
            timer.Timeout += DisableCollision;
            Timers.AddChild(timer);
        }

        private void DisableCollision()
        {
            CollisionShape2D collisionShape2D = GetChild<CollisionShape2D>(DisabledPointIndex);
            collisionShape2D.Disabled = true;
            line.RemovePoint(0);
            Timers.GetChild(0).QueueFree();
            DisabledPointIndex++;
        }

        public int GetPointIndex(Vector2 point)
        {
            for (int i = 0; i < line.Points.Length; i++)
            {
                if (point == line.Points[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetRecentPointIndex(Vector2 point)
        {
            float lastDistance = float.MaxValue;
            for (int i = 0; i < line.Points.Length; i++)
            {
                float distance = point.DistanceSquaredTo(line.Points[i]);
                if (distance < lastDistance)
                {
                    lastDistance = distance;
                }
                else if (distance < 50)
                {
                    return i;
                }
            }
            //错误结果
            return -1;
        }

        public Vector2 GetRecentPoint(Vector2 point)
        {
            float lastDistance = float.MaxValue;
            for (int i = 0; i < line.Points.Length; i++)
            {
                float distance = point.DistanceSquaredTo(line.Points[i]);
                if (distance < lastDistance)
                {
                    lastDistance = distance;
                }
                else if (distance < 25)
                {
                    return line.Points[i];
                }
            }
            return point;
        }


        public void ResetLine()
        {
            //直接移除玩家平面内多余的线条
            if (faceId == 0 && lineId != GameEntry.Player.playerEntity.currentLine)
            {
                GameEntry.Entity.DelLine(lineId);
            }

            for (int i = 2; i < GetChildCount(); i++)
            {
                GetChild(i).QueueFree();
            }

            foreach (Node timer in Timers.GetChildren())
            {
                timer.QueueFree();
            }

            line.Points = new List<Vector2>().ToArray();
            DisabledPointIndex = 2;
        }
    }

}
