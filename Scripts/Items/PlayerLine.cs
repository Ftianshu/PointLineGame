using Godot;
using System;
using System.Collections.Generic;

namespace Survival
{
    public partial class PlayerLine : Line
    {
        public override void OnInit()
        {
            faceId = 0;
        }

        public override void OnEnemyEnter(Area2D area)
        {

        }

        public override void OnEnemyExit(Area2D area)
        {

        }

        public override void OnPlayerEnter(Node2D body)
        {
            // 平面连接
            //GD.Print("plas");
            PlayerController player = body as PlayerController;
            if (player.currentLine == lineId)
            {
                // 创造平面
                GameEntry.Entity.CreateFace("face", GameEntry.Face.GetFacePoints(line, GetRecentPointIndex(player.Position)));
                GameEntry.Entity.ResetFaceLine(faceId);
                return;
            }

            // 连接线
            GameEntry.Face.LinkLines(this, GameEntry.Entity.GetLine(player.currentLine),
                 new LinkPoint(lineId, player.currentLine,
                    GetRecentPoint(player.Position), GameEntry.Entity.GetLine(player.currentLine).GetRecentPoint(player.Position)));

        }

        public override void OnPlayerExit(Node2D body)
        {

        }
    }

}
