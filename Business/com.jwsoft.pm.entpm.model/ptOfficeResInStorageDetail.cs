namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ptOfficeResInStorageDetail
    {
        private decimal _amount;
        private string _handleman;
        private int _instorageid;
        private decimal _instorageprice;
        private int _materialid;
        private int _recordid;

        public decimal Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }

        public string HandleMan
        {
            get
            {
                return this._handleman;
            }
            set
            {
                this._handleman = value;
            }
        }

        public int InStorageID
        {
            get
            {
                return this._instorageid;
            }
            set
            {
                this._instorageid = value;
            }
        }

        public decimal InStoragePrice
        {
            get
            {
                return this._instorageprice;
            }
            set
            {
                this._instorageprice = value;
            }
        }

        public int MaterialID
        {
            get
            {
                return this._materialid;
            }
            set
            {
                this._materialid = value;
            }
        }

        public int RecordID
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
    }
}

