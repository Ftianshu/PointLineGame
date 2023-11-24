using Godot;
using System;

namespace Survival
{
    public partial class coin : Area2D
    {
        [Export]
        private int cout = 0;
        public override void _Ready()
        {
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();
            BodyEntered += OnPicked;
        }

        private void OnPicked(Node2D body)
        {
            QueueFree();
            //玩家获得金币
            //GD.Print("Gain Money");
            GameEntry.Player.GainMoney(1);
            //GameEntry.Player.GainItem(cout);
        }
    }
}