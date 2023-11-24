using Godot;
using System.Collections.Generic;

namespace Survival
{

    public class UIManager
    {
        //private UIItem
        private Node UIRoot = null;

        private Dictionary<string, Control> uiForms;
        public UIManager(Node uiRoot)
        {
            UIRoot = uiRoot;
            uiForms = new Dictionary<string, Control>();
        }

        public Control OpenUIForm(UIFormId uiFormId)
        {
            DataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow((int)uiFormId);
            if (uiForms.TryGetValue(drUIForm.AssetName, out var uiForm))
            {
                uiForm.Show();
                return uiForm;
            }

            uiForm = (Control)GD.Load<PackedScene>(AssetUtility.GetUIFormAsset(drUIForm.AssetName)).Instantiate();
            uiForms.Add(drUIForm.AssetName, uiForm);
            UIRoot.AddChild(uiForm);
            return uiForm;
        }

        public void CloseUIForm(UIFormId uiFormId)
        {
            DataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow((int)uiFormId);
            // if (uiForms.TryGetValue(drUIForm.AssetName, out var uiForm))
            // {
            //     uiForms.Remove(drUIForm.AssetName);
            //     uiForm.QueueFree();
            // }

            if (uiForms.TryGetValue(drUIForm.AssetName, out var uiForm))
            {
                uiForm.Hide();
            }
        }

        public Control GetUIForm(UIFormId uiFormId)
        {
            DataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow((int)uiFormId);
            if (uiForms.TryGetValue(drUIForm.AssetName, out var uiForm))
            {
                return uiForm;
            }
            return null;
        }

        public void CloseUIForm(Control node)
        {
            node.Hide();
        }
    }
}
