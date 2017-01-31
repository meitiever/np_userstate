using System;
using System.Linq;

using Nop.Core.Data;
using Nop.Core.Caching;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Plugin.Misc.UserStateManagement.Domain;

namespace Nop.Plugin.Misc.UserStateManagement.Services
{
    /// <summary>
    /// Tax rate service
    /// </summary>
    public class UserStateManagementService : IUserStateManagementService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IFlowManagerService _flowManagerService;
        private readonly IRepository<Domain.UserState> _userStateRepository;
        private readonly ICustomerService _customerService;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="flowManagerService">flow Manager manager</param>
        /// <param name="customerService">customer service</param>
        /// <param name="userStateRepository">Tax rate repository</param>
        public UserStateManagementService(IEventPublisher eventPublisher,
            ICacheManager cacheManager,
            IFlowManagerService flowManagerService,
            ICustomerService customerService,
            IRepository<Domain.UserState> userStateRepository)
        {
            this._eventPublisher = eventPublisher;
            this._cacheManager = cacheManager;
            this._customerService = customerService;
            this._userStateRepository = userStateRepository;
            this._flowManagerService = flowManagerService;
        }

        #endregion

        #region Methods

        public UserStatusEnum GetStatus(int customerId)
        {
            var first = _userStateRepository.Table.FirstOrDefault(c => c.CustomerId == customerId);
            return first != null ? first.Status : UserStatusEnum.NotInIt;
        }

        public int GetStateId(int customerId)
        {
            var first = _userStateRepository.Table.FirstOrDefault(c => c.CustomerId == customerId);
            return first != null ? first.StatusId : -1;
        }

        public UserStatusEnum GetInnerStatus(int stateId)
        {
            return _flowManagerService.GetInnerState(stateId);
        }

        public UserStatusEnum GetUserStatus(int stateId)
        {
            return _flowManagerService.GetUserStatus(stateId);
        }

        public int AddOrUpdateEventStatus(int customerId, UserStatusEnum status)
        {
            var first = _userStateRepository.Table.FirstOrDefault(c => c.CustomerId == customerId);
            
            if(first != null)
            {
                //Update
                first.Status = status;
                _userStateRepository.Update(first);
            }
            else
            {
                //Add new
                var item = new UserState { Status = status, CustomerId = customerId };
                _userStateRepository.Insert(item);
            }
            return _flowManagerService.GetStateId(status);
        }

        public int Previous(int customerId, int statusId)
        {
            var first = _userStateRepository.Table.FirstOrDefault(c => c.CustomerId == customerId);
            //flow manager, go to pervious. get the last step id and save back to Db.
            var previous = _flowManagerService.Previous(statusId);
            first.StatusId = previous;
            _userStateRepository.Update(first);
            return previous;
        }

        public int Next(int customerId, int statusId)
        {
            var first = _userStateRepository.Table.FirstOrDefault(c => c.CustomerId == customerId);
            //flow manager, go to next. get the next step id and save back to Db.
            var next = _flowManagerService.Next(statusId);
            first.StatusId = next;
            _userStateRepository.Update(first);
            return next;
        }

        #endregion
    }
}
