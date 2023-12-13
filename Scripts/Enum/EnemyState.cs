namespace Survival
{
    /// <summary>
    /// 平面编号。
    /// </summary>
    public enum EnemyState : byte
    {
        MoveToPlayer = 0,

        RushToPlayer = 1,

        Accumulate = 2,

        Exhausted = 3,

        MoveToTarget = 4,


    }
}
