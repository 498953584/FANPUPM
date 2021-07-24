namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudConModify
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
            return (this.ModifyId == ((BudConModify) obj).ModifyId);
        }

        public override int GetHashCode()
        {
            return this.ModifyId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ModifyId.ToString();
        }

        public virtual decimal ApprovalAmount { get; set; }

        public virtual DateTime? ApprovalDate { get; set; }

        public virtual decimal BudAmount { get; set; }

        public virtual string ConInModifyID { get; set; }

        public virtual int Flowstate { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual DateTime LastModifyDate { get; set; }

        public virtual string LastModifyUser { get; set; }

        public virtual string ModifyCode { get; set; }

        public virtual string ModifyContent { get; set; }

        public virtual string ModifyFileCode { get; set; }

        public virtual string ModifyId { get; set; }

        public virtual string Note { get; set; }

        public virtual string PrjId { get; set; }

        public virtual decimal ReportAmount { get; set; }
    }
}

