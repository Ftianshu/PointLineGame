using Godot;
using System;

namespace Survival
{
    public class DamageImpact : IImpact
    {
        public void Cause(Node2D body, Skill data)
        {
            Enemy enemy = (Enemy)body;
            enemy.HP -= data.damage;
            // TODO:添加HUD
            GameEntry.Entity.CreateHud((int)data.damage, enemy.Position);
        }
    }
}
