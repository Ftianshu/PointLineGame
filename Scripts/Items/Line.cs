using Godot;
using System;
using System.Collections.Generic;

namespace Survival
{
    public abstract partial class Line : Area2D
    {
        private Line2D line;
        public int lineId;
        public int faceId;

        public List<int> linkedLines = new List<int>();
        public List<LinkPoint> linkPoints = new List<LinkPoint>();

        public Node Timers;

        public float PointLifetime = 3f;
        private double time = 0;
        private int count = 0;

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
            if (lastPoint.DistanceTo(position) < 5)
                return;

            line.AddPoint(position);
            if (line.Points.Length <= 1)
            {
                lastPoint = position;
                return;
            }

            var collision = new CollisionShape2D();

            AddChild(collision);
            count++;

            var segment = new SegmentShape2D();
            segment.A = line.Points[line.Points.Length - 2];
            segment.B = line.Points[line.Points.Length - 1];
            collision.Shape = segment;
            lastPoint = position;

            Timer timer = new Timer();
            timer.WaitTime = 3f;
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


        private void PointDestroy(Point point)
        {
            // for (int i = 0; i < linkPoints.Count; i++)
            // {
            //     if (linkPoints[i].Contain(point))
            //     {
            //         if (linkPoints[i].point1 == point)
            //         {
            //             linkedLines.Remove(linkPoints[i].point2.lineId);
            //             Line line = GameEntry.Entity.GetLine(linkPoints[i].point2.lineId);
            //             line.linkedLines.Remove(lineId);
            //             line.linkPoints.Remove(linkPoints[i]);
            //         }
            //         else
            //         {
            //             linkedLines.Remove(linkPoints[i].point1.lineId);
            //             Line line = GameEntry.Entity.GetLine(linkPoints[i].point1.lineId);
            //             line.linkedLines.Remove(lineId);
            //             line.linkPoints.Remove(linkPoints[i]);
            //         }

            //         linkPoints.RemoveAt(i);

            //         //GD.Print("PointDestroy");
            //         break;
            //     }
            // }
            // if (linkPoints.Contains(point))
            // {
            //     linkPoints.Remove(point);
            //     linkedLines.Remove(point.lineId);
            // }
            // line.RemovePoint(0);
        }
    }

}
