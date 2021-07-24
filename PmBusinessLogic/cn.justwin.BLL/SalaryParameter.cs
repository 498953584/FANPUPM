namespace cn.justwin.BLL
{
    using System;
    using System.Collections.Generic;

    public class SalaryParameter
    {
        private Dictionary<string, string> paras = new Dictionary<string, string>();

        public SalaryParameter()
        {
            this.paras.Add("Days", "实上天数");
            this.paras.Add("ActualDays", "工日");
        }

        public Dictionary<string, string> Paras
        {
            get
            {
                return this.paras;
            }
        }
    }
}

