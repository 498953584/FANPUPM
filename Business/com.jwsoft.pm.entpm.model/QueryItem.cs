namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class QueryItem
    {
        private int _dataType;
        private bool _isEmpty;
        private string _itemName = "";
        private string _itemSign = "";
        private string _itemValue = "";
        private JoinType _join = JoinType.And;
        private string _operator;
        private string _strsql = "";
        private string _tooltip = "";

        public int DataType
        {
            get
            {
                return this._dataType;
            }
            set
            {
                this._dataType = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this._isEmpty;
            }
            set
            {
                this._isEmpty = value;
            }
        }

        public string ItemName
        {
            get
            {
                return this._itemName;
            }
            set
            {
                this._itemName = value;
            }
        }

        public string ItemSign
        {
            get
            {
                return this._itemSign;
            }
            set
            {
                this._itemSign = value;
            }
        }

        public string ItemValue
        {
            get
            {
                return this._itemValue;
            }
            set
            {
                this._itemValue = value;
            }
        }

        public JoinType Join
        {
            get
            {
                return this._join;
            }
            set
            {
                this._join = value;
            }
        }

        public string Operator
        {
            get
            {
                return this._operator;
            }
            set
            {
                this._operator = value;
            }
        }

        public string StrSql
        {
            get
            {
                return this._strsql;
            }
            set
            {
                this._strsql = value;
            }
        }

        public string ToolTip
        {
            get
            {
                return this._tooltip;
            }
            set
            {
                this._tooltip = value;
            }
        }
    }
}

