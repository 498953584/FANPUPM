namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SalaryItemModel
    {
        private string _isvalid;
        private int _itemid;
        private string _itemname;
        private string _itemtype;

        public string IsValid
        {
            get
            {
                return this._isvalid;
            }
            set
            {
                this._isvalid = value;
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

        public string ItemType
        {
            get
            {
                return this._itemtype;
            }
            set
            {
                this._itemtype = value;
            }
        }
    }
}

