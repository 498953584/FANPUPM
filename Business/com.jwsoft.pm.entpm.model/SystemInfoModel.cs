namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SystemInfoModel
    {
        private int _auditstate;
        private int _classid;
        private string _corpcode;
        private string _isCurrent;
        private DateTime _recorddate;
        private Guid _recordid;
        private string _remark;
        private DateTime _signdate;
        private string _signman;
        private string _systemcode;
        private string _systemname;
        private string _systemtype;
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

        public int ClassID
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
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

        public string IsCurrent
        {
            get
            {
                return this._isCurrent;
            }
            set
            {
                this._isCurrent = value;
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

        public DateTime SignDate
        {
            get
            {
                return this._signdate;
            }
            set
            {
                this._signdate = value;
            }
        }

        public string SignMan
        {
            get
            {
                return this._signman;
            }
            set
            {
                this._signman = value;
            }
        }

        public string SystemCode
        {
            get
            {
                return this._systemcode;
            }
            set
            {
                this._systemcode = value;
            }
        }

        public string SystemName
        {
            get
            {
                return this._systemname;
            }
            set
            {
                this._systemname = value;
            }
        }

        public string SystemType
        {
            get
            {
                return this._systemtype;
            }
            set
            {
                this._systemtype = value;
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

