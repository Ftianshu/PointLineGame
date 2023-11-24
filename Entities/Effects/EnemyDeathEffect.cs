using Godot;
using System;

namespace Name
{
    public partial class EnemyDeathEffect : Node2D
    {
        public override void _Ready()
        {
            GetNode<GpuParticles2D>("GPUParticles2D").Emitting = true;
        }


    }

}
