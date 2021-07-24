namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SalaryIPIModel
    {
        private int? _auditstate;
        private string _classcode;
        private string _corpcode;
        private string _isissuepay;
        private int _issuemonth;
        private int _issueyear;
        private DateTime? _recorddate;
        private Guid _recordid;
        private string _remark;
        private int? _templetid;
        private string _usercode;

        public int? AuditState
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

        public string ClassCode
        {
            get
            {
                return this._classcode;
            }
            set
            {
                this._classcode = value;
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

        public string IsIssuePay
        {
            get
            {
                return this._isissuepay;
            }
            set
            {
                this._isissuepay = value;
            }
        }

        public int IssueMonth
        {
            get
            {
                return this._issuemonth;
            }
            set
            {
                this._issuemonth = value;
            }
        }

        public int IssueYear
        {
            get
            {
                return this._issueyear;
            }
            set
            {
                this._issueyear = value;
            }
        }

        public DateTime? RecordDate
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

        public int? TempletID
        {
            get
            {
                return this._templetid;
            }
            set
            {
                this._templetid = value;
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

