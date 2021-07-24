namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class TaskBookRation
    {
        private int _NoteID;
        private decimal _Quantity;
        private string _RationCode;
        private string _Remark;
        private string _TaskBookCode;

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

        public decimal Quantity
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

        public string RationCode
        {
            get
            {
                return this._RationCode;
            }
            set
            {
                this._RationCode = value;
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

        public string TaskBookCode
        {
            get
            {
                return this._TaskBookCode;
            }
            set
            {
                this._TaskBookCode = value;
            }
        }
    }
}

