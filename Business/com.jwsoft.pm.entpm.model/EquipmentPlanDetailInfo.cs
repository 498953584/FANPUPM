namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class EquipmentPlanDetailInfo : BaseInfo1
    {
        private decimal _BudgetPrice;
        private string _EquipmentCode;
        private decimal _EquipmentCount;
        private string _EquipmentName;
        private int _EquipmentType;
        private int _ItemID;
        private string _PlanCode;
        private string _Remark;
        private string _resourceName;
        private string _ResourceNo;
        private string _ResourceUnit;
        private string _Specification;
        private decimal _SumPrice;

        public decimal BudgetPrice
        {
            get
            {
                return this._BudgetPrice;
            }
            set
            {
                this._BudgetPrice = value;
            }
        }

        public string EquipmentCode
        {
            get
            {
                return this._EquipmentCode;
            }
            set
            {
                this._EquipmentCode = value;
            }
        }

        public decimal EquipmentCount
        {
            get
            {
                return this._EquipmentCount;
            }
            set
            {
                this._EquipmentCount = value;
            }
        }

        public string EquipmentName
        {
            get
            {
                return this._EquipmentName;
            }
            set
            {
                this._EquipmentName = value;
            }
        }

        public int EquipmentType
        {
            get
            {
                return this._EquipmentType;
            }
            set
            {
                this._EquipmentType = value;
            }
        }

        public int ItemID
        {
            get
            {
                return this._ItemID;
            }
            set
            {
                this._ItemID = value;
            }
        }

        public string PlanCode
        {
            get
            {
                return this._PlanCode;
            }
            set
            {
                this._PlanCode = value;
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

        public string resourceName
        {
            get
            {
                return this._resourceName;
            }
            set
            {
                this._resourceName = value;
            }
        }

        public string ResourceNo
        {
            get
            {
                return this._ResourceNo;
            }
            set
            {
                this._ResourceNo = value;
            }
        }

        public string ResourceUnit
        {
            get
            {
                return this._ResourceUnit;
            }
            set
            {
                this._ResourceUnit = value;
            }
        }

        public string Specification
        {
            get
            {
                return this._Specification;
            }
            set
            {
                this._Specification = value;
            }
        }

        public decimal SumPrice
        {
            get
            {
                return this._SumPrice;
            }
            set
            {
                this._SumPrice = value;
            }
        }
    }
}

