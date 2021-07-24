namespace cn.justwin.salaryModel
{
    using System;

    [Serializable]
    public class MonthSalaryModel
    {
        private decimal? _actualdays;
        private decimal? _award;
        private string _currentmonth;
        private decimal? _days;
        private decimal? _deductelse;
        private string _id;
        private decimal? _laborprotection;
        private decimal? _paid;
        private decimal? _pretax;
        private decimal? _tax;
        private string _usercode;

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
                return this._days;
            }
            set
            {
                this._days = value;
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

        public string Id
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

        public decimal? LaborProtection
        {
            get
            {
                return this._laborprotection;
            }
            set
            {
                this._laborprotection = value;
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

        public decimal? PreTax
        {
            get
            {
                return this._pretax;
            }
            set
            {
                this._pretax = value;
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

