namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SalarySAModel
    {
        private string _adjustreason;
        private int? _auditstate;
        private string _deptname;
        private string _dutyname;
        private string _employeecode;
        private string _isconfirm;
        private decimal? _newsalary;
        private decimal? _oldsalary;
        private DateTime? _recorddate;
        private Guid _recordid;
        private string _remark;
        private string _usercode;

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

        public string DeptName
        {
            get
            {
                return this._deptname;
            }
            set
            {
                this._deptname = value;
            }
        }

        public string DutyName
        {
            get
            {
                return this._dutyname;
            }
            set
            {
                this._dutyname = value;
            }
        }

        public string EmployeeCode
        {
            get
            {
                return this._employeecode;
            }
            set
            {
                this._employeecode = value;
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

        public decimal? NewSalary
        {
            get
            {
                return this._newsalary;
            }
            set
            {
                this._newsalary = value;
            }
        }

        public decimal? OldSalary
        {
            get
            {
                return this._oldsalary;
            }
            set
            {
                this._oldsalary = value;
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

