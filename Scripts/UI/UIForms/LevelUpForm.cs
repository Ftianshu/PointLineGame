using Godot;
using System.Collections.Generic;

namespace Survival
{
    public partial class LevelUpForm : Panel
    {
        //private string[] Descs = new string[10] { "速度+", "加速度+", "平面伤害+", "线条伤害+", "转向速度+", "最大生命值+", "护甲+", "闪避+", "幸运+" };
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
            int id = (int)(GD.Randi() % 9);
            while (ids.Count < 4)
            {
                if (!ids.Contains(id))
                {
                    ids.Add(id);
                    //GD.Print(id);
                }
                id = (int)(GD.Randi() % 9);
            }
            //更新
            Node root = GetNode("LevelUp");
            for (int i = 0; i < root.GetChildCount(); i++)
            {
                //根据概率生成不稀有度的选项
                DRLevelUpOption levelUp = GameEntry.DataTable.GetDataTable<DRLevelUpOption>().GetDataRow(GameEntry.Player.level - 1);
                int rarity = (int)(GD.Randi() % 100);
                if (rarity < levelUp.Prob0)
                {
                    root.GetChild<LevelUpOption>(i).SetDesc(ids[i], 0);
                }
                else if (rarity < levelUp.Prob0 + levelUp.Prob1)
                {
                    root.GetChild<LevelUpOption>(i).SetDesc(ids[i], 1);
                }
                else if (rarity < levelUp.Prob0 + levelUp.Prob1 + levelUp.Prob2)
                {
                    root.GetChild<LevelUpOption>(i).SetDesc(ids[i], 2);
                }
                else if (rarity < levelUp.Prob0 + levelUp.Prob1 + levelUp.Prob2 + levelUp.Prob3)
                {
                    root.GetChild<LevelUpOption>(i).SetDesc(ids[i], 3);
                }
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
