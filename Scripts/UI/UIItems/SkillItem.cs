using Godot;
using System;

namespace Survival
{
    public partial class SkillItem : TextureRect
    {
        public int id;

        public int type = 1;

        //public int slotId;

        public int level;

        public override void _Ready()
        {
            CustomMinimumSize = new Vector2(50, 50);
            ExpandMode = ExpandModeEnum.IgnoreSize;
            //动态加载item图片
            UpdateSkill();
        }

        public void UpdateSkill()
        {
            if (type == 1)
            {
                DRSkill drSkill = GameEntry.DataTable.GetDataTable<DRSkill>().GetDataRow(id);
                Texture2D texture = (Texture2D)GD.Load(AssetUtility.GetSkillItemSpriteAsset(drSkill.UIAssetName));
                Texture = texture;
            }
            else if (type == 0)
            {
                DRMainSkill drMainSkill = GameEntry.DataTable.GetDataTable<DRMainSkill>().GetDataRow(id);
                Texture2D texture = (Texture2D)GD.Load(AssetUtility.GetSkillItemSpriteAsset(drMainSkill.AssetName));
                Texture = texture;
            }
            // TODO:添加面板
            // 
            // var desc = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("SkillDesc"));
            // Control control = (Control)desc.Instantiate();
            // ((Label)control.GetNode("Container/Name")).Text = drSkill.AssetName;
            // ((Label)control.GetNode("Container/Desc")).Text = drSkill.Desc;
            // control.Position = Position + new Vector2(50, 50);
            // control.Hide();
            // AddChild(control);
        }

        public override Variant _GetDragData(Vector2 atPosition)
        {
            var preview = new TextureRect();
            preview.Texture = Texture;
            SetDragPreview(preview);
            return this;
        }
    }

}
