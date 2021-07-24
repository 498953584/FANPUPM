namespace cn.justwin.opm.Fire
{
    using System;

    public class FireModel
    {
        private DateTime _addtime;
        private string _adduser;
        private string _bCode;
        private string _bName;
        private string _bType;
        private string _cause;
        private int _codeId;
        private string _dutyUser;
        private DateTime _endTime;
        private int _flowstate;
        private int _i_xh;
        private string _isvalid;
        private Guid _prjId;
        private string _remark;
        private Guid _uId;

        public DateTime Addtime
        {
            get
            {
                return this._addtime;
            }
            set
            {
                this._addtime = value;
            }
        }

        public string Adduser
        {
            get
            {
                return this._adduser;
            }
            set
            {
                this._adduser = value;
            }
        }

        public string BCode
        {
            get
            {
                return this._bCode;
            }
            set
            {
                this._bCode = value;
            }
        }

        public string BName
        {
            get
            {
                return this._bName;
            }
            set
            {
                this._bName = value;
            }
        }

        public string BType
        {
            get
            {
                return this._bType;
            }
            set
            {
                this._bType = value;
            }
        }

        public string Cause
        {
            get
            {
                return this._cause;
            }
            set
            {
                this._cause = value;
            }
        }

        public int CodeId
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

        public string DutyUser
        {
            get
            {
                return this._dutyUser;
            }
            set
            {
                this._dutyUser = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return this._endTime;
            }
            set
            {
                this._endTime = value;
            }
        }

        public int Flowstate
        {
            get
            {
                return this._flowstate;
            }
            set
            {
                this._flowstate = value;
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

        public string Isvalid
        {
            get
            {
                return this._isvalid;
            }
            set
            {
                this._isvalid = value;
            }
        }

        public Guid PrjId
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

        public Guid UId
        {
            get
            {
                return this._uId;
            }
            set
            {
                this._uId = value;
            }
        }
    }
}

