using Godot;
using System;

namespace Survival
{

    public partial class MoveToTarget : Node
    {
        private TargetEnemy me;

        public override void _Ready()
        {
            me = (TargetEnemy)GetParent();
        }
        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;

            if (me.Target.DistanceTo(me.Position) < 5)
            {
                QueueFree();
            }

            Vector2 direct = me.Target - me.Position;

            me.Rotation = MathF.Atan2(direct.X, -direct.Y);
            GameEntry.Entity.AddEnemyLinePoint(me.Position, me.lineId);
            me.Position += direct.Normalized() * me.Speed * (float)delta;
        }
    }
}
