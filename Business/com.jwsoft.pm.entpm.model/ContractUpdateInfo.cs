namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ContractUpdateInfo
    {
        private string _ContractCode;
        private string _ContractType;
        private int _ID;
        private string _ProjectCode;
        private string _UpdateCode = "";
        private string _UpdateContent = "";
        private DateTime _UpdateDate;
        private string _UpdateMoney = "0";
        private string _UpdateType = "";

        public string ContractCode
        {
            get
            {
                return this._ContractCode;
            }
            set
            {
                this._ContractCode = value;
            }
        }

        public string ContractType
        {
            get
            {
                return this._ContractType;
            }
            set
            {
                this._ContractType = value;
            }
        }

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string UpdateCode
        {
            get
            {
                return this._UpdateCode;
            }
            set
            {
                this._UpdateCode = value;
            }
        }

        public string UpdateContent
        {
            get
            {
                return this._UpdateContent;
            }
            set
            {
                this._UpdateContent = value;
            }
        }

        public DateTime UpdateDate
        {
            get
            {
                return this._UpdateDate;
            }
            set
            {
                this._UpdateDate = value;
            }
        }

        public string UpdateMoney
        {
            get
            {
                return this._UpdateMoney;
            }
            set
            {
                this._UpdateMoney = value;
            }
        }

        public string UpdateType
        {
            get
            {
                return this._UpdateType;
            }
            set
            {
                this._UpdateType = value;
            }
        }
    }
}

