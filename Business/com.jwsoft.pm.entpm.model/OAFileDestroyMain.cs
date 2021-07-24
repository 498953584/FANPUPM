namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAFileDestroyMain
    {
        private int _auditstate;
        private string _isconfirm;
        private string _librarycode;
        private DateTime _recorddate;
        private Guid _recordid;
        private string _remark;
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

        public string IsConfirm
        {
            get
            {
                return this._isconfirm;
            }
            set
            {
                this._isconfirm = value;
            }
        }

        public string LibraryCode
        {
            get
            {
                return this._librarycode;
            }
            set
            {
                this._librarycode = value;
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

