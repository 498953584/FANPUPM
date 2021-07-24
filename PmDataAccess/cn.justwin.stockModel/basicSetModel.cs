namespace cn.justwin.stockModel
{
    using System;

    public class basicSetModel
    {
        private string _paraid;
        private string _paraname;
        private string _paravalue;
        private string _remark;

        public string Paraid
        {
            get
            {
                return this._paraid;
            }
            set
            {
                this._paraid = value;
            }
        }

        public string Paraname
        {
            get
            {
                return this._paraname;
            }
            set
            {
                this._paraname = value;
            }
        }

        public string Paravalue
        {
            get
            {
                return this._paravalue;
            }
            set
            {
                this._paravalue = value;
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

