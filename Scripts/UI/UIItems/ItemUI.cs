using Godot;
using System;

namespace Survival
{
    public abstract partial class ItemUI : TextureRect
    {
        public int id;

        public int capacity = 1;

        public int type = 0;

        public int num = 1;

        public int slotId;

        private bool isShowDesc = false;

        public override void _Ready()
        {
            MouseEntered += OnMouseEnter;
            MouseExited += OnMouseExit;
            CustomMinimumSize = new Vector2(50, 50);
            if (Texture != null)
            {
                return;
            }

            //动态加载item图片
            ExpandMode = ExpandModeEnum.IgnoreSize;
            LoadTexture();
        }

        public abstract void LoadTexture();

        public abstract void UseItem();

        public async void OnMouseEnter()
        {
            isShowDesc = true;
            await ToSignal(GetTree().CreateTimer(0.3), "timeout");
            if (isShowDesc)
                ShowDesc();
        }

        public void ShowDesc()
        {
            GetChild<Control>(0).Show();
        }

        public void HideDesc()
        {
            GetChild<Control>(0).Hide();
        }

        public void OnMouseExit()
        {
            isShowDesc = false;
            if (GetChild<Control>(0).Visible)
            {
                HideDesc();
            }
        }



        public override void _GuiInput(InputEvent inputEvent)
        {
            if (inputEvent is InputEventMouseButton mbe && mbe.DoubleClick && mbe.ButtonIndex == MouseButton.Left)
            {
                UseItem();
            }
        }

        public override Variant _GetDragData(Vector2 atPosition)
        {
            var preview = new TextureRect();
            preview.Texture = Texture;
            SetDragPreview(preview);
            return this;
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
