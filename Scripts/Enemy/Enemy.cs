using Godot;
using System;

namespace Survival
{

    public partial class Enemy : Area2D
    {
        [Export]
        public int Speed;

        public int id = 60000;

        public float HP;
        // public IImpact[] impacts;
        private int MaxHP;

        private int Attack;

        private int Exp;

        public override void _Ready()
        {
            BodyEntered += OnAttack;
            AreaEntered += OnBeAttacked;
            //读取表格中数据
            DREnemy drEnemy = GameEntry.DataTable.GetDataTable<DREnemy>().GetDataRow(id);
            MaxHP = drEnemy.HP;
            Attack = drEnemy.Attack;
            Exp = drEnemy.Exp;
            HP = MaxHP;
            Position = new Vector2(-100, 0);
            OnLoad();
        }

        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;
            OnUpdate();
            if (IsDead())
            {
                OnDead();
            }
        }

        public void OnLoad()
        {

        }

        public void OnUpdate()
        {

        }

        public void OnAttack(Node2D body)
        {
            //HP -= 5;
            //GameEntry.Player.HP -= Attack;
            // GameEntry.Player.DisPlayerCollsionOneSecond();
            // if (impacts != null)
            // {
            //     for (int i = 0; i < impacts.Length; i++)
            //     {
            //         impacts[i].Cause(body, this);
            //     }
            // }
        }

        public void OnBeAttacked(Area2D area)
        {
            // if (area.Name != "face")
            // {
            //     return;
            // }
            Face face = area as Face;
            //GD.Print(area.Name);
            HP -= face.GetDamage();
        }

        private bool IsDead()
        {
            if (HP <= 0)
            {
                return true;
            }
            return false;
        }

        private void OnDead()
        {
            QueueFree();
            GameEntry.Entity.CreateEffect("EnemyDeathEffect", Position);
            GameEntry.Entity.CreateEnemy("Ordinary");
        }
    }
}
