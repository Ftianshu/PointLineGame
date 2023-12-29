using Godot;
using System;

namespace Survival
{

    public partial class OrdinaryEnemy : Enemy
    {

        public EnemyState State;

        // public IImpact[] impacts;


        private bool IsDead()
        {
            if (HP <= 0)
            {
                return true;
            }
            return false;
        }

        public override void OnDead()
        {
            QueueFree();
            GameEntry.Entity.CreateEffect("EnemyDeathEffect", Position);
            // 掉落经验材料
            GameEntry.Entity.CreateEntity("Other/ExpPoint", Position);
            EmitSignal("enemyDeath");
        }

        public override void OnLoad()
        {
            var behavior = new MoveToPlayer();
            State = EnemyState.MoveToPlayer;
            behavior.Name = "MoveToPlayer";
            AddChild(behavior);
        }

        public override void OnUpdate(double delta)
        {
            switch (State)
            {
                case EnemyState.MoveToPlayer:
                    {

                        break;
                    }
                case EnemyState.RushToPlayer:
                    {
                        //暂时啥也不做
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
    }
}
