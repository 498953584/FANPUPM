namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class BudPreReimburseModifyDetail
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
            return (this.Id == ((BudPreReimburseModifyDetail) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual decimal AfterCost { get; set; }

        public virtual decimal BeginCost { get; set; }

        public virtual string CBSCode { get; set; }

        public virtual string Id { get; set; }

        public virtual string ModifyId { get; set; }

        public virtual string ModifyReason { get; set; }

        public virtual string Name { get; set; }

        public virtual string Note { get; set; }
    }
}

