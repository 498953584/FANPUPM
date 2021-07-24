namespace cn.justwin.epm.datum
{
    using System;

    [Serializable]
    public class datumClassModel
    {
        private bool _isdelete;
        private bool _isfixup;
        private bool _isvalid;
        private bool _isvisible;
        private int _parentid;
        private string _remark;
        private int _typeid;
        private string _typename;

        public bool isDelete
        {
            get
            {
                return this._isdelete;
            }
            set
            {
                this._isdelete = value;
            }
        }

        public bool IsFixup
        {
            get
            {
                return this._isfixup;
            }
            set
            {
                this._isfixup = value;
            }
        }

        public bool IsValid
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

        public bool IsVisible
        {
            get
            {
                return this._isvisible;
            }
            set
            {
                this._isvisible = value;
            }
        }

        public int ParentId
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
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

        public int TypeId
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this._typename;
            }
            set
            {
                this._typename = value;
            }
        }
    }
}

