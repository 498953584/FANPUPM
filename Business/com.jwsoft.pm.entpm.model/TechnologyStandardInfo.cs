namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class TechnologyStandardInfo
    {
        private int _MainId;
        private string _PrjCode = "";
        private string _Remark = "";
        private string _TechGuid = "";
        private string _TechnologyClassify = "";
        private int _TechnologyCriterion;
        private string _TechnologyCriterionID = "";
        private string _TechnologyName = "";
        private string _TechnologyPromulgateTime = "";

        public int MainId
        {
            get
            {
                return this._MainId;
            }
            set
            {
                this._MainId = value;
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

        public string TechGuid
        {
            get
            {
                return this._TechGuid;
            }
            set
            {
                this._TechGuid = value;
            }
        }

        public string TechnologyClassify
        {
            get
            {
                return this._TechnologyClassify;
            }
            set
            {
                this._TechnologyClassify = value;
            }
        }

        public int TechnologyCriterion
        {
            get
            {
                return this._TechnologyCriterion;
            }
            set
            {
                this._TechnologyCriterion = value;
            }
        }

        public string TechnologyCriterionID
        {
            get
            {
                return this._TechnologyCriterionID;
            }
            set
            {
                this._TechnologyCriterionID = value;
            }
        }

        public string TechnologyName
        {
            get
            {
                return this._TechnologyName;
            }
            set
            {
                this._TechnologyName = value;
            }
        }

        public string TechnologyPromulgateTime
        {
            get
            {
                return this._TechnologyPromulgateTime;
            }
            set
            {
                this._TechnologyPromulgateTime = value;
            }
        }
    }
}

