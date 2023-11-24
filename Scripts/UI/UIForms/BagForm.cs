using Godot;

namespace Survival
{
    public partial class BagForm : Control
    {
        private ItemUI[] bagItems = new ItemUI[35];
        [Export]
        private Node BagRoot;
        private ItemUI[] WeaponItems = new ItemUI[10];
        [Export]
        private Node WeaponSlotRoot;
        [Export]
        private Node ProptiesRoot;

        public override void _Ready()
        {
            //ShowItems();
            VisibilityChanged += OnShow;

        }

        public void OnShow()
        {
            if (Visible)
            {
                //背包被重新打开
                GameEntry.Game.Pause();
                UpdatePropData();
                UpdateWeaponItem();
                UpdateOtherProp();
            }
            else
            {
                GameEntry.Player.UpdateWeaponSkill();
                GameEntry.Game.ReCover();
                GameEntry.Player.UpdatePlayerDataToPlayer();
            }
        }

        // private void ShowItems()
        // {

        // }

        // 更新武器状态
        private void UpdateWeaponItem()
        {
            for (int i = 0; i < bagItems.Length; i++)
            {
                if (IsInstanceValid(bagItems[i]))
                {
                    if (bagItems[i].GetType().Name == "WeaponUI")
                    {
                        ((WeaponUI)bagItems[i]).UpdateWeaponState();
                    }
                }
            }
        }

        private void UpdatePropData()
        {
            for (int i = 0; i < 10; i++)
            {
                ProptiesRoot.GetChild(i).GetChild<Label>(0).Text = GameEntry.Player.playerData[i].ToString();
            }
        }

        private void UpdateOtherProp()
        {
            Node otherProp = GetNode("RoleWeapon/OtherProp");

            //otherProp.GetChild(0).GetChild<Label>(0).Text = 
            //otherProp.GetChild(1).GetChild<Label>(0).Text = 
        }

        public void AddItem(ItemUI newItem)
        {
            //先看看有没有未满的槽
            for (int i = 0; i < bagItems.Length; i++)
            {
                if (bagItems[i] != null && IsInstanceValid(bagItems[i]))
                {
                    if (bagItems[i].type == newItem.type && newItem.num + bagItems[i].num <= bagItems[i].capacity)
                    {
                        bagItems[i].num += newItem.num;
                        return;
                    }
                }
            }

            int index = GetRecentEmptySlot();
            bagItems[index] = newItem;
            ItemSlot itemSlot = (ItemSlot)BagRoot.GetChild(index);
            itemSlot.AddItem(newItem);
        }

        private int GetRecentEmptySlot()
        {
            for (int i = 0; i < bagItems.Length; i++)
            {
                if (bagItems[i] != null && IsInstanceValid(bagItems[i]))
                {
                    continue;
                }
                return i;
            }
            return -1;
        }

        public void WearWeapon(ItemUI item)
        {
            for (int i = 0; i < WeaponSlotRoot.GetChildCount(); i++)
            {
                ItemSlot weaponSlot = WeaponSlotRoot.GetChild<ItemSlot>(i);
                if (weaponSlot._CanDropData(Vector2.Zero, item))
                {
                    weaponSlot._DropData(Vector2.Zero, item);
                    return;
                }
            }
        }

        public void MoveItemToEmptySlot(ItemUI item)
        {
            int index = GetRecentEmptySlot();
            ItemSlot itemSlot = (ItemSlot)BagRoot.GetChild(index);
            itemSlot._DropData(Vector2.Zero, item);
        }



        public void OnCloseBtnClick()
        {
            GameEntry.UI.CloseUIForm(this);
        }

        public void ItemChanged(int from, int to)
        {
            if (from >= 100)
            {
                int index = from - 100;
                Node slot = WeaponSlotRoot.GetChild(index);
                WeaponItems[index] = (ItemUI)GetVaildNode(slot);
                slot.GetNode<Control>("Icon").Visible = true;
                GameEntry.Player.TakeOffWeapon(index);
                UpdatePropData();
            }
            else
            {
                bagItems[from] = (ItemUI)GetVaildNode(BagRoot.GetChild(from));
            }
            if (to >= 100)
            {
                int index = to - 100;
                Node slot = WeaponSlotRoot.GetChild(index);
                WeaponItems[index] = (ItemUI)GetVaildNode(slot);
                slot.GetNode<Control>("Icon").Visible = false;
                GameEntry.Player.WearWeapon(index, WeaponItems[index]);
                UpdatePropData();
            }
            else
            {
                bagItems[to] = (ItemUI)GetVaildNode(BagRoot.GetChild(to));

            }
            //GD.Print("Item form:" + from + "  Item to:" + to);
        }

        private Node GetVaildNode(Node parent)
        {
            for (int i = 0; i < parent.GetNode("Items").GetChildCount(); i++)
            {
                if (IsInstanceValid(parent.GetNode("Items").GetChild(i)))
                {
                    return parent.GetNode("Items").GetChild(i);
                }
            }
            return null;
        }
    }
}
