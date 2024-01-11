using Godot;
using System;
using System.Collections.Generic;

namespace Survival
{
    public partial class EnemyLine : Line
    {
        public override void OnInit()
        {
            faceId = 1;
        }

        public override void OnEnemyEnter(Area2D area)
        {
            TargetEnemy enemy = area as TargetEnemy;
            if (enemy.lineId == lineId)
            {
                // 创造平面
                GameEntry.Entity.CreateFace("face", GameEntry.Face.GetFacePoints(line, GetRecentPointIndex(enemy.Position)), FaceId.EnemyFace);
                GameEntry.Entity.ResetFaceLine(faceId);
                return;
            }

            GD.Print(GetPointIndex(GetRecentPoint(enemy.Position)));
            // 连接线
            GameEntry.Face.LinkLines(this, GameEntry.Entity.GetLine(enemy.lineId),
                 new LinkPoint(lineId, enemy.lineId,
                    GetRecentPoint(enemy.Position), GameEntry.Entity.GetLine(enemy.lineId).GetRecentPoint(enemy.Position)));

        }

        public override void OnEnemyExit(Area2D area)
        {

        }

        public override void OnPlayerEnter(Node2D body)
        {

        }

        public override void OnPlayerExit(Node2D body)
        {

        }
    }

}
