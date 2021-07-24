namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTPrjInfoRank
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
            return (this.RankId == ((PTPrjInfoRank) obj).RankId);
        }

        public override int GetHashCode()
        {
            return this.RankId.GetHashCode();
        }

        public override string ToString()
        {
            return this.RankId.ToString();
        }

        public virtual Guid PrjGuid { get; set; }

        public virtual string PrjRank { get; set; }

        public virtual string RankId { get; set; }

        public virtual string RankLevel { get; set; }
    }
}

