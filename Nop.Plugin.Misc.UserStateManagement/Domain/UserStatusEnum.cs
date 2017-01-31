namespace Nop.Plugin.Misc.UserStateManagement.Domain
{
    /// <summary>
    /// Represents the shipping status enumeration
    /// </summary>
    public enum UserStatusEnum
    {
        NotInIt = -1,

        Create = 0,
        /// <summary>
        /// Assigned
        /// </summary>
        Approval = 1,
        /// <summary>
        /// Not yet Assigned
        /// </summary>
        Invest = 2,
        /// <summary>
        /// Not yet Assigned
        /// </summary>
        Decision = 3,
        /// <summary>
        /// Not yet Assigned
        /// </summary>
        Management = 4,
        /// <summary>
        /// Not yet Assigned
        /// </summary>
        Quite = 6,
        /// <summary>
        /// Not yet Assigned
        /// </summary>
        End = 6,
    }
}
