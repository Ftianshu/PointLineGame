using Godot;
using System;

namespace Survival
{
    public class FireDamageImpact : IImpact
    {
        public void Cause(Node2D body, Skill data)
        {
            Enemy enemy = (Enemy)body;
            enemy.HP -= data.damage * GameEntry.Player.FireDamageRatio;
        }
    }
}
