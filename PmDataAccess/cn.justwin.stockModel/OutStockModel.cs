namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class OutStockModel
    {
        private string _corp;
        private decimal _number;
        private string _orcode;
        private string _orsid;
        private string _scode;
        private decimal _sprice;
        private string _taskId;

        public string corp
        {
            get
            {
                return this._corp;
            }
            set
            {
                this._corp = value;
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

        public string orcode
        {
            get
            {
                return this._orcode;
            }
            set
            {
                this._orcode = value;
            }
        }

        public string orsid
        {
            get
            {
                return this._orsid;
            }
            set
            {
                this._orsid = value;
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

        public decimal sprice
        {
            get
            {
                return this._sprice;
            }
            set
            {
                this._sprice = value;
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
    }
}

