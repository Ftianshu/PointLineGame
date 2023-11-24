//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------


using System;
using Godot;

namespace Survival
{
    public static class DataTableExtension
    {
        private const string DataRowClassPrefixName = "Survival.DR";
        internal static readonly char[] DataSplitSeparators = new char[] { '\t' };
        internal static readonly char[] DataTrimSeparators = new char[] { '\"' };

        // public static void LoadDataTable(this DataTableComponent dataTableComponent, string dataTableName, string dataTableAssetName, object userData)
        // {
        //     if (string.IsNullOrEmpty(dataTableName))
        //     {
        //         return;
        //     }

        //     string[] splitedNames = dataTableName.Split('_');
        //     if (splitedNames.Length > 2)
        //     {
        //         return;
        //     }

        //     string dataRowClassName = DataRowClassPrefixName + splitedNames[0];
        //     Type dataRowType = Type.GetType(dataRowClassName);
        //     if (dataRowType == null)
        //     {
        //         return;
        //     }

        //     string name = splitedNames.Length > 1 ? splitedNames[1] : null;
        //     DataTableBase dataTable = dataTableComponent.CreateDataTable(dataRowType, name);
        //     dataTable.ReadData(dataTableAssetName, Constant.AssetPriority.DataTableAsset, userData);
        // }

        // public static Color ParseColor(string value)
        // {
        //     string[] splitedValue = value.Split(',');
        //     return new Color(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        // }

        // public static Quaternion ParseQuaternion(string value)
        // {
        //     string[] splitedValue = value.Split(',');
        //     return new Quaternion(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        // }

        // public static Vector2 ParseVector2(string value)
        // {
        //     string[] splitedValue = value.Split(',');
        //     return new Vector2(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]));
        // }

        // public static Vector3 ParseVector3(string value)
        // {
        //     string[] splitedValue = value.Split(',');
        //     return new Vector3(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]));
        // }

        // public static Vector4 ParseVector4(string value)
        // {
        //     string[] splitedValue = value.Split(',');
        //     return new Vector4(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        // }
    }
}
