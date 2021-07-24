namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class WFInstance
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
            return (this.NoteID == ((WFInstance) obj).NoteID);
        }

        public override int GetHashCode()
        {
            return this.NoteID.GetHashCode();
        }

        public override string ToString()
        {
            return this.NoteID.ToString();
        }

        public virtual DateTime? ArriveTime { get; set; }

        public virtual DateTime? AuditDate { get; set; }

        public virtual string AuditInfo { get; set; }

        public virtual string AuditRemark { get; set; }

        public virtual int? AuditResult { get; set; }

        public virtual int? During { get; set; }

        public virtual int? ID { get; set; }

        public virtual string IsAllPass { get; set; }

        public virtual string IsInsertedFrontNode { get; set; }

        public virtual string IsSendMsg { get; set; }

        public virtual int? NodeID { get; set; }

        public virtual string NodeName { get; set; }

        public virtual int NoteID { get; set; }

        public virtual string Operator { get; set; }

        public virtual DateTime? OutOfTime { get; set; }

        public virtual int? Sing { get; set; }

        public virtual decimal? TheOrder { get; set; }
    }
}

