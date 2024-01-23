using Godot;
using System;
using System.Linq;

namespace Survival
{
    public partial class Trail : Line2D
    {
        [Export]
        public int PointCount;

        private double time;

        public override void _Process(double delta)
        {
            if (Points.Length > PointCount)
            {
                RemovePoint(0);
            }
            DrawPoint();
        }

        private void DrawPoint()
        {
            AddPoint((Owner as Area2D).GlobalPosition);
        }

    }

}
