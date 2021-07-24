namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class BudPreReimburseApplyDetail
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
            return (this.Id == ((BudPreReimburseApplyDetail) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual string ApplyId { get; set; }

        public virtual string CBSCode { get; set; }

        public virtual decimal Cost { get; set; }

        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Note { get; set; }
    }
}

