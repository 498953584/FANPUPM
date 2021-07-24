namespace cn.justwin.stockDAL
{
    using System;

    public class AllocationStockModel
    {
        private string _acode;
        private string _asid;
        private string _corp;
        private decimal _number;
        private string _scode;
        private decimal _sprice;

        public override string ToString()
        {
            return string.Concat(new object[] { Convert.ToString(this.Sprice), this.Number, this.Corp, this.SCode, this.ACode });
        }

        public string ACode
        {
            get
            {
                return this._acode;
            }
            set
            {
                this._acode = value;
            }
        }

        public string Asid
        {
            get
            {
                return this._asid;
            }
            set
            {
                this._asid = value;
            }
        }

        public string Corp
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

        public decimal Number
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

        public string SCode
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

        public decimal Sprice
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
    }
}

