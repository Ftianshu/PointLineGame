
namespace Survival
{
    public static class AssetUtility
    {

        public static string GetEntityAsset(string assetName)
        {
            return "Entities/" + assetName + ".tscn";
        }

        public static string GetEnemyAsset(string assetName)
        {
            return "Entities/Enemys/" + assetName + ".tscn";
        }

        public static string GetSkillEntityAsset(string assetName)
        {
            return "Entities/Skills/" + assetName + ".tscn";
        }

        public static string GetItemAsset(string assetName)
        {
            return "Entities/Items/" + assetName + ".tscn";
        }

        public static string GetPointAsset(string assetName)
        {
            return "Entities/Point/" + assetName + ".tscn";
        }
        public static string GetFaceAsset(string assetName)
        {
            return "Entities/Face/" + assetName + ".tscn";
        }

        public static string GetEffectAsset(string assetName)
        {
            return "Entities/Effects/" + assetName + ".tscn";
        }

        public static string GetUIItemAsset(string assetName)
        {
            return "UIItems/" + assetName + ".tscn";
        }


        public static string GetUIFormAsset(string assetName)
        {
            return "UIForms/" + assetName + ".tscn";
        }

        public static string GetDataTableAsset(string assetName)
        {
            return "DataTables/" + assetName + ".txt";
        }

        public static string GetMapAsset(string assetName)
        {
            return "Scenes/Maps/" + assetName + ".tscn";
        }

        public static string GetItemSpriteAsset(string assetName)
        {
            return "Sprites/Items/" + assetName + ".png";
        }

        public static string GetSkillBookSpriteAsset(string assetName)
        {
            return "Sprites/Items/SkillBooks/" + assetName + ".png";
        }

        public static string GetItemUISpriteAsset(string assetName)
        {
            return "Sprites/ItemUIs/" + assetName + ".png";
        }

        public static string GetSkillItemSpriteAsset(string assetName)
        {
            return "Sprites/ItemUIs/SkillUIs/" + assetName + ".png";
        }
    }
}
