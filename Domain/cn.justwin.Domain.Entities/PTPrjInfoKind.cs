namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTPrjInfoKind
    {
        private PTPrjInfoZTBDetail detail;

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
            return (this.KindId == ((PTPrjInfoKind) obj).KindId);
        }

        public override int GetHashCode()
        {
            return this.KindId.GetHashCode();
        }

        public override string ToString()
        {
            return this.KindId.ToString();
        }

        public virtual PTPrjInfoZTBDetail Detail
        {
            get
            {
                return this.detail;
            }
            set
            {
                this.detail = value;
            }
        }

        public virtual string KindId { get; set; }

        public virtual Guid PrjGuid { get; set; }

        public virtual string PrjKind { get; set; }

        public virtual decimal ProfessionalCost { get; set; }
    }
}

