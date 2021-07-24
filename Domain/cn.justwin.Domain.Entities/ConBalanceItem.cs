namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ConBalanceItem
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
            return (this.Id == ((ConBalanceItem) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual string BalanceId { get; set; }

        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Note { get; set; }

        public virtual decimal Qty { get; set; }

        public virtual string Type { get; set; }

        public virtual decimal UnitPrice { get; set; }
    }
}

