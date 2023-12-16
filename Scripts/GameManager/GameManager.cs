using Godot;
using System;

namespace Survival
{
    //管理游戏流程
    public class GameManager
    {

        private double m_Timer = 0;

        public bool gameStart = false;

        public bool gamePause = false;

        public GameManager()
        {

        }

        public void EnterMainMenu()
        {
            //打开主界面
            GameEntry.UI.OpenUIForm(UIFormId.MenuForm);
        }

        public void EnterRoleMenu()
        {
            //打开角色界面
            GameEntry.UI.OpenUIForm(UIFormId.RoleForm);
        }

        //游戏开始
        public void GameStart()
        {
            //加载背景

            //生成玩家
            GameEntry.Player.CreatePlayer();

            //创建坐标系
            GameEntry.Entity.CreateCoordinate();

            //打开UI
            GameEntry.UI.OpenUIForm(UIFormId.GamingUI);

            //开始波次生成敌人
            GameEntry.Enemy.GenerateNextWaveEnemy();
        }

        public void Update(double delta)
        {
            if (gamePause)
            {
                return;
            }
            m_Timer += delta;
            if (m_Timer > 2)
            {
                //GameEntry.Map.GenerateEnemy();
                m_Timer = 0;
            }
        }

        public void Pause()
        {
            gamePause = true;
        }

        public void ReCover()
        {
            gamePause = false;
        }

    }
}
