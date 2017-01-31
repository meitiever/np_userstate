using Autofac;
using Nop.Data;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Web.Framework.Mvc;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.UserStateManagement.Data;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Misc.UserStateManagement.Services;

namespace Nop.Plugin.Misc.UserStateManagement.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_user_state";

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<UserStateManagementService>().As<IUserStateManagementService>().InstancePerLifetimeScope();
            builder.RegisterType<FlowManagerService>().As<IFlowManagerService>().InstancePerLifetimeScope();
            builder.RegisterType<FileDataCacheManager>().As<IFileDataCacheManager>().InstancePerLifetimeScope();
            
            //data context
            this.RegisterPluginDataContext<UserStateObjectContext>(builder, CONTEXT_NAME);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<Domain.UserState>>()
                .As<IRepository<Domain.UserState>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
