namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CodeLibraryInfo
    {
        private int _Flag;
        private int _ID;
        private string _NodeName;
        private string _NodeValue;
        private int _PNode;

        public int Flag
        {
            get
            {
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        public string NodeName
        {
            get
            {
                return this._NodeName;
            }
            set
            {
                this._NodeName = value;
            }
        }

        public string NodeValue
        {
            get
            {
                return this._NodeValue;
            }
            set
            {
                this._NodeValue = value;
            }
        }

        public int PNode
        {
            get
            {
                return this._PNode;
            }
            set
            {
                this._PNode = value;
            }
        }
    }
}

