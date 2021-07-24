namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class Fund_Plan_MonthMainInfo
    {
        private int _FlowState;
        private Guid _monthplanid;
        private DateTime _operatetime;
        private string _operatorcode;
        private string _operatorName;
        private int _planmonth;
        private string _plantype;
        private int _planyear;
        private Guid _prjguid;
        private string _prjName;
        private string _remark;

        public int FlowState
        {
            get
            {
                return this._FlowState;
            }
            set
            {
                this._FlowState = value;
            }
        }

        public Guid MonthPlanID
        {
            get
            {
                return this._monthplanid;
            }
            set
            {
                this._monthplanid = value;
            }
        }

        public DateTime OperateTime
        {
            get
            {
                return this._operatetime;
            }
            set
            {
                this._operatetime = value;
            }
        }

        public string OperatorCode
        {
            get
            {
                return this._operatorcode;
            }
            set
            {
                this._operatorcode = value;
            }
        }

        public string OperatorName
        {
            get
            {
                return this._operatorName;
            }
            set
            {
                this._operatorName = value;
            }
        }

        public int PlanMonth
        {
            get
            {
                return this._planmonth;
            }
            set
            {
                this._planmonth = value;
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

        public int PlanYear
        {
            get
            {
                return this._planyear;
            }
            set
            {
                this._planyear = value;
            }
        }

        public Guid PrjGuid
        {
            get
            {
                return this._prjguid;
            }
            set
            {
                this._prjguid = value;
            }
        }

        public string PrjName
        {
            get
            {
                return this._prjName;
            }
            set
            {
                this._prjName = value;
            }
        }

        public string Remark
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
    }
}

