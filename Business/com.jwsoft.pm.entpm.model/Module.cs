namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class Module
    {
        private bool _BottomNode;
        private string _ChildNum;
        private string _FilePath;
        private string _ID;
        private string _ImagePath;
        private int _Level;
        private string _ModuleCode;
        private string _ModuleName;
        private string _SerialNo;

        public bool BottomNode
        {
            get
            {
                return this._BottomNode;
            }
            set
            {
                this._BottomNode = value;
            }
        }

        public string ChildNum
        {
            get
            {
                return this._ChildNum;
            }
            set
            {
                this._ChildNum = value;
            }
        }

        public string FilePath
        {
            get
            {
                return this._FilePath;
            }
            set
            {
                this._FilePath = value;
            }
        }

        public string ID
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

        public string ImagePath
        {
            get
            {
                return this._ImagePath;
            }
            set
            {
                this._ImagePath = value;
            }
        }

        public int Level
        {
            get
            {
                return this._Level;
            }
            set
            {
                this._Level = value;
            }
        }

        public string ModuleCode
        {
            get
            {
                return this._ModuleCode;
            }
            set
            {
                this._ModuleCode = value;
            }
        }

        public string ModuleName
        {
            get
            {
                return this._ModuleName;
            }
            set
            {
                this._ModuleName = value;
            }
        }

        public string SerialNo
        {
            get
            {
                return this._SerialNo;
            }
            set
            {
                this._SerialNo = value;
            }
        }
    }
}

