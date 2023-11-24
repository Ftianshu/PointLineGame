//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2023-09-27 20:33:33.802
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Godot;


namespace Survival
{
    /// <summary>
    /// 功法表。
    /// </summary>
    public class DRMainSkill : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取功法编号。
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
        /// 获取名称。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能描述。
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取条件。
        /// </summary>
        public string Condition
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取属性种类。
        /// </summary>
        public int Type
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取效果。
        /// </summary>
        public string Effect
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取境界。
        /// </summary>
        public int Realm
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取提供技能槽。
        /// </summary>
        public int SkillSlot
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取花费时间。
        /// </summary>
        public int Cost
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
            AssetName = columnStrings[index++];
            Name = columnStrings[index++];
            Description = columnStrings[index++];
            Condition = columnStrings[index++];
            Type = int.Parse(columnStrings[index++]);
            Effect = columnStrings[index++];
            Realm = int.Parse(columnStrings[index++]);
            SkillSlot = int.Parse(columnStrings[index++]);
            Cost = int.Parse(columnStrings[index++]);

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
                    Name = binaryReader.ReadString();
                    Description = binaryReader.ReadString();
                    Condition = binaryReader.ReadString();
                    Type = binaryReader.Read7BitEncodedInt32();
                    Effect = binaryReader.ReadString();
                    Realm = binaryReader.Read7BitEncodedInt32();
                    SkillSlot = binaryReader.Read7BitEncodedInt32();
                    Cost = binaryReader.Read7BitEncodedInt32();
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
