namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class TreasuryModel : ICloneable
    {
        private bool? _isContrast;
        private string _prjCode;
        private string _ptcode;
        private string _taddress;
        private string _tcode;
        private string _texplain;
        private string _tflag;
        private string _tid;
        private string _tname;

        public TreasuryModel()
        {
        }

        public TreasuryModel(string tid, string tcode, string tname, string ptcode, string tflag, string taddress, string texplain, string prjCode, bool? isContrast)
        {
            this._tid = tid;
            this._tcode = tcode;
            this._tname = tname;
            this._ptcode = ptcode;
            this._tflag = tflag;
            this._taddress = taddress;
            this._texplain = texplain;
            this._prjCode = prjCode;
            this._isContrast = isContrast;
        }

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        public bool? IsContrast
        {
            get
            {
                return this._isContrast;
            }
            set
            {
                this._isContrast = value;
            }
        }

        public string PrjCode
        {
            get
            {
                return this._prjCode;
            }
            set
            {
                this._prjCode = value;
            }
        }

        public string ptcode
        {
            get
            {
                return this._ptcode;
            }
            set
            {
                this._ptcode = value;
            }
        }

        public string taddress
        {
            get
            {
                return this._taddress;
            }
            set
            {
                this._taddress = value;
            }
        }

        public string tcode
        {
            get
            {
                return this._tcode;
            }
            set
            {
                this._tcode = value;
            }
        }

        public string texplain
        {
            get
            {
                return this._texplain;
            }
            set
            {
                this._texplain = value;
            }
        }

        public string tflag
        {
            get
            {
                return this._tflag;
            }
            set
            {
                this._tflag = value;
            }
        }

        public string tid
        {
            get
            {
                return this._tid;
            }
            set
            {
                this._tid = value;
            }
        }

        public string tname
        {
            get
            {
                return this._tname;
            }
            set
            {
                this._tname = value;
            }
        }
    }
}

