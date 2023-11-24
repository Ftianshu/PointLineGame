using Godot;
using System;

namespace Survival
{

    public partial class MoveToPlayer : Node
    {
        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;

            Vector2 target = GameEntry.Player.playerEntity.Position;
            Enemy me = (Enemy)GetParent();
            Vector2 direct = target - me.Position;
            me.Position += direct.Normalized() * me.Speed * (float)delta;
        }
    }
}
