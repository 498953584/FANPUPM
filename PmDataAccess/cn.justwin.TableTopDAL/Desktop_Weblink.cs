namespace cn.justwin.TableTopDAL
{
    using System;

    public class Desktop_Weblink
    {
        private DateTime _addtime;
        private int _linkid;
        private int _orderid;
        private string _remark;
        private string _usercode;
        private string _webaddr;
        private string _webname;

        public DateTime AddTime
        {
            get
            {
                return this._addtime;
            }
            set
            {
                this._addtime = value;
            }
        }

        public int LinkID
        {
            get
            {
                return this._linkid;
            }
            set
            {
                this._linkid = value;
            }
        }

        public int orderId
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

        public string WebAddr
        {
            get
            {
                return this._webaddr;
            }
            set
            {
                this._webaddr = value;
            }
        }

        public string WebName
        {
            get
            {
                return this._webname;
            }
            set
            {
                this._webname = value;
            }
        }
    }
}

