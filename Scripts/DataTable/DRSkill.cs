//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2023-09-27 20:33:33.800
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Godot;


namespace Survival
{
    /// <summary>
    /// 技能表。
    /// </summary>
    public class DRSkill : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取技能编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取资源名称。
        /// </summary>
        public string AssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能图标资源。
        /// </summary>
        public string UIAssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取学习花费时间。
        /// </summary>
        public int CostTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取学习要求。
        /// </summary>
        public string Condition
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取描述。
        /// </summary>
        public string Desc
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取属性类型。
        /// </summary>
        public int Type
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取冷却时间。
        /// </summary>
        public float CD
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取持续时间。
        /// </summary>
        public float Duration
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能能级。
        /// </summary>
        public int Level
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取稀有度。
        /// </summary>
        public int Rarity
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取释放类型。
        /// </summary>
        public int ReleaseType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能效果。
        /// </summary>
        public string Impact
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
            AssetName = columnStrings[index++];
            UIAssetName = columnStrings[index++];
            CostTime = int.Parse(columnStrings[index++]);
            Condition = columnStrings[index++];
            Desc = columnStrings[index++];
            Type = int.Parse(columnStrings[index++]);
            CD = float.Parse(columnStrings[index++]);
            Duration = float.Parse(columnStrings[index++]);
            Level = int.Parse(columnStrings[index++]);
            Rarity = int.Parse(columnStrings[index++]);
            ReleaseType = int.Parse(columnStrings[index++]);
            Impact = columnStrings[index++];

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
                    AssetName = binaryReader.ReadString();
                    UIAssetName = binaryReader.ReadString();
                    CostTime = binaryReader.Read7BitEncodedInt32();
                    Condition = binaryReader.ReadString();
                    Desc = binaryReader.ReadString();
                    Type = binaryReader.Read7BitEncodedInt32();
                    CD = binaryReader.ReadSingle();
                    Duration = binaryReader.ReadSingle();
                    Level = binaryReader.Read7BitEncodedInt32();
                    Rarity = binaryReader.Read7BitEncodedInt32();
                    ReleaseType = binaryReader.Read7BitEncodedInt32();
                    Impact = binaryReader.ReadString();
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
