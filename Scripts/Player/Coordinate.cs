using Godot;
using System.Collections.Generic;

namespace Survival
{


    public partial class Coordinate : Node2D
    {
        // 刻度设置为玩家当前伤害的两倍
        public float radio;

        public override void _Ready()
        {
            radio = GameEntry.Player.faceDamage * 2 / 540;
        }
        // 画直线
        public void DrawLine(float start, float slope)
        {
            float y = -start / radio;
            Line2D line = new Line2D();
            List<Vector2> points = new List<Vector2>();
            if (slope == 0)
            {
                points.Add(new Vector2(0, y));
                points.Add(new Vector2(540, y));
            }
            else
            {
                points.Add(new Vector2(0, y));
                points.Add(new Vector2(540, y + 540 * slope));
            }
            line.Points = points.ToArray();
            AddChild(line);
        }


        public void DrawPoint(int x, int y)
        {

        }
    }
}
