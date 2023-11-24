//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2023-09-27 20:33:33.799
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Godot;


namespace Survival
{
    /// <summary>
    /// 敌人表。
    /// </summary>
    public class DREnemy : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取敌人编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取血量。
        /// </summary>
        public int HP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取伤害。
        /// </summary>
        public int Attack
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取经验。
        /// </summary>
        public int Exp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取等级。
        /// </summary>
        public int Level
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取类型。
        /// </summary>
        public int Type
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
            index++;
            HP = int.Parse(columnStrings[index++]);
            Attack = int.Parse(columnStrings[index++]);
            Exp = int.Parse(columnStrings[index++]);
            Level = int.Parse(columnStrings[index++]);
            Type = int.Parse(columnStrings[index++]);

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
                    HP = binaryReader.Read7BitEncodedInt32();
                    Attack = binaryReader.Read7BitEncodedInt32();
                    Exp = binaryReader.Read7BitEncodedInt32();
                    Level = binaryReader.Read7BitEncodedInt32();
                    Type = binaryReader.Read7BitEncodedInt32();
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
