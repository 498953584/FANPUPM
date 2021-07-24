namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CostSubjectInfo
    {
        private int _FirstNum;
        private bool _IsEdit;
        private bool _IsHaveChild;
        private bool _IsValid = true;
        private int _SecNum;
        private int _SubjectID;
        private string _SubjectName;

        public int FirstNum
        {
            get
            {
                return this._FirstNum;
            }
            set
            {
                this._FirstNum = value;
            }
        }

        public bool IsEdit
        {
            get
            {
                return this._IsEdit;
            }
            set
            {
                this._IsEdit = value;
            }
        }

        public bool IsHaveChild
        {
            get
            {
                return this._IsHaveChild;
            }
            set
            {
                this._IsHaveChild = value;
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

        public int SecNum
        {
            get
            {
                return this._SecNum;
            }
            set
            {
                this._SecNum = value;
            }
        }

        public int SubjectID
        {
            get
            {
                return this._SubjectID;
            }
            set
            {
                this._SubjectID = value;
            }
        }

        public string SubjectName
        {
            get
            {
                return this._SubjectName;
            }
            set
            {
                this._SubjectName = value;
            }
        }
    }
}

