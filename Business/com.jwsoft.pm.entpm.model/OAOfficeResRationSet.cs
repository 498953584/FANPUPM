namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAOfficeResRationSet
    {
        private decimal _ration;
        private string _rationtype;
        private string _recordcode;
        private DateTime _recorddate;
        private string _remark;
        private string _usercode;

        public decimal Ration
        {
            get
            {
                return this._ration;
            }
            set
            {
                this._ration = value;
            }
        }

        public string RationType
        {
            get
            {
                return this._rationtype;
            }
            set
            {
                this._rationtype = value;
            }
        }

        public string RecordCode
        {
            get
            {
                return this._recordcode;
            }
            set
            {
                this._recordcode = value;
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

