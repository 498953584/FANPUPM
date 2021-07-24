namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConConfigContract
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
            return (this.Id == ((ConConfigContract) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual string ContractId { get; set; }

        public virtual decimal? HighBalanceAlarmLimit { get; set; }

        public virtual decimal? HighPayAlarmLimit { get; set; }

        public virtual string Id { get; set; }

        public virtual int? IncomeAlarmDays { get; set; }

        public virtual bool IsBalanceAlarm { get; set; }

        public virtual bool IsIncomeAlarm { get; set; }

        public virtual bool IsPaymentAlarm { get; set; }

        public virtual bool IsPayoutAlarm { get; set; }

        public virtual decimal? LowBalanceAlarmLowerLimit { get; set; }

        public virtual decimal? LowBalanceAlarmUpperLimit { get; set; }

        public virtual decimal? LowPayAlarmLowerLimit { get; set; }

        public virtual decimal? LowPayAlarmUpperLimit { get; set; }

        public virtual decimal? MidBalanceAlarmLowerLimit { get; set; }

        public virtual decimal? MidBalanceAlarmUpperLimit { get; set; }

        public virtual decimal? MidPayAlarmLowerLimit { get; set; }

        public virtual decimal? MidPayAlarmUpperLimit { get; set; }

        public virtual int? PayoutAlarmDays { get; set; }
    }
}

