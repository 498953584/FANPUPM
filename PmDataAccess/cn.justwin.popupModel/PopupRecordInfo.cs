namespace cn.justwin.popupModel
{
    using System;

    public class PopupRecordInfo
    {
        private string id;
        private string module;
        private string popupId;
        public DateTime popupTime;
        private string userCode;

        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string Module
        {
            get
            {
                return this.module;
            }
            set
            {
                this.module = value;
            }
        }

        public string PopupId
        {
            get
            {
                return this.popupId;
            }
            set
            {
                this.popupId = value;
            }
        }

        public DateTime PopupTime
        {
            get
            {
                return this.popupTime;
            }
            set
            {
                this.popupTime = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this.userCode;
            }
            set
            {
                this.userCode = value;
            }
        }
    }
}

