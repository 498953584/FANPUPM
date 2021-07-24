namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class TaskListInfo
    {
        private int _ChildNum = -2147483648;
        private decimal _CompleteCount;
        private decimal _ContractPrice = 0.0M;
        private decimal _Cost;
        private DateTime _EndDate = DateTime.Now;
        private bool _IsValid = true;
        private long _NoteID;
        private string _ParentTaskCode = string.Empty;
        private bool _Pivotal;
        private Guid _ProjectCode = Guid.Empty;
        private string _Quality = string.Empty;
        private decimal _Quantity;
        private string _QuantityUnit = string.Empty;
        private string _Remark = string.Empty;
        private string _Safety = string.Empty;
        private DateTime _StartDate = DateTime.Now;
        private decimal _SynthPrice;
        private string _TaskCode = string.Empty;
        private string _TaskName = string.Empty;
        private int _TaskState = -2147483648;
        private int _WorkDay;
        private int _WorkLayer = -2147483648;

        public int ChildNum
        {
            get
            {
                return this._ChildNum;
            }
            set
            {
                this._ChildNum = value;
            }
        }

        public decimal CompleteCount
        {
            get
            {
                return this._CompleteCount;
            }
            set
            {
                this._CompleteCount = value;
            }
        }

        public decimal ContractPrice
        {
            get
            {
                return this._ContractPrice;
            }
            set
            {
                this._ContractPrice = value;
            }
        }

        public decimal Cost
        {
            get
            {
                return this._Cost;
            }
            set
            {
                this._Cost = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this._IsValid = value;
            }
        }

        public long NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                this._NoteID = value;
            }
        }

        public string ParentTaskCode
        {
            get
            {
                return this._ParentTaskCode;
            }
            set
            {
                this._ParentTaskCode = value;
            }
        }

        public bool Pivotal
        {
            get
            {
                return this._Pivotal;
            }
            set
            {
                this._Pivotal = value;
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

        public string Quality
        {
            get
            {
                return this._Quality;
            }
            set
            {
                this._Quality = value;
            }
        }

        public decimal Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
            }
        }

        public string QuantityUnit
        {
            get
            {
                return this._QuantityUnit;
            }
            set
            {
                this._QuantityUnit = value;
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

        public string Safety
        {
            get
            {
                return this._Safety;
            }
            set
            {
                this._Safety = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this._StartDate = value;
            }
        }

        public decimal SynthPrice
        {
            get
            {
                return this._SynthPrice;
            }
            set
            {
                this._SynthPrice = value;
            }
        }

        public string TaskCode
        {
            get
            {
                return this._TaskCode;
            }
            set
            {
                this._TaskCode = value;
            }
        }

        public string TaskName
        {
            get
            {
                return this._TaskName;
            }
            set
            {
                this._TaskName = value;
            }
        }

        public int TaskState
        {
            get
            {
                return this._TaskState;
            }
            set
            {
                this._TaskState = value;
            }
        }

        public int WorkDay
        {
            get
            {
                return this._WorkDay;
            }
            set
            {
                this._WorkDay = value;
            }
        }

        public int WorkLayer
        {
            get
            {
                return this._WorkLayer;
            }
            set
            {
                this._WorkLayer = value;
            }
        }
    }
}

