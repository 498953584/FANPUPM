namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class RedeployMain
    {
        private int _depositoryid;
        private DateTime _recorddate;
        private DateTime _redeploydate;
        private int _redeployid;
        private decimal _redeploymoney;
        private string _redeploynumber;
        private string _remark;
        private string _transactperson;
        private string _transactstate;
        private string _usercode;

        public int DepositoryID
        {
            get
            {
                return this._depositoryid;
            }
            set
            {
                this._depositoryid = value;
            }
        }

        public DateTime RecordDate
        {
            get
            {
                return this._recorddate;
            }
            set
            {
                this._recorddate = value;
            }
        }

        public DateTime RedeployDate
        {
            get
            {
                return this._redeploydate;
            }
            set
            {
                this._redeploydate = value;
            }
        }

        public int Redeployid
        {
            get
            {
                return this._redeployid;
            }
            set
            {
                this._redeployid = value;
            }
        }

        public decimal RedeployMoney
        {
            get
            {
                return this._redeploymoney;
            }
            set
            {
                this._redeploymoney = value;
            }
        }

        public string RedeployNumber
        {
            get
            {
                return this._redeploynumber;
            }
            set
            {
                this._redeploynumber = value;
            }
        }

        public string Remark
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

        public string TransactPerson
        {
            get
            {
                return this._transactperson;
            }
            set
            {
                this._transactperson = value;
            }
        }

        public string TransactState
        {
            get
            {
                return this._transactstate;
            }
            set
            {
                this._transactstate = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this._usercode;
            }
            set
            {
                this._usercode = value;
            }
        }
    }
}

