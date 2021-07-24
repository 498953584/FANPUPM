namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SalaryGAModel
    {
        private DateTime? _applydate;
        private int? _auditstate;
        private string _isconfirm;
        private DateTime? _recorddate;
        private Guid _recordid;
        private string _remark;
        private string _usercode;

        public DateTime? ApplyDate
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

