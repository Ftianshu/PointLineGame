using Godot;
using System;

namespace Survival
{
    public partial class PreEnemy : Area2D
    {
        public Vector2 Target;
        public string AssetName;
        private double timeDelta = 0;

        public override void _Ready()
        {

        }

        public override void _Process(double delta)
        {
            timeDelta += delta;
            if (timeDelta > 3)
            {
                //生成敌人
                GameEntry.Entity.CreateEnemy(AssetName, Position);
                //
                QueueFree();
            }
        }

    }
}
