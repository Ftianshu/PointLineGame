using Godot;
using System;

namespace Survival
{
    public partial class PlayerPoint : Point
    {
        private double time = 0;


        public override float disabledTime { get; set; }

        public override void _Ready()
        {
            base._Ready();
            disabledTime = 0.5f;
            Lifetime = GameEntry.Player.pointLife;
        }

        public override void OnPlayerEnter(Node2D body)
        {
            PlayerController player = body as PlayerController;
            if (!player.IsLinking && !player.isRushing)
            {
                player.IsLinking = true;
                Line line = GameEntry.Entity.GetLine(player.currentLine);
                GameEntry.Face.LinkLines(player.currentLine, lineId, new LinkPoint(this, line.GetChild<Point>(line.GetChildCount() - 4)));
            }
        }

        public override void OnPlayerExit(Node2D body)
        {
            (body as PlayerController).IsLinking = false;
        }

        public override void OnEnemyEnter(Area2D area)
        {

        }

        public override void OnEnemyExit(Area2D area)
        {

        }
    }
}
