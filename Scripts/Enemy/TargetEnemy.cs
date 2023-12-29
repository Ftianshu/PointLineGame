using Godot;
using System;

namespace Survival
{

    public partial class TargetEnemy : Enemy
    {

        public EnemyState State;

        public Vector2 Target;

        private double timer;

        private double AccumulateTime = 2;

        private double ExhaustedTime = 1;

        public int lineId;

        public override void OnDead()
        {
            QueueFree();
            GameEntry.Entity.CreateEffect("EnemyDeathEffect", Position);
            GameEntry.Entity.DelLine(lineId);
            EmitSignal("enemyDeath");
            //GameEntry.Entity.CreateEnemy("Ordinary");
        }

        public override void OnLoad()
        {
            State = EnemyState.MoveToTarget;
            var behavior = new MoveToTarget();
            behavior.Name = "MoveToTarget";
            AddChild(behavior);
            lineId = GameEntry.Entity.CreateEnemyLine();
        }

        public override void OnUpdate(double delta)
        {
            switch (State)
            {
                case EnemyState.MoveToTarget:
                    {
                        if (!HasNode("MoveToTarget"))
                        {
                            OnDead();
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public override void OnAttack(Node2D body)
        {
            // 对玩家造成伤害
            GameEntry.Player.CauseDamage(Attack);
        }

        public override void OnBeAttacked(Area2D area)
        {
            if (area.GetType().ToString() == "Survival.Face")
            {
                HP -= (area as Face).GetDamage();
            }
        }

        public void SetTarget(Vector2 target)
        {
            Target = target;
        }
    }
}
