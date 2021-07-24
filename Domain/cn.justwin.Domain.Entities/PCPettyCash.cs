namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class PCPettyCash
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
            return (this.Id == ((PCPettyCash) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual string Account { get; set; }

        [DataMember]
        public virtual string Applicant { get; set; }

        public virtual DateTime ApplicationDate { get; set; }

        public virtual string ApplicationReason { get; set; }

        public virtual string Bank { get; set; }

        public virtual decimal Cash { get; set; }

        public virtual DateTime CashDate { get; set; }

        public virtual DateTime CleanDate { get; set; }

        public virtual string CleanNote { get; set; }

        public virtual int FlowState { get; set; }

        [DataMember]
        public virtual string Id { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual bool IsRepay { get; set; }

        public virtual string Matter { get; set; }

        public virtual DateTime ModifyDate { get; set; }

        public virtual string ModifyUser { get; set; }

        public virtual string Payee { get; set; }

        public virtual string Payer { get; set; }

        public virtual string PrjTypeCode { get; set; }

        public virtual PTPrjInfo Project { get; set; }

        public virtual decimal RepayCash { get; set; }

        public virtual int RepayFlowState { get; set; }

        public virtual string State { get; set; }
    }
}

