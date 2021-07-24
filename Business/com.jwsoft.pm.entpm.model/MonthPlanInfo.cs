namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class MonthPlanInfo
    {
        private DateTime _EndDate = DateTime.Now;
        private int _NoteID;
        private int _ResouceType;
        private DateTime _StartDate = DateTime.Now;
        private DateTime _WeaveDate = DateTime.Now;
        private string _WeaveDepartment = "";
        private string _WeavePerson = "";

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

        public int ResouceType
        {
            get
            {
                return this._ResouceType;
            }
            set
            {
                this._ResouceType = value;
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

        public DateTime WeaveDate
        {
            get
            {
                return this._WeaveDate;
            }
            set
            {
                this._WeaveDate = value;
            }
        }

        public string WeaveDepartment
        {
            get
            {
                return this._WeaveDepartment;
            }
            set
            {
                this._WeaveDepartment = value;
            }
        }

        public string WeavePerson
        {
            get
            {
                return this._WeavePerson;
            }
            set
            {
                this._WeavePerson = value;
            }
        }
    }
}

