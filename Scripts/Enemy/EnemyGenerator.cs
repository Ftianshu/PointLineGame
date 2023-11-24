using Godot;
using System;

namespace Survival
{

    public class EnemyGenerator
    {

        public EnemyGenerator()
        {

        }

        public void GenerateEnemy()
        {
            GameEntry.Entity.CreateEntity("Enemys/Enemy0");
        }
    }
}
