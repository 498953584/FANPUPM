namespace cn.justwin.opm
{
    using System;

    public class PrjInfoModel
    {
        private string _businessmanager;
        private int _i_ChildNum;
        private string _PrjGuid;
        private int _PrjState;
        private DateTime? _projstartdate;
        private string _RecordDate;
        private string _startmanager;
        private string _startremark;
        private string _TypeCode;

        public string BusinessManager
        {
            get
            {
                return this._businessmanager;
            }
            set
            {
                this._businessmanager = value;
            }
        }

        public int I_ChildNum
        {
            get
            {
                return this._i_ChildNum;
            }
            set
            {
                this._i_ChildNum = value;
            }
        }

        public string PrjGuid
        {
            get
            {
                return this._PrjGuid;
            }
            set
            {
                this._PrjGuid = value;
            }
        }

        public int PrjState
        {
            get
            {
                return this._PrjState;
            }
            set
            {
                this._PrjState = value;
            }
        }

        public DateTime? ProjStartDate
        {
            get
            {
                return this._projstartdate;
            }
            set
            {
                this._projstartdate = value;
            }
        }

        public string RecordDate
        {
            get
            {
                return this._RecordDate;
            }
            set
            {
                this._RecordDate = value;
            }
        }

        public string StartManager
        {
            get
            {
                return this._startmanager;
            }
            set
            {
                this._startmanager = value;
            }
        }

        public string StartRemark
        {
            get
            {
                return this._startremark;
            }
            set
            {
                this._startremark = value;
            }
        }

        public string TypeCode
        {
            get
            {
                return this._TypeCode;
            }
            set
            {
                this._TypeCode = value;
            }
        }
    }
}

