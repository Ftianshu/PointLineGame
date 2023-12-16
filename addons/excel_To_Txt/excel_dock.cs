
using Godot;
using System;
using Survival.Editor;
using Survival;

[Tool]
public partial class excel_dock : ScrollContainer
{
    string[] DataTableNames = { "Enemy", "UIForm" };
    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {

    }


    public void _on_pressed()
    {

        foreach (string dataTableName in DataTableManager.DataRowAssetName)
        {
            DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
            if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
            {
                //
                break;
            }

            DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
            DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
        }
    }
}