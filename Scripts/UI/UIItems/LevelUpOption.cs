using Godot;
using System;
using System.Collections;

namespace Survival
{
    public partial class LevelUpOption : Panel
    {
        private int m_Id;
        private DRLevelUpOption m_LevelUpOption;
        //private int m_Rarity;
        public override void _Ready()
        {
            MouseEntered += OnMouseIn;
            MouseExited += OnMouseExited;
        }

        public override void _GuiInput(InputEvent inputEvent)
        {
            if (inputEvent is InputEventMouseButton mbe && mbe.ButtonIndex == MouseButton.Left)
            {
                Selected();
            }
        }

        public void SetDesc(DRLevelUpOption levelUpOption)
        {
            m_Id = levelUpOption.Id;
            m_LevelUpOption = levelUpOption;
            string desc = "";
            desc += "[color=blue]" + levelUpOption.Name + "[/color]" + "\n\n";
            switch (levelUpOption.Type)
            {
                case "属性提升":
                    {
                        string[] keyValue = levelUpOption.Impact.Split(";");
                        foreach (string item in keyValue)
                        {
                            (string key, string value) = (item.Split(":")[0], item.Split(":")[1]);
                            desc += "[color=green]" + key + "[/color]";
                            desc += ":";
                            desc += "[color=red]" + value + "[/color]";
                            desc += "\n";
                        }
                        break;
                    }
                case "契约效果":
                    {
                        desc += levelUpOption.Impact;
                        break;
                    }
            }
            GetNode<RichTextLabel>("Desc").Text = desc;
        }

        public void Selected()
        {
            switch (m_LevelUpOption.Type)
            {
                case "属性提升":
                    {
                        string[] keyValue = m_LevelUpOption.Impact.Split(";");
                        foreach (string item in keyValue)
                        {
                            (string key, string value) = (item.Split(":")[0], item.Split(":")[1]);
                            switch (key)
                            {
                                case "加速度":
                                    {
                                        GameEntry.Player.Acceleration += float.Parse(value);

                                        break;
                                    }
                                case "最大速度":
                                    {
                                        GameEntry.Player.MaxSpeed += float.Parse(value);
                                        break;
                                    }
                                case "线伤":
                                    {
                                        GameEntry.Player.lineDamage += float.Parse(value);
                                        break;
                                    }
                                case "面伤":
                                    {
                                        GameEntry.Player.faceDamage += float.Parse(value);
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                case "契约效果":
                    {
                        switch (m_LevelUpOption.Name)
                        {
                            case "大圈主义":
                                {
                                    GameEntry.Player.faceDamageType = FaceDamageType.IncreaseWithArea;
                                    break;
                                }
                        }
                        break;
                    }
            }
            GameEntry.UI.CloseUIForm(UIFormId.LevelUpForm);
        }

        private void OnMouseIn()
        {
            (Material as ShaderMaterial).SetShaderParameter("mouseIn", true);
        }

        private void OnMouseExited()
        {
            (Material as ShaderMaterial).SetShaderParameter("mouseIn", false);
        }
    }
}
