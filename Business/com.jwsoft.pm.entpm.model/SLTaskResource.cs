namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SLTaskResource
    {
        private decimal _fee;
        private decimal _fee1;
        private int _noteID;
        private string _projectCode;
        private decimal _quantity;
        private string _resourceCode;
        private int _resourceStyle;
        private string _taskCode;
        private decimal _unitPrice;
        private int _wbsType;

        public decimal Fee
        {
            get
            {
                return this._fee;
            }
            set
            {
                this._fee = value;
            }
        }

        public decimal Fee1
        {
            get
            {
                return this._fee1;
            }
            set
            {
                this._fee1 = value;
            }
        }

        public int NoteID
        {
            get
            {
                return this._noteID;
            }
            set
            {
                this._noteID = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this._projectCode;
            }
            set
            {
                this._projectCode = value;
            }
        }

        public decimal Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                this._quantity = value;
            }
        }

        public string ResourceCode
        {
            get
            {
                return this._resourceCode;
            }
            set
            {
                this._resourceCode = value;
            }
        }

        public int ResourceStyle
        {
            get
            {
                return this._resourceStyle;
            }
            set
            {
                this._resourceStyle = value;
            }
        }

        public string TaskCode
        {
            get
            {
                return this._taskCode;
            }
            set
            {
                this._taskCode = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return this._unitPrice;
            }
            set
            {
                this._unitPrice = value;
            }
        }

        public int WbsType
        {
            get
            {
                return this._wbsType;
            }
            set
            {
                this._wbsType = value;
            }
        }
    }
}

