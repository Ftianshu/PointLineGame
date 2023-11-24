namespace Survival
{
    /// <summary>
    /// 界面编号。
    /// </summary>
    public enum UIFormId : byte
    {
        Undefined = 0,

        /// <summary>
        /// 弹出框。
        /// </summary>
        DialogForm = 1,

        /// <summary>
        /// 主菜单。
        /// </summary>
        MenuForm = 100,

        /// <summary>
        /// 设置。
        /// </summary>
        SettingForm = 101,

        /// <summary>
        /// 关于。
        /// </summary>
        GamingUI = 102,

        /// <summary>
        /// 出生界面
        /// </summary>
        RoleForm = 103,


        BagForm = 200,

        SkillForm = 201,

        LevelUpForm = 202,
    }
}
