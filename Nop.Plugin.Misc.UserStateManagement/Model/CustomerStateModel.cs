using System.Web.Mvc;
using Nop.Admin.Models.Customers;
using Nop.Web.Framework;

namespace Nop.Plugin.Misc.UserStateManagement.Model
{
    public class CustomerStateModel : CustomerModel
    {
        public CustomerStateModel()
        {
            State = new CustomerState
            {
                State  = "交材料",
                UserState = "开始",
                StateId  = 1
            };

        }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.State")]
        [AllowHtml]
        public CustomerState State { get; set; }
    }
}
