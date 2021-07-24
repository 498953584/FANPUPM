namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SalaryPModel
    {
        private string _parametercode;
        private string _parametername;
        private int? _parametervalue;
        private string _remark;

        public string ParameterCode
        {
            get
            {
                return this._parametercode;
            }
            set
            {
                this._parametercode = value;
            }
        }

        public string ParameterName
        {
            get
            {
                return this._parametername;
            }
            set
            {
                this._parametername = value;
            }
        }

        public int? ParameterValue
        {
            get
            {
                return this._parametervalue;
            }
            set
            {
                this._parametervalue = value;
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

