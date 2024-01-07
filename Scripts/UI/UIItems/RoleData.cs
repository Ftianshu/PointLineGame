using Godot;
using System;
using System.Collections;

namespace Survival
{
    public partial class RoleData : Panel
    {
        private Node propNames;
        private Node propDatas;

        public override void _Ready()
        {
            propNames = GetNode("PropNames");
            propDatas = GetNode("PropDatas");
            //同步玩家数据
            UpdateDatas();
        }

        public void UpdateDatas()
        {
            propDatas.GetChild<Label>(0).Text = GameEntry.Player.level.ToString();
            propDatas.GetChild<Label>(1).Text = GameEntry.Player.MaxHP.ToString();
            propDatas.GetChild<Label>(2).Text = GameEntry.Player.faceDamage.ToString();
            propDatas.GetChild<Label>(3).Text = GameEntry.Player.lineDamage.ToString();
            propDatas.GetChild<Label>(4).Text = GameEntry.Player.MaxSpeed.ToString();
            propDatas.GetChild<Label>(5).Text = GameEntry.Player.Acceleration.ToString();
            //propDatas.GetChild<Label>(6).Text = GameEntry.Player.level.ToString();
            //propDatas.GetChild<Label>(7).Text = GameEntry.Player.level.ToString();
            //propDatas.GetChild<Label>(8).Text = GameEntry.Player.level.ToString();
        }


        public void ShowDesc()
        {

        }
    }
}
