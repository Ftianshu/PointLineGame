using Godot;
using System;

namespace Survival
{

    public partial class GamingUI : Control
    {
        private float time;

        private Label TimeLable;

        private Label WaveCount;

        public override void _Ready()
        {
            TimeLable = GetNode<Label>("Time");
            WaveCount = GetNode<Label>("WaveCount");

            GameEntry.Enemy.Connect("waveUpdate", new Callable(this, "UpdateWaveLable"));
        }

        public override void _Process(double delta)
        {
            if (GameEntry.Game.gamePause)
                return;

            int tmp = (int)time;
            time += (float)delta;
            if ((int)time > tmp)
                UpdateTimeLabel();
        }

        private void UpdateTimeLabel()
        {
            int fen = (int)time / 60;
            int second = (int)time % 60;
            TimeLable.Text = (fen < 10 ? "0" + fen : fen) + ":" + (second < 10 ? "0" + second : second);
        }

        public void UpdateWaveLable()
        {
            //更新Bo
            WaveCount.Text = "第" + GameEntry.Enemy.currentWave + "波";
        }
    }
}
