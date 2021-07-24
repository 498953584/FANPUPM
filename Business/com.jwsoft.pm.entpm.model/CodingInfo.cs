namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class CodingInfo
    {
        private int _ChildNumber;
        private int _CodeID;
        private string _CodeName = "";
        private int _I_xh;
        private bool _IsDefault;
        private bool _IsFixed;
        private bool _IsValid = true;
        private bool _IsVisible = true;
        private int _NoteID;
        private string _Owner;
        private int _ParentCodeID;
        private string _ParentCodeList = "";
        private int _TypeID;

        public int ChildNumber
        {
            get
            {
                return this._ChildNumber;
            }
            set
            {
                this._ChildNumber = value;
            }
        }

        public int CodeID
        {
            get
            {
                return this._CodeID;
            }
            set
            {
                this._CodeID = value;
            }
        }

        public string CodeName
        {
            get
            {
                return this._CodeName;
            }
            set
            {
                this._CodeName = value;
            }
        }

        public int I_xh
        {
            get
            {
                return this._I_xh;
            }
            set
            {
                this._I_xh = value;
            }
        }

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

        public bool IsFixed
        {
            get
            {
                return this._IsFixed;
            }
            set
            {
                this._IsFixed = value;
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

        public bool IsVisible
        {
            get
            {
                return this._IsVisible;
            }
            set
            {
                this._IsVisible = value;
            }
        }

        public int NodeID
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

        public string Owner
        {
            get
            {
                return this._Owner;
            }
            set
            {
                this._Owner = value;
            }
        }

        public int ParentCodeID
        {
            get
            {
                return this._ParentCodeID;
            }
            set
            {
                this._ParentCodeID = value;
            }
        }

        public string ParentCodeList
        {
            get
            {
                return this._ParentCodeList;
            }
            set
            {
                this._ParentCodeList = value;
            }
        }

        public string SignCode2 { get; set; }

        public int TypeID
        {
            get
            {
                return this._TypeID;
            }
            set
            {
                this._TypeID = value;
            }
        }
    }
}

