namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class HRLeaveStat
    {
        private int _deptcode;
        private decimal _holiday1;
        private decimal _holiday2;
        private decimal _holiday3;
        private decimal _holiday4;
        private decimal _holiday5;
        private decimal _holiday6;
        private decimal _holiday7;
        private int _imonth;
        private int _iyear;
        private decimal _later;
        private decimal _leaveearly;
        private string _usercode;

        public int DeptCode
        {
            get
            {
                return this._deptcode;
            }
            set
            {
                this._deptcode = value;
            }
        }

        public decimal Holiday1
        {
            get
            {
                return this._holiday1;
            }
            set
            {
                this._holiday1 = value;
            }
        }

        public decimal Holiday2
        {
            get
            {
                return this._holiday2;
            }
            set
            {
                this._holiday2 = value;
            }
        }

        public decimal Holiday3
        {
            get
            {
                return this._holiday3;
            }
            set
            {
                this._holiday3 = value;
            }
        }

        public decimal Holiday4
        {
            get
            {
                return this._holiday4;
            }
            set
            {
                this._holiday4 = value;
            }
        }

        public decimal Holiday5
        {
            get
            {
                return this._holiday5;
            }
            set
            {
                this._holiday5 = value;
            }
        }

        public decimal Holiday6
        {
            get
            {
                return this._holiday6;
            }
            set
            {
                this._holiday6 = value;
            }
        }

        public decimal Holiday7
        {
            get
            {
                return this._holiday7;
            }
            set
            {
                this._holiday7 = value;
            }
        }

        public int iMonth
        {
            get
            {
                return this._imonth;
            }
            set
            {
                this._imonth = value;
            }
        }

        public int iYear
        {
            get
            {
                return this._iyear;
            }
            set
            {
                this._iyear = value;
            }
        }

        public decimal Later
        {
            get
            {
                return this._later;
            }
            set
            {
                this._later = value;
            }
        }

        public decimal LeaveEarly
        {
            get
            {
                return this._leaveearly;
            }
            set
            {
                this._leaveearly = value;
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

