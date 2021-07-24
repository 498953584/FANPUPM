namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class MaterialApplyBill
    {
        private string _ApplyBillCode;
        private DateTime _ApplyDate;
        private string _ApplyMan;
        private int _ApplyType;
        private Guid _ApplyUniqueCode;
        private DateTime _AuditDate;
        private string _AuditInfo;
        private string _AuditMan;
        private bool _AuditResult;
        private bool _IsAudit;
        private Guid _ProjectCode;
        private string _Remark;

        public string ApplyBillCode
        {
            get
            {
                return this._ApplyBillCode;
            }
            set
            {
                this._ApplyBillCode = value;
            }
        }

        public DateTime ApplyDate
        {
            get
            {
                return this._ApplyDate;
            }
            set
            {
                this._ApplyDate = value;
            }
        }

        public string ApplyMan
        {
            get
            {
                return this._ApplyMan;
            }
            set
            {
                this._ApplyMan = value;
            }
        }

        public int ApplyType
        {
            get
            {
                return this._ApplyType;
            }
            set
            {
                this._ApplyType = value;
            }
        }

        public Guid ApplyUniqueCode
        {
            get
            {
                return this._ApplyUniqueCode;
            }
            set
            {
                this._ApplyUniqueCode = value;
            }
        }

        public DateTime AuditDate
        {
            get
            {
                return this._AuditDate;
            }
            set
            {
                this._AuditDate = value;
            }
        }

        public string AuditInfo
        {
            get
            {
                return this._AuditInfo;
            }
            set
            {
                this._AuditInfo = value;
            }
        }

        public string AuditMan
        {
            get
            {
                return this._AuditMan;
            }
            set
            {
                this._AuditMan = value;
            }
        }

        public bool AuditResult
        {
            get
            {
                return this._AuditResult;
            }
            set
            {
                this._AuditResult = value;
            }
        }

        public bool IsAudit
        {
            get
            {
                return this._IsAudit;
            }
            set
            {
                this._IsAudit = value;
            }
        }

        public Guid ProjectCode
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
    }
}

