using System;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.UserStateManagement.Domain;

namespace Nop.Plugin.Misc.UserStateManagement.Services
{
    /// <summary>
    /// Tax rate service interface
    /// </summary>
    public interface IFlowManagerService
    {
        /// <summary>
        /// Product Id
        /// </summary>
        /// <param name="currentId">current id</param>
        int Next(int currentId);

        /// <summary>
        /// Product Id
        /// </summary>
        /// <param name="previousId">previous id</param>
        int Previous(int previousId);

        int GetStateId(UserStatusEnum status);

        UserStatusEnum GetInnerState(int stateId);

        UserStatusEnum GetUserStatus(int stateId);
    }
}
