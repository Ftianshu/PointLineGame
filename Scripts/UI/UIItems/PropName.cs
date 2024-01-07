using Godot;
using System;
using System.Collections;

namespace Survival
{
    public partial class PropName : Label
    {
        public override void _Ready()
        {
            MouseEntered += ShowDesc;
            MouseExited += HideDesc;
        }

        public void ShowDesc()
        {
            GetChild<Panel>(0).Visible = true;
        }

        public void HideDesc()
        {
            GetChild<Panel>(0).Visible = false;
        }
    }
}
