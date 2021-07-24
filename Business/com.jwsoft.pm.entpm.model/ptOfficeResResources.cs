namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ptOfficeResResources
    {
        private string _gettype;
        private decimal _planfee;
        private int _recordid;
        private string _rescode;
        private string _resname;
        private string _typecode;
        private string _unit;
        private string _usetype;

        public string GetType
        {
            get
            {
                return this._gettype;
            }
            set
            {
                this._gettype = value;
            }
        }

        public decimal PlanFee
        {
            get
            {
                return this._planfee;
            }
            set
            {
                this._planfee = value;
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

        public string ResCode
        {
            get
            {
                return this._rescode;
            }
            set
            {
                this._rescode = value;
            }
        }

        public string ResName
        {
            get
            {
                return this._resname;
            }
            set
            {
                this._resname = value;
            }
        }

        public string TypeCode
        {
            get
            {
                return this._typecode;
            }
            set
            {
                this._typecode = value;
            }
        }

        public string Unit
        {
            get
            {
                return this._unit;
            }
            set
            {
                this._unit = value;
            }
        }

        public string UseType
        {
            get
            {
                return this._usetype;
            }
            set
            {
                this._usetype = value;
            }
        }
    }
}

