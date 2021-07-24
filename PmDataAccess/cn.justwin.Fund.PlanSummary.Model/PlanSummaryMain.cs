namespace cn.justwin.Fund.PlanSummary.Model
{
    using System;

    [Serializable]
    public class PlanSummaryMain
    {
        private int? _flowstate;
        private Guid _mid;
        private string _planname;
        private string _plantype;
        private string _remark;
        private string _reporter;
        private DateTime? _reporttime;

        public int? FlowState
        {
            get
            {
                return this._flowstate;
            }
            set
            {
                this._flowstate = value;
            }
        }

        public Guid MID
        {
            get
            {
                return this._mid;
            }
            set
            {
                this._mid = value;
            }
        }

        public string PlanName
        {
            get
            {
                return this._planname;
            }
            set
            {
                this._planname = value;
            }
        }

        public string PlanType
        {
            get
            {
                return this._plantype;
            }
            set
            {
                this._plantype = value;
            }
        }

        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public string Reporter
        {
            get
            {
                return this._reporter;
            }
            set
            {
                this._reporter = value;
            }
        }

        public DateTime? ReportTime
        {
            get
            {
                return this._reporttime;
            }
            set
            {
                this._reporttime = value;
            }
        }
    }
}

