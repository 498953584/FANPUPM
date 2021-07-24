namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ptOfficeResClass
    {
        private int _i_childnum;
        private int _i_xh;
        private string _isvalid;
        private DateTime _recorddate;
        private string _remark;
        private string _typecode;
        private string _typename;
        private string _usercode;

        public int i_ChildNum
        {
            get
            {
                return this._i_childnum;
            }
            set
            {
                this._i_childnum = value;
            }
        }

        public int i_xh
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

        public DateTime RecordDate
        {
            get
            {
                return this._recorddate;
            }
            set
            {
                this._recorddate = value;
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

        public string TypeCode
        {
            get
            {
                return this._typecode;
            }
            set
            {
                this._typecode = value;
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

        public string UserCode
        {
            get
            {
                return this._usercode;
            }
            set
            {
                this._usercode = value;
            }
        }
    }
}

