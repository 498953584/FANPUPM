namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class ProjectForepart
    {
        private DateTime _AddDate = DateTime.Now;
        private string _Contact = "";
        private string _FundQuarry = "";
        private string _Locus = "";
        private string _MainWork = "";
        private int _OwnerCode;
        private string _PrjFollow = string.Empty;
        private int _PrjState;
        private Guid _ProjectCode = Guid.Empty;
        private string _ProjectIntro = "";
        private string _ProjectManualCode = "";
        private string _ProjectName = "";
        private int _ProjectType;
        private decimal _RekonPrice;
        private string _Remark = "";
        private ProjectState _State = ProjectState.Init;

        public DateTime AddDate
        {
            get
            {
                return this._AddDate;
            }
            set
            {
                this._AddDate = value;
            }
        }

        public string Contact
        {
            get
            {
                return this._Contact;
            }
            set
            {
                this._Contact = value;
            }
        }

        public string FundQuarry
        {
            get
            {
                return this._FundQuarry;
            }
            set
            {
                this._FundQuarry = value;
            }
        }

        public string Locus
        {
            get
            {
                return this._Locus;
            }
            set
            {
                this._Locus = value;
            }
        }

        public string MainWork
        {
            get
            {
                return this._MainWork;
            }
            set
            {
                this._MainWork = value;
            }
        }

        public int OwnerCode
        {
            get
            {
                return this._OwnerCode;
            }
            set
            {
                this._OwnerCode = value;
            }
        }

        public string PrjFollow
        {
            get
            {
                return this._PrjFollow;
            }
            set
            {
                this._PrjFollow = value;
            }
        }

        public int Prjtate
        {
            get
            {
                return this._PrjState;
            }
            set
            {
                this._PrjState = value;
            }
        }

        public Guid ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string ProjectIntro
        {
            get
            {
                return this._ProjectIntro;
            }
            set
            {
                this._ProjectIntro = value;
            }
        }

        public string ProjectManualCode
        {
            get
            {
                return this._ProjectManualCode;
            }
            set
            {
                this._ProjectManualCode = value;
            }
        }

        public string ProjectName
        {
            get
            {
                return this._ProjectName;
            }
            set
            {
                this._ProjectName = value;
            }
        }

        public int ProjectType
        {
            get
            {
                return this._ProjectType;
            }
            set
            {
                this._ProjectType = value;
            }
        }

        public decimal RekonPrice
        {
            get
            {
                return this._RekonPrice;
            }
            set
            {
                this._RekonPrice = value;
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

        public ProjectState State
        {
            get
            {
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }
    }
}

