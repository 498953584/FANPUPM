namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class PaymentTargetModel
    {
        private string conTargetId;
        private string id;
        private DateTime inputDate;
        private string inputUser;
        private decimal paymentAmount;
        private string paymentId;

        public string ConTargetId
        {
            get
            {
                return this.conTargetId;
            }
            set
            {
                this.conTargetId = value;
            }
        }

        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public DateTime InputDate
        {
            get
            {
                return this.inputDate;
            }
            set
            {
                this.inputDate = value;
            }
        }

        public string InputUser
        {
            get
            {
                return this.inputUser;
            }
            set
            {
                this.inputUser = value;
            }
        }

        public decimal PaymentAmount
        {
            get
            {
                return this.paymentAmount;
            }
            set
            {
                this.paymentAmount = value;
            }
        }

        public string PaymentId
        {
            get
            {
                return this.paymentId;
            }
            set
            {
                this.paymentId = value;
            }
        }
    }
}

