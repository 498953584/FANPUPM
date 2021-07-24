namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class DeptInfo
    {
        private int _childCount;
        private string _deptCode;
        private string _deptFullName;
        private string _deptIdentify;
        private string _deptLevel;
        private string _deptName;
        private int _deptSerial;
        private string _deptShortName;
        private string _isValid;
        private string _parentCode;

        public int ChildCount
        {
            get
            {
                return this._childCount;
            }
            set
            {
                this._childCount = value;
            }
        }

        public string DeptCode
        {
            get
            {
                return this._deptCode;
            }
            set
            {
                this._deptCode = value;
            }
        }

        public string DeptFullName
        {
            get
            {
                return this._deptFullName;
            }
            set
            {
                this._deptFullName = value;
            }
        }

        public string DeptIdentify
        {
            get
            {
                return this._deptIdentify;
            }
            set
            {
                this._deptIdentify = value;
            }
        }

        public string DeptLevel
        {
            get
            {
                return this._deptLevel;
            }
            set
            {
                this._deptLevel = value;
            }
        }

        public string DeptName
        {
            get
            {
                return this._deptName;
            }
            set
            {
                this._deptName = value;
            }
        }

        public int DeptSerial
        {
            get
            {
                return this._deptSerial;
            }
            set
            {
                this._deptSerial = value;
            }
        }

        public string DeptShortName
        {
            get
            {
                return this._deptShortName;
            }
            set
            {
                this._deptShortName = value;
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

        public string ParentCode
        {
            get
            {
                return this._parentCode;
            }
            set
            {
                this._parentCode = value;
            }
        }
    }
}

