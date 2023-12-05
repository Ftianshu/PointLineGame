using Godot;
using System;

namespace Survival
{
    public class LinkPoint
    {
        public Point point1;
        public Point point2;

        public LinkPoint(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public bool Contain(Point point)
        {
            if (point1 == point || point2 == point)
            {
                return true;
            }

            return false;
        }
    }
}
