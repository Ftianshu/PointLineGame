using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survival
{
    public partial class GameEntry : Node
    {
        public static DataTableManager DataTable
        {
            get;
            private set;
        }

        public static GameManager Game
        {
            get;
            private set;
        }

        public static UIManager UI
        {
            get;
            private set;
        }

        public static MapManager Map
        {
            get;
            private set;
        }

        public static EntityManager Entity
        {
            get;
            private set;
        }

        public static PlayerManager Player
        {
            get;
            private set;
        }

        public static SkillManager Skill
        {
            get;
            private set;
        }

        public static FaceManager Face
        {
            get;
            private set;
        }

        public override void _Ready()
        {
            //初始化，加载静态资源
            Init();
            PreLoadData();

            //进入主界面
            Game.EnterMainMenu();
        }

        public override void _Process(double delta)
        {
            if (!Game.gameStart)
            {
                return;
            }

            Game.Update(delta);
        }

        private void Init()
        {
            DataTable = new DataTableManager();
            Game = new GameManager();
            UI = new UIManager(GetNode<Node>("UIRoot"));
            Map = new MapManager(GetNode<Node>("MapRoot"));
            Entity = new EntityManager(GetNode<Node>("EntityRoot"), GetNode<Node>("EntityRoot/EnemyRoot"));
            Player = new PlayerManager();
            Skill = new SkillManager(GetNode<Node>("SkillTimerRoot"));
            Face = new FaceManager();
        }

        private void PreLoadData()
        {
            string[] DataRowAssetName = DataTableManager.DataRowAssetName;
            foreach (string item in DataRowAssetName)
            {
                DataTable.LoadDataTable(item, AssetUtility.GetDataTableAsset(item));
            }
        }

    }

}
