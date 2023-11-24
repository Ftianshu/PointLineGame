using Godot;
using System;

namespace Survival
{

    public partial class ItemSlot : PanelContainer
    {
        [Export]
        private int slotId;

        private ItemUI currentItem;

        [Signal]
        public delegate void itemChangedEventHandler(int from, int to);

        [Export]
        public int[] canDropType;

        public override void _Ready()
        {
            //根据slotId加载框
            Connect("itemChanged", new Callable(GameEntry.UI.GetUIForm(UIFormId.BagForm), "ItemChanged"));
        }

        public override void _DropData(Vector2 atPosition, Variant data)
        {
            ItemUI item = (ItemUI)data;
            //空，则放过来
            if (currentItem == null || !IsInstanceValid(currentItem))
            {
                ItemUI clone = (ItemUI)item.Duplicate();
                CloneItemData(clone, item);

                item.QueueFree();
                currentItem = clone;
                clone.slotId = slotId;
                clone.HideDesc();
                GetNode("Items").AddChild(clone);
            } //物品相同则堆叠
            else if (currentItem.id == item.id)
            {
                if (currentItem.num + item.num <= currentItem.capacity)
                {
                    currentItem.num += item.num;
                    item.QueueFree();
                    return;
                }

                item.num = currentItem.num + item.num - currentItem.capacity;
                currentItem.num = currentItem.capacity;
                return;
            }//物品不同则交换
            else
            {
                //判断对方是否可以接受这边的物品
                ItemSlot other = (ItemSlot)item.GetParent().GetParent();
                if (!other._CanDropData(new Vector2(0, 0), currentItem))
                {
                    //将物品移动到最近的空槽
                    ItemUI clone3 = (ItemUI)item.Duplicate();
                    CloneItemData(clone3, item);
                    GameEntry.Player.GainItem(clone3);
                    item.QueueFree();
                    EmitSignal("itemChanged", item.slotId, clone3.slotId);
                    return;
                }

                ItemUI clone = (ItemUI)item.Duplicate();
                ItemUI clone2 = (ItemUI)currentItem.Duplicate();
                //我加入对方，对方加入我
                CloneItemData(clone, item);
                CloneItemData(clone2, currentItem);
                clone2.slotId = item.slotId;
                clone.slotId = slotId;
                //隐藏描述框
                clone.HideDesc();
                clone2.HideDesc();


                item.GetParent().AddChild(clone2);
                other.currentItem = clone2;
                GetNode("Items").AddChild(clone);

                item.QueueFree();
                currentItem.QueueFree();

                currentItem = clone;
            }

            EmitSignal("itemChanged", item.slotId, slotId);
        }

        public override bool _CanDropData(Vector2 atPosition, Variant data)
        {
            for (int i = 0; i < canDropType.Length; i++)
            {
                if (((ItemUI)data).type == canDropType[i])
                {
                    return true;
                }
            }
            return false;
        }

        public void AddItem(ItemUI newItem)
        {
            GetNode("Items").AddChild(newItem);
            newItem.slotId = slotId;
            currentItem = newItem;
        }

        private void CloneItemData(ItemUI a, ItemUI b)
        {
            a.id = b.id;
            a.type = b.type;
            a.capacity = b.capacity;
        }
    }
}
