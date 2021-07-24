namespace cn.justwin.salaryModel
{
    using System;

    public class SamTemplateItemModel
    {
        private string _itemcode;
        private string _itemid;
        private string _itemname;
        private string _remark;

        public string ItemCode
        {
            get
            {
                return this._itemcode;
            }
            set
            {
                this._itemcode = value;
            }
        }

        public string ItemID
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

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }
    }
}

