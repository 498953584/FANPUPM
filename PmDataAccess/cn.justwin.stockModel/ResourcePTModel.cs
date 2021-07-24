namespace cn.justwin.stockModel
{
    using System;

    public class ResourcePTModel
    {
        private int _isDefault;
        private string _rptcode;
        private string _rptexplain;
        private string _rptid;
        private string _rptIsShow;
        private string _rptname;

        public int IsDefault
        {
            get
            {
                return this._isDefault;
            }
            set
            {
                this._isDefault = value;
            }
        }

        public string rptCode
        {
            get
            {
                return this._rptcode;
            }
            set
            {
                this._rptcode = value;
            }
        }

        public string rptExplain
        {
            get
            {
                return this._rptexplain;
            }
            set
            {
                this._rptexplain = value;
            }
        }

        public string rptId
        {
            get
            {
                return this._rptid;
            }
            set
            {
                this._rptid = value;
            }
        }

        public string rptIsShow
        {
            get
            {
                return this._rptIsShow;
            }
            set
            {
                this._rptIsShow = value;
            }
        }

        public string rptName
        {
            get
            {
                return this._rptname;
            }
            set
            {
                this._rptname = value;
            }
        }
    }
}

