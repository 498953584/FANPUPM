namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquShipOilRecord
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
            return (this.RecordId == ((EquShipOilRecord) obj).RecordId);
        }

        public override int GetHashCode()
        {
            return this.RecordId.GetHashCode();
        }

        public override string ToString()
        {
            return this.RecordId.ToString();
        }

        public virtual decimal? AfterHeight { get; set; }

        public virtual string ApplyId { get; set; }

        public virtual decimal ApprovalOilQty { get; set; }

        public virtual decimal? BeforeHeight { get; set; }

        public virtual decimal FinalOilQty { get; set; }

        public virtual int FlowState { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string Note { get; set; }

        public virtual string RecordId { get; set; }

        public virtual DateTime? ReportDate { get; set; }

        public virtual string ReportUser { get; set; }

        public virtual decimal? ShipDisplayQty { get; set; }
    }
}

