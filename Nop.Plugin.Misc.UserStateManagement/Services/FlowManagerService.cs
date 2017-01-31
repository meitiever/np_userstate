using System;
using System.Linq;
using Nop.Core.Caching;
using System.Web.Hosting;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Serialization;
using Nop.Plugin.Misc.UserStateManagement.Domain;
using Nop.Plugin.Misc.UserStateManagement.Model;
using Nop.Services.Customers;
using Nop.Services.Events;


namespace Nop.Plugin.Misc.UserStateManagement.Services
{
    /// <summary>
    /// Tax rate service
    /// </summary>
    public class FlowManagerService : IFlowManagerService
    {
        #region Fields

        private static readonly string DealApprovalRelativePath = ConfigurationManager.AppSettings["WorkFlowSteps"]; // "~/App_Data/Workflow/DealApproval.xml";
        private static readonly string DealApprovalFilePath = HostingEnvironment.MapPath(DealApprovalRelativePath);
        
        private readonly IEventPublisher _eventPublisher;
        private readonly ICustomerService _customerService;
        private readonly ICacheManager _cacheManager;
        private readonly IFileDataCacheManager _fileDataCacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="customerService">customer service</param>
        /// <param name="fileDataCacheManager">file data service</param>
        /// <param name="userStateRepository">Tax rate repository</param>
        public FlowManagerService(IEventPublisher eventPublisher,
            ICacheManager cacheManager,
            ICustomerService customerService,
            IFileDataCacheManager fileDataCacheManager)
        {
            this._eventPublisher = eventPublisher;
            this._cacheManager = cacheManager;
            this._customerService = customerService;
            this._fileDataCacheManager = fileDataCacheManager;
        }

        #endregion

        #region Utility

        private List<WorkFlowStep> ApprovalSteps
        {
            get
            {
                return _fileDataCacheManager.Get<List<WorkFlowStep>>("DealApprovalProcess",
                    DealApprovalFilePath, GetApprovalSteps);
            }
        }

        private static List<WorkFlowStep> GetApprovalSteps(string filePath)
        {
            var xs = new XmlSerializer(typeof(Sequence));

            Sequence value;
            using (var reader = System.IO.File.OpenRead(DealApprovalFilePath))
            {
                value = (Sequence)xs.Deserialize(reader);
            }

            var result = value.Step.Select(d => new WorkFlowStep
            {
                CanBack = d.CanBack,
                CanStop = d.CanStop,
                Description = d.Value.Trim(),
                Index = d.Index,
                InnerState = d.InnerState,
                IsTerminal = d.IsTerminal
            }).ToList();

            return result;
        }
        #endregion

        #region Methods

        public int GetStateId(UserStatusEnum status)
        {
            return ApprovalSteps.First().Index;
        }

        public UserStatusEnum GetInnerState(int statusId)
        {
            //flow manager, read inner state from flow design file.
            var step = ApprovalSteps[statusId];
            var innerState = step.InnerState;
            return (UserStatusEnum)Enum.Parse(typeof(UserStatusEnum), innerState);
        }

        public UserStatusEnum GetUserStatus(int statusId)
        {
            //flow manager, read user state from flow design file.
            var step = ApprovalSteps[statusId];
            var userState = step.Description;
            return (UserStatusEnum)Enum.Parse(typeof(UserStatusEnum), userState);
        }

        public int Previous(int statusId)
        {
            //flow manager, go to pervious. get the last step id and save back to Db.
            var current = ApprovalSteps[statusId];
            if(current.CanBack)
                return statusId > 0 ? --statusId : 0;
            return statusId;
        }
        
        public int Next(int statusId)
        {
            //flow manager, go to next. get the next step id and save back to Db.
            if (statusId < ApprovalSteps.Count - 1)
                return ++statusId;
            return statusId;
        }

        #endregion
    }
}
