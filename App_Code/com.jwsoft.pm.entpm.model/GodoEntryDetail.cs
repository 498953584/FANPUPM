namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class GodoEntryDetail
    {
        private string _entrytype;
        private int _godoentrydetailid;
        private int _godoentryid;
        private int _materialid;
        private decimal _planprice;
        private int _projectId;
        private string _projectname;
        private decimal _scalar;
        private int _taskId;
        private string _taskname;
        private decimal _unitprice;

        public string EntryType
        {
            get
            {
                return this._entrytype;
            }
            set
            {
                this._entrytype = value;
            }
        }

        public int GodoEntryDetailID
        {
            get
            {
                return this._godoentrydetailid;
            }
            set
            {
                this._godoentrydetailid = value;
            }
        }

        public int GodoEntryID
        {
            get
            {
                return this._godoentryid;
            }
            set
            {
                this._godoentryid = value;
            }
        }

        public int MaterialId
        {
            get
            {
                return this._materialid;
            }
            set
            {
                this._materialid = value;
            }
        }

        public decimal PlanPrice
        {
            get
            {
                return this._planprice;
            }
            set
            {
                this._planprice = value;
            }
        }

        public int ProjectId
        {
            get
            {
                return this._projectId;
            }
            set
            {
                this._projectId = value;
            }
        }

        public string ProjectName
        {
            get
            {
                return this._projectname;
            }
            set
            {
                this._projectname = value;
            }
        }

        public decimal Scalar
        {
            get
            {
                return this._scalar;
            }
            set
            {
                this._scalar = value;
            }
        }

        public int TaskId
        {
            get
            {
                return this._taskId;
            }
            set
            {
                this._taskId = value;
            }
        }

        public string TaskName
        {
            get
            {
                return this._taskname;
            }
            set
            {
                this._taskname = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return this._unitprice;
            }
            set
            {
                this._unitprice = value;
            }
        }
    }
}

