namespace cn.justwin.opm.epcm.model
{
    using System;

    public class BuildBriefModel
    {
        private DateTime _addTime;
        private string _addUser;
        private string _briefId;
        private string _briefType;
        private string _buildSituation;
        private int _flowState;
        private string _isValid;
        private string _manaSituation;
        private string _prjId;
        private string _problem;
        private string _record;
        private string _remark;
        private string _sN;
        private string _type;

        public DateTime AddTime
        {
            get
            {
                return this._addTime;
            }
            set
            {
                this._addTime = value;
            }
        }

        public string AddUser
        {
            get
            {
                return this._addUser;
            }
            set
            {
                this._addUser = value;
            }
        }

        public string BriefId
        {
            get
            {
                return this._briefId;
            }
            set
            {
                this._briefId = value;
            }
        }

        public string BriefType
        {
            get
            {
                return this._briefType;
            }
            set
            {
                this._briefType = value;
            }
        }

        public string BuildSituation
        {
            get
            {
                return this._buildSituation;
            }
            set
            {
                this._buildSituation = value;
            }
        }

        public int FlowState
        {
            get
            {
                return this._flowState;
            }
            set
            {
                this._flowState = value;
            }
        }

        public string IsValid
        {
            get
            {
                return this._isValid;
            }
            set
            {
                this._isValid = value;
            }
        }

        public string ManaSituation
        {
            get
            {
                return this._manaSituation;
            }
            set
            {
                this._manaSituation = value;
            }
        }

        public string PrjId
        {
            get
            {
                return this._prjId;
            }
            set
            {
                this._prjId = value;
            }
        }

        public string Problem
        {
            get
            {
                return this._problem;
            }
            set
            {
                this._problem = value;
            }
        }

        public string Record
        {
            get
            {
                return this._record;
            }
            set
            {
                this._record = value;
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

        public string SN
        {
            get
            {
                return this._sN;
            }
            set
            {
                this._sN = value;
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

