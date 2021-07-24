namespace cn.justwin.TableTopDAL
{
    using System;

    public class DeskTopDalModel
    {
        private string _mnewname;
        private string _modelid;
        private int? _orderid;
        private string _usercode;

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

