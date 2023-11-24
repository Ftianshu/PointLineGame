using Godot;
using System;

namespace Survival
{
    public interface IImpact
    {
        public void Cause(Node2D body, Skill data);
    }
}
