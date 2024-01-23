using Godot;
using System;

namespace Survival
{

    public partial class PropEnemy : Enemy
    {
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
        }

        public override void OnLoad()
        {
            //加载图片
        }


        public override void OnAttack(Node2D body)
        {

        }

        public override void OnBeAttacked(Area2D area)
        {
            if (area.GetType().Name == "Face")
            {
                if ((area as Face).faceId != 1)
                {
                    HP -= 1;
                }
            }
        }

        public override void OnUpdate(double delta)
        {

        }
    }
}
