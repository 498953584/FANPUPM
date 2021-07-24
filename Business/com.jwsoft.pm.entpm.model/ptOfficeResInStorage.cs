namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ptOfficeResInStorage
    {
        private string _billcode;
        private int _depotid;
        private DateTime _indate;
        private int _instorageid;
        private DateTime _recorddate;
        private string _remark;
        private string _transactor;
        private string _usercode;

        public string BillCode
        {
            get
            {
                return this._billcode;
            }
            set
            {
                this._billcode = value;
            }
        }

        public int DepotID
        {
            get
            {
                return this._depotid;
            }
            set
            {
                this._depotid = value;
            }
        }

        public DateTime InDate
        {
            get
            {
                return this._indate;
            }
            set
            {
                this._indate = value;
            }
        }

        public int InStorageID
        {
            get
            {
                return this._instorageid;
            }
            set
            {
                this._instorageid = value;
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

        public string Transactor
        {
            get
            {
                return this._transactor;
            }
            set
            {
                this._transactor = value;
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

