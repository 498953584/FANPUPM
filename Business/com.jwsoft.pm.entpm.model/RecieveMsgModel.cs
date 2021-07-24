namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class RecieveMsgModel
    {
        private Guid _instancecode;
        private string _lookurl;
        private DateTime _recievedate;
        private string _recieveusercode;
        private decimal? _theorder;
        private string _v_yhdm;

        public Guid InstanceCode
        {
            get
            {
                return this._instancecode;
            }
            set
            {
                this._instancecode = value;
            }
        }

        public string LookUrl
        {
            get
            {
                return this._lookurl;
            }
            set
            {
                this._lookurl = value;
            }
        }

        public DateTime RecieveDate
        {
            get
            {
                return this._recievedate;
            }
            set
            {
                this._recievedate = value;
            }
        }

        public string RecieveUserCode
        {
            get
            {
                return this._recieveusercode;
            }
            set
            {
                this._recieveusercode = value;
            }
        }

        public decimal? TheOrder
        {
            get
            {
                return this._theorder;
            }
            set
            {
                this._theorder = value;
            }
        }

        public string v_yhdm
        {
            get
            {
                return this._v_yhdm;
            }
            set
            {
                this._v_yhdm = value;
            }
        }
    }
}

