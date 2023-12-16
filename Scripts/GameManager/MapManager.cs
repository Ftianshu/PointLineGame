using Godot;
using System;
using System.Collections.Generic;

namespace Survival
{

    public class MapManager
    {
        private Node MapRoot;
        private int currentMap;
        private int[] enemyIds;
        private int[] enemyProbs;
        public MapManager(Node mapRoot)
        {
            MapRoot = mapRoot;

        }

        public void ChangeMap(int mapId)
        {
            MapRoot.GetChild(0).QueueFree();
            LoadMap(mapId);
        }

        public void LoadMap(int mapId)
        {
            // currentMap = mapId;
            // var map = GD.Load<PackedScene>(AssetUtility.GetMapAsset("Map" + mapId.ToString()));
            // MapRoot.AddChild(map.Instantiate());
            // //加载地图敌人信息
            // DRMap drMap = GameEntry.DataTable.GetDataTable<DRMap>().GetDataRow(mapId);
            // string[] enemyStr = drMap.Enemy.Split("/");
            // string[] enemyProbStr = drMap.EnemyProb.Split("/");
            // enemyProbs = IntArrayToStringArray(enemyProbStr);
            // enemyIds = IntArrayToStringArray(enemyStr);
        }

        //生成敌人
        public void GenerateEnemy()
        {
            uint random = GD.Randi() % 100;
            uint index = 0;
            int sum = 0;
            for (uint i = 0; i < enemyProbs.Length; i++)
            {
                if (random < enemyProbs[i] + sum)
                {
                    index = i;
                    break;
                }
                sum += enemyProbs[i];
            }
            GameEntry.Entity.CreateEnemy("Enemy" + enemyIds[index]);
        }

        public Vector2 GetRandomPositionInMap()
        {
            CollisionShape2D shapeNode = MapRoot.GetChild(0).GetNode<CollisionShape2D>("Shape2D");
            Rect2 shapeSize = shapeNode.Shape.GetRect();
            Vector2 randomPosition = Vector2.Zero;
            randomPosition.X = (GD.Randi() % shapeSize.Size.X) - shapeSize.Size.X / 2;
            randomPosition.Y = (GD.Randi() % shapeSize.Size.Y) - shapeSize.Size.Y / 2;

            return randomPosition;
        }

        private int[] IntArrayToStringArray(string[] strArr)
        {

            int[] intArr = new int[strArr.Length];
            for (int i = 0; i < strArr.Length; i++)
            {
                intArr[i] = int.Parse(strArr[i]);
            }
            return intArr;
        }


    }
}
