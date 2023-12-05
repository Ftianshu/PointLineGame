namespace Survival
{
    /// <summary>
    /// 平面编号。
    /// </summary>
    public enum FaceDamageType : byte
    {
        BaseMode = 0,

        IncreaseWithArea = 1,

        DecreaseWithArea = 2,

        IncreaseWithGirth = 3,

        DecreaseWithGirth = 4,

        Other = 5
    }
}
