namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class EntStandardInfo
    {
        private Guid _ClassID = Guid.Empty;
        private string _EnterGuid = "";
        private int _MainKey;
        private string _Remark = "";
        private string _TechnologyClassify = "";
        private string _TechnologyCriterionID = "";
        private string _TechnologyName = "";
        private string _TechnologyPromulgateTime = "";

        public Guid ClassID
        {
            get
            {
                return this._ClassID;
            }
            set
            {
                this._ClassID = value;
            }
        }

        public string EnterGuid
        {
            get
            {
                return this._EnterGuid;
            }
            set
            {
                this._EnterGuid = value;
            }
        }

        public int MainKey
        {
            get
            {
                return this._MainKey;
            }
            set
            {
                this._MainKey = value;
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

