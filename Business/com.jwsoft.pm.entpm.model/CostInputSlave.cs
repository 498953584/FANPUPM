namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CostInputSlave
    {
        private int _ChildID;
        private string _CostCode;
        private string _ItemName;
        private decimal _Price;
        private Guid _RecordID;
        private string _Remark;

        public int ChildID
        {
            get
            {
                return this._ChildID;
            }
            set
            {
                this._ChildID = value;
            }
        }

        public string CostCode
        {
            get
            {
                return this._CostCode;
            }
            set
            {
                this._CostCode = value;
            }
        }

        public string ItemName
        {
            get
            {
                return this._ItemName;
            }
            set
            {
                this._ItemName = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
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
    }
}

