namespace cn.justwin.Fund.prjReturn
{
    using System;

    [Serializable]
    public class PrjReturnModel
    {
        private string _fr_code;
        private DateTime? _fr_data;
        private decimal? _fr_deduct;
        private int? _fr_flowstate;
        private Guid _fr_id;
        private decimal? _fr_interest;
        private decimal? _fr_money;
        private string _fr_name;
        private string _fr_remark;
        private DateTime? _fr_time;
        private string _fr_user;
        private Guid _loanid;

        public string FR_Code
        {
            get
            {
                return this._fr_code;
            }
            set
            {
                this._fr_code = value;
            }
        }

        public DateTime? FR_data
        {
            get
            {
                return this._fr_data;
            }
            set
            {
                this._fr_data = value;
            }
        }

        public decimal? FR_deduct
        {
            get
            {
                return this._fr_deduct;
            }
            set
            {
                this._fr_deduct = value;
            }
        }

        public int? FR_flowState
        {
            get
            {
                return this._fr_flowstate;
            }
            set
            {
                this._fr_flowstate = value;
            }
        }

        public Guid FR_id
        {
            get
            {
                return this._fr_id;
            }
            set
            {
                this._fr_id = value;
            }
        }

        public decimal? FR_interest
        {
            get
            {
                return this._fr_interest;
            }
            set
            {
                this._fr_interest = value;
            }
        }

        public decimal? FR_Money
        {
            get
            {
                return this._fr_money;
            }
            set
            {
                this._fr_money = value;
            }
        }

        public string FR_name
        {
            get
            {
                return this._fr_name;
            }
            set
            {
                this._fr_name = value;
            }
        }

        public string FR_remark
        {
            get
            {
                return this._fr_remark;
            }
            set
            {
                this._fr_remark = value;
            }
        }

        public DateTime? FR_Time
        {
            get
            {
                return this._fr_time;
            }
            set
            {
                this._fr_time = value;
            }
        }

        public string FR_user
        {
            get
            {
                return this._fr_user;
            }
            set
            {
                this._fr_user = value;
            }
        }

        public Guid LoanID
        {
            get
            {
                return this._loanid;
            }
            set
            {
                this._loanid = value;
            }
        }
    }
}

