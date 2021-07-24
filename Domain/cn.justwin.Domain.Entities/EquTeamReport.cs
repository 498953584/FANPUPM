namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquTeamReport
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
            return (this.TeamID == ((EquTeamReport) obj).TeamID);
        }

        public override int GetHashCode()
        {
            return this.TeamID.GetHashCode();
        }

        public override string ToString()
        {
            return this.TeamID.ToString();
        }

        public virtual string ContractID { get; set; }

        public virtual string DayId { get; set; }

        public virtual string TeamID { get; set; }

        public virtual decimal UnitPrice { get; set; }

        public virtual decimal WorkDuration { get; set; }
    }
}

