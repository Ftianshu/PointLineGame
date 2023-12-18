using Godot;
using System;
using System.Collections.Generic;

namespace Survival
{
    public partial class PlayerLine : Line
    {
        public override void OnEnemyEnter(Area2D area)
        {

        }

        public override void OnEnemyExit(Area2D area)
        {

        }

        public override void OnPlayerEnter(Node2D body)
        {
            GD.Print("paluyeru");
        }

        public override void OnPlayerExit(Node2D body)
        {

        }
    }

}
