namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ProviderGoodsModel
    {
        private string _comment;
        private int _materialid;
        private int _providerid;
        private DateTime _recorddate;
        private string _usercode;

        public string Comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
            }
        }

        public int MaterialId
        {
            get
            {
                return this._materialid;
            }
            set
            {
                this._materialid = value;
            }
        }

        public int ProviderId
        {
            get
            {
                return this._providerid;
            }
            set
            {
                this._providerid = value;
            }
        }

        public DateTime RecordDate
        {
            get
            {
                return this._recorddate;
            }
            set
            {
                this._recorddate = value;
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
    }
}

