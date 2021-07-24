namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class MonthProductValueDetail
    {
        private decimal _AccSuperQuantity;
        private decimal _AccumulativeQuantity;
        private DateTime _FillDate = DateTime.MaxValue;
        private decimal _MonthOverQuantity;
        private int _NoteID = -2147483648;
        private decimal _PlanAccumulativeQuantity;
        private decimal _PlanOverQuantity;
        private decimal _PlanProductValue;
        private decimal _ProductValue;
        private Guid _ProjectCode = Guid.Empty;
        private decimal _Quantity;
        private decimal _ReportQuantity;
        private DateTime _StatDate = DateTime.MaxValue;
        private decimal _SuperQuantity;
        private decimal _SynthPrice;
        private string _TaskCode = string.Empty;
        public TaskListInfo _TaskList = new TaskListInfo();
        private string _Unit = string.Empty;

        public decimal AccSuperQuantity
        {
            get
            {
                return this._AccSuperQuantity;
            }
            set
            {
                this._AccSuperQuantity = value;
            }
        }

        public decimal AccumulativeQuantity
        {
            get
            {
                return this._AccumulativeQuantity;
            }
            set
            {
                this._AccumulativeQuantity = value;
            }
        }

        public DateTime FillDate
        {
            get
            {
                return this._FillDate;
            }
            set
            {
                this._FillDate = value;
            }
        }

        public decimal MonthOverQuantity
        {
            get
            {
                return this._MonthOverQuantity;
            }
            set
            {
                this._MonthOverQuantity = value;
            }
        }

        public int NoteID
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

        public decimal PlanAccumulativeQuantity
        {
            get
            {
                return this._PlanAccumulativeQuantity;
            }
            set
            {
                this._PlanAccumulativeQuantity = value;
            }
        }

        public decimal PlanOverQuantity
        {
            get
            {
                return this._PlanOverQuantity;
            }
            set
            {
                this._PlanOverQuantity = value;
            }
        }

        public decimal PlanProductValue
        {
            get
            {
                return this._PlanProductValue;
            }
            set
            {
                this._PlanProductValue = value;
            }
        }

        public decimal ProductValue
        {
            get
            {
                return this._ProductValue;
            }
            set
            {
                this._ProductValue = value;
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

        public decimal ReportQuantity
        {
            get
            {
                return this._ReportQuantity;
            }
            set
            {
                this._ReportQuantity = value;
            }
        }

        public DateTime StatDate
        {
            get
            {
                return this._StatDate;
            }
            set
            {
                this._StatDate = value;
            }
        }

        public decimal SuperQuantity
        {
            get
            {
                return this._SuperQuantity;
            }
            set
            {
                this._SuperQuantity = value;
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

        public TaskListInfo TaskList
        {
            get
            {
                return this._TaskList;
            }
            set
            {
                this._TaskList = value;
            }
        }

        public string Unit
        {
            get
            {
                return this._Unit;
            }
            set
            {
                this._Unit = value;
            }
        }
    }
}

