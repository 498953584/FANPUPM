namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SalaryTIModel
    {
        private string _formula;
        private string _isdisplay;
        private string _isedit;
        private int _itemid;
        private string _itemname;
        private int _templetid;
        private int? _theorder;

        public string Formula
        {
            get
            {
                return this._formula;
            }
            set
            {
                this._formula = value;
            }
        }

        public string IsDisplay
        {
            get
            {
                return this._isdisplay;
            }
            set
            {
                this._isdisplay = value;
            }
        }

        public string IsEdit
        {
            get
            {
                return this._isedit;
            }
            set
            {
                this._isedit = value;
            }
        }

        public int ItemID
        {
            get
            {
                return this._itemid;
            }
            set
            {
                this._itemid = value;
            }
        }

        public string ItemName
        {
            get
            {
                return this._itemname;
            }
            set
            {
                this._itemname = value;
            }
        }

        public int TempletID
        {
            get
            {
                return this._templetid;
            }
            set
            {
                this._templetid = value;
            }
        }

        public int? TheOrder
        {
            get
            {
                return this._theorder;
            }
            set
            {
                this._theorder = value;
            }
        }
    }
}

