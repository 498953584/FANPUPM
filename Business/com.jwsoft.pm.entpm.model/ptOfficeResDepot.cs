namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ptOfficeResDepot
    {
        private string _corpcode;
        private int _depotid;
        private string _depotname;
        private string _isvalid;
        private string _remark;

        public string CorpCode
        {
            get
            {
                return this._corpcode;
            }
            set
            {
                this._corpcode = value;
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

        public string DepotName
        {
            get
            {
                return this._depotname;
            }
            set
            {
                this._depotname = value;
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

