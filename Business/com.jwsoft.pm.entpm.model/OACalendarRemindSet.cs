namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OACalendarRemindSet
    {
        private DateTime _enddate;
        private Guid _infoguid;
        private string _ismessage;
        private string _issms;
        private int _remindhour;
        private int _remindminute;
        private int _remindtype;

        public DateTime EndDate
        {
            get
            {
                return this._enddate;
            }
            set
            {
                this._enddate = value;
            }
        }

        public Guid InfoGuid
        {
            get
            {
                return this._infoguid;
            }
            set
            {
                this._infoguid = value;
            }
        }

        public string IsMessage
        {
            get
            {
                return this._ismessage;
            }
            set
            {
                this._ismessage = value;
            }
        }

        public string IsSms
        {
            get
            {
                return this._issms;
            }
            set
            {
                this._issms = value;
            }
        }

        public int RemindHour
        {
            get
            {
                return this._remindhour;
            }
            set
            {
                this._remindhour = value;
            }
        }

        public int RemindMinute
        {
            get
            {
                return this._remindminute;
            }
            set
            {
                this._remindminute = value;
            }
        }

        public int RemindType
        {
            get
            {
                return this._remindtype;
            }
            set
            {
                this._remindtype = value;
            }
        }
    }
}

