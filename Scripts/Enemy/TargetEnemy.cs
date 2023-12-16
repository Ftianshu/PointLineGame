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
            //GameEntry.Entity.CreateEnemy("Ordinary");
        }

        public override void OnLoad()
        {
            State = EnemyState.MoveToTarget;
            var behavior = new MoveToTarget();
            behavior.Name = "MoveToTarget";
            AddChild(behavior);
            Target = new Vector2(200, 0);
            lineId = GameEntry.Face.GenerateLineId();
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

        public void SetTarget(Vector2 target)
        {
            Target = target;
        }
    }
}
