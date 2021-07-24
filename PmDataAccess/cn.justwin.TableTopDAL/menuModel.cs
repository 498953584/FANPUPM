namespace cn.justwin.TableTopDAL
{
    using System;

    public class menuModel
    {
        private string _mnewname;
        private string _modelid;
        private int? _orderid;
        private string _usercode;
        private DateTime addTime;
        private string sequence;

        public DateTime AddTime
        {
            get
            {
                return this.addTime;
            }
            set
            {
                this.addTime = value;
            }
        }

        public string MNewName
        {
            get
            {
                return this._mnewname;
            }
            set
            {
                this._mnewname = value;
            }
        }

        public string ModelId
        {
            get
            {
                return this._modelid;
            }
            set
            {
                this._modelid = value;
            }
        }

        public int? orderId
        {
            get
            {
                return this._orderid;
            }
            set
            {
                this._orderid = value;
            }
        }

        public string Sequence
        {
            get
            {
                return this.sequence;
            }
            set
            {
                this.sequence = value;
            }
        }

        public string userCode
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

