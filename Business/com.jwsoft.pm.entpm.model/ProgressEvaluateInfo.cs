namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ProgressEvaluateInfo
    {
        private string _Appraise;
        private string _AppraisePeople;
        private DateTime _AppraiseTime = DateTime.Today;
        private string _MainID;
        private string _ParentMainID;

        public string Appraise
        {
            get
            {
                return this._Appraise;
            }
            set
            {
                this._Appraise = value;
            }
        }

        public string AppraisePeople
        {
            get
            {
                return this._AppraisePeople;
            }
            set
            {
                this._AppraisePeople = value;
            }
        }

        public DateTime AppraiseTime
        {
            get
            {
                return this._AppraiseTime;
            }
            set
            {
                this._AppraiseTime = value;
            }
        }

        public string MainID
        {
            get
            {
                return this._MainID;
            }
            set
            {
                this._MainID = value;
            }
        }

        public string ParentMainID
        {
            get
            {
                return this._ParentMainID;
            }
            set
            {
                this._ParentMainID = value;
            }
        }
    }
}

