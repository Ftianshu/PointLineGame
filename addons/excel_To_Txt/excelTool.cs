#if TOOLS
using Godot;
using System;

[Tool]
public partial class excelTool : EditorPlugin
{
    Control dock;
    public override void _EnterTree()
    {
        dock = (Control)GD.Load<PackedScene>("addons/excel_To_Txt/excel_dock.tscn").Instantiate();
        AddControlToDock(DockSlot.LeftBr, dock);
    }


    public override void _ExitTree()
    {
        // Remove the dock.
        RemoveControlFromDocks(dock);
        // Erase the control from the memory.
        dock.Free();
    }
}
#endif
