namespace cn.justwin.AccountManage.prjaccount
{
    using cn.justwin.contractModel;
    using System;

    public class prjaccountModel
    {
        private Guid _accountID;
        private string _accountnum;
        private string _acountname;
        private DateTime? _activedata;
        private string _activeman;
        private string _authorizer;
        private string _contractnum;
        private PayoutContractModel _countractModel;
        private DateTime? _creatdata;
        private string _createman;
        private int _id;
        private int? _isactivity;
        private string _isnullify;
        private Guid _prjnum;
        private string _remark;

        public Guid accountID
        {
            get
            {
                return this._accountID;
            }
            set
            {
                this._accountID = value;
            }
        }

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

        public string acountName
        {
            get
            {
                return this._acountname;
            }
            set
            {
                this._acountname = value;
            }
        }

        public DateTime? activeData
        {
            get
            {
                return this._activedata;
            }
            set
            {
                this._activedata = value;
            }
        }

        public string activeMan
        {
            get
            {
                return this._activeman;
            }
            set
            {
                this._activeman = value;
            }
        }

        public string authorizer
        {
            get
            {
                return this._authorizer;
            }
            set
            {
                this._authorizer = value;
            }
        }

        public string contractNum
        {
            get
            {
                return this._contractnum;
            }
            set
            {
                this._contractnum = value;
            }
        }

        public PayoutContractModel countractModel
        {
            get
            {
                return this._countractModel;
            }
            set
            {
                this._countractModel = value;
            }
        }

        public DateTime? creatData
        {
            get
            {
                return this._creatdata;
            }
            set
            {
                this._creatdata = value;
            }
        }

        public string createMan
        {
            get
            {
                return this._createman;
            }
            set
            {
                this._createman = value;
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

        public int? isActivity
        {
            get
            {
                return this._isactivity;
            }
            set
            {
                this._isactivity = value;
            }
        }

        public string isnullify
        {
            get
            {
                return this._isnullify;
            }
            set
            {
                this._isnullify = value;
            }
        }

        public Guid prjNum
        {
            get
            {
                return this._prjnum;
            }
            set
            {
                this._prjnum = value;
            }
        }

        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }
    }
}

