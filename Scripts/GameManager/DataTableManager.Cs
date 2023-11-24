using System;
using System.Collections.Generic;
using Godot;

namespace Survival
{

    public class DataTableManager
    {
        private const string DataRowClassPrefixName = "Survival.DR";
        internal static readonly char[] DataSplitSeparators = new char[] { '\t' };
        internal static readonly char[] DataTrimSeparators = new char[] { '\"' };

        public static readonly string[] DataRowAssetName = {
            "Map",
            "UIForm",
            "Enemy",
            "Skill",
            "Weapon",
            "MainSkill"
        };

        private Dictionary<Type, DataTableBase> m_DataTables;
        public DataTableManager()
        {
            m_DataTables = new Dictionary<Type, DataTableBase>();
        }

        public void LoadDataTable(string dataTableName, string dataTableAssetName)
        {
            if (string.IsNullOrEmpty(dataTableName))
            {
                return;
            }

            string[] splitedNames = dataTableName.Split('_');
            if (splitedNames.Length > 2)
            {
                return;
            }

            string dataRowClassName = DataRowClassPrefixName + splitedNames[0];
            Type dataRowType = Type.GetType(dataRowClassName);
            if (dataRowType == null)
            {

                return;
            }

            string name = splitedNames.Length > 1 ? splitedNames[1] : null;
            DataTableBase dataTable = CreateDataTable(dataRowType, name);
            dataTable.ReadData(dataTableAssetName, 60);
        }

        public DataTableBase CreateDataTable(Type dataRowType, string name)
        {
            Type dataTableType = typeof(DataTable<>).MakeGenericType(dataRowType);
            DataTableBase dataTable = (DataTableBase)Activator.CreateInstance(dataTableType, name);
            m_DataTables.Add(dataRowType, dataTable);
            return dataTable;
        }

        public DataTable<T> GetDataTable<T>() where T : class, IDataRow, new()
        {
            DataTableBase dataTable = null;
            if (m_DataTables.TryGetValue(typeof(T), out dataTable))
            {
                return (DataTable<T>)dataTable;
            }
            return null;
        }


    }

}