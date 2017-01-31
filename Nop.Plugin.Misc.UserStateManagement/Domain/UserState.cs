
using Nop.Core;

namespace Nop.Plugin.Misc.UserStateManagement.Domain
{
    /// <summary>
    /// Represents the shipping status enumeration
    /// </summary>
    public class UserState : BaseEntity
    {
        /// <summary>
        /// Gets or sets the store identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the tax category identifier
        /// </summary>
        public int StatusId { get; set; }

        public UserStatusEnum Status
        {
            get
            {
                return (UserStatusEnum)this.StatusId;
            }
            set
            {
                this.StatusId = (int)value;
            }
        }
    }
}
