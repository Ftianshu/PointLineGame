using Godot;
using System;

namespace Survival
{

    public class PlayerManager
    {
        public int money = 0;
        private float m_HP;
        private float m_Exp;
        public int level;
        public int next_level_Exp;

        public FaceDamageType faceDamageType = FaceDamageType.BaseMode;

        public float faceDamage = 5;

        public FaceDamageType lineDamageType = FaceDamageType.BaseMode;

        public float lineDamage = 5;
        public float pointLife = 3f;

        public float HP
        {
            set
            {
                m_HP = value;
                playerEntity.EmitSignal("playerHPChanged");
            }
            get
            {
                return m_HP;
            }
        }
        public float Exp
        {
            set
            {
                if (value >= next_level_Exp)
                {
                    m_Exp = value - next_level_Exp;
                    PlayerLevelup();
                }
                else
                {
                    m_Exp = value;
                }

                playerEntity.EmitSignal("playerExpChanged");
            }
            get
            {
                return m_Exp;
            }
        }
        //等级所需表
        private int[] levelExp = new int[20] { 10, 20, 40, 80, 150, 230, 300, 400, 600, 1000, 1500, 2500, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 20000 };
        public float MaxHP;
        public PlayerController playerEntity;
        public PlayerManager()
        {

        }

        public void CreatePlayer()
        {
            string playerPath = "Players/";
            playerEntity = (PlayerController)GameEntry.Entity.CreateEntity(playerPath + "Player");
        }


        private void PlayerLevelup()
        {
            level++;
            next_level_Exp = levelExp[level];
            playerEntity.EmitSignal("playerLevelChanged");
            //显示升级选择属性界面
            GameEntry.UI.OpenUIForm(UIFormId.LevelUpForm);
        }


        public void GainMoney(int gain)
        {
            money += gain;

            playerEntity.EmitSignal("playerMoneyChanged");
        }
    }
}
