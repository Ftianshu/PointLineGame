using Godot;
using System;

namespace Survival
{

    public partial class AttractToPlayer : Node
    {
        private ExpPoint me;

        public override void _Ready()
        {
            me = (ExpPoint)GetParent();
        }
        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;


            Vector2 direct = GameEntry.Player.playerEntity.ToGlobal(new Vector2(0, -6)) - me.Position;
            me.Position += direct.Normalized() * me.Speed * (float)delta;
        }
    }
}
