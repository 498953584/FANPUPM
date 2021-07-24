namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class MonthPlanResourceInfo
    {
        private DateTime _EndDate = DateTime.Now;
        private decimal _IntendingTime;
        private bool _IsHave = true;
        private int _NoteID;
        private Guid _ProjectCode = Guid.Empty;
        private ResourceInfo _Resource = new ResourceInfo();
        private string _ResourceCode = "";
        private int _RFlag;
        private int _SourceType;
        private string _Specification = "";
        private DateTime _StartDate = DateTime.Now;
        private string _TaskCode = "";

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

        public decimal IntendingTime
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

        public int NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                this._NoteID = value;
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

        public int RFlag
        {
            get
            {
                return this._RFlag;
            }
            set
            {
                this._RFlag = value;
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

        public string TaskCode
        {
            get
            {
                return this._TaskCode;
            }
            set
            {
                this._TaskCode = value;
            }
        }
    }
}

