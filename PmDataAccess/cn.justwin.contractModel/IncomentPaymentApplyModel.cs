namespace cn.justwin.contractModel
{
    using System;

    public class IncomentPaymentApplyModel
    {
        private string _code;
        private bool _containPending;
        private string _contractId;
        private int _flowState;
        private string _id;
        private DateTime _inputDate;
        private string _inputPerson;
        private string _notes;
        private decimal _paymentAmount;
        private DateTime _paymentDate;
        private string _paymentMode;
        private string _paymentPenson;

        public string Code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }

        public bool ContainPending
        {
            get
            {
                return this._containPending;
            }
            set
            {
                this._containPending = value;
            }
        }

        public string ContractId
        {
            get
            {
                return this._contractId;
            }
            set
            {
                this._contractId = value;
            }
        }

        public int FlowState
        {
            get
            {
                return this._flowState;
            }
            set
            {
                this._flowState = value;
            }
        }

        public string Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public DateTime InputDate
        {
            get
            {
                return this._inputDate;
            }
            set
            {
                this._inputDate = value;
            }
        }

        public string InputPerson
        {
            get
            {
                return this._inputPerson;
            }
            set
            {
                this._inputPerson = value;
            }
        }

        public string Notes
        {
            get
            {
                return this._notes;
            }
            set
            {
                this._notes = value;
            }
        }

        public decimal PaymentAmount
        {
            get
            {
                return this._paymentAmount;
            }
            set
            {
                this._paymentAmount = value;
            }
        }

        public DateTime PaymentDate
        {
            get
            {
                return this._paymentDate;
            }
            set
            {
                this._paymentDate = value;
            }
        }

        public string PaymentMode
        {
            get
            {
                return this._paymentMode;
            }
            set
            {
                this._paymentMode = value;
            }
        }

        public string PaymentPenson
        {
            get
            {
                return this._paymentPenson;
            }
            set
            {
                this._paymentPenson = value;
            }
        }
    }
}

