namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ContractAskPayInfo
    {
        private string _AskPayCode = "";
        private string _AskPayContent = "";
        private DateTime _AskPayDate;
        private string _AskPayIdea = "";
        private string _AskPayType = "";
        private string _ContractCode;
        private string _ContractType;
        private int _ID;
        private string _ProjectCode;

        public string AskPayCode
        {
            get
            {
                return this._AskPayCode;
            }
            set
            {
                this._AskPayCode = value;
            }
        }

        public string AskPayContent
        {
            get
            {
                return this._AskPayContent;
            }
            set
            {
                this._AskPayContent = value;
            }
        }

        public DateTime AskPayDate
        {
            get
            {
                return this._AskPayDate;
            }
            set
            {
                this._AskPayDate = value;
            }
        }

        public string AskPayIdea
        {
            get
            {
                return this._AskPayIdea;
            }
            set
            {
                this._AskPayIdea = value;
            }
        }

        public string AskPayType
        {
            get
            {
                return this._AskPayType;
            }
            set
            {
                this._AskPayType = value;
            }
        }

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
    }
}

