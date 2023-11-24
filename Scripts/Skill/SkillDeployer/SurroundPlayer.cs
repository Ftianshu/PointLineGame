using Godot;
using System;

namespace Survival
{
    public partial class SurroundPlayer : Node
    {
        private Area2D parent;

        private double m_Time;
        private float Range = 50;
        private int Speed = 200;
        public override void _Ready()
        {
            parent = (Area2D)GetParent();
            //parent.Position = GameEntry.Player.playerEntity.Position;
        }
        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;
            // m_Time += delta;
            // //朝敌人前进
            // Vector2 velocity = target * Speed * (float)delta;

            // parent.Position += velocity;
        }

    }
}
