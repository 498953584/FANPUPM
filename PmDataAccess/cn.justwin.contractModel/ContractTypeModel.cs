namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class ContractTypeModel
    {
        private string _cbscode;
        private DateTime? _inputdate;
        private string _inputperson;
        private string _isValid;
        private string _notes;
        private string _typecode;
        private string _typeid;
        private string _typename;
        private string _typeShort;
        private string _usercodes;

        public string CBSCode
        {
            get
            {
                return this._cbscode;
            }
            set
            {
                this._cbscode = value;
            }
        }

        public DateTime? InputDate
        {
            get
            {
                return this._inputdate;
            }
            set
            {
                this._inputdate = value;
            }
        }

        public string InputPerson
        {
            get
            {
                return this._inputperson;
            }
            set
            {
                this._inputperson = value;
            }
        }

        public string IsValid
        {
            get
            {
                return this._isValid;
            }
            set
            {
                this._isValid = value;
            }
        }

        public string Notes
        {
            get
            {
                return this._notes;
            }
            set
            {
                this._notes = value;
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

        public string TypeID
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this._typename;
            }
            set
            {
                this._typename = value;
            }
        }

        public string TypeShort
        {
            get
            {
                return this._typeShort;
            }
            set
            {
                this._typeShort = value;
            }
        }

        public string UserCodes
        {
            get
            {
                return this._usercodes;
            }
            set
            {
                this._usercodes = value;
            }
        }
    }
}

