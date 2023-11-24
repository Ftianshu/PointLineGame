using Godot;
using System;

namespace Survival
{
    public partial class TrackRecentEnemy : Node
    {
        private Area2D parent;

        private Node2D recentEnemy;
        private int Speed = 200;
        public override void _Ready()
        {
            parent = (Area2D)GetParent();
            parent.Position = GameEntry.Player.playerEntity.Position;
            FindRecentEnemy();
        }
        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;

            //朝敌人前进
            if (IsInstanceValid(recentEnemy))
            {
                Vector2 velocity = (parent.Position - recentEnemy.Position).Normalized() * Speed * (float)delta;
                parent.Position += velocity;
                return;
            }
        }

        private void FindRecentEnemy()
        {
            //找到最近的敌人
            Node2D enemyRoot = (Node2D)GameEntry.Entity.EnemyRoot;
            if (enemyRoot.GetChildCount() == 0)
            {
                return;
            }
            float recentDist = float.MaxValue;

            foreach (Node2D enemy in enemyRoot.GetChildren())
            {
                float distance = enemy.Position.DistanceSquaredTo(parent.Position);
                if (distance < recentDist)
                {
                    recentDist = distance;
                    recentEnemy = enemy;
                }
            }
        }
    }
}
