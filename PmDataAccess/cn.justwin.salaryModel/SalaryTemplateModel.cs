namespace cn.justwin.salaryModel
{
    using System;

    [Serializable]
    public class SalaryTemplateModel
    {
        private DateTime? _beginDate;
        private bool _enabled;
        private DateTime? _endDate;
        private string _formula;
        private string _templateID;
        private string _templateName;
        private string _templateType;

        public SalaryTemplateModel()
        {
        }

        public SalaryTemplateModel(string templateID, string templateName, string formula, bool enabled, DateTime? beginDate, DateTime? endDate, string templateType)
        {
            this._templateID = templateID;
            this._templateName = templateName;
            this._formula = formula;
            this._enabled = enabled;
            this._beginDate = beginDate;
            this._endDate = endDate;
            this._templateType = templateType;
        }

        public DateTime? BeginDate
        {
            get
            {
                return this._beginDate;
            }
            set
            {
                this._beginDate = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {
                this._enabled = value;
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return this._endDate;
            }
            set
            {
                this._endDate = value;
            }
        }

        public string Formula
        {
            get
            {
                return this._formula;
            }
            set
            {
                this._formula = value;
            }
        }

        public string TemplateID
        {
            get
            {
                return this._templateID;
            }
            set
            {
                this._templateID = value;
            }
        }

        public string TemplateName
        {
            get
            {
                return this._templateName;
            }
            set
            {
                this._templateName = value;
            }
        }

        public string TemplateType
        {
            get
            {
                return this._templateType;
            }
            set
            {
                this._templateType = value;
            }
        }
    }
}

