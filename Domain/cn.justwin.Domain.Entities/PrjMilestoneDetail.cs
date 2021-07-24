namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PrjMilestoneDetail
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
            return (this.Id == ((PrjMilestoneDetail) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual int DepCode { get; set; }

        public virtual string DepName { get; set; }

        public virtual decimal ForeCast { get; set; }

        public virtual string Id { get; set; }

        public virtual decimal NextForeCast { get; set; }

        public virtual string PrjCode { get; set; }

        public virtual string PrjName { get; set; }

        public virtual DateTime RptDate { get; set; }

        public virtual int Stone1 { get; set; }

        public virtual int Stone2 { get; set; }

        public virtual int Stone3 { get; set; }

        public virtual int Stone4 { get; set; }

        public virtual int Stone5 { get; set; }

        public virtual int Stone6 { get; set; }

        public virtual int Stone7 { get; set; }

        public virtual int Stone8 { get; set; }

        public virtual int Stone9 { get; set; }

        public virtual decimal StoreAmount { get; set; }

        public virtual decimal StoreSwitchRate { get; set; }

        public virtual string UserCode { get; set; }

        public virtual string UserName { get; set; }
    }
}

