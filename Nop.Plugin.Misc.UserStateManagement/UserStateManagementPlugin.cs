using Nop.Core.Plugins;
using Nop.Plugin.Misc.UserStateManagement.Data;
using Nop.Web.Framework.Menu;
using Nop.Services.Localization;

using SiteMapNode = Nop.Web.Framework.Menu.SiteMapNode;

namespace Nop.Plugin.Misc.UserStateManagement
{
    public class UserStateManagementPlugin : BasePlugin, IAdminMenuPlugin
    {
        private readonly UserStateObjectContext _context;

        public UserStateManagementPlugin(UserStateObjectContext context)
        {
            _context = context;
        }

        public bool Authenticate()
        {
            return true;
        }
        
        public override void Install()
        {
            _context.Install();
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.StateManage", "状态管理", "zh-CN");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.StateId", "状态ID", "zh-CN");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.LastState", "上一个状态", "zh-CN");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.CurrentState", "当前状态", "zh-CN");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.InnerState", "内部状态", "zh-CN");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.Previous", "上一步", "zh-CN");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.Next", "下一步", "zh-CN");

            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.StateManage", "State Management", "en-US");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.StateId", "State Id", "en-US");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.LastState", "Last State", "en-US");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.CurrentState", "Current State", "en-US");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.InnerState", "Inner State", "en-US");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.Previous", "Previous", "en-US");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.Next", "Next", "en-US");
            base.Install();
        }

        public override void Uninstall()
        {
            _context.Uninstall();
            //locales
            this.DeletePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.StateManage");
            this.DeletePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.StateId");
            this.DeletePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.LastState");
            this.DeletePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.CurrentState");
            this.DeletePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.InnerState");
            this.DeletePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.Previous");
            this.DeletePluginLocaleResource("Nop.Plugin.Misc.UserStateManagement.Next");

            base.Uninstall();
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {   }
    }
}


