namespace cn.justwin.opm
{
    using System;

    public class PrjStageInfoModel
    {
        private DateTime _addtime;
        private string _adduser;
        private string _businessName;
        private int _corpID;
        private int _filetype;
        private int _flowstate;
        private int _i_xh;
        private string _infocode;
        private string _infocontent;
        private string _infoid;
        private string _infoname;
        private string _isfile;
        private string _isvalid;
        private string _prjID;
        private string _prjstageid;
        private string _remark;
        private int codeID;

        public DateTime AddTime
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

        public string AddUser
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

        public string BusinessName
        {
            get
            {
                return this._businessName;
            }
            set
            {
                this._businessName = value;
            }
        }

        public int CodeID
        {
            get
            {
                return this.codeID;
            }
            set
            {
                this.codeID = value;
            }
        }

        public int CorpID
        {
            get
            {
                return this._corpID;
            }
            set
            {
                this._corpID = value;
            }
        }

        public int FileType
        {
            get
            {
                return this._filetype;
            }
            set
            {
                this._filetype = value;
            }
        }

        public int FlowState
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

        public string InfoCode
        {
            get
            {
                return this._infocode;
            }
            set
            {
                this._infocode = value;
            }
        }

        public string InfoContent
        {
            get
            {
                return this._infocontent;
            }
            set
            {
                this._infocontent = value;
            }
        }

        public string InfoID
        {
            get
            {
                return this._infoid;
            }
            set
            {
                this._infoid = value;
            }
        }

        public string InfoName
        {
            get
            {
                return this._infoname;
            }
            set
            {
                this._infoname = value;
            }
        }

        public string IsFile
        {
            get
            {
                return this._isfile;
            }
            set
            {
                this._isfile = value;
            }
        }

        public string IsValid
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

        public string PrjID
        {
            get
            {
                return this._prjID;
            }
            set
            {
                this._prjID = value;
            }
        }

        public string PrjStageID
        {
            get
            {
                return this._prjstageid;
            }
            set
            {
                this._prjstageid = value;
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
    }
}

