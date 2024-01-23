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
            if (GameEntry.Game.gamePause)
                return;

            timeDelta += delta;

            if (timeDelta > 3)
            {
                //生成敌人
                Enemy enemy = GameEntry.Entity.CreateEnemy(AssetName, Position) as Enemy;
                enemy.Connect("enemyDeath", new Callable(GameEntry.Enemy, "OnEnemyDeath"));
                //
                QueueFree();
            }

            (GetNode<Sprite2D>("Sprite2D").Material as ShaderMaterial).SetShaderParameter("alpha", MathF.Abs(MathF.Cos((float)timeDelta * 1.2f * MathF.PI)));
        }

    }
}
