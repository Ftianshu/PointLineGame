using Godot;
using System;

public partial class Hud : Marker2D
{
    // 持续时间
    public int duration;
    // 数值
    public int num;
    public override void _Ready()
    {
        // 初始化位置
        // Position = 
        GetNode<Label>("Label").Text = num.ToString();
    }

    public override void _Process(double delta)
    {

    }

    public void OnDistroy()
    {

    }
}
