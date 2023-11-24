using Godot;
using System;

namespace Survival
{

    public partial class ProptyLabel : Label
    {
        private int num;

        public override void _Ready()
        {
            num = int.Parse(Text);
        }
        public void OnAddBtnClick()
        {
            num++;
            Text = num.ToString();
            if (num > 0)
            {
                Show();
            }
        }

        public void OnSubBtnClick()
        {
            num--;
            Text = num.ToString();
            if (num == 0)
            {
                Hide();
            }
        }
    }

}