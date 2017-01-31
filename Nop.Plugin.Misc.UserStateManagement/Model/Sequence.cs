
namespace Nop.Plugin.Misc.UserStateManagement.Model
{
    using System.Xml.Serialization;
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Sequence
    {

        private SequenceStep[] stepField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Step")]
        public SequenceStep[] Step
        {
            get
            {
                return this.stepField;
            }
            set
            {
                this.stepField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SequenceStep
    {

        private byte indexField;

        private string innerStateField;

        private bool canBackField;

        private bool canBackFieldSpecified;

        private bool canStopField;

        private bool canStopFieldSpecified;

        private string valueField;

        private bool isTerminal;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InnerState
        {
            get
            {
                return this.innerStateField;
            }
            set
            {
                this.innerStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool CanBack
        {
            get
            {
                return this.canBackField;
            }
            set
            {
                this.canBackField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CanBackSpecified
        {
            get
            {
                return this.canBackFieldSpecified;
            }
            set
            {
                this.canBackFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool CanStop
        {
            get
            {
                return this.canStopField;
            }
            set
            {
                this.canStopField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CanStopSpecified
        {
            get
            {
                return this.canStopFieldSpecified;
            }
            set
            {
                this.canStopFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsTerminal
        {
            get
            {
                return this.isTerminal;
            }
            set
            {
                this.isTerminal = value;
            }
        }
    }
}
