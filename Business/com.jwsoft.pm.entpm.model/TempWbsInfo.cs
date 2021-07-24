namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class TempWbsInfo
    {
        private decimal _Content;
        private int _NoteID;
        private string _ProjectCode;
        private string _Quantity;
        private string _Remark;
        private string _SumPrice;
        private string _TaskCode;
        private string _TaskName;
        private string _Unit;
        private string _UnitPrice;

        public decimal Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        public int NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                this._NoteID = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
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

        public string SumPrice
        {
            get
            {
                return this._SumPrice;
            }
            set
            {
                this._SumPrice = value;
            }
        }

        public string TaskCode
        {
            get
            {
                return this._TaskCode;
            }
            set
            {
                this._TaskCode = value;
            }
        }

        public string TaskName
        {
            get
            {
                return this._TaskName;
            }
            set
            {
                this._TaskName = value;
            }
        }

        public string Unit
        {
            get
            {
                return this._Unit;
            }
            set
            {
                this._Unit = value;
            }
        }

        public string UnitPrice
        {
            get
            {
                return this._UnitPrice;
            }
            set
            {
                this._UnitPrice = value;
            }
        }
    }
}

