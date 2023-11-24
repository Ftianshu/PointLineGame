using Godot;
using System;
using System.Collections.Generic;

namespace Survival
{
    public partial class Line : Node2D
    {
        private Line2D line;

        public float PointLifetime = 3f;
        private double time = 0;
        private int count = 0;


        public override void _Ready()
        {
            line = GetNode<Line2D>("Line2D");

        }

        private void OnPlayerEnter()
        {
            ClearPoints();
        }

        public void ClearPoints()
        {
            line.ClearPoints();
        }

        public void AddPoint(Vector2 position)
        {
            line.AddPoint(position);
            // if (line.Points.Length <= 1)
            // {
            //     return;
            // }

            // var collision = new CollisionShape2D();

            // AddChild(collision);
            // count++;

            // var segment = new SegmentShape2D();
            // segment.A = line.Points[line.Points.Length - 2];
            // segment.B = line.Points[line.Points.Length - 1];
            // collision.Shape = segment;
        }

        private void PointDestroy()
        {
            line.RemovePoint(0);
        }

        // private void UpdateCollision()
        // {
        //     for (int i = 0; i < points.Length; i++)
        //     {
        //         var segment = new SegmentShape2D();


        //     }
        // }
    }

}
