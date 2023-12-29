using Godot;
using System;

namespace Survival
{
    public partial class ExpPoint : Area2D
    {
        public float Exp = 5;
        public float Speed = 200;
        public override void _Ready()
        {
            BodyEntered += OnPicked;
            AreaEntered += OnAttract;
        }

        private void OnAttract(Area2D area)
        {
            if (HasNode("AttractToPlayer"))
            {
                return;
            }
            var behavior = new AttractToPlayer();
            behavior.Name = "AttractToPlayer";
            AddChild(behavior);
        }


        private void OnPicked(Node2D body)
        {
            GameEntry.Player.GainExp(Exp);
            QueueFree();
        }

    }
}
