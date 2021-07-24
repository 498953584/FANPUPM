namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SupplyPlanMain
    {
        private DateTime _auditdate;
        private string _auditinfo;
        private string _auditman;
        private int _auditresult;
        private string _contractCode;
        private DateTime _createtime;
        private Guid _FlowGuid = Guid.Empty;
        private int _ischecked;
        private string _maker;
        private string _planCode;
        private int _planid;
        private string _reason;
        private string _remark;
        private DateTime _supplydate;
        private string _type;

        public DateTime AuditDate
        {
            get
            {
                return this._auditdate;
            }
            set
            {
                this._auditdate = value;
            }
        }

        public string AuditInfo
        {
            get
            {
                return this._auditinfo;
            }
            set
            {
                this._auditinfo = value;
            }
        }

        public string AuditMan
        {
            get
            {
                return this._auditman;
            }
            set
            {
                this._auditman = value;
            }
        }

        public int AuditResult
        {
            get
            {
                return this._auditresult;
            }
            set
            {
                this._auditresult = value;
            }
        }

        public string ContractCode
        {
            get
            {
                return this._contractCode;
            }
            set
            {
                this._contractCode = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return this._createtime;
            }
            set
            {
                this._createtime = value;
            }
        }

        public Guid FlowGuid
        {
            get
            {
                return this._FlowGuid;
            }
            set
            {
                this._FlowGuid = value;
            }
        }

        public int IsChecked
        {
            get
            {
                return this._ischecked;
            }
            set
            {
                this._ischecked = value;
            }
        }

        public string PlanCode
        {
            get
            {
                return this._planCode;
            }
            set
            {
                this._planCode = value;
            }
        }

        public int PlanID
        {
            get
            {
                return this._planid;
            }
            set
            {
                this._planid = value;
            }
        }

        public string PlanMaker
        {
            get
            {
                return this._maker;
            }
            set
            {
                this._maker = value;
            }
        }

        public string Reason
        {
            get
            {
                return this._reason;
            }
            set
            {
                this._reason = value;
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

        public DateTime SupplyDate
        {
            get
            {
                return this._supplydate;
            }
            set
            {
                this._supplydate = value;
            }
        }

        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

