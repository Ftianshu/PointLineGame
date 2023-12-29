using Godot;
using System;

namespace Survival
{
    public partial class RoleState : Control
    {
        [Export]
        private Label moneyUI;
        // [Export]
        // private Label HPUI;
        public override void _Ready()
        {
            GameEntry.Player.playerEntity.Connect("playerExpChanged", new Callable(this, "OnPlayerExpChanged"));
            GameEntry.Player.playerEntity.Connect("playerHPChanged", new Callable(this, "OnPlayerHPChanged"));
            GetNode<ProgressBar>("HPBar").Value = GameEntry.Player.HP / GameEntry.Player.MaxHP * 100f;
        }

        public void OnPlayerHPChanged()
        {
            //GetNode<Label>("HP").Text = GameEntry.Player.HP + "/" + GameEntry.Player.MaxHP;
            GetNode<ProgressBar>("HPBar").Value = GameEntry.Player.HP / GameEntry.Player.MaxHP * 100f;
        }

        public void OnPlayerExpChanged()
        {
            // GetNode<Label>("Exp").Text = GameEntry.Player.Exp + "/" + GameEntry.Player.next_level_Exp;
            GetNode<ProgressBar>("ExpBar").Value = GameEntry.Player.Exp / GameEntry.Player.next_level_Exp * 100f;
        }
    }

}
