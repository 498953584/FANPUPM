namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class inputReceiptYear
    {
        private string _ExaminePeople;
        private DateTime _ExamineTime = DateTime.Today;
        private int _MainId;
        private string _PlanYear;
        private string _PrjCode;
        private string _Remark;
        private string _SerialNumber;
        private string _TabPeople;
        private DateTime _TabTime = DateTime.Today;

        public string ExaminePeople
        {
            get
            {
                return this._ExaminePeople;
            }
            set
            {
                this._ExaminePeople = value;
            }
        }

        public DateTime ExamineTime
        {
            get
            {
                return this._ExamineTime;
            }
            set
            {
                this._ExamineTime = value;
            }
        }

        public int MainId
        {
            get
            {
                return this._MainId;
            }
            set
            {
                this._MainId = value;
            }
        }

        public string PlanYear
        {
            get
            {
                return this._PlanYear;
            }
            set
            {
                this._PlanYear = value;
            }
        }

        public string PrjCode
        {
            get
            {
                return this._PrjCode;
            }
            set
            {
                this._PrjCode = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string SerialNumber
        {
            get
            {
                return this._SerialNumber;
            }
            set
            {
                this._SerialNumber = value;
            }
        }

        public string TabPeople
        {
            get
            {
                return this._TabPeople;
            }
            set
            {
                this._TabPeople = value;
            }
        }

        public DateTime TabTime
        {
            get
            {
                return this._TabTime;
            }
            set
            {
                this._TabTime = value;
            }
        }
    }
}

