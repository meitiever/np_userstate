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
    public interface IUserStateManagementService
    {
        /// <summary>
        /// Product Id
        /// </summary>
        /// <param name="id">product id</param>
        int GetStateId(int id);

        /// <summary>
        /// Product Id
        /// </summary>
        /// <param name="stateId">state id</param>
        UserStatusEnum GetInnerStatus(int stateId);

        /// <summary>
        /// Product Id
        /// </summary>
        /// <param name="stateId">state id</param>
        UserStatusEnum GetUserStatus(int stateId);

        /// <summary>
        /// Gets all tax rates
        /// </summary>
        /// <returns>Tax rates</returns>
        int AddOrUpdateEventStatus(int customerId, UserStatusEnum status);

        /// <summary>
        /// Gets all tax rates
        /// </summary>
        /// <returns>Tax rates</returns>
        int Previous(int customerId, int statusId);

        /// <summary>
        /// Gets all tax rates
        /// </summary>
        /// <returns>Tax rates</returns>
        int Next(int customerId, int statusId);
    }
}
