namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ptOfficeResStockCheck
    {
        private int _checkrecordid;
        private int _depotid;
        private string _isconfirm;
        private DateTime _recorddate;
        private string _usercode;

        public int CheckRecordID
        {
            get
            {
                return this._checkrecordid;
            }
            set
            {
                this._checkrecordid = value;
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

        public string IsConfirm
        {
            get
            {
                return this._isconfirm;
            }
            set
            {
                this._isconfirm = value;
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

