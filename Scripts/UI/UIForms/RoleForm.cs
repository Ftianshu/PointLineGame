using Godot;
using System;

namespace Survival
{

    public partial class RoleForm : Control
    {
        public int leftPoints = 100;

        [Export]
        private Label leftPoint;

        [Export]
        private Label[] proptyLabels;

        private int[] proptys = new int[10];
        private int[] cost = new int[10] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };

        public override void _Ready()
        {
            GD.Randomize();
        }
        public void OnRollDataBtnClick()
        {
            leftPoints = 100;
            proptys = new int[10];
            while (leftPoints != 0)
            {
                uint id = GD.Randi() % 10;
                if (leftPoints >= cost[id])
                {
                    leftPoints -= cost[id];
                    proptys[id]++;
                }
            }
            UpdateProptyLabel();
        }

        public void OnSubBtnClick(int id)
        {
            proptys[id]--;
            leftPoints += cost[id];
            if (proptys[id] <= 0)
            {
                proptyLabels[id].GetNode<Control>("Sub").Hide();
            }
            UpdateAddBtnState();
            proptyLabels[id].Text = proptys[id].ToString();
            UpdatePoint();
        }

        public void OnAddBtnClick(int id)
        {
            if (proptys[id] <= 0)
            {
                proptyLabels[id].GetNode<Control>("Sub").Show();
            }
            proptys[id]++;
            leftPoints -= cost[id];
            UpdateAddBtnState();
            proptyLabels[id].Text = proptys[id].ToString();
            UpdatePoint();
        }

        private void UpdateAddBtnState()
        {
            for (int i = 0; i < cost.Length; i++)
            {
                if (leftPoints < cost[i])
                {
                    proptyLabels[i].GetNode<Control>("Add").Hide();
                }
                else
                {
                    proptyLabels[i].GetNode<Control>("Add").Show();
                }
            }
        }

        private void UpdateSubBtnState()
        {
            for (int i = 0; i < cost.Length; i++)
            {
                if (proptys[i] == 0)
                {
                    proptyLabels[i].GetNode<Control>("Sub").Hide();
                }
                else
                {
                    proptyLabels[i].GetNode<Control>("Sub").Show();
                }
            }
        }

        private void UpdatePoint()
        {
            leftPoint.Text = leftPoints.ToString();
        }

        private void UpdateProptyLabel()
        {
            for (int i = 0; i < proptyLabels.Length; i++)
            {
                proptyLabels[i].Text = proptys[i].ToString();
            }
            UpdatePoint();
            UpdateAddBtnState();
            UpdateSubBtnState();
        }

        public void OnPlayerTabChanged(int index)
        {
            GameEntry.Player.playerIndex = index;
        }
        public void OnConfirmBtnClick()
        {
            GameEntry.Player.SetPlayerData(proptys);
            QueueFree();
            GameEntry.Game.GameStart();
        }
    }
}
