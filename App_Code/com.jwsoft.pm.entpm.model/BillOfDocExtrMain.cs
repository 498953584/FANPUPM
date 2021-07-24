namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class BillOfDocExtrMain
    {
        private DateTime _billofdocextrdate;
        private int _billofdocextrid;
        private decimal _billofdocextrmoney;
        private string _billofdocextrnumber;
        private int _depositoryid;
        private string _pulldowndept;
        private string _pulldownperson;
        private DateTime _recorddate;
        private string _remark;
        private string _transactperson;
        private string _transactstate;
        private string _usercode;

        public DateTime BillOfDocExtrDate
        {
            get
            {
                return this._billofdocextrdate;
            }
            set
            {
                this._billofdocextrdate = value;
            }
        }

        public int BillOfDocExtrID
        {
            get
            {
                return this._billofdocextrid;
            }
            set
            {
                this._billofdocextrid = value;
            }
        }

        public decimal BillOfDocExtrMoney
        {
            get
            {
                return this._billofdocextrmoney;
            }
            set
            {
                this._billofdocextrmoney = value;
            }
        }

        public string BillOfDocExtrNumber
        {
            get
            {
                return this._billofdocextrnumber;
            }
            set
            {
                this._billofdocextrnumber = value;
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

        public string PullDownDept
        {
            get
            {
                return this._pulldowndept;
            }
            set
            {
                this._pulldowndept = value;
            }
        }

        public string PullDownPerson
        {
            get
            {
                return this._pulldownperson;
            }
            set
            {
                this._pulldownperson = value;
            }
        }

        public DateTime RecordDate
        {
            get
            {
                return this._recorddate;
            }
            set
            {
                this._recorddate = value;
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

        public string TransactPerson
        {
            get
            {
                return this._transactperson;
            }
            set
            {
                this._transactperson = value;
            }
        }

        public string TransactState
        {
            get
            {
                return this._transactstate;
            }
            set
            {
                this._transactstate = value;
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

