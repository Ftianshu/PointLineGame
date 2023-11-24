using Godot;
using System;

namespace Survival
{
    public partial class Baton : Node
    {
        private Area2D parent;
        public float Speed = 200;
        public override void _Ready()
        {
            parent = (Area2D)GetParent();
            parent.Position = GameEntry.Player.playerEntity.Position;
        }

        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;

            //朝鼠标位置前进
            Vector2 mousePosition = GetViewport().GetMousePosition();
            Vector2 direct = mousePosition - GetViewport().GetVisibleRect().Size / 2;
            Vector2 velocity = direct.Normalized() * Speed * (float)delta;

            parent.Position += velocity;
        }
    }
}
