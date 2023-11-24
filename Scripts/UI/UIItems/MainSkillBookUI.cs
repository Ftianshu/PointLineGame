using Godot;
using System;

namespace Survival
{
    public partial class MainSkillBookUI : ItemUI
    {
        public override void LoadTexture()
        {
            DRMainSkill drMainSkill = GameEntry.DataTable.GetDataTable<DRMainSkill>().GetDataRow(id);
            Texture2D texture = (Texture2D)GD.Load(AssetUtility.GetItemUISpriteAsset("RarityBooks/" + GetTypeString(drMainSkill.Type) + drMainSkill.Realm));
            Texture = texture;
            //添加面板
            var desc = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("SkillDesc"));
            Control control = (Control)desc.Instantiate();
            control.GetNode<Label>("Container/Name").Text = drMainSkill.AssetName;
            control.GetNode<Label>("Container/Desc").Text = drMainSkill.Description;
            Texture2D skillIcon = (Texture2D)GD.Load(AssetUtility.GetSkillItemSpriteAsset(drMainSkill.AssetName));
            control.GetNode<TextureRect>("SkillIcon").Texture = skillIcon;
            control.Position = Position + new Vector2(50, 50);
            control.Hide();
            AddChild(control);
        }

        public override void UseItem()
        {
            GD.Print("学习技能");
            GameEntry.Skill.skillForm.LearnMainSkill(id);
            QueueFree();
        }

        private string GetTypeString(int type)
        {
            switch (type)
            {
                case 0: return "metal";
                case 1: return "wood";
                case 2: return "water";
                case 3: return "fire";
                case 4: return "solid";
                default: return "";
            }
        }

        // private void UseItem()
        // {
        //     GD.Print("使用道具");
        //     if (type == 0)
        //     {
        //         //技能书类型
        //         //弹窗：是否花费$Time学习此技能
        //         //如果确认
        //         GameEntry.Skill.LearnSkill(id);
        //         QueueFree();
        //     }
        //     else
        //     {
        //         //如果是武器，将武器放入对应的装备槽
        //         if (slotId >= 100)
        //         {
        //             //卸下装备
        //             GameEntry.Player.bag.MoveItemToEmptySlot(this);
        //             return;
        //         }
        //         GameEntry.Player.bag.WearWeapon(this);
        //     }
        // }
    }

}
