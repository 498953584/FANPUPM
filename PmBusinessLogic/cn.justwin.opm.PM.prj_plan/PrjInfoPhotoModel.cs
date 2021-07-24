namespace cn.justwin.opm.PM.prj_plan
{
    using System;

    [Serializable]
    public class PrjInfoPhotoModel
    {
        private DateTime? _addtime;
        private string _adduser;
        private int? _i_xh = 0;
        private string _isvalid;
        private string _photocode;
        private string _photoname;
        private string _photopath;
        private int _phototype;
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

        public string PhotoCode
        {
            get
            {
                return this._photocode;
            }
            set
            {
                this._photocode = value;
            }
        }

        public string PhotoName
        {
            get
            {
                return this._photoname;
            }
            set
            {
                this._photoname = value;
            }
        }

        public string PhotoPath
        {
            get
            {
                return this._photopath;
            }
            set
            {
                this._photopath = value;
            }
        }

        public int PhotoType
        {
            get
            {
                return this._phototype;
            }
            set
            {
                this._phototype = value;
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

