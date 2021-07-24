namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class DocumentClass
    {
        private string _classdiscrip;
        private string _classid;
        private string _classmaster;
        private string _classname;
        private int _classorder;
        private decimal _compointvalue_pusher;
        private decimal _compointvalue_reader;
        private decimal _downloadadd_pusher;
        private decimal _downloadminus_reader;
        private string _parentclassid;
        private decimal _pushpointvalue;

        public string ClassDiscrip
        {
            get
            {
                return this._classdiscrip;
            }
            set
            {
                this._classdiscrip = value;
            }
        }

        public string ClassID
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public string ClassMaster
        {
            get
            {
                return this._classmaster;
            }
            set
            {
                this._classmaster = value;
            }
        }

        public string ClassName
        {
            get
            {
                return this._classname;
            }
            set
            {
                this._classname = value;
            }
        }

        public int ClassOrder
        {
            get
            {
                return this._classorder;
            }
            set
            {
                this._classorder = value;
            }
        }

        public decimal ComPointValue_Pusher
        {
            get
            {
                return this._compointvalue_pusher;
            }
            set
            {
                this._compointvalue_pusher = value;
            }
        }

        public decimal ComPointValue_Reader
        {
            get
            {
                return this._compointvalue_reader;
            }
            set
            {
                this._compointvalue_reader = value;
            }
        }

        public decimal DownLoadAdd_Pusher
        {
            get
            {
                return this._downloadadd_pusher;
            }
            set
            {
                this._downloadadd_pusher = value;
            }
        }

        public decimal DownLoadMinus_Reader
        {
            get
            {
                return this._downloadminus_reader;
            }
            set
            {
                this._downloadminus_reader = value;
            }
        }

        public string ParentClassID
        {
            get
            {
                return this._parentclassid;
            }
            set
            {
                this._parentclassid = value;
            }
        }

        public decimal PushPointValue
        {
            get
            {
                return this._pushpointvalue;
            }
            set
            {
                this._pushpointvalue = value;
            }
        }
    }
}

