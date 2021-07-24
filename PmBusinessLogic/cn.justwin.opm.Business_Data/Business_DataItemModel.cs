namespace cn.justwin.opm.Business_Data
{
    using System;

    public class Business_DataItemModel
    {
        private DateTime _addTime;
        private string _addUser;
        private string _codeId;
        private DateTime _designDate;
        private int _flowState;
        private int _i_xh;
        private string _isValid;
        private string _pCode;
        private string _pName;
        private string _remark;
        private string _uid;

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

        public string CodeId
        {
            get
            {
                return this._codeId;
            }
            set
            {
                this._codeId = value;
            }
        }

        public DateTime DesignDate
        {
            get
            {
                return this._designDate;
            }
            set
            {
                this._designDate = value;
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

        public int I_xh
        {
            get
            {
                return this._i_xh;
            }
            set
            {
                this._i_xh = value;
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

        public string PCode
        {
            get
            {
                return this._pCode;
            }
            set
            {
                this._pCode = value;
            }
        }

        public string PName
        {
            get
            {
                return this._pName;
            }
            set
            {
                this._pName = value;
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

        public string UID
        {
            get
            {
                return this._uid;
            }
            set
            {
                this._uid = value;
            }
        }
    }
}

