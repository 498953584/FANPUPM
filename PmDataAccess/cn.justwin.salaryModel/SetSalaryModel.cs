namespace cn.justwin.salaryModel
{
    using System;

    [Serializable]
    public class SetSalaryModel
    {
        private string _account;
        private DateTime _addtime;
        private decimal? _basicwage;
        private decimal? _daylaborersprice;
        private decimal? _hoursDay;
        private string _id;
        private decimal? _livingsubsidy;
        private decimal? _lowestSalaryDay;
        private decimal? _other;
        private decimal? _phonesubsidies;
        private string _templateid;
        private string _usercode;

        public string Account
        {
            get
            {
                return this._account;
            }
            set
            {
                this._account = value;
            }
        }

        public DateTime AddTime
        {
            get
            {
                return this._addtime;
            }
            set
            {
                this._addtime = value;
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

        public decimal? DayLaborersPrice
        {
            get
            {
                return this._daylaborersprice;
            }
            set
            {
                this._daylaborersprice = value;
            }
        }

        public decimal? HoursDay
        {
            get
            {
                return this._hoursDay;
            }
            set
            {
                this._hoursDay = value;
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

        public decimal? LivingSubsidy
        {
            get
            {
                return this._livingsubsidy;
            }
            set
            {
                this._livingsubsidy = value;
            }
        }

        public decimal? LowestSalaryDay
        {
            get
            {
                return this._lowestSalaryDay;
            }
            set
            {
                this._lowestSalaryDay = value;
            }
        }

        public decimal? Other
        {
            get
            {
                return this._other;
            }
            set
            {
                this._other = value;
            }
        }

        public decimal? PhoneSubsidies
        {
            get
            {
                return this._phonesubsidies;
            }
            set
            {
                this._phonesubsidies = value;
            }
        }

        public string TemplateID
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

