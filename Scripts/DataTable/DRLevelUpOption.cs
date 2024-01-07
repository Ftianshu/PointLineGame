//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2024-01-07 13:07:05.724
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Godot;


namespace Survival
{
    /// <summary>
    /// 升级概率表。
    /// </summary>
    public class DRLevelUpOption : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取等级。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取普通概率。
        /// </summary>
        public int Prob0
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取稀有概率。
        /// </summary>
        public int Prob1
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取史诗概率。
        /// </summary>
        public int Prob2
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取传说概率。
        /// </summary>
        public int Prob3
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
            Prob0 = int.Parse(columnStrings[index++]);
            Prob1 = int.Parse(columnStrings[index++]);
            Prob2 = int.Parse(columnStrings[index++]);
            Prob3 = int.Parse(columnStrings[index++]);

            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Prob0 = binaryReader.Read7BitEncodedInt32();
                    Prob1 = binaryReader.Read7BitEncodedInt32();
                    Prob2 = binaryReader.Read7BitEncodedInt32();
                    Prob3 = binaryReader.Read7BitEncodedInt32();
                }
            }

            return true;
        }

        private KeyValuePair<int, int>[] m_Prob = null;

        public int ProbCount
        {
            get
            {
                return m_Prob.Length;
            }
        }

    }
}
