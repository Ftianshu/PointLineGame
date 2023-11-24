using Godot;
using System;
using System.Collections.Generic;

namespace Survival
{

    public partial class SkillDeployer : Timer
    {
        public int skillId;

        public DRSkill drSkill = null;

        public SkillDeployer(int id)
        {
            skillId = id;
        }

        public override void _Ready()
        {
            WaitTime = 2;
            Autostart = true;
            Timeout += GenerateSkill;
        }

        public void ReStart()
        {
            //重新更新技能信息
            if (skillId != 0)
            {
                drSkill = GameEntry.DataTable.GetDataTable<DRSkill>().GetDataRow(skillId);
                WaitTime = drSkill.CD;
            }
        }

        public void GenerateSkill()
        {
            //GD.Print("释放技能" + skillId);
            Skill skill = (Skill)GameEntry.Entity.CreateSkill(drSkill.AssetName);
            skill.id = skillId;
            skill.InitData(drSkill);
            //判断技能释放是否由功法控制
            //if()
            //根据技能释放类型，添加行动逻辑
            switch (drSkill.ReleaseType)
            {
                case 0: skill.AddChild(new AttackRecentEnemy()); break;
                case 1: skill.AddChild(new AttackRecentEnemyRange()); break;
                case 2: skill.AddChild(new AttackFace()); break;
            }
            //skill.AddChild(new TrackRecentEnemy());
            //skill.AddChild(new FollowCursor());

        }


    }
}
