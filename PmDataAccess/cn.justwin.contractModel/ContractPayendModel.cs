namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class ContractPayendModel
    {
        private string _annex;
        private string _bwasperson;
        private string _contractid;
        private string _inperson;
        private DateTime? _intime;
        private string _modifystate;
        private string _otherexplain;
        private string _payendid;
        private string _projectcondition;
        private string _provisionmatter;
        private string payendtopics;
        private string wasperson;

        public string Annex
        {
            get
            {
                return this._annex;
            }
            set
            {
                this._annex = value;
            }
        }

        public string BWasPerson
        {
            get
            {
                return this._bwasperson;
            }
            set
            {
                this._bwasperson = value;
            }
        }

        public string ContractID
        {
            get
            {
                return this._contractid;
            }
            set
            {
                this._contractid = value;
            }
        }

        public string InPerson
        {
            get
            {
                return this._inperson;
            }
            set
            {
                this._inperson = value;
            }
        }

        public DateTime? InTime
        {
            get
            {
                return this._intime;
            }
            set
            {
                this._intime = value;
            }
        }

        public string ModifyState
        {
            get
            {
                return this._modifystate;
            }
            set
            {
                this._modifystate = value;
            }
        }

        public string OtherExplain
        {
            get
            {
                return this._otherexplain;
            }
            set
            {
                this._otherexplain = value;
            }
        }

        public string PayendID
        {
            get
            {
                return this._payendid;
            }
            set
            {
                this._payendid = value;
            }
        }

        public string PayendTopics
        {
            get
            {
                return this.payendtopics;
            }
            set
            {
                this.payendtopics = value;
            }
        }

        public string ProjectCondition
        {
            get
            {
                return this._projectcondition;
            }
            set
            {
                this._projectcondition = value;
            }
        }

        public string ProvisionMatter
        {
            get
            {
                return this._provisionmatter;
            }
            set
            {
                this._provisionmatter = value;
            }
        }

        public string WasPerson
        {
            get
            {
                return this.wasperson;
            }
            set
            {
                this.wasperson = value;
            }
        }
    }
}

