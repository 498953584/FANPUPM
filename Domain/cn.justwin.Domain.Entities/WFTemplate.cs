namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class WFTemplate
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
            return (this.TemplateID == ((WFTemplate) obj).TemplateID);
        }

        public override int GetHashCode()
        {
            return this.TemplateID.GetHashCode();
        }

        public override string ToString()
        {
            return this.TemplateID.ToString();
        }

        public virtual string BusinessClass { get; set; }

        public virtual string BusinessCode { get; set; }

        public virtual string CorpCode { get; set; }

        public virtual string IsAbnormality { get; set; }

        public virtual string IsComplete { get; set; }

        public virtual string IsGeneral { get; set; }

        public virtual bool? IsValid { get; set; }

        public virtual int? RecordID { get; set; }

        public virtual string Remark { get; set; }

        public virtual string TemplateCode { get; set; }

        public virtual int TemplateID { get; set; }

        public virtual string TemplateName { get; set; }
    }
}

