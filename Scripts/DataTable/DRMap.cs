//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2023-09-27 20:33:33.797
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Godot;


namespace Survival
{
    /// <summary>
    /// 地图表。
    /// </summary>
    public class DRMap : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取地图编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取地图名称。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取地图描述。
        /// </summary>
        public string Desc
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取敌人编号。
        /// </summary>
        public string Enemy
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取敌人刷新概率（%）。
        /// </summary>
        public string EnemyProb
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取地图Boss编号。
        /// </summary>
        public string Boss
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            Name = columnStrings[index++];
            Desc = columnStrings[index++];
            Enemy = columnStrings[index++];
            EnemyProb = columnStrings[index++];
            Boss = columnStrings[index++];

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Name = binaryReader.ReadString();
                    Desc = binaryReader.ReadString();
                    Enemy = binaryReader.ReadString();
                    EnemyProb = binaryReader.ReadString();
                    Boss = binaryReader.ReadString();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
