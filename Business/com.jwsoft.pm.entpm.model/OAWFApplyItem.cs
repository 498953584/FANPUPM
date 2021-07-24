namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAWFApplyItem
    {
        private int _auditstate;
        private string _businessclass;
        private string _filepath;
        private string _originalname;
        private DateTime _recorddate;
        private Guid _recordid;
        private string _remark;
        private int _templateid;
        private string _title;
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

        public string BusinessClass
        {
            get
            {
                return this._businessclass;
            }
            set
            {
                this._businessclass = value;
            }
        }

        public string FilePath
        {
            get
            {
                return this._filepath;
            }
            set
            {
                this._filepath = value;
            }
        }

        public string OriginalName
        {
            get
            {
                return this._originalname;
            }
            set
            {
                this._originalname = value;
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

        public int TemplateID
        {
            get
            {
                return this._templateid;
            }
            set
            {
                this._templateid = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
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

