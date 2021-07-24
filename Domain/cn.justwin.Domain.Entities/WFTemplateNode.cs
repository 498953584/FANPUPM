namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class WFTemplateNode
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (base.GetType() != obj.GetType())
            {
                return false;
            }
            return (this.Id == ((WFTemplateNode) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual string AuditMain { get; set; }

        public virtual string AuditorType { get; set; }

        public virtual string ConditionDescription { get; set; }

        public virtual string DepCode { get; set; }

        public virtual string DueMode { get; set; }

        public virtual int? During { get; set; }

        public virtual string FrontNode { get; set; }

        public virtual string Id { get; set; }

        public virtual string IsAllPass { get; set; }

        public virtual string IsSecValidate { get; set; }

        public virtual string IsSelReceiver { get; set; }

        public virtual string IsSendMsg { get; set; }

        public virtual int NodeID { get; set; }

        public virtual string NodeName { get; set; }

        public virtual string Operater { get; set; }

        public virtual int TemplateID { get; set; }
    }
}

