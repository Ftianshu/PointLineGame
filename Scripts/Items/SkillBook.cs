using Godot;
using System;

namespace Survival
{
    public partial class SkillBook : Area2D
    {
        public int id = 1;
        public override void _Ready()
        {
            DRSkill drSkill = GameEntry.DataTable.GetDataTable<DRSkill>().GetDataRow(id);
            //动态加载item图片
            Texture2D texture = (Texture2D)GD.Load(AssetUtility.GetSkillBookSpriteAsset("SkillBook" + drSkill.Type));
            GetNode<Sprite2D>("Sprite2D").Texture = texture;

            BodyEntered += OnPicked;
        }

        private void OnPicked(Node2D body)
        {
            QueueFree();
            //玩家获得对应技能书
            GameEntry.Player.GainSkillBook(id);
        }
    }
}
