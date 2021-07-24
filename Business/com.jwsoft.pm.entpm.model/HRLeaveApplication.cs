namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class HRLeaveApplication
    {
        private int _auditstate;
        private DateTime _backdate;
        private DateTime _begindate;
        private string _confirmuser;
        private decimal _days;
        private string _isapply;
        private string _isconfirm;
        private decimal _leavedays;
        private int _leavetype;
        private string _reason;
        private DateTime _recorddate;
        private Guid _recordid;
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

        public DateTime BackDate
        {
            get
            {
                return this._backdate;
            }
            set
            {
                this._backdate = value;
            }
        }

        public DateTime BeginDate
        {
            get
            {
                return this._begindate;
            }
            set
            {
                this._begindate = value;
            }
        }

        public string ConfirmUser
        {
            get
            {
                return this._confirmuser;
            }
            set
            {
                this._confirmuser = value;
            }
        }

        public decimal Days
        {
            get
            {
                return this._days;
            }
            set
            {
                this._days = value;
            }
        }

        public string IsApply
        {
            get
            {
                return this._isapply;
            }
            set
            {
                this._isapply = value;
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

        public decimal LeaveDays
        {
            get
            {
                return this._leavedays;
            }
            set
            {
                this._leavedays = value;
            }
        }

        public int LeaveType
        {
            get
            {
                return this._leavetype;
            }
            set
            {
                this._leavetype = value;
            }
        }

        public string Reason
        {
            get
            {
                return this._reason;
            }
            set
            {
                this._reason = value;
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

