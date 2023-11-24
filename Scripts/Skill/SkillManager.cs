using Godot;
using System;
using System.Collections.Generic;

namespace Survival
{

    public class SkillManager
    {
        private const int MaxInstallSkillNum = 15;
        private const int MaxLearnedSkillNum = 30;
        private int[] installSkillIds;
        private int[] learnedSkillIds;
        private DRMainSkill[] mainSkills;
        private SkillDeployer[] skillDeployers;
        private SkillDeployer[] weaponSkillDeployers;
        private Node skillDeloyerRoot;

        public SkillForm skillForm;
        public SkillManager(Node skillRoot)
        {
            skillDeloyerRoot = skillRoot;
            Init();
        }

        public void Init()
        {
            installSkillIds = new int[MaxInstallSkillNum];
            learnedSkillIds = new int[MaxLearnedSkillNum];
            mainSkills = new DRMainSkill[8];
            skillDeployers = new SkillDeployer[MaxInstallSkillNum];
            weaponSkillDeployers = new SkillDeployer[10];
            for (int i = 0; i < MaxInstallSkillNum; i++)
            {
                skillDeployers[i] = new SkillDeployer(0);
                skillDeloyerRoot.AddChild(skillDeployers[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                weaponSkillDeployers[i] = new SkillDeployer(0);
                skillDeloyerRoot.AddChild(weaponSkillDeployers[i]);
            }
            Pause();
        }

        public void InitSkillForm()
        {
            skillForm = (SkillForm)GameEntry.UI.OpenUIForm(UIFormId.SkillForm);
            skillForm.Hide();
        }

        public void Pause()
        {
            foreach (SkillDeployer skillDeployer in skillDeployers)
            {
                skillDeployer.Paused = true;
            }

            foreach (SkillDeployer weaponSkillDeployer in weaponSkillDeployers)
            {
                weaponSkillDeployer.Paused = true;
            }
        }

        public void ReCover()
        {
            foreach (SkillDeployer skillDeployer in skillDeployers)
            {
                if (skillDeployer.skillId != 0)
                    skillDeployer.Paused = false;
            }

            foreach (SkillDeployer weaponSkillDeployer in weaponSkillDeployers)
            {
                if (weaponSkillDeployer.skillId != 0)
                    weaponSkillDeployer.Paused = false;
            }
        }

        public void UpdateInstallSkills(int[] newInstallSkill)
        {
            for (int i = 0; i < newInstallSkill.Length; i++)
            {
                if (newInstallSkill[i] != skillDeployers[i].skillId)
                {
                    skillDeployers[i].skillId = newInstallSkill[i];
                    skillDeployers[i].ReStart();
                    return;
                }
            }
        }

        public void UpdateWeaponSkills(int[] newWeaponSkill)
        {
            for (int i = 0; i < newWeaponSkill.Length; i++)
            {
                if (newWeaponSkill[i] != weaponSkillDeployers[i].skillId)
                {
                    weaponSkillDeployers[i].skillId = newWeaponSkill[i];
                    weaponSkillDeployers[i].ReStart();
                    return;
                }
            }
        }

        public void InstallMainSkill(int id)
        {
            DRMainSkill drMainSkill = GameEntry.DataTable.GetDataTable<DRMainSkill>().GetDataRow(id);
            mainSkills[drMainSkill.Realm] = drMainSkill;
            //UpdateSkillData();
            //添加对应技能槽
            skillForm.AddSkillSlot(drMainSkill.Realm, drMainSkill.SkillSlot);
        }

        // private void UpdateSkillData()
        // {
        //     skillForm.SkillCount = 0;
        //     for (int i = 0; i < mainSkills.Length; i++)
        //     {
        //         if (mainSkills[i] == null)
        //             continue;
        //         skillForm.SkillCount += mainSkills[i].SkillSlot;
        //     }

        // }
    }
}
