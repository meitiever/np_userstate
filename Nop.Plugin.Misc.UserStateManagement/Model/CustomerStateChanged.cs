using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Admin.Models.Customers;
using Nop.Web.Framework;

namespace Nop.Plugin.Misc.UserStateManagement.Model
{
    public class CustomerStateChanged : CustomerModel
    {
        [NopResourceDisplayName("Nop.Plugin.Misc.UserStateManagement.StateId")]
        public int CurrentStateId { get; set; }
        [NopResourceDisplayName("Nop.Plugin.Misc.UserStateManagement.CurrentState")]
        public string CurrentState { get; set; }
        [NopResourceDisplayName("Nop.Plugin.Misc.UserStateManagement.CurrentState")]
        public string InnerState { get; set; }
        [NopResourceDisplayName("Nop.Plugin.Misc.UserStateManagement.Last")]
        public int LastStateId { get; set; }
        [NopResourceDisplayName("Nop.Plugin.Misc.UserStateManagement.LastState")]
        public int LastState { get; set; }
    }

    public class CustomerState : CustomerModel
    {
        public CustomerState()
        {
        }

        [NopResourceDisplayName("Nop.Plugin.Misc.UserStateManagement.InnerState")]
        public string State { get; set; }
        [NopResourceDisplayName("Nop.Plugin.Misc.UserStateManagement.CurrentState")]
        public string UserState { get; set; }
        [NopResourceDisplayName("Nop.Plugin.Misc.UserStateManagement.StateId")]
        public int StateId { get; set; }
    }
}
