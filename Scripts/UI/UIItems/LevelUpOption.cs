using Godot;
using System;
using System.Collections;

namespace Survival
{
    public partial class LevelUpOption : Panel
    {
        private int[][] propNum = new int[4][] {
            new int[9] {5, 1, 2, 1, 1, 3, 1, 5, 10},
            new int[9] {5, 1, 2, 1, 1, 3, 1, 5, 10},
            new int[9] {5, 1, 2, 1, 1, 3, 1, 5, 10},
            new int[9] {5, 1, 2, 1, 1, 3, 1, 5, 10}};

        private string[] m_Props = new string[9] { "速度", "加速度", "平面伤害", "线条伤害", "转向速度", "最大生命值", "护甲", "%闪避", "幸运" };
        private int m_Id;
        private int m_Rarity;
        public override void _Ready()
        {
            // MouseEntered += OnMouseIn;
            // MouseExited += OnMouseExited;
        }


        public void SetDesc(int id, int rarity)
        {
            m_Id = id;
            m_Rarity = rarity;
            string desc = "";
            desc += "[color=green]" + "+" + propNum[rarity][id] + "[/color]" + " ";
            (Material as ShaderMaterial).SetShaderParameter("rarity", rarity);
            desc += "[color=white]" + m_Props[id] + "[/color]" + "\n\n";
            GetNode<RichTextLabel>("Desc").Text = desc;
        }

        public void Selected()
        {
            switch (m_Id)
            {
                case 0:
                    {
                        GameEntry.Player.MaxSpeed += propNum[m_Rarity][m_Id];
                        break;
                    }
                case 1:
                    {
                        GameEntry.Player.Acceleration += propNum[m_Rarity][m_Id];
                        break;
                    }
                case 2:
                    {
                        GameEntry.Player.faceDamage += propNum[m_Rarity][m_Id];
                        break;
                    }
                case 3:
                    {
                        GameEntry.Player.lineDamage += propNum[m_Rarity][m_Id];
                        break;
                    }
                case 4:
                    {
                        //GameEntry.Player. += propNum[m_Rarity][m_Id];
                        break;
                    }
                case 5:
                    {
                        GameEntry.Player.MaxHP += propNum[m_Rarity][m_Id];
                        break;
                    }
                case 6:
                    {
                        //GameEntry.Player.MaxSpeed += propNum[m_Rarity][m_Id];
                        break;
                    }
                case 7:
                    {
                        //GameEntry.Player.MaxSpeed += propNum[m_Rarity][m_Id];
                        break;
                    }
                case 8:
                    {
                        //GameEntry.Player.MaxSpeed += propNum[m_Rarity][m_Id];
                        break;
                    }
            }
            GameEntry.UI.CloseUIForm(UIFormId.LevelUpForm);
        }

        private void OnMouseIn()
        {

        }

        private void OnMouseExited()
        {

        }

        public void ShowDesc()
        {

        }
    }
}
