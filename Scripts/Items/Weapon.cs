using Godot;
using System;

namespace Survival
{
    public partial class Weapon : Area2D
    {
        public int id = 1000;
        //武器部位
        public int type = 1;
        public override void _Ready()
        {
            DRWeapon drWeapon = GameEntry.DataTable.GetDataTable<DRWeapon>().GetDataRow(id);
            type = drWeapon.Type;
            //动态加载item图片
            Texture2D texture = (Texture2D)GD.Load(AssetUtility.GetItemSpriteAsset(drWeapon.AssetName));
            GetNode<Sprite2D>("Sprite2D").Texture = texture;

            BodyEntered += OnPicked;
        }

        private void OnPicked(Node2D body)
        {
            QueueFree();
            //玩家获得对应装备
            GameEntry.Player.GainWeapon(id, type);
        }
    }
}
