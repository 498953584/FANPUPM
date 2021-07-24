namespace cn.justwin.epm.datum
{
    using System;

    [Serializable]
    public class DatumModel
    {
        private DateTime _adddate;
        private string _affairclass;
        private string _affairtitle;
        private int? _ca;
        private string _context;
        private int _datumclass;
        private Guid _i_id;
        private Guid _prjcode;
        private string _remark;

        public DateTime AddDate
        {
            get
            {
                return this._adddate;
            }
            set
            {
                this._adddate = value;
            }
        }

        public string AffairClass
        {
            get
            {
                return this._affairclass;
            }
            set
            {
                this._affairclass = value;
            }
        }

        public string AffairTitle
        {
            get
            {
                return this._affairtitle;
            }
            set
            {
                this._affairtitle = value;
            }
        }

        public int? CA
        {
            get
            {
                return this._ca;
            }
            set
            {
                this._ca = value;
            }
        }

        public string Context
        {
            get
            {
                return this._context;
            }
            set
            {
                this._context = value;
            }
        }

        public int DatumClass
        {
            get
            {
                return this._datumclass;
            }
            set
            {
                this._datumclass = value;
            }
        }

        public Guid i_id
        {
            get
            {
                return this._i_id;
            }
            set
            {
                this._i_id = value;
            }
        }

        public Guid PrjCode
        {
            get
            {
                return this._prjcode;
            }
            set
            {
                this._prjcode = value;
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

