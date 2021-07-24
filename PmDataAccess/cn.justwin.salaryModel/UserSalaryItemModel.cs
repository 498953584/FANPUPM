namespace cn.justwin.salaryModel
{
    using System;

    [Serializable]
    public class UserSalaryItemModel
    {
        private string _itemcode;
        private decimal? _itemvalue;
        private string _usercode;
        private string _useritemid;

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

        public decimal? ItemValue
        {
            get
            {
                return this._itemvalue;
            }
            set
            {
                this._itemvalue = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this._usercode;
            }
            set
            {
                this._usercode = value;
            }
        }

        public string UserItemID
        {
            get
            {
                return this._useritemid;
            }
            set
            {
                this._useritemid = value;
            }
        }
    }
}

