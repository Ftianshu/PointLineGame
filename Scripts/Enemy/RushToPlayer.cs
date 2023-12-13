using Godot;
using System;

namespace Survival
{

    public partial class RushToPlayer : Node
    {
        private Vector2 target;

        private RushEnemy0 me;
        public override void _Ready()
        {
            target = GameEntry.Player.playerEntity.Position;
            me = (RushEnemy0)GetParent();
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
            GameEntry.Face.AddPoint(me.Position, (int)FaceId.EnemyFace, me.lineId);
            me.Position += direct.Normalized() * me.RushSpeed * (float)delta;
        }
    }


}