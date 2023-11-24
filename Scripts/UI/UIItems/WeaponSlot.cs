using Godot;
using System;

namespace Survival
{

    public partial class WeaponSlot : ItemSlot
    {
        public override bool _CanDropData(Vector2 atPosition, Variant data)
        {
            if (base._CanDropData(atPosition, data))
            {
                return ((WeaponUI)data).isCanUse;
            }
            return false;
        }
    }
}
