namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CriterionModel
    {
        private Guid _CriterionCode = Guid.NewGuid();
        private string _CriterionName = "";
        private string _Flage = "";
        private DateTime _PublishDate = DateTime.Now;
        private string _Publisher = "";
        private string _PublishYhdm = "";
        private string _Remark = "";

        public Guid CriterionCode
        {
            get
            {
                return this._CriterionCode;
            }
            set
            {
                this._CriterionCode = value;
            }
        }

        public string CriterionName
        {
            get
            {
                return this._CriterionName;
            }
            set
            {
                this._CriterionName = value;
            }
        }

        public string Flage
        {
            get
            {
                return this._Flage;
            }
            set
            {
                this._Flage = value;
            }
        }

        public DateTime PublishDate
        {
            get
            {
                return this._PublishDate;
            }
            set
            {
                this._PublishDate = value;
            }
        }

        public string Publisher
        {
            get
            {
                return this._Publisher;
            }
            set
            {
                this._Publisher = value;
            }
        }

        public string PublishYhdm
        {
            get
            {
                return this._PublishYhdm;
            }
            set
            {
                this._PublishYhdm = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }
    }
}

