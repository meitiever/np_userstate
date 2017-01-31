using System;

namespace Nop.Plugin.Misc.UserStateManagement.Model
{
    public class WorkFlowStep
    {
        public string InnerState { get; set; }

        public int Index { get; set; }

        public bool CanBack { get; set; }

        public bool CanStop { get; set; }

        public string Description { get; set; }

        public bool IsTerminal { get; set; }
    }
}
