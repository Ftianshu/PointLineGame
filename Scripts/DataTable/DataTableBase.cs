using System;
using System.IO;
using Godot;

namespace Survival
{

    public abstract class DataTableBase
    {
        private readonly string m_Name;
        /// <summary>
        /// 初始化数据表基类的新实例。
        /// </summary>
        /// <param name="name">数据表名称。</param>
        public DataTableBase(string name)
        {
            m_Name = name ?? string.Empty;
        }

        /// <summary>
        /// 获取数据表名称。
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// 获取数据表行的类型。
        /// </summary>
        public abstract Type Type
        {
            get;
        }

        /// <summary>
        /// 获取数据表行数。
        /// </summary>
        public abstract int Count
        {
            get;
        }

        /// <summary>
        /// 增加数据表行。
        /// </summary>
        /// <param name="dataRowString">要解析的数据表行字符串。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否增加数据表行成功。</returns>
        public abstract bool AddDataRow(string dataRowString, object userData);

        public void ReadData(string dataTableAssetName, int priority)
        {
            string[] lines = File.ReadAllLines(dataTableAssetName);

            foreach (string line in lines)
            {
                if (line[0] == '#')
                {
                    continue;
                }
                if (!AddDataRow(line, null))
                {
                    throw new Exception();
                }
            }
        }
    }
}
