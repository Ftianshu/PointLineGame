using Godot;
using System;

namespace Survival
{
    public class LinkPoint
    {
        public Vector2 point1;
        public Vector2 point2;

        public int lineId1;

        public int lineId2;

        public LinkPoint(int lineId1, int lineId2, Vector2 point1, Vector2 point2)
        {
            this.lineId1 = lineId1;
            this.lineId2 = lineId2;
            this.point1 = point1;
            this.point2 = point2;
        }

        public bool Contain(Vector2 point)
        {
            if (point1 == point || point2 == point)
            {
                return true;
            }

            return false;
        }
    }
}
