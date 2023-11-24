using Godot;
using System;

namespace Survival
{
    public partial class LevelUpOption : Panel
    {
        [Signal]
        public delegate void selectEventHandler(int id);
        private int m_Id;
        //private int m_Rarity;
        public override void _Ready()
        {
            //连接form
            Connect("select", new Callable(GameEntry.UI.GetUIForm(UIFormId.LevelUpForm), "ChooseOption"));
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

        public void SetDesc(int id, string desc)
        {
            m_Id = id;
            GetNode<Label>("Desc").Text = desc;
        }

        public void Selected()
        {
            EmitSignal("select", m_Id);
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
