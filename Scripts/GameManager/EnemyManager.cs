using Godot;
using System;

namespace Survival
{
    // 主要管理Enemy的波次，以及一些公共数据
    public class EnemyManager
    {
        public int currentBo = 0;
        public float lineDamage = 5;
        public float pointLife = 8f;

        public EnemyManager()
        {

        }
    }
}
