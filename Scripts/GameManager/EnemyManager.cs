using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Survival
{
    // 主要管理Enemy的波次，以及一些公共数据
    public partial class EnemyManager : Node
    {
        public int currentWave = 0;
        public float lineDamage = 5;
        public float pointLife = 10f;

        private double nextEnemyTime = double.MaxValue;

        private int currentIndex = 0;

        private int currentLivingEnemy = 0;

        private double timeDelta = 0;

        public List<DREnemyGenerator> EnemyMessage = new List<DREnemyGenerator>();

        [Signal]
        public delegate void waveUpdateEventHandler();

        public void BandGameUI()
        {
            //GameEntry.UI.GetUIForm(UIFormId.GamingUI)
        }

        //生成下一波敌人
        public void GenerateNextWaveEnemy()
        {
            //读取当前bo的所有敌人信息,从Database中读取这波敌人的信息,存入List中
            UpdateEnemyMessage();
            //生成敌人
            GenerateEnemy();

            currentWave++;

            EmitSignal("waveUpdate");
        }

        private void UpdateEnemyMessage()
        {
            DataTable<DREnemyGenerator> dtEnemy = GameEntry.DataTable.GetDataTable<DREnemyGenerator>();
            dtEnemy.GetDataRows((DREnemyGenerator drEnemy) =>
            {
                if (drEnemy.WaveCount == currentWave)
                {
                    return true;
                }
                return false;
            }, EnemyMessage);
            // for (int i = 0; i < EnemyMessage.Count; i++)
            // {
            //     GD.Print(EnemyMessage[i].EnemyId);
            // }
            //初始化时钟位置
            currentIndex = 0;
            currentLivingEnemy = EnemyMessage.Count;
        }

        public void GenerateEnemy()
        {
            if (EnemyMessage.Count == 0)
            {
                return;
            }
            switch (EnemyMessage[currentIndex].GenerateType)
            {
                case "指定位置":
                    {
                        if (EnemyMessage[currentIndex].Target != new Vector2(-10000, -10000))
                        {
                            TargetEnemy enemy = GameEntry.Entity.CreateEnemy(EnemyMessage[currentIndex].AssetName) as TargetEnemy;
                            enemy.Position = EnemyMessage[currentIndex].Position;
                            enemy.SetTarget(EnemyMessage[currentIndex].Target);
                            enemy.Connect("enemyDeath", new Callable(this, "OnEnemyDeath"));
                        }
                        else
                        {
                            Enemy enemy = GameEntry.Entity.CreateEnemy(EnemyMessage[currentIndex].AssetName) as Enemy;
                            enemy.Position = EnemyMessage[currentIndex].Position;
                            enemy.Connect("enemyDeath", new Callable(this, "OnEnemyDeath"));
                        }

                        break;
                    }
                case "随机位置":
                    {
                        // 生成位置标记

                        break;
                    }
            }
            timeDelta = 0;
            nextEnemyTime = EnemyMessage[currentIndex].NextEnemyTime;
            currentIndex++;

        }

        private void UpdateWave()
        {

        }

        public override void _Process(double delta)
        {
            timeDelta += delta;
            if (timeDelta > nextEnemyTime && currentIndex < EnemyMessage.Count)
            {
                GenerateEnemy();
            }
        }

        public void OnEnemyDeath()
        {
            currentLivingEnemy--;
            //进入下一波，或者进入一些别的流程
            if (currentLivingEnemy < 1)
            {
                GenerateNextWaveEnemy();
            }
        }

    }
}
