using Godot;
using System;

namespace Survival
{

    public class PlayerManager
    {
        public int playerIndex = 0;
        // public PlayerData basicData;
        // public PlayerData playerData;
        // public PlayerData weaponData;

        //玩家数据：0-力量；1-体质；2-敏捷；3-精神；4-魅力；5-10 金木水火土灵根。
        public int[] basicData = new int[10];
        public int[] playerData = new int[10];
        public int[] weaponData = new int[10];
        public int money = 0;
        private float m_HP;
        private float m_Exp;
        public float FireDamageRatio = 1;
        public int level;
        public int next_level_Exp;

        public FaceDamageType faceDamageType = FaceDamageType.BaseMode;

        public float faceDamage = 5;
        public float HP
        {
            set
            {
                m_HP = value;
                playerEntity.EmitSignal("playerHPChanged");
            }
            get
            {
                return m_HP;
            }
        }
        public float Exp
        {
            set
            {
                if (value >= next_level_Exp)
                {
                    m_Exp = value - next_level_Exp;
                    PlayerLevelup();
                }
                else
                {
                    m_Exp = value;
                }

                playerEntity.EmitSignal("playerExpChanged");
            }
            get
            {
                return m_Exp;
            }
        }
        //等级所需表
        private int[] levelExp = new int[20] { 10, 20, 40, 80, 150, 230, 300, 400, 600, 1000, 1500, 2500, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 20000 };
        public float MaxHP;
        public PlayerController playerEntity;

        public BagForm bag;

        private DRWeapon[] weapons = new DRWeapon[3];
        public PlayerManager()
        {

        }

        public void CreatePlayer()
        {
            string playerPath = "Players/";
            playerEntity = (PlayerController)GameEntry.Entity.CreateEntity(playerPath + "Player");
            // UpdatePlayerData();
            // UpdatePlayerDataToPlayer();
            //读取exp表
            // m_HP = MaxHP;
            // m_Exp = 0;
            // level = 0;
            // next_level_Exp = 10;
        }

        // //使玩家无敌1s
        // public void DisPlayerCollsionOneSecond()
        // {
        //     playerEntity.DisPlayerCollsionOneSecond();
        // }

        private void PlayerLevelup()
        {
            level++;
            next_level_Exp = levelExp[level];
            playerEntity.EmitSignal("playerLevelChanged");
            //显示升级选择属性界面
            GameEntry.UI.OpenUIForm(UIFormId.LevelUpForm);
        }

        public void SetPlayerData(int[] data)
        {
            basicData = data;
        }

        public void InitBag()
        {
            bag = (BagForm)GameEntry.UI.OpenUIForm(UIFormId.BagForm);
            bag.Hide();
        }

        public void GainMoney(int gain)
        {
            money += gain;

            playerEntity.EmitSignal("playerMoneyChanged");
        }

        public void GainItem(int itemId)
        {
            var c = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("ItemUI"));
            ItemUI item = c.Instantiate<ItemUI>();
            item.id = itemId;
            bag.AddItem(item);
        }

        public void WearWeapon(int index, ItemUI newItem)
        {
            weapons[index] = GameEntry.DataTable.GetDataTable<DRWeapon>().GetDataRow(newItem.id);
            UpdateWeaponData();
            UpdatePlayerData();
        }

        public void TakeOffWeapon(int index)
        {
            weapons[index] = null;
            UpdateWeaponData();
            UpdatePlayerData();
        }

        private void UpdatePlayerData()
        {
            for (int i = 0; i < playerData.Length; i++)
            {
                playerData[i] = weaponData[i] + basicData[i];
            }
        }

        private void UpdateWeaponData()
        {
            int[] data = new int[5];
            foreach (DRWeapon weapon in weapons)
            {
                if (weapon == null)
                    continue;
                data[0] += weapon.Strength;
                data[1] += weapon.Physique;
                data[2] += weapon.Agile;
                data[3] += weapon.Soul;
                data[4] += weapon.Charm;
            }
            for (int i = 0; i < data.Length; i++)
            {
                weaponData[i] = data[i];
            }
        }

        public void UpdateWeaponSkill()
        {
            int[] weaponSkills = new int[weapons.Length];
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i] == null)
                    continue;
                weaponSkills[i] = weapons[i].Skill;
            }
            GameEntry.Skill.UpdateWeaponSkills(weaponSkills);
        }

        public void GainItem(ItemUI item)
        {
            bag.AddItem(item);
        }

        public void GainWeapon(int weaponId, int type)
        {
            // var c = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("ItemUI"));
            // ItemUI item = c.Instantiate<ItemUI>();
            ItemUI item = new WeaponUI();
            item.id = weaponId;
            item.type = type;
            bag.AddItem(item);
        }

        public void GainSkillBook(int skillId)
        {
            // var c = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("ItemUI"));
            // ItemUI item = c.Instantiate<ItemUI>();
            ItemUI item = new SkillBookUI();
            item.id = skillId;
            bag.AddItem(item);
        }

        public void GainMainSkillBook(int skillId)
        {
            // var c = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("ItemUI"));
            // ItemUI item = c.Instantiate<ItemUI>();
            ItemUI item = new MainSkillBookUI();
            item.id = skillId;
            bag.AddItem(item);
        }


        //用playerData中的数据转化成用户实际的数据
        public void UpdatePlayerDataToPlayer()
        {
            //体质->最大生命值 转化 1：3
            MaxHP = playerData[1] * 3 + 10;
            //playerEntity.Speed = playerData[2] * 50;
            playerEntity.EmitSignal("playerHPChanged");
        }
    }
}
