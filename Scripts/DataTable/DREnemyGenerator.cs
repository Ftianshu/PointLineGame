//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2023-12-16 09:33:06.454
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Godot;


namespace Survival
{
    /// <summary>
    /// 敌人生成表。
    /// </summary>
    public class DREnemyGenerator : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取生成编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取波次。
        /// </summary>
        public int WaveCount
        {
            get;
            private set;
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
        /// 获取生成模式。
        /// </summary>
        public string GenerateType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取生成位置。
        /// </summary>
        public Vector2 Position
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取目标位置。
        /// </summary>
        public Vector2 Target
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取经验。
        /// </summary>
        public float Exp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取血量。
        /// </summary>
        public float Hp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击。
        /// </summary>
        public float Attack
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取生成下一个敌人的间隔。
        /// </summary>
        public float NextEnemyTime
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
            WaveCount = int.Parse(columnStrings[index++]);
            AssetName = columnStrings[index++];
            GenerateType = columnStrings[index++];
            Position = DataTableExtension.ParseVector2(columnStrings[index++]);
            Target = DataTableExtension.ParseVector2(columnStrings[index++]);
            Exp = float.Parse(columnStrings[index++]);
            Hp = float.Parse(columnStrings[index++]);
            Attack = float.Parse(columnStrings[index++]);
            NextEnemyTime = float.Parse(columnStrings[index++]);

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
                    WaveCount = binaryReader.Read7BitEncodedInt32();
                    AssetName = binaryReader.ReadString();
                    GenerateType = binaryReader.ReadString();
                    Position = binaryReader.ReadVector2();
                    Target = binaryReader.ReadVector2();
                    Exp = binaryReader.ReadSingle();
                    Hp = binaryReader.ReadSingle();
                    Attack = binaryReader.ReadSingle();
                    NextEnemyTime = binaryReader.ReadSingle();
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
