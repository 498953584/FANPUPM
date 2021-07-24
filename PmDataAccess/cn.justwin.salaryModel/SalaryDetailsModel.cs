namespace cn.justwin.salaryModel
{
    using System;

    [Serializable]
    public class SalaryDetailsModel
    {
        private decimal? _actualdays;
        private decimal? _award;
        private decimal? _basicwage;
        private string _calcmode;
        private string _currentmonth;
        private decimal? _daywage;
        private decimal? _deductelse;
        private decimal? _fullsalary;
        private decimal? _housingsubsidy;
        private string _id;
        private string _operatorcode;
        private decimal? _othersubsidy;
        private decimal? _overtimewage;
        private decimal? _paid;
        private string _paymode;
        private DateTime? _printtime;
        private string _salarycode;
        private decimal? _socialsecurity;
        private decimal? _tax;
        private decimal? _taxformersalary;
        private string _usercode;
        private decimal? days;

        public decimal? ActualDays
        {
            get
            {
                return this._actualdays;
            }
            set
            {
                this._actualdays = value;
            }
        }

        public decimal? Award
        {
            get
            {
                return this._award;
            }
            set
            {
                this._award = value;
            }
        }

        public decimal? BasicWage
        {
            get
            {
                return this._basicwage;
            }
            set
            {
                this._basicwage = value;
            }
        }

        public string CalcMode
        {
            get
            {
                return this._calcmode;
            }
            set
            {
                this._calcmode = value;
            }
        }

        public string CurrentMonth
        {
            get
            {
                return this._currentmonth;
            }
            set
            {
                this._currentmonth = value;
            }
        }

        public decimal? Days
        {
            get
            {
                return this.days;
            }
            set
            {
                this.days = value;
            }
        }

        public decimal? DayWage
        {
            get
            {
                return this._daywage;
            }
            set
            {
                this._daywage = value;
            }
        }

        public decimal? DeductElse
        {
            get
            {
                return this._deductelse;
            }
            set
            {
                this._deductelse = value;
            }
        }

        public decimal? FullSalary
        {
            get
            {
                return this._fullsalary;
            }
            set
            {
                this._fullsalary = value;
            }
        }

        public decimal? HousingSubsidy
        {
            get
            {
                return this._housingsubsidy;
            }
            set
            {
                this._housingsubsidy = value;
            }
        }

        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string OperatorCode
        {
            get
            {
                return this._operatorcode;
            }
            set
            {
                this._operatorcode = value;
            }
        }

        public decimal? OtherSubsidy
        {
            get
            {
                return this._othersubsidy;
            }
            set
            {
                this._othersubsidy = value;
            }
        }

        public decimal? OvertimeWage
        {
            get
            {
                return this._overtimewage;
            }
            set
            {
                this._overtimewage = value;
            }
        }

        public decimal? Paid
        {
            get
            {
                return this._paid;
            }
            set
            {
                this._paid = value;
            }
        }

        public string PayMode
        {
            get
            {
                return this._paymode;
            }
            set
            {
                this._paymode = value;
            }
        }

        public DateTime? PrintTime
        {
            get
            {
                return this._printtime;
            }
            set
            {
                this._printtime = value;
            }
        }

        public string SalaryCode
        {
            get
            {
                return this._salarycode;
            }
            set
            {
                this._salarycode = value;
            }
        }

        public decimal? SocialSecurity
        {
            get
            {
                return this._socialsecurity;
            }
            set
            {
                this._socialsecurity = value;
            }
        }

        public decimal? Tax
        {
            get
            {
                return this._tax;
            }
            set
            {
                this._tax = value;
            }
        }

        public decimal? TaxFormerSalary
        {
            get
            {
                return this._taxformersalary;
            }
            set
            {
                this._taxformersalary = value;
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

