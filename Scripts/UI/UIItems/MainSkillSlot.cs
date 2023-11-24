using Godot;
using System;

namespace Survival
{

    public partial class MainSkillSlot : SkillSlot
    {
        [Export]
        private int level;
        public override void _Ready()
        {
            base._Ready();
            SetDrag(true);
        }
        public override void _DropData(Vector2 atPosition, Variant data)
        {
            base._DropData(atPosition, data);
            GameEntry.Skill.InstallMainSkill(skillItem.id);
            SetDrag(false);
        }

        public void SetDrag(bool isCanDrag)
        {
            GetNode<Control>("Mask").Visible = !isCanDrag;
            GetNode<Control>("Name").Visible = isCanDrag;
        }

        public override bool _CanDropData(Vector2 atPosition, Variant data)
        {
            if (!IsInstanceValid(skillItem) && ((SkillItem)data).level == level && ((SkillItem)data).type == 0)
            {
                return true;
            }
            return false;
        }
    }
}
