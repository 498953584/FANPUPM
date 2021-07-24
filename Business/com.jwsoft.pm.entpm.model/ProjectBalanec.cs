namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ProjectBalanec
    {
        private decimal _auditprice;
        private DateTime _balanecdate;
        private decimal _balanecprice;
        private int _id;
        private Guid _projectcode;

        public decimal AuditPrice
        {
            get
            {
                return this._auditprice;
            }
            set
            {
                this._auditprice = value;
            }
        }

        public DateTime BalanecDate
        {
            get
            {
                return this._balanecdate;
            }
            set
            {
                this._balanecdate = value;
            }
        }

        public decimal BalanecPrice
        {
            get
            {
                return this._balanecprice;
            }
            set
            {
                this._balanecprice = value;
            }
        }

        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public Guid ProjectCode
        {
            get
            {
                return this._projectcode;
            }
            set
            {
                this._projectcode = value;
            }
        }
    }
}

