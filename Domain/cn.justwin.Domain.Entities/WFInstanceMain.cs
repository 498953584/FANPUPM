namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class WFInstanceMain
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
            return (this.ID == ((WFInstanceMain) obj).ID);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return this.ID.ToString();
        }

        public virtual string BusinessClass { get; set; }

        public virtual string BusinessCode { get; set; }

        public virtual int ID { get; set; }

        public virtual Guid? InstanceCode { get; set; }

        public virtual string Organiger { get; set; }

        public virtual DateTime? StartTime { get; set; }

        public virtual int? TemplateID { get; set; }
    }
}

