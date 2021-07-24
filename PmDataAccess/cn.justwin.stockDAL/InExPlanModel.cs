namespace cn.justwin.stockDAL
{
    using System;

    public class InExPlanModel
    {
        private string _id;
        private DateTime? _iepdate;
        private string _iepname;
        private string _iepnum;
        private int? _ieptype;
        private string _prjnum;
        private int? _state;

        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public DateTime? IEPdate
        {
            get
            {
                return this._iepdate;
            }
            set
            {
                this._iepdate = value;
            }
        }

        public string IEPname
        {
            get
            {
                return this._iepname;
            }
            set
            {
                this._iepname = value;
            }
        }

        public string IEPNum
        {
            get
            {
                return this._iepnum;
            }
            set
            {
                this._iepnum = value;
            }
        }

        public int? IEPtype
        {
            get
            {
                return this._ieptype;
            }
            set
            {
                this._ieptype = value;
            }
        }

        public string prjNum
        {
            get
            {
                return this._prjnum;
            }
            set
            {
                this._prjnum = value;
            }
        }

        public int? state
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
            }
        }
    }
}

