using Godot;
using System;
using System.Collections.Generic;

namespace Survival
{
    //事件系统
    //事件分为通用事件，地图事件
    //每过一个季度都有可能触发事件
    //通用事件：每个季度必然会触发一个
    //地图事件：触发概率由地图决定，每个事件相对独立。
    public class EventManager
    {
        public EventManager()
        {

        }

        public void NewYear()
        {
            //选取一些事件
            //GameEntry.DataTable.GetDataTable<DRGEvent>()
            //打开事件表
            //事件触发
        }
    }
}
