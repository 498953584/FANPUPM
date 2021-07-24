namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class HROrgAdjust
    {
        private string _adjustcontent;
        private string _adjustreason;
        private int _auditstate;
        private string _corpcode;
        private DateTime _recorddate;
        private Guid _recordid;
        private string _remark;
        private string _usercode;

        public string AdjustContent
        {
            get
            {
                return this._adjustcontent;
            }
            set
            {
                this._adjustcontent = value;
            }
        }

        public string AdjustReason
        {
            get
            {
                return this._adjustreason;
            }
            set
            {
                this._adjustreason = value;
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

