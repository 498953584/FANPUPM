namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ContractAlertInfo
    {
        private int _alertcode;
        private string _alertto;
        private decimal _beforedate;
        private string _contractcode;
        private DateTime _happendate;
        private decimal _stageamount;
        private decimal _validdate;

        public int AlertCode
        {
            get
            {
                return this._alertcode;
            }
            set
            {
                this._alertcode = value;
            }
        }

        public string AlertTo
        {
            get
            {
                return this._alertto;
            }
            set
            {
                this._alertto = value;
            }
        }

        public decimal BeforeDate
        {
            get
            {
                return this._beforedate;
            }
            set
            {
                this._beforedate = value;
            }
        }

        public string ContractCode
        {
            get
            {
                return this._contractcode;
            }
            set
            {
                this._contractcode = value;
            }
        }

        public DateTime HappenDate
        {
            get
            {
                return this._happendate;
            }
            set
            {
                this._happendate = value;
            }
        }

        public decimal StageAmount
        {
            get
            {
                return this._stageamount;
            }
            set
            {
                this._stageamount = value;
            }
        }

        public decimal ValidDate
        {
            get
            {
                return this._validdate;
            }
            set
            {
                this._validdate = value;
            }
        }
    }
}

