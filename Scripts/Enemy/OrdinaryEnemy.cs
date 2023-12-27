using Godot;
using System;

namespace Survival
{

    public partial class OrdinaryEnemy : Enemy
    {

        public EnemyState State;

        // public IImpact[] impacts;

        public override void _Ready()
        {
            base._Ready();
            var behavior = new MoveToPlayer();
            State = EnemyState.MoveToPlayer;
            behavior.Name = "MoveToPlayer";
            AddChild(behavior);
            Position = new Vector2(-100, 0);
            OnLoad();
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
        }


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
            GameEntry.Entity.CreateEnemy("Ordinary");
        }

        public override void OnLoad()
        {
            throw new NotImplementedException();
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
            // 播放动画
        }
    }
}
