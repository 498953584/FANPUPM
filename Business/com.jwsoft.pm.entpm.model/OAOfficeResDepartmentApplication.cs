namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAOfficeResDepartmentApplication
    {
        private Guid _acrecordid;
        private DateTime _applydate;
        private int _applydepartment;
        private string _applyman;
        private int _auditstate;
        private string _corpcode;
        private Guid _darecordid;
        private string _isabove;
        private string _iscomplete;
        private string _issubmit;
        private DateTime _recorddate;
        private string _remark;
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

        public DateTime ApplyDate
        {
            get
            {
                return this._applydate;
            }
            set
            {
                this._applydate = value;
            }
        }

        public int ApplyDepartment
        {
            get
            {
                return this._applydepartment;
            }
            set
            {
                this._applydepartment = value;
            }
        }

        public string ApplyMan
        {
            get
            {
                return this._applyman;
            }
            set
            {
                this._applyman = value;
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

        public Guid DARecordID
        {
            get
            {
                return this._darecordid;
            }
            set
            {
                this._darecordid = value;
            }
        }

        public string IsAbove
        {
            get
            {
                return this._isabove;
            }
            set
            {
                this._isabove = value;
            }
        }

        public string IsComplete
        {
            get
            {
                return this._iscomplete;
            }
            set
            {
                this._iscomplete = value;
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

