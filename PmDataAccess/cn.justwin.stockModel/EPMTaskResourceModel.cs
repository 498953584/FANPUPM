namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class EPMTaskResourceModel
    {
        private decimal? _content;
        private decimal? _fee;
        private decimal? _fee1;
        private int _noteid;
        private Guid _projectcode;
        private decimal? _quantity;
        private string _rationitem;
        private string _resourcecode;
        private string _resourcename;
        private int _resourcestyle;
        private string _resourceunit;
        private string _stepcode;
        private string _taskcode;
        private decimal _unitprice;
        private Guid _versioncode;
        private decimal _wastage;
        private int? _wbstype;

        public decimal? Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        public decimal? Fee
        {
            get
            {
                return this._fee;
            }
            set
            {
                this._fee = value;
            }
        }

        public decimal? Fee1
        {
            get
            {
                return this._fee1;
            }
            set
            {
                this._fee1 = value;
            }
        }

        public int NoteID
        {
            get
            {
                return this._noteid;
            }
            set
            {
                this._noteid = value;
            }
        }

        public Guid ProjectCode
        {
            get
            {
                return this._projectcode;
            }
            set
            {
                this._projectcode = value;
            }
        }

        public decimal? Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                this._quantity = value;
            }
        }

        public string RationItem
        {
            get
            {
                return this._rationitem;
            }
            set
            {
                this._rationitem = value;
            }
        }

        public string ResourceCode
        {
            get
            {
                return this._resourcecode;
            }
            set
            {
                this._resourcecode = value;
            }
        }

        public string ResourceName
        {
            get
            {
                return this._resourcename;
            }
            set
            {
                this._resourcename = value;
            }
        }

        public int ResourceStyle
        {
            get
            {
                return this._resourcestyle;
            }
            set
            {
                this._resourcestyle = value;
            }
        }

        public string ResourceUnit
        {
            get
            {
                return this._resourceunit;
            }
            set
            {
                this._resourceunit = value;
            }
        }

        public string StepCode
        {
            get
            {
                return this._stepcode;
            }
            set
            {
                this._stepcode = value;
            }
        }

        public string TaskCode
        {
            get
            {
                return this._taskcode;
            }
            set
            {
                this._taskcode = value;
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

        public Guid VersionCode
        {
            get
            {
                return this._versioncode;
            }
            set
            {
                this._versioncode = value;
            }
        }

        public decimal Wastage
        {
            get
            {
                return this._wastage;
            }
            set
            {
                this._wastage = value;
            }
        }

        public int? WbsType
        {
            get
            {
                return this._wbstype;
            }
            set
            {
                this._wbstype = value;
            }
        }
    }
}

