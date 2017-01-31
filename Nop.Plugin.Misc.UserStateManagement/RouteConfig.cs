using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Misc.UserStateManagement
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.Misc.UserStateManagement.Manage",
                "UserStateManagement/Configure",
                new { controller = "UserStateManagement", action = "Configure" },
                new[] { "Nop.Plugin.Misc.UserStateManagement.Controllers" }
            );

            var route = routes.MapRoute("Plugin.Misc.UserStateManagement.CustomerEdit",
                "Admin/Customer/Edit/{Id}",
                new { controller = "UserStateManagement", action = "OverrideCustomerEdit", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.UserStateManagement.Controllers" }
            );

            routes.Remove(route);
            routes.Insert(0, route);
        }
    }
}
