namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class InputItemInfo : CostSubjectInfo
    {
        private int _ChildMainId;
        private decimal _DevotionMoney;
        private int _MainID;
        private string _Remark;

        public int ChildMainId
        {
            get
            {
                return this._ChildMainId;
            }
            set
            {
                this._ChildMainId = value;
            }
        }

        public decimal DevotionMoney
        {
            get
            {
                return this._DevotionMoney;
            }
            set
            {
                this._DevotionMoney = value;
            }
        }

        public int MainID
        {
            get
            {
                return this._MainID;
            }
            set
            {
                this._MainID = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }
    }
}

