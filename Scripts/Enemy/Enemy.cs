using Godot;
using System;

namespace Survival
{

    public abstract partial class Enemy : Area2D
    {
        [Export]
        public int Speed;
        [Export]
        public int RushSpeed;

        public bool IsLinking;

        public bool IsBeAttacked;

        public int id = 60000;

        public int lastDamageForm = -1;

        public float HP;
        // public IImpact[] impacts;
        public int MaxHP;

        public int Attack;

        public int Exp;

        public override void _Ready()
        {
            BodyEntered += OnAttack;
            AreaEntered += OnBeAttacked;
            AreaExited += OnPointLeave;
            //读取表格中数据
            DREnemy drEnemy = GameEntry.DataTable.GetDataTable<DREnemy>().GetDataRow(id);
            MaxHP = drEnemy.HP;
            Attack = drEnemy.Attack;
            Exp = drEnemy.Exp;
            HP = MaxHP;
            OnLoad();
        }

        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;
            OnUpdate(delta);
            if (IsDead())
            {
                OnDead();
            }
        }

        public abstract void OnLoad();

        public abstract void OnUpdate(double delta);

        public abstract void OnAttack(Node2D body);

        public abstract void OnBeAttacked(Area2D area);

        public void OnPointLeave(Area2D area)
        {
            if (area.GetType().ToString() == "Survival.Point")
            {
                foreach (Area2D area2D in GetOverlappingAreas())
                {
                    if (area2D == area)
                    {
                        GD.Print("此方案不科学");
                        return;
                    }
                }
                lastDamageForm = -1;
            }
        }



        private bool IsDead()
        {
            if (HP <= 0)
            {
                return true;
            }
            return false;
        }

        public abstract void OnDead();
    }
}
