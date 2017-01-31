using Nop.Data.Mapping;

namespace Nop.Plugin.Misc.UserStateManagement.Data
{
    public partial class UserStateManageMap : NopEntityTypeConfiguration<Domain.UserState>
    {
        public UserStateManageMap()
        {
            this.ToTable("UserStateExtension");
            this.HasKey(tr => tr.Id);
        }
    }
}