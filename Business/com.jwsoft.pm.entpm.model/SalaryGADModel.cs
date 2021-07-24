namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SalaryGADModel
    {
        private string _adjustreason;
        private string _deptname;
        private int _detailid;
        private string _dutyname;
        private string _employeecode;
        private decimal? _newsalary;
        private decimal? _oldsalary;
        private Guid _recordid;

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

        public int DetailID
        {
            get
            {
                return this._detailid;
            }
            set
            {
                this._detailid = value;
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
    }
}

