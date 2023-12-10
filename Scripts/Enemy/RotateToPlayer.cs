using Godot;
using System;

namespace Survival
{

    public partial class RotateToPlayer : Node
    {
        private Enemy me;

        public override void _Ready()
        {
            me = (Enemy)GetParent();
        }
        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;

            Vector2 target = GameEntry.Player.playerEntity.Position;
            Vector2 direct = target - me.Position;
            me.Rotation = MathF.Atan2(direct.X, -direct.Y);
        }
    }
}
