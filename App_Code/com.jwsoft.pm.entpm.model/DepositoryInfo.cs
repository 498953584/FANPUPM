namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class DepositoryInfo
    {
        private int _depoclassid;
        private string _depositorycode;
        private int _depositoryid;
        private string _depositoryname;
        private string _principal;
        private string _purview;
        private string _remark;

        public int DepoClassID
        {
            get
            {
                return this._depoclassid;
            }
            set
            {
                this._depoclassid = value;
            }
        }

        public string DepositoryCode
        {
            get
            {
                return this._depositorycode;
            }
            set
            {
                this._depositorycode = value;
            }
        }

        public int DepositoryID
        {
            get
            {
                return this._depositoryid;
            }
            set
            {
                this._depositoryid = value;
            }
        }

        public string DepositoryName
        {
            get
            {
                return this._depositoryname;
            }
            set
            {
                this._depositoryname = value;
            }
        }

        public string Principal
        {
            get
            {
                return this._principal;
            }
            set
            {
                this._principal = value;
            }
        }

        public string Purview
        {
            get
            {
                return this._purview;
            }
            set
            {
                this._purview = value;
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

