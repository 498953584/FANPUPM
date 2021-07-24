namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OABooksApplyMain
    {
        private int _auditstate;
        private string _corpcode;
        private string _isinstorage;
        private DateTime _recorddate;
        private Guid _recordid;
        private string _remark;
        private decimal _summoney;
        private string _usercode;

        public int AuditState
        {
            get
            {
                return this._auditstate;
            }
            set
            {
                this._auditstate = value;
            }
        }

        public string CorpCode
        {
            get
            {
                return this._corpcode;
            }
            set
            {
                this._corpcode = value;
            }
        }

        public string IsInStorage
        {
            get
            {
                return this._isinstorage;
            }
            set
            {
                this._isinstorage = value;
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

        public Guid RecordID
        {
            get
            {
                return this._recordid;
            }
            set
            {
                this._recordid = value;
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

        public decimal SumMoney
        {
            get
            {
                return this._summoney;
            }
            set
            {
                this._summoney = value;
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

