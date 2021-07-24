namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class EquipmentMonthInfo
    {
        private DateTime _EndDate = DateTime.Now;
        private int _IntendingTime;
        private bool _IsHave = true;
        private Guid _ProjectCode = Guid.Empty;
        private ResourceInfo _Resource = new ResourceInfo();
        private string _ResourceCode = "";
        private string _ScheduleCode = "";
        private int _SourceType;
        private string _Specification = "";
        private DateTime _StartDate = DateTime.Now;

        public DateTime EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;
            }
        }

        public int IntendingTime
        {
            get
            {
                return this._IntendingTime;
            }
            set
            {
                this._IntendingTime = value;
            }
        }

        public bool IsHave
        {
            get
            {
                return this._IsHave;
            }
            set
            {
                this._IsHave = value;
            }
        }

        public Guid ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public ResourceInfo Resource
        {
            get
            {
                return this._Resource;
            }
            set
            {
                this._Resource = value;
            }
        }

        public string ResourceCode
        {
            get
            {
                return this._ResourceCode;
            }
            set
            {
                this._ResourceCode = value;
            }
        }

        public string ScheduleCode
        {
            get
            {
                return this._ScheduleCode;
            }
            set
            {
                this._ScheduleCode = value;
            }
        }

        public int SourceType
        {
            get
            {
                return this._SourceType;
            }
            set
            {
                this._SourceType = value;
            }
        }

        public string Specification
        {
            get
            {
                return this._Specification;
            }
            set
            {
                this._Specification = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this._StartDate = value;
            }
        }
    }
}

