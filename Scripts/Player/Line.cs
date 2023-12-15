using Godot;
using System;
using System.Collections.Generic;

namespace Survival
{
    public partial class Line : Node2D
    {
        private Line2D line;
        public int lineId;
        public int faceId;

        public List<int> linkedLines = new List<int>();
        public List<LinkPoint> linkPoints = new List<LinkPoint>();

        public float PointLifetime = 3f;
        private double time = 0;
        private int count = 0;


        public override void _Ready()
        {
            line = GetNode<Line2D>("Line2D");
            linkedLines.Add(lineId);
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

        private void PointDestroy(Point point)
        {
            for (int i = 0; i < linkPoints.Count; i++)
            {
                if (linkPoints[i].Contain(point))
                {
                    if (linkPoints[i].point1 == point)
                    {
                        linkedLines.Remove(linkPoints[i].point2.lineId);
                        Line line = GameEntry.Entity.GetLine(linkPoints[i].point2.lineId);
                        line.linkedLines.Remove(lineId);
                        line.linkPoints.Remove(linkPoints[i]);
                    }
                    else
                    {
                        linkedLines.Remove(linkPoints[i].point1.lineId);
                        Line line = GameEntry.Entity.GetLine(linkPoints[i].point1.lineId);
                        line.linkedLines.Remove(lineId);
                        line.linkPoints.Remove(linkPoints[i]);
                    }

                    linkPoints.RemoveAt(i);

                    //GD.Print("PointDestroy");
                    break;
                }
            }
            // if (linkPoints.Contains(point))
            // {
            //     linkPoints.Remove(point);
            //     linkedLines.Remove(point.lineId);
            // }
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
