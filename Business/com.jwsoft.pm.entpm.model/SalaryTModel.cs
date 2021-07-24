namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SalaryTModel
    {
        private DateTime? _begindate;
        private string _classcode;
        private DateTime? _enddate;
        private DateTime? _recorddate;
        private int _templetid;
        private string _templetname;
        private string _usercode;
        private int? _usestate;

        public DateTime? BeginDate
        {
            get
            {
                return this._begindate;
            }
            set
            {
                this._begindate = value;
            }
        }

        public string ClassCode
        {
            get
            {
                return this._classcode;
            }
            set
            {
                this._classcode = value;
            }
        }

        public DateTime? EndDate
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

        public DateTime? RecordDate
        {
            get
            {
                return this._recorddate;
            }
            set
            {
                this._recorddate = value;
            }
        }

        public int TempletID
        {
            get
            {
                return this._templetid;
            }
            set
            {
                this._templetid = value;
            }
        }

        public string TempletName
        {
            get
            {
                return this._templetname;
            }
            set
            {
                this._templetname = value;
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

        public int? UseState
        {
            get
            {
                return this._usestate;
            }
            set
            {
                this._usestate = value;
            }
        }
    }
}

