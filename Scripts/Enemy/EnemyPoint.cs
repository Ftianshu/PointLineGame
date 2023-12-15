using Godot;
using System;

namespace Survival
{
    public partial class EnemyPoint : Point
    {

        private double time = 0;

        private float disabledTime = 0.5f;


        public override void _Ready()
        {
            base._Ready();
            Lifetime = GameEntry.Enemy.pointLife;
        }

        public override void OnEnemyEnter(Area2D area)
        {
            if (area.GetType().ToString() == "Survival.TargetEnemy")
            {

                TargetEnemy enemy = area as TargetEnemy;
                if (!enemy.IsLinking)
                {
                    enemy.IsLinking = true;
                    Line line = GameEntry.Entity.GetLine(enemy.lineId);
                    GameEntry.Face.LinkLines(enemy.lineId, lineId, new LinkPoint(this, line.GetChild<Point>(line.GetChildCount() - 4)));
                }
            }
        }

        public override void OnEnemyExit(Area2D area)
        {
            (area as Enemy).IsLinking = false;
        }

        public override void OnPlayerEnter(Node2D body)
        {
            // 造成伤害
            // GameEntry.Player.HP -= GameEntry.Enemy.lineDamage;
        }

        public override void OnPlayerExit(Node2D body)
        {

        }


    }
}
