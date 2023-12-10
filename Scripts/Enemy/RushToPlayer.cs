using Godot;
using System;

namespace Survival
{

    public partial class RushToPlayer : Node
    {
        private Vector2 target;

        private Enemy me;
        public override void _Ready()
        {
            target = GameEntry.Player.playerEntity.Position;
            me = (Enemy)GetParent();
        }
        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;

            if (target.DistanceTo(me.Position) < 5)
            {
                QueueFree();
            }

            Vector2 direct = target - me.Position;
            me.Position += direct.Normalized() * me.RushSpeed * (float)delta;
        }
    }


}