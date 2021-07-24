namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class InventoryMain
    {
        private int _auditstate;
        private int _depositoryid;
        private DateTime _inventorydate;
        private int _inventoryid;
        private string _inventoryperson;
        private DateTime _recorddate;
        private Guid _recordid;
        private string _remark;
        private string _transactstate;
        private string _usercode;

        public int AuditState
        {
            get
            {
                return this._auditstate;
            }
            set
            {
                this._auditstate = value;
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

        public DateTime InventoryDate
        {
            get
            {
                return this._inventorydate;
            }
            set
            {
                this._inventorydate = value;
            }
        }

        public int InventoryID
        {
            get
            {
                return this._inventoryid;
            }
            set
            {
                this._inventoryid = value;
            }
        }

        public string InventoryPerson
        {
            get
            {
                return this._inventoryperson;
            }
            set
            {
                this._inventoryperson = value;
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

        public Guid RecordID
        {
            get
            {
                return this._recordid;
            }
            set
            {
                this._recordid = value;
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

        public string TransactState
        {
            get
            {
                return this._transactstate;
            }
            set
            {
                this._transactstate = value;
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

