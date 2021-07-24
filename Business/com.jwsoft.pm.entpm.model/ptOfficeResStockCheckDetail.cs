namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ptOfficeResStockCheckDetail
    {
        private decimal _accountnumber;
        private int _checkdetailid;
        private decimal _checknumber;
        private int _checkrecordid;
        private int _materialid;
        private string _remark;

        public decimal AccountNumber
        {
            get
            {
                return this._accountnumber;
            }
            set
            {
                this._accountnumber = value;
            }
        }

        public int CheckDetailID
        {
            get
            {
                return this._checkdetailid;
            }
            set
            {
                this._checkdetailid = value;
            }
        }

        public decimal CheckNumber
        {
            get
            {
                return this._checknumber;
            }
            set
            {
                this._checknumber = value;
            }
        }

        public int CheckRecordID
        {
            get
            {
                return this._checkrecordid;
            }
            set
            {
                this._checkrecordid = value;
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
    }
}

