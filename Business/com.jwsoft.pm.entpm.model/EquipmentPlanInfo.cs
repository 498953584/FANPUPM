namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class EquipmentPlanInfo : BaseInfo1
    {
        private DateTime _EnterDate;
        private DateTime _ExitDate;
        private int _IsAuditing;
        private int _IsFullUsed;
        private string _PlanCode;
        private DateTime _PlanCreatTime;
        private int _PlanID;
        private string _PlanMaker;
        private Guid _PrjCode;
        private string _Remark;
        private string _ScheduleName;

        public DateTime EnterDate
        {
            get
            {
                return this._EnterDate;
            }
            set
            {
                this._EnterDate = value;
            }
        }

        public DateTime ExitDate
        {
            get
            {
                return this._ExitDate;
            }
            set
            {
                this._ExitDate = value;
            }
        }

        public int IsAuditing
        {
            get
            {
                return this._IsAuditing;
            }
            set
            {
                this._IsAuditing = value;
            }
        }

        public int IsFullUsed
        {
            get
            {
                return this._IsFullUsed;
            }
            set
            {
                this._IsFullUsed = value;
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

        public DateTime PlanCreatTime
        {
            get
            {
                return this._PlanCreatTime;
            }
            set
            {
                this._PlanCreatTime = value;
            }
        }

        public int PlanID
        {
            get
            {
                return this._PlanID;
            }
            set
            {
                this._PlanID = value;
            }
        }

        public string PlanMaker
        {
            get
            {
                return this._PlanMaker;
            }
            set
            {
                this._PlanMaker = value;
            }
        }

        public Guid PrjCode
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

        public string ScheduleName
        {
            get
            {
                return this._ScheduleName;
            }
            set
            {
                this._ScheduleName = value;
            }
        }
    }
}

