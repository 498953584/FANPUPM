namespace cn.justwin.opm.PM.prj_plan
{
    using System;

    public class PrjInfoCorpModel
    {
        private DateTime? _addtime;
        private string _adduser;
        private int? _corpid;
        private string _corpname;
        private string _corptype;
        private int? _i_xh = 0;
        private string _isvalid;
        private string _linkman;
        private string _linktel;
        private Guid _prjguid;
        private string _remark;
        private Guid _uid;

        public DateTime? AddTime
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

        public int? CorpId
        {
            get
            {
                return this._corpid;
            }
            set
            {
                this._corpid = value;
            }
        }

        public string CorpName
        {
            get
            {
                return this._corpname;
            }
            set
            {
                this._corpname = value;
            }
        }

        public string CorpType
        {
            get
            {
                return this._corptype;
            }
            set
            {
                this._corptype = value;
            }
        }

        public int? I_xh
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
                return this._isvalid;
            }
            set
            {
                this._isvalid = value;
            }
        }

        public string LinkMan
        {
            get
            {
                return this._linkman;
            }
            set
            {
                this._linkman = value;
            }
        }

        public string LinkTel
        {
            get
            {
                return this._linktel;
            }
            set
            {
                this._linktel = value;
            }
        }

        public Guid PrjGuid
        {
            get
            {
                return this._prjguid;
            }
            set
            {
                this._prjguid = value;
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

        public Guid UID
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

