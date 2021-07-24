namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAOfficeResJoinDrawItem
    {
        private DateTime _drawdate;
        private int _drawitemid;
        private int _factnumber;
        private decimal _sumfee;
        private string _usercode;

        public DateTime DrawDate
        {
            get
            {
                return this._drawdate;
            }
            set
            {
                this._drawdate = value;
            }
        }

        public int DrawItemID
        {
            get
            {
                return this._drawitemid;
            }
            set
            {
                this._drawitemid = value;
            }
        }

        public int FactNumber
        {
            get
            {
                return this._factnumber;
            }
            set
            {
                this._factnumber = value;
            }
        }

        public decimal SumFee
        {
            get
            {
                return this._sumfee;
            }
            set
            {
                this._sumfee = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this._usercode;
            }
            set
            {
                this._usercode = value;
            }
        }
    }
}

