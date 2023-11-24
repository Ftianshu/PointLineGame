using Godot;
using System;

namespace Survival
{
    public partial class AttackRecentEnemyRange : Node
    {
        private Area2D parent;

        //private Node2D recentEnemy;
        private int Speed = 200;
        public override void _Ready()
        {
            parent = (Area2D)GetParent();
            parent.Position = FindRecentEnemyPosition();
        }
        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;

            //朝敌人前进
            // Vector2 velocity = target * Speed * (float)delta;

            // parent.Position += velocity;
        }

        private Vector2 FindRecentEnemyPosition()
        {
            //找到最近的敌人
            Node2D enemyRoot = (Node2D)GameEntry.Entity.EnemyRoot;
            if (enemyRoot.GetChildCount() == 0)
            {
                return GameEntry.Player.playerEntity.Position;
            }
            float recentDist = float.MaxValue;
            Vector2 recentEnemyPosition = Vector2.Zero;
            foreach (Node2D enemy in enemyRoot.GetChildren())
            {
                float distance = enemy.Position.DistanceSquaredTo(parent.Position);
                if (distance < recentDist)
                {
                    recentDist = distance;
                    recentEnemyPosition = enemy.Position;
                }
            }
            return recentEnemyPosition;
        }
    }
}
