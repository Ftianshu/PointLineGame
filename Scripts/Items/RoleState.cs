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

        }

        public void OnPlayerHPChanged()
        {
            GetNode<Label>("HP").Text = GameEntry.Player.HP + "/" + GameEntry.Player.MaxHP;
            GetNode<TextureProgressBar>("HPBar").Value = GameEntry.Player.HP / GameEntry.Player.MaxHP * 100f;
        }

        public void OnPlayerExpChanged()
        {
            GetNode<Label>("Exp").Text = GameEntry.Player.Exp + "/" + GameEntry.Player.next_level_Exp;
            GetNode<TextureProgressBar>("ExpBar").Value = GameEntry.Player.Exp / GameEntry.Player.next_level_Exp * 100f;
        }
    }

}
