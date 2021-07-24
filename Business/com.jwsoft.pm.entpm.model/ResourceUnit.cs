namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class ResourceUnit : IResourceUnit, IUnitType
    {
        private bool _IsDefault;
        private bool _IsValid = true;
        private int _NoteUniqueID;
        private decimal _Quotiety;
        private string _ResourceCode = "";
        private int _UnitID;
        private string _UnitName = "";

        public bool IsDefault
        {
            get
            {
                return this._IsDefault;
            }
            set
            {
                this._IsDefault = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this._IsValid = value;
            }
        }

        public int NoteUniqueID
        {
            get
            {
                return this._NoteUniqueID;
            }
            set
            {
                this._NoteUniqueID = value;
            }
        }

        public decimal Quotiety
        {
            get
            {
                return this._Quotiety;
            }
            set
            {
                this._Quotiety = value;
            }
        }

        public string ResourceCode
        {
            get
            {
                return this._ResourceCode;
            }
            set
            {
                this._ResourceCode = value;
            }
        }

        public int UnitID
        {
            get
            {
                return this._UnitID;
            }
            set
            {
                this._UnitID = value;
            }
        }

        public string UnitName
        {
            get
            {
                return this._UnitName;
            }
            set
            {
                this._UnitName = value;
            }
        }
    }
}

