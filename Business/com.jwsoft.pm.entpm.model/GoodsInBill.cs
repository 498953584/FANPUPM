namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class GoodsInBill
    {
        private int _BuyDept;
        private string _Checker;
        private string _ContractCode;
        private string _InAddr;
        private DateTime _InDate;
        private int _PlanID;
        private string _prjname;
        private Guid _ProjectCode;
        private string _Reciver;
        private string _Remark;
        private string _Sender;
        private string _StockInBillCode;
        private Guid _StockInCode;
        private int _SupplyMan;

        public int BuyDept
        {
            get
            {
                return this._BuyDept;
            }
            set
            {
                this._BuyDept = value;
            }
        }

        public string Checker
        {
            get
            {
                return this._Checker;
            }
            set
            {
                this._Checker = value;
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

        public string InAddr
        {
            get
            {
                return this._InAddr;
            }
            set
            {
                this._InAddr = value;
            }
        }

        public DateTime InDate
        {
            get
            {
                return this._InDate;
            }
            set
            {
                this._InDate = value;
            }
        }

        public int PlanID
        {
            get
            {
                return this._PlanID;
            }
            set
            {
                this._PlanID = value;
            }
        }

        public string prjName
        {
            get
            {
                return this._prjname;
            }
            set
            {
                this._prjname = value;
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

        public string Reciver
        {
            get
            {
                return this._Reciver;
            }
            set
            {
                this._Reciver = value;
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

        public string StockInBillCode
        {
            get
            {
                return this._StockInBillCode;
            }
            set
            {
                this._StockInBillCode = value;
            }
        }

        public Guid StockInCode
        {
            get
            {
                return this._StockInCode;
            }
            set
            {
                this._StockInCode = value;
            }
        }

        public int SupplyMan
        {
            get
            {
                return this._SupplyMan;
            }
            set
            {
                this._SupplyMan = value;
            }
        }
    }
}

