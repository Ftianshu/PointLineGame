using Godot;
using System;

namespace Survival
{

    public partial class MenuBackground : Panel
    {
        private Node[] groups;
        private double time = 0;

        private int state = 0;

        public override void _Ready()
        {
            groups = new Node[4];
            for (int i = 0; i < 4; i++)
            {
                groups[i] = GetNode("DrawLineGroup" + (i + 1));
            }
            for (int i = 0; i < 10; i++)
            {
                var entity = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("DrawLineAnima"));
                Node2D n = entity.Instantiate<Node2D>();
                n.Position = new Vector2(100 * i, 0);
                groups[0].AddChild(n);
            }

            for (int i = 0; i < 5; i++)
            {
                var entity = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("DrawLineAnima"));
                Node2D n = entity.Instantiate<Node2D>();
                n.Position = new Vector2(0, 100 * i + 100);
                groups[0].AddChild(n);
            }

            for (int i = 0; i < 10; i++)
            {
                var entity = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("DrawLineAnima"));
                Node2D n = entity.Instantiate<Node2D>();
                n.Position = new Vector2(100 * i, GetViewportRect().Size.Y);
                n.Rotate(-Mathf.Pi / 2);
                groups[1].AddChild(n);
            }

            for (int i = 0; i < 5; i++)
            {
                var entity = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("DrawLineAnima"));
                Node2D n = entity.Instantiate<Node2D>();
                n.Position = new Vector2(0, 100 * i + 100);
                n.Rotate(-Mathf.Pi / 2);
                groups[1].AddChild(n);
            }

            StartGroupAnima(0);
        }

        public override void _Process(double delta)
        {
            time = time + delta;
            switch (state)
            {
                case 0:
                    {
                        if (time > 5)
                        {
                            // 改变动画模式
                            StartGroupAnima(1);
                            state = 1;
                        }
                        break;
                    }
                case 1:
                    {
                        if (time > 10)
                        {
                            // 改变动画模式
                        }
                        break;
                    }
            }
        }

        private void StartGroupAnima(int index)
        {
            for (int i = 0; i < groups[index].GetChildCount(); i++)
            {
                groups[index].GetChild<DrawLineAnima>(i).SetAnimation("DrawLine");
            }
        }
    }

}