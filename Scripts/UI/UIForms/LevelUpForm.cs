using Godot;
using System.Collections.Generic;

namespace Survival
{
    public partial class LevelUpForm : Panel
    {
        //private string[] Descs = new string[10] { "力量+", "体质+", "敏捷+", "精神+", "魅力+", "金灵根+", "木灵根+", "水灵根+", "火灵根+", "土灵根+" };
        public override void _Ready()
        {
            VisibilityChanged += OnShow;
            //第一次打开不会触发OnShow
            GameEntry.Game.Pause();
            UpdateOptions();
        }

        public void OnShow()
        {
            if (Visible)
            {
                GameEntry.Game.Pause();
                UpdateOptions();
            }
            else
            {
                GameEntry.Game.ReCover();
            }
        }

        //更新升级的三个选项
        public void UpdateOptions()
        {
            //先抽出新的三个选项
            List<int> ids = new List<int>();
            int id = (int)(GD.Randi() % 4);
            while (ids.Count < 3)
            {
                if (!ids.Contains(id))
                {
                    ids.Add(id);
                    //GD.Print(id);
                }
                id = (int)(GD.Randi() % 4);
            }
            //更新
            Node root = GetNode("LevelUp");
            for (int i = 0; i < root.GetChildCount(); i++)
            {
                DRLevelUpOption drLevelUp = GameEntry.DataTable.GetDataTable<DRLevelUpOption>().GetDataRow(ids[i]);
                root.GetChild<LevelUpOption>(i).SetDesc(drLevelUp);
            }
        }


        //选择其中的一个奖励
        public void ChooseOption(int id)
        {
            //根据所选id找到对应的奖励
            //
            //设置参数
            //关闭页面
            GameEntry.UI.CloseUIForm(UIFormId.LevelUpForm);
        }
    }

}
