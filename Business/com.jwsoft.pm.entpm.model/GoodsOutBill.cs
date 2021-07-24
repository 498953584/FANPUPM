namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class GoodsOutBill
    {
        private string _LLDept;
        private string _LLMan;
        private string _OutBillCode;
        private Guid _OutCode;
        private DateTime _OutDate;
        private Guid _ProjectCode;
        private string _Remark;
        private string _Sender;

        public string LLDept
        {
            get
            {
                return this._LLDept;
            }
            set
            {
                this._LLDept = value;
            }
        }

        public string LLMan
        {
            get
            {
                return this._LLMan;
            }
            set
            {
                this._LLMan = value;
            }
        }

        public string OutBillCode
        {
            get
            {
                return this._OutBillCode;
            }
            set
            {
                this._OutBillCode = value;
            }
        }

        public Guid OutCode
        {
            get
            {
                return this._OutCode;
            }
            set
            {
                this._OutCode = value;
            }
        }

        public DateTime OutDate
        {
            get
            {
                return this._OutDate;
            }
            set
            {
                this._OutDate = value;
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

        public string Sender
        {
            get
            {
                return this._Sender;
            }
            set
            {
                this._Sender = value;
            }
        }
    }
}

