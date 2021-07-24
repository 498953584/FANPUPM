namespace cn.justwin.Fund.RequestPayment.Model
{
    using System;

    [Serializable]
    public class RequestPayMainModel
    {
        private int? _flowstate;
        private Guid _prjguid;
        private string _remark;
        private string _rpaycode;
        private DateTime? _rpaydate;
        private Guid _rpaymainid;
        private string _rpayusercode;

        public int? FlowState
        {
            get
            {
                return this._flowstate;
            }
            set
            {
                this._flowstate = value;
            }
        }

        public Guid PrjGuid
        {
            get
            {
                return this._prjguid;
            }
            set
            {
                this._prjguid = value;
            }
        }

        public string ReMark
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

        public string RPayCode
        {
            get
            {
                return this._rpaycode;
            }
            set
            {
                this._rpaycode = value;
            }
        }

        public DateTime? RPayDate
        {
            get
            {
                return this._rpaydate;
            }
            set
            {
                this._rpaydate = value;
            }
        }

        public Guid RPayMainID
        {
            get
            {
                return this._rpaymainid;
            }
            set
            {
                this._rpaymainid = value;
            }
        }

        public string RPayUserCode
        {
            get
            {
                return this._rpayusercode;
            }
            set
            {
                this._rpayusercode = value;
            }
        }
    }
}

