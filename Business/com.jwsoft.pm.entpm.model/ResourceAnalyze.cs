namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ResourceAnalyze
    {
        private int _Excursion;
        private int _NoteId;
        private Guid _ProjectCode;
        private string _ResourceCode;
        private int _ResourceStyle;
        private string _TaskCode;
        private decimal _UseQuantity;

        public int Excursion
        {
            get
            {
                return this._Excursion;
            }
            set
            {
                this._Excursion = value;
            }
        }

        public int NoteId
        {
            get
            {
                return this._NoteId;
            }
            set
            {
                this._NoteId = value;
            }
        }

        public Guid ProjectCode
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

        public int ResourceStyle
        {
            get
            {
                return this._ResourceStyle;
            }
            set
            {
                this._ResourceStyle = value;
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

        public decimal UseQuantity
        {
            get
            {
                return this._UseQuantity;
            }
            set
            {
                this._UseQuantity = value;
            }
        }
    }
}

