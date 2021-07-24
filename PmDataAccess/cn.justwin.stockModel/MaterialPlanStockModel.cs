namespace cn.justwin.stockModel
{
    using System;

    public class MaterialPlanStockModel
    {
        private string _arrivalDate;
        private string _designCode;
        private decimal _number;
        private string _remark;
        private string _scode;
        private string _taskId;
        private string _wpcode;
        private string _wpsid = Guid.NewGuid().ToString();

        public string ArrivalDate
        {
            get
            {
                return this._arrivalDate;
            }
            set
            {
                this._arrivalDate = value;
            }
        }

        public string DesignCode
        {
            get
            {
                return this._designCode;
            }
            set
            {
                this._designCode = value;
            }
        }

        public decimal number
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
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

        public string scode
        {
            get
            {
                return this._scode;
            }
            set
            {
                this._scode = value;
            }
        }

        public string TaskId
        {
            get
            {
                return this._taskId;
            }
            set
            {
                this._taskId = value;
            }
        }

        public string wpcode
        {
            get
            {
                return this._wpcode;
            }
            set
            {
                this._wpcode = value;
            }
        }

        public string wpsid
        {
            get
            {
                return this._wpsid;
            }
            set
            {
                this._wpsid = value;
            }
        }
    }
}

