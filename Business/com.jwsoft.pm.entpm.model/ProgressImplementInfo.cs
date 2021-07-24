namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ProgressImplementInfo
    {
        private string _ActualizeCircs;
        private string _AnnexName = "";
        private string _ExaminePeople;
        private DateTime _ExamineTime = DateTime.Today;
        private string _FillPeople;
        private DateTime _FillTime = DateTime.Today;
        private string _MainID;
        private string _PlanId;
        private string _ProgressGuid;
        private string _Remark;

        public string ActualizeCircs
        {
            get
            {
                return this._ActualizeCircs;
            }
            set
            {
                this._ActualizeCircs = value;
            }
        }

        public string AnnexName
        {
            get
            {
                return this._AnnexName;
            }
            set
            {
                this._AnnexName = value;
            }
        }

        public string ExaminePeople
        {
            get
            {
                return this._ExaminePeople;
            }
            set
            {
                this._ExaminePeople = value;
            }
        }

        public DateTime ExamineTime
        {
            get
            {
                return this._ExamineTime;
            }
            set
            {
                this._ExamineTime = value;
            }
        }

        public string FillPeople
        {
            get
            {
                return this._FillPeople;
            }
            set
            {
                this._FillPeople = value;
            }
        }

        public DateTime FillTime
        {
            get
            {
                return this._FillTime;
            }
            set
            {
                this._FillTime = value;
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
    }
}

