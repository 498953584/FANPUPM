namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SpoilageMain
    {
        private int _auditstate;
        private int _depositoryid;
        private DateTime _recorddate;
        private string _remark;
        private DateTime _spoilagedate;
        private int _spoilageid;
        private decimal _spoilagemoney;
        private string _spoilagenumber;
        private string _transactperson;
        private string _transactstate;
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

        public DateTime SpoilageDate
        {
            get
            {
                return this._spoilagedate;
            }
            set
            {
                this._spoilagedate = value;
            }
        }

        public int SpoilageID
        {
            get
            {
                return this._spoilageid;
            }
            set
            {
                this._spoilageid = value;
            }
        }

        public decimal SpoilageMoney
        {
            get
            {
                return this._spoilagemoney;
            }
            set
            {
                this._spoilagemoney = value;
            }
        }

        public string SpoilageNumber
        {
            get
            {
                return this._spoilagenumber;
            }
            set
            {
                this._spoilagenumber = value;
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

