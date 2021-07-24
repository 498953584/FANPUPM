namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ConstructTaskBook
    {
        private DateTime _ConstructDate = DateTime.Now;
        private int _ConstructUnit;
        private int _NoteID;
        private Guid _ProjectCode = Guid.Empty;
        private string _QualityAndSafety = string.Empty;
        private string _RecordPerson = string.Empty;
        private string _TaskBookCode = string.Empty;
        private string _TaskCode = string.Empty;
        private decimal _TodayComplete;
        private string _TodayWorkRemark = string.Empty;
        private string _WorkContent = string.Empty;

        public DateTime ConstructDate
        {
            get
            {
                return this._ConstructDate;
            }
            set
            {
                this._ConstructDate = value;
            }
        }

        public int ConstructUnit
        {
            get
            {
                return this._ConstructUnit;
            }
            set
            {
                this._ConstructUnit = value;
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

        public string QualityAndSafety
        {
            get
            {
                return this._QualityAndSafety;
            }
            set
            {
                this._QualityAndSafety = value;
            }
        }

        public string RecordPerson
        {
            get
            {
                return this._RecordPerson;
            }
            set
            {
                this._RecordPerson = value;
            }
        }

        public string TaskBookCode
        {
            get
            {
                return this._TaskBookCode;
            }
            set
            {
                this._TaskBookCode = value;
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

        public decimal TodayComplete
        {
            get
            {
                return this._TodayComplete;
            }
            set
            {
                this._TodayComplete = value;
            }
        }

        public string TodayWorkRemark
        {
            get
            {
                return this._TodayWorkRemark;
            }
            set
            {
                this._TodayWorkRemark = value;
            }
        }

        public string WorkContent
        {
            get
            {
                return this._WorkContent;
            }
            set
            {
                this._WorkContent = value;
            }
        }
    }
}

