namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ContractCountInfo
    {
        private string _ContractCode;
        private string _ContractType;
        private DateTime _CountDate;
        private string _ProjectCode;
        private Guid _RecordID;
        private string _Remark;
        private string _ReportUser;

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

        public DateTime CountDate
        {
            get
            {
                return this._CountDate;
            }
            set
            {
                this._CountDate = value;
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

        public Guid RecordID
        {
            get
            {
                return this._RecordID;
            }
            set
            {
                this._RecordID = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string ReportUser
        {
            get
            {
                return this._ReportUser;
            }
            set
            {
                this._ReportUser = value;
            }
        }
    }
}

