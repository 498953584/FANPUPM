namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class RealTime
    {
        private decimal _amount;
        private int _depositoryid;
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

        public int DepositoryID
        {
            get
            {
                return this._depositoryid;
            }
            set
            {
                this._depositoryid = value;
            }
        }

        public int MaterialId
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

