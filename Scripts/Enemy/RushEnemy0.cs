using Godot;
using System;

namespace Survival
{

    public partial class RushEnemy0 : Enemy
    {

        public EnemyState State;

        private double timer;

        private double AccumulateTime = 2;

        private double ExhaustedTime = 1;

        private bool IsDead()
        {
            if (HP <= 0)
            {
                return true;
            }
            return false;
        }

        private void OnDead()
        {
            QueueFree();
            GameEntry.Entity.CreateEffect("EnemyDeathEffect", Position);
            GameEntry.Entity.CreateEnemy("Ordinary");
        }

        public override void OnLoad()
        {
            State = EnemyState.MoveToPlayer;
            var behavior = new MoveToPlayer();
            behavior.Name = "MoveToPlayer";
            AddChild(behavior);
            var behavior2 = new RotateToPlayer();
            behavior2.Name = "RotateToPlayer";
            AddChild(behavior2);
            Position = new Vector2(-100, 0);
        }

        public override void OnUpdate(double delta)
        {
            switch (State)
            {
                case EnemyState.MoveToPlayer:
                    {
                        float distance = GameEntry.Player.playerEntity.Position.DistanceTo(Position);
                        if (distance < 50)
                        {
                            GD.Print("Rush");
                            State = EnemyState.Accumulate;

                            RemoveChild(GetNode("MoveToPlayer"));
                            //

                        }
                        break;
                    }
                case EnemyState.RushToPlayer:
                    {
                        if (!HasNode("RushToPlayer"))
                        {
                            State = EnemyState.Exhausted;
                        }
                        break;
                    }
                case EnemyState.Accumulate:
                    {
                        //播放蓄力动画,等动画结束后冲刺
                        timer += delta;
                        if (timer > AccumulateTime)
                        {
                            State = EnemyState.RushToPlayer;
                            var behavior = new RushToPlayer();
                            behavior.Name = "RushToPlayer";
                            AddChild(behavior);
                            RemoveChild(GetNode("RotateToPlayer"));
                            timer = 0;
                        }
                        break;
                    }
                case EnemyState.Exhausted:
                    {
                        //冲刺完后，一段时间不会移动，
                        timer += delta;
                        if (timer > ExhaustedTime)
                        {
                            State = EnemyState.MoveToPlayer;
                            var behavior = new MoveToPlayer();
                            behavior.Name = "MoveToPlayer";
                            AddChild(behavior);
                            var behavior2 = new RotateToPlayer();
                            behavior2.Name = "RotateToPlayer";
                            AddChild(behavior2);
                            timer = 0;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public override void OnAttack(Node2D body)
        {
            //throw new NotImplementedException();
        }

        public override void OnBeAttacked(Area2D area)
        {
            if (area.GetType().ToString() == "Survival.Face")
            {
                Face face = area as Face;
                HP -= face.GetDamage();
            }
            else if (area.GetType().ToString() == "Survival.Point")
            {
                Point point = area as Point;
                if (lastDamageForm != point.lineId)
                {
                    lastDamageForm = point.lineId;
                    //造成伤害
                    //GD.Print("造成伤害");
                }
            }
        }
    }
}