using Godot;
using System;

namespace Survival
{
    public partial class Anchor : Sprite2D
    {
        private Line2D line;

        public override void _Ready()
        {
            line = GetNode<Line2D>("Line2D");
        }

        public override void _Process(double delta)
        {
            line.AddPoint(Position);
        }
    }

}
