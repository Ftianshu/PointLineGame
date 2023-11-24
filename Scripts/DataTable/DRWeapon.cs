//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2023-09-27 20:33:33.801
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Godot;


namespace Survival
{
    /// <summary>
    /// 武器表。
    /// </summary>
    public class DRWeapon : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取武器编号。
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
        /// 获取武器图标。
        /// </summary>
        public string UIAssetName
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
        /// 获取要求。
        /// </summary>
        public string Condition
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
        /// 获取种类（部位）。
        /// </summary>
        public int Type
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取附带技能编号。
        /// </summary>
        public int Skill
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取力量。
        /// </summary>
        public int Strength
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取体质。
        /// </summary>
        public int Physique
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取敏捷。
        /// </summary>
        public int Agile
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取精神。
        /// </summary>
        public int Soul
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取魅力。
        /// </summary>
        public int Charm
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
            UIAssetName = columnStrings[index++];
            Desc = columnStrings[index++];
            Condition = columnStrings[index++];
            Rarity = int.Parse(columnStrings[index++]);
            Type = int.Parse(columnStrings[index++]);
            Skill = int.Parse(columnStrings[index++]);
            Strength = int.Parse(columnStrings[index++]);
            Physique = int.Parse(columnStrings[index++]);
            Agile = int.Parse(columnStrings[index++]);
            Soul = int.Parse(columnStrings[index++]);
            Charm = int.Parse(columnStrings[index++]);

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
                    Desc = binaryReader.ReadString();
                    Condition = binaryReader.ReadString();
                    Rarity = binaryReader.Read7BitEncodedInt32();
                    Type = binaryReader.Read7BitEncodedInt32();
                    Skill = binaryReader.Read7BitEncodedInt32();
                    Strength = binaryReader.Read7BitEncodedInt32();
                    Physique = binaryReader.Read7BitEncodedInt32();
                    Agile = binaryReader.Read7BitEncodedInt32();
                    Soul = binaryReader.Read7BitEncodedInt32();
                    Charm = binaryReader.Read7BitEncodedInt32();
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
