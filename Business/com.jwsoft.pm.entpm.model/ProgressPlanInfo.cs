namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ProgressPlanInfo
    {
        private string _ApplicationName;
        private int _AuditState;
        private DateTime _CompletedDate = DateTime.Today;
        private string _DeclareUnitView;
        private string _PanelView;
        private string _PermissionPeople;
        private string _PermissionView;
        private string _PlanCode;
        private string _PlanId;
        private string _PlanSort;
        private string _PrjCode;
        private string _PrjName;
        private string _ProgressGuid;
        private string _Remark;
        private string _ResultsHolders;
        private string _ResultsName;
        private string _strAuditState;
        private string _VettingCommitteeView;

        public string ApplicationName
        {
            get
            {
                return this._ApplicationName;
            }
            set
            {
                this._ApplicationName = value;
            }
        }

        public int AuditState
        {
            get
            {
                return this._AuditState;
            }
            set
            {
                this._AuditState = value;
            }
        }

        public DateTime CompletedDate
        {
            get
            {
                return this._CompletedDate;
            }
            set
            {
                this._CompletedDate = value;
            }
        }

        public string DeclareUnitView
        {
            get
            {
                return this._DeclareUnitView;
            }
            set
            {
                this._DeclareUnitView = value;
            }
        }

        public string PanelView
        {
            get
            {
                return this._PanelView;
            }
            set
            {
                this._PanelView = value;
            }
        }

        public string PermissionPeople
        {
            get
            {
                return this._PermissionPeople;
            }
            set
            {
                this._PermissionPeople = value;
            }
        }

        public string PermissionView
        {
            get
            {
                return this._PermissionView;
            }
            set
            {
                this._PermissionView = value;
            }
        }

        public string PlanCode
        {
            get
            {
                return this._PlanCode;
            }
            set
            {
                this._PlanCode = value;
            }
        }

        public string PlanId
        {
            get
            {
                return this._PlanId;
            }
            set
            {
                this._PlanId = value;
            }
        }

        public string PlanSort
        {
            get
            {
                return this._PlanSort;
            }
            set
            {
                this._PlanSort = value;
            }
        }

        public string PrjCode
        {
            get
            {
                return this._PrjCode;
            }
            set
            {
                this._PrjCode = value;
            }
        }

        public string PrjName
        {
            get
            {
                return this._PrjName;
            }
            set
            {
                this._PrjName = value;
            }
        }

        public string ProgressGuid
        {
            get
            {
                return this._ProgressGuid;
            }
            set
            {
                this._ProgressGuid = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string ResultsHolders
        {
            get
            {
                return this._ResultsHolders;
            }
            set
            {
                this._ResultsHolders = value;
            }
        }

        public string ResultsName
        {
            get
            {
                return this._ResultsName;
            }
            set
            {
                this._ResultsName = value;
            }
        }

        public string strAuditState
        {
            get
            {
                switch (this.AuditState)
                {
                    case 0:
                        this._strAuditState = "待审";
                        break;

                    case 1:
                        this._strAuditState = "审核不通过";
                        break;

                    case 2:
                        this._strAuditState = "审核通过";
                        break;

                    case 3:
                        this._strAuditState = "审核不通过";
                        break;

                    case 4:
                        this._strAuditState = "审核通过";
                        break;
                }
                return this._strAuditState;
            }
        }

        public string VettingCommitteeView
        {
            get
            {
                return this._VettingCommitteeView;
            }
            set
            {
                this._VettingCommitteeView = value;
            }
        }
    }
}

