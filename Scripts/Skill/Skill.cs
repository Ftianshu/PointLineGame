using Godot;
using System;

namespace Survival
{
    public partial class Skill : Area2D
    {
        public int id;
        public int attackCount = 1;
        public float damage = 0;
        public IImpact[] impacts;
        public float duration;
        public override void _Ready()
        {
            AreaEntered += OnAttack;
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();
        }

        private void OnAttack(Node2D body)
        {
            attackCount--;
            if (attackCount <= 0)
            {
                QueueFree();
            }

            if (impacts != null)
            {
                for (int i = 0; i < impacts.Length; i++)
                {
                    impacts[i].Cause(body, this);
                }
            }
        }

        private void OnDead()
        {
            QueueFree();
        }

        public void InitData(DRSkill drSkill)
        {
            duration = drSkill.Duration;
            //添加持续时间计时器
            AddDeadTimer();

            impacts = GetImpacts(drSkill);
        }

        private void AddDeadTimer()
        {
            Timer timer = new Timer();
            timer.WaitTime = duration;
            timer.Timeout += OnDead;
            timer.OneShot = true;
            timer.Autostart = true;
            AddChild(timer);
        }

        private IImpact[] GetImpacts(DRSkill drSkill)
        {

            string[] impactsStr = drSkill.Impact.Split("/");
            IImpact[] impacts = new IImpact[impactsStr.Length];
            for (int i = 0; i < impactsStr.Length; i++)
            {
                string impactName = impactsStr[i].Split(":")[0];
                string impactData = impactsStr[i].Split(":")[1];
                switch (impactName)
                {
                    case "伤害":
                        {
                            impacts[i] = new DamageImpact();
                            damage += float.Parse(impactData);
                            break;
                        }
                    case "火系伤害":
                        {
                            impacts[i] = new FireDamageImpact();
                            damage += float.Parse(impactData);
                            break;
                        }
                    case "附加伤害":
                        {
                            AddExtraDamage(impactData);
                            break;
                        }
                }
            }
            return impacts;
        }

        private void AddExtraDamage(string impactData)
        {
            string[] impactsStr = impactData.Split("+");
            for (int i = 0; i < impactsStr.Length; i++)
            {
                string impactName = impactsStr[i].Split("*")[0];
                float data = float.Parse(impactsStr[i].Split("*")[1]);
                switch (impactName)
                {
                    case "力量":
                        {
                            damage += GameEntry.Player.playerData[0] * data;
                            break;
                        }
                    case "体质":
                        {
                            damage += GameEntry.Player.playerData[1] * data;
                            break;
                        }
                    case "敏捷":
                        {
                            damage += GameEntry.Player.playerData[2] * data;
                            break;
                        }
                    case "精神":
                        {
                            damage += GameEntry.Player.playerData[3] * data;
                            break;
                        }
                    case "魅力":
                        {
                            damage += GameEntry.Player.playerData[4] * data;
                            break;
                        }
                    case "金灵根":
                        {
                            damage += GameEntry.Player.playerData[5] * data;
                            break;
                        }
                    case "木灵根":
                        {
                            damage += GameEntry.Player.playerData[6] * data;
                            break;
                        }
                    case "水灵根":
                        {
                            damage += GameEntry.Player.playerData[7] * data;
                            break;
                        }
                    case "火灵根":
                        {
                            damage += GameEntry.Player.playerData[8] * data;
                            break;
                        }
                    case "土灵根":
                        {
                            damage += GameEntry.Player.playerData[9] * data;
                            break;
                        }
                }
            }
        }
    }

}
