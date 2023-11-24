using Godot;
using System.Collections.Generic;

namespace Survival
{
    public partial class SkillForm : Control
    {
        private int equipSkillCount = 0;
        private int mainSkillCount = 1;

        private SkillItem selectedSkill = null;

        public int SkillCount
        {
            set
            {
                equipSkillCount = value;
                //UpdateSkills();
            }
            get
            {
                return equipSkillCount;
            }
        }
        [Export]
        private Node learnedSkillRoot;
        [Export]
        private Node equipSkillRoot;
        [Export]
        private Node mainSkillRoot;

        public override void _Ready()
        {
            VisibilityChanged += OnShow;
            UpdateSkills();
        }

        public void UpdateSkills()
        {
            for (int i = 0; i < mainSkillRoot.GetChildCount(); i++)
            {
                SkillSlot skillSlot = mainSkillRoot.GetChild<SkillSlot>(i);
                skillSlot.IsValid = i < mainSkillCount;
            }
        }

        public void OnShow()
        {
            if (Visible)
            {
                //被重新打开
                GameEntry.Game.Pause();
            }
            else
            {
                UpdateSkill();
                GameEntry.Game.ReCover();
            }
        }

        public bool LearnSkill(int skillId)
        {
            int index = GetRecentlySkillIndex();
            if (index == -1)
            {
                return false;
            }

            SkillItem newSkillItem = new SkillItem();
            newSkillItem.id = skillId;
            //newSkillItem.slotId = index;
            learnedSkillRoot.GetChild<SkillSlot>(index).skillItem = newSkillItem;
            learnedSkillRoot.GetChild(index).GetNode("Items").AddChild(newSkillItem);
            return true;
        }

        public bool LearnMainSkill(int skillId)
        {
            int index = GetRecentlySkillIndex();
            if (index == -1)
            {
                return false;
            }

            SkillItem newSkillItem = new SkillItem();
            newSkillItem.id = skillId;
            newSkillItem.type = 0;
            //newSkillItem.slotId = index;
            learnedSkillRoot.GetChild<SkillSlot>(index).skillItem = newSkillItem;
            learnedSkillRoot.GetChild(index).GetNode("Items").AddChild(newSkillItem);
            return true;
        }

        public void InstallSkill(SkillItem item)
        {
            int index = GetRecentlyEquipSkillIndex(item);
            if (index == -1)
            {
                return;
            }
            //item.slotId = index;
            equipSkillRoot.GetChild<SkillSlot>(index).skillItem = item;
            equipSkillRoot.GetChild(index).GetNode("Items").AddChild(item);
        }

        public void InstallMainSkill(SkillItem item)
        {
            int index = GetRecentlyMainSkillIndex(item);
            if (index == -1)
            {
                return;
            }

            //item.slotId = index;
            mainSkillRoot.GetChild<SkillSlot>(index).skillItem = item;
            mainSkillRoot.GetChild(index).GetNode("Items").AddChild(item);
        }

        private int GetRecentlyMainSkillIndex(SkillItem item)
        {
            for (int i = 0; i < mainSkillRoot.GetChildCount(); i++)
            {
                if (IsContainVaildItem(mainSkillRoot.GetChild(i)))
                {
                    continue;
                }
                if (mainSkillRoot.GetChild<SkillSlot>(i)._CanDropData(Vector2.Zero, item))
                    return i;
            }
            return -1;
        }

        private int GetRecentlyEquipSkillIndex(SkillItem item)
        {
            for (int i = 0; i < equipSkillRoot.GetChildCount(); i++)
            {
                if (IsContainVaildItem(equipSkillRoot.GetChild(i)))
                {
                    continue;
                }
                if (equipSkillRoot.GetChild<SkillSlot>(i)._CanDropData(Vector2.Zero, item))
                    return i;
            }
            return -1;
        }

        private int GetRecentlySkillIndex()
        {
            for (int i = 0; i < learnedSkillRoot.GetChildCount(); i++)
            {
                if (IsContainVaildItem(learnedSkillRoot.GetChild(i)))
                {
                    continue;
                }
                return i;
            }
            return -1;
        }

        private bool IsContainVaildItem(Node parent)
        {
            if (parent.GetNode("Items").GetChildCount() == 0)
            {
                return false;
            }
            for (int i = 0; i < parent.GetNode("Items").GetChildCount(); i++)
            {
                if (IsInstanceValid(parent.GetNode("Items").GetChild(i)))
                {
                    return true;
                }
            }
            return false;
        }

        public void OnSkillChanged(int from, int to)
        {

        }

        public void OnSelectSkillSlot()
        {

        }

        public void ShowOrHideEquipSkills(int level)
        {
            equipSkillRoot.GetChild<Control>(level).Visible = !equipSkillRoot.GetChild<Control>(level).Visible;
        }

        public void AddSkillSlot(int level, int count)
        {
            equipSkillRoot.GetChild<Control>(level).Visible = true;
            for (int i = 0; i < count; i++)
            {
                var slot = GD.Load<PackedScene>("res://UIItems/SkillSlot.tscn");
                SkillSlot skillSlot = slot.Instantiate<SkillSlot>();
                skillSlot.canDropType = new int[1] { 1 };
                equipSkillRoot.GetChild(level).AddChild(skillSlot);
            }
        }

        //升级技能
        public void LevelUpSkill()
        {
            //更新ui
            selectedSkill.id += 1000;
            selectedSkill.UpdateSkill();
            //花费相应的时间
            //DRSkill drSkill = GameEntry.DataTable.GetDataTable<DRSkill>().GetDataRow(selectedSkill.id);
            //播放一段修炼动画
        }

        //设置选择的技能
        public void SetSelectedSkill(SkillItem item)
        {
            selectedSkill = item;
        }

        private void UpdateSkill()
        {
            int[] installSkills = GetEquipSkills();

            GameEntry.Skill.UpdateInstallSkills(installSkills);
        }

        private int[] GetEquipSkills()
        {
            int[] installSkills = new int[15];
            int index = 0;
            for (int i = 0; i < equipSkillRoot.GetChildCount(); i++)
            {
                Node node = equipSkillRoot.GetChild(i);
                for (int j = 0; j < node.GetChildCount(); j++)
                {
                    if (IsContainVaildItem(node.GetChild(j)))
                        installSkills[index++] = node.GetChild<SkillSlot>(j).skillItem.id;
                }
            }
            return installSkills;
        }

        public void BreakUpMainSkill(int level)
        {

        }

        public void OnClose()
        {
            GameEntry.UI.CloseUIForm(this);
            selectedSkill = null;
        }
    }
}
