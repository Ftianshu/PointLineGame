using Godot;
using System;

namespace Survival
{
    public partial class AttackFace : Node
    {
        private Area2D parent;
        public float distance = 50;
        public override void _Ready()
        {
            // parent = (Area2D)GetParent();
            // Vector2 offset = GameEntry.Player.playerEntity.isFlip ? new Vector2(-50, 0) : new Vector2(50, 0);
            // parent.GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = GameEntry.Player.playerEntity.isFlip;
            // parent.Position = GameEntry.Player.playerEntity.Position + offset;
            // ((Skill)parent).attackCount = 100;
        }

    }
}
