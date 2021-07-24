namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class MonthProductValue
    {
        private DateTime _EndDate = DateTime.MinValue;
        private DateTime _FillDate = DateTime.MaxValue;
        private int _IsHaveReprot = -2147483648;
        private int _IsHaveStat = -2147483648;
        private int _NoteID = -2147483648;
        private Guid _ProjectCode = Guid.Empty;
        private string _ReportPerson = string.Empty;
        private DateTime _StartDate = DateTime.MinValue;
        private DateTime _StatDate = DateTime.MinValue;

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

        public DateTime FillDate
        {
            get
            {
                return this._FillDate;
            }
            set
            {
                this._FillDate = value;
            }
        }

        public int IsHaveReprot
        {
            get
            {
                return this._IsHaveReprot;
            }
            set
            {
                this._IsHaveReprot = value;
            }
        }

        public int IsHaveStat
        {
            get
            {
                return this._IsHaveStat;
            }
            set
            {
                this._IsHaveStat = value;
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

        public string ReportPerson
        {
            get
            {
                return this._ReportPerson;
            }
            set
            {
                this._ReportPerson = value;
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

        public DateTime StatDate
        {
            get
            {
                return this._StatDate;
            }
            set
            {
                this._StatDate = value;
            }
        }
    }
}

