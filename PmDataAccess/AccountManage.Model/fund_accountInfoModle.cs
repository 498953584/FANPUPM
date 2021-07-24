namespace AccountManage.Model
{
    using System;

    [Serializable]
    public class fund_accountInfoModle
    {
        private string _accountnum;
        private int _id;
        private int? _isvalid = 0;
        private int? _opclass = 0;
        private string _opman;
        private decimal? _opmoney;
        private DateTime? _optime = new DateTime?(DateTime.Now);
        private int? _optype;
        private string _syspeop;

        public string accountNum
        {
            get
            {
                return this._accountnum;
            }
            set
            {
                this._accountnum = value;
            }
        }

        public int id
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

        public int? isValid
        {
            get
            {
                return this._isvalid;
            }
            set
            {
                this._isvalid = value;
            }
        }

        public int? opClass
        {
            get
            {
                return this._opclass;
            }
            set
            {
                this._opclass = value;
            }
        }

        public string opMan
        {
            get
            {
                return this._opman;
            }
            set
            {
                this._opman = value;
            }
        }

        public decimal? opMoney
        {
            get
            {
                return this._opmoney;
            }
            set
            {
                this._opmoney = value;
            }
        }

        public DateTime? opTime
        {
            get
            {
                return this._optime;
            }
            set
            {
                this._optime = value;
            }
        }

        public int? opType
        {
            get
            {
                return this._optype;
            }
            set
            {
                this._optype = value;
            }
        }

        public string sysPeop
        {
            get
            {
                return this._syspeop;
            }
            set
            {
                this._syspeop = value;
            }
        }
    }
}

