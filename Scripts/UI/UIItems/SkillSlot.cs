using Godot;
using System;

namespace Survival
{

    public partial class SkillSlot : PanelContainer
    {
        [Export]
        private int slotId;

        private bool isValid = true;
        public bool IsValid
        {
            set
            {
                isValid = value;
                //UpdateState();
            }
            get
            {
                return isValid;
            }
        }
        // [Signal]
        // public delegate void skillChangedEventHandler(int from, int to);
        [Signal]
        public delegate void selectSlotEventHandler();
        public SkillItem skillItem;

        [Export]
        public int[] canDropType;
        private int canDropLevel;

        public override void _Ready()
        {
            Connect("selectSlot", new Callable(GameEntry.UI.GetUIForm(UIFormId.SkillForm), "OnSelectSkillSlot"));
            //Connect("skillChanged", new Callable(GameEntry.UI.GetUIForm(UIFormId.SkillForm), "OnSkillChanged"));
            //UpdateState();
        }

        public void UpdateState()
        {
            //Visible = isValid;
            //GetNode<Control>("Lock").Visible = !isValid;
        }

        public override void _GuiInput(InputEvent inputEvent)
        {
            if (skillItem != null && IsInstanceValid(skillItem) && inputEvent is InputEventMouseButton mbe && mbe.ButtonIndex == MouseButton.Left)
            {
                EmitSignal("selectSlot");
            }
        }

        public override void _DropData(Vector2 atPosition, Variant data)
        {
            SkillItem item = (SkillItem)data;

            //空，则放过来
            if (skillItem == null || !IsInstanceValid(skillItem))
            {
                SkillItem clone = (SkillItem)item.Duplicate();
                CloneItemData(clone, item);

                item.QueueFree();
                skillItem = clone;
                //clone.slotId = slotId;
                GetNode("Items").AddChild(clone);
            } //技能相同则不动
            else if (skillItem.id == item.id)
            {
                return;
            }//技能不同则交换
            else
            {
                //判断对方是否可以接受这边的物品
                SkillSlot other = (SkillSlot)item.GetParent().GetParent();
                if (!other._CanDropData(Vector2.Zero, skillItem))
                {
                    //将物品移动到最近的空槽
                    GameEntry.Skill.skillForm.LearnSkill(item.id);
                    item.QueueFree();
                    return;
                }

                SkillItem clone = (SkillItem)item.Duplicate();
                SkillItem clone2 = (SkillItem)skillItem.Duplicate();
                //我加入对方，对方加入我
                CloneItemData(clone, item);
                CloneItemData(clone2, skillItem);

                // clone2.slotId = item.slotId;
                // clone.slotId = slotId;
                clone2.id = skillItem.id;
                clone.id = item.id;


                item.GetParent().AddChild(clone2);
                other.skillItem = clone2;
                GetNode("Items").AddChild(clone);

                item.QueueFree();
                skillItem.QueueFree();

                skillItem = clone;
            }

            //EmitSignal("skillChanged", item.slotId, slotId);
        }

        public override bool _CanDropData(Vector2 atPosition, Variant data)
        {
            if (!isValid)
            {
                return false;
            }
            for (int i = 0; i < canDropType.Length; i++)
            {
                if (((SkillItem)data).type == canDropType[i])
                {
                    //if (((SkillItem)data).level == canDropLevel)
                    return true;
                }
            }
            return false;
        }

        private void CloneItemData(SkillItem a, SkillItem b)
        {
            a.id = b.id;
            a.type = b.type;
            a.level = b.level;
        }
    }
}
