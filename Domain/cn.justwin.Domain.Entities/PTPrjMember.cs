namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTPrjMember
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
            return (this.PrjMemberId == ((PTPrjMember) obj).PrjMemberId);
        }

        public override int GetHashCode()
        {
            return this.PrjMemberId.GetHashCode();
        }

        public override string ToString()
        {
            return this.PrjMemberId.ToString();
        }

        public virtual string EducationalBackground { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string MemberCode { get; set; }

        public virtual string Note { get; set; }

        public virtual string PastPerformance { get; set; }

        public virtual string Post { get; set; }

        public virtual string PostAndCompetency { get; set; }

        public virtual Guid? PrjGuid { get; set; }

        public virtual Guid PrjMemberId { get; set; }

        public virtual string Technical { get; set; }

        public virtual string TrainingInformation { get; set; }
    }
}

