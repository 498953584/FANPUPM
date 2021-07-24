namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAOfficeResApplicationCollect
    {
        private Guid _acrecordid;
        private string _applytype;
        private int _auditstate;
        private string _corpcode;
        private int _imonth;
        private string _issubmit;
        private int _iyear;
        private DateTime _recorddate;
        private string _remark;
        private decimal _summoney;
        private string _usercode;

        public Guid ACRecordID
        {
            get
            {
                return this._acrecordid;
            }
            set
            {
                this._acrecordid = value;
            }
        }

        public string ApplyType
        {
            get
            {
                return this._applytype;
            }
            set
            {
                this._applytype = value;
            }
        }

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

        public int iMonth
        {
            get
            {
                return this._imonth;
            }
            set
            {
                this._imonth = value;
            }
        }

        public string IsSubmit
        {
            get
            {
                return this._issubmit;
            }
            set
            {
                this._issubmit = value;
            }
        }

        public int iYear
        {
            get
            {
                return this._iyear;
            }
            set
            {
                this._iyear = value;
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

