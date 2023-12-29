using Godot;
using System;

namespace Survival
{

    public class PlayerManager
    {
        public int money = 0;
        private float m_HP;
        private float m_Exp;
        private float m_MaxSpeed;
        private float m_Acceleration;
        public int level;

        public int next_level_Exp;

        public FaceDamageType faceDamageType = FaceDamageType.BaseMode;

        public float faceDamage = 5;

        public FaceDamageType lineDamageType = FaceDamageType.BaseMode;

        public float lineDamage = 5;
        public float pointLife = 3f;

        public float ExpRatio = 1;
        public float Acceleration
        {
            set
            {
                m_Acceleration = value;
                playerEntity.Acceleration = value;
                playerEntity.DeAcceleration = 2 * m_Acceleration;
            }
            get
            {
                return m_Acceleration;
            }
        }
        public float MaxSpeed
        {
            set
            {
                m_MaxSpeed = value;
                playerEntity.MaxSpeed = value;
            }
            get
            {
                return m_MaxSpeed;
            }
        }
        public float Exp
        {
            private set
            {

            }
            get
            {
                return m_Exp;
            }
        }

        public float HP
        {
            private set
            {
                m_HP = value;
                playerEntity.EmitSignal("playerHPChanged");
            }
            get
            {
                return m_HP;
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
            // TODO:获取玩家初始数据
            HP = 10;
            MaxHP = 10;
            MaxSpeed = 100;
            Acceleration = 0.5f;
        }


        private void PlayerLevelup()
        {
            level++;
            next_level_Exp = levelExp[level];
            //playerEntity.EmitSignal("playerLevelChanged");
            //显示升级选择属性界面
            GameEntry.UI.OpenUIForm(UIFormId.LevelUpForm);
        }



        public void CauseDamage(float damage)
        {
            HP -= damage;
            if (IsPlayerDead())
            {
                // GameOver
                GD.Print("GameOver");
                // 打开失败结算页面
            }
        }

        public void GainExp(float exp)
        {
            m_Exp += exp * ExpRatio;
            if (m_Exp >= next_level_Exp)
            {
                m_Exp = m_Exp - next_level_Exp;
                PlayerLevelup();
            }


            playerEntity.EmitSignal("playerExpChanged");
        }

        public bool IsPlayerDead()
        {
            if (m_HP < 0)
                return true;
            return false;
        }


        public void GainMoney(int gain)
        {
            money += gain;

            playerEntity.EmitSignal("playerMoneyChanged");
        }
    }
}
