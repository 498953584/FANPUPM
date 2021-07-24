namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class IncometInvoiceModel
    {
        private decimal? _amount;
        private string _annex;
        private string _bankcode;
        private string _contact;
        private string _contractid;
        private int? _flowstate;
        private DateTime? _inputdate;
        private string _inputperson;
        private string _invoicecode;
        private DateTime? _invoicedate;
        private string _invoiceid;
        private string _invoiceno;
        private string _invoicetype;
        private string _notes;
        private string _organizationcode;
        private string _payee;
        private string _payer;
        private string _project;
        private string _taxno;
        private string _transactor;

        public decimal? Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }

        public string Annex
        {
            get
            {
                return this._annex;
            }
            set
            {
                this._annex = value;
            }
        }

        public string BankCode
        {
            get
            {
                return this._bankcode;
            }
            set
            {
                this._bankcode = value;
            }
        }

        public string Contact
        {
            get
            {
                return this._contact;
            }
            set
            {
                this._contact = value;
            }
        }

        public string ContractID
        {
            get
            {
                return this._contractid;
            }
            set
            {
                this._contractid = value;
            }
        }

        public int? FlowState
        {
            get
            {
                return this._flowstate;
            }
            set
            {
                this._flowstate = value;
            }
        }

        public DateTime? InputDate
        {
            get
            {
                return this._inputdate;
            }
            set
            {
                this._inputdate = value;
            }
        }

        public string InputPerson
        {
            get
            {
                return this._inputperson;
            }
            set
            {
                this._inputperson = value;
            }
        }

        public string InvoiceCode
        {
            get
            {
                return this._invoicecode;
            }
            set
            {
                this._invoicecode = value;
            }
        }

        public DateTime? InvoiceDate
        {
            get
            {
                return this._invoicedate;
            }
            set
            {
                this._invoicedate = value;
            }
        }

        public string InvoiceID
        {
            get
            {
                return this._invoiceid;
            }
            set
            {
                this._invoiceid = value;
            }
        }

        public string InvoiceNo
        {
            get
            {
                return this._invoiceno;
            }
            set
            {
                this._invoiceno = value;
            }
        }

        public string InvoiceType
        {
            get
            {
                return this._invoicetype;
            }
            set
            {
                this._invoicetype = value;
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

        public string OrganizationCode
        {
            get
            {
                return this._organizationcode;
            }
            set
            {
                this._organizationcode = value;
            }
        }

        public string Payee
        {
            get
            {
                return this._payee;
            }
            set
            {
                this._payee = value;
            }
        }

        public string Payer
        {
            get
            {
                return this._payer;
            }
            set
            {
                this._payer = value;
            }
        }

        public string Project
        {
            get
            {
                return this._project;
            }
            set
            {
                this._project = value;
            }
        }

        public string TaxNo
        {
            get
            {
                return this._taxno;
            }
            set
            {
                this._taxno = value;
            }
        }

        public string Transactor
        {
            get
            {
                return this._transactor;
            }
            set
            {
                this._transactor = value;
            }
        }
    }
}

