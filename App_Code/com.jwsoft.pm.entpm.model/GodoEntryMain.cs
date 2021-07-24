namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class GodoEntryMain
    {
        private int _depositoryid;
        private DateTime _godoentrydate;
        private int _godoentryid;
        private decimal _godoentrymoney;
        private string _godoentrynumber;
        private DateTime _recorddate;
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

        public DateTime GodoEntryDate
        {
            get
            {
                return this._godoentrydate;
            }
            set
            {
                this._godoentrydate = value;
            }
        }

        public int GodoEntryID
        {
            get
            {
                return this._godoentryid;
            }
            set
            {
                this._godoentryid = value;
            }
        }

        public decimal GodoEntryMoney
        {
            get
            {
                return this._godoentrymoney;
            }
            set
            {
                this._godoentrymoney = value;
            }
        }

        public string GodoEntryNumber
        {
            get
            {
                return this._godoentrynumber;
            }
            set
            {
                this._godoentrynumber = value;
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

