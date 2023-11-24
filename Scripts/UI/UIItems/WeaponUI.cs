using Godot;
using System;

namespace Survival
{
    public partial class WeaponUI : ItemUI
    {
        public bool isCanUse = true;
        public override void LoadTexture()
        {
            DRWeapon drWeapon = GameEntry.DataTable.GetDataTable<DRWeapon>().GetDataRow(id);
            Texture2D texture = (Texture2D)GD.Load(AssetUtility.GetItemUISpriteAsset(drWeapon.UIAssetName));
            Texture = texture;
            //添加面板
            var desc = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("WeaponDesc"));
            Control control = (Control)desc.Instantiate();
            control.GetNode<Label>("Container/Name").Text = drWeapon.AssetName;
            control.GetNode<Label>("Container/Desc").Text = drWeapon.Desc;
            control.GetNode<Label>("Container/Propty").Text = "力量：" + drWeapon.Strength + "\n" +
            "体质：" + drWeapon.Physique + "\n" + "敏捷：" + drWeapon.Agile + "\n" + "精神：" + drWeapon.Soul + "\n" + "魅力：" + drWeapon.Charm;
            // 判断武器是否可以使用
            isCanUse = IsWeaponCanUsed(drWeapon.Condition);

            control.GetNode<TextureRect>("WeaponIcon").Texture = texture;
            control.Position = Position + new Vector2(50, 50);
            control.Hide();
            AddChild(control);
        }

        public void UpdateWeaponState()
        {
            //TODO:更新武器是否可用，更新UI状态
            DRWeapon drWeapon = GameEntry.DataTable.GetDataTable<DRWeapon>().GetDataRow(id);
            isCanUse = IsWeaponCanUsed(drWeapon.Condition);
            GD.Print("武器可用：" + isCanUse);
        }

        public override void UseItem()
        {
            //TODO:判断武器是否可以使用，如果不可使用则给予玩家震动反馈
            if (!isCanUse)
            {
                return;
            }
            if (slotId >= 100)
            {
                //卸下装备
                GameEntry.Player.bag.MoveItemToEmptySlot(this);
                return;
            }
            GameEntry.Player.bag.WearWeapon(this);
        }

        private bool IsWeaponCanUsed(string condition)
        {
            if (condition == "/")
                return true;
            string[] conditions = condition.Split("/");
            //有一个条件不满足就返回false,全满足返回true
            for (int i = 0; i < conditions.Length; i++)
            {
                string conditionName = conditions[i].Split(":")[0];
                string conditionData = conditions[i].Split(":")[1];
                switch (conditionName)
                {
                    case "修为":
                        {
                            if (GameEntry.Player.level < int.Parse(conditionData))
                                return false;
                            break;
                        }
                    case "力量":
                        {
                            if (GameEntry.Player.basicData[0] < int.Parse(conditionData))
                            {
                                return false;
                            }
                            break;
                        }
                    case "敏捷":
                        {
                            if (GameEntry.Player.basicData[1] < int.Parse(conditionData))
                            {
                                return false;
                            }
                            break;
                        }
                }
            }
            return true;
        }

        // 添加词条
        private void AddPropty(int type, int rarity)
        {
            string[] Propties = new string[] { "武器伤害", "武器冷却", "吸血" };
            string[] SpecialProp = new string[] { "附带技能" };
            switch (type)
            {
                case 0:
                    {
                        //
                        break;
                    }
                case 1:
                    {
                        //
                        break;
                    }
                case 2:
                    {
                        //
                        break;
                    }
                case 3:
                    {
                        //
                        break;
                    }
                case 4:
                    {
                        //
                        break;
                    }
            }
        }

        private void AddWeaponProp(int rarity)
        {
            string[] Propties = new string[] { "武器伤害", "武器冷却", "吸血" };
            string[] SpecialProp = new string[] { "附带技能" };
            switch (rarity)
            {
                case 0:
                    {
                        //无词条
                        break;
                    }
                case 1:
                    {
                        //随机一个白色词条
                        break;
                    }
                case 2:
                    {
                        //随机一个蓝色词条
                        break;
                    }
                case 3:
                    {
                        //随机两个词条或一个
                        break;
                    }
                case 4:
                    {
                        //随机两个词条
                        break;
                    }
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
