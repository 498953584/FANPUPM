namespace cn.justwin.stockBLL.Fund.FundConfig
{
    using cn.justwin.Fund.FundConfig;
    using System;
    using System.Collections.Generic;

    public class configBll
    {
        private configDAL config = new configDAL();

        public List<configModel> GetList(string strWhere)
        {
            return this.config.GetList(strWhere);
        }

        public void Update(Dictionary<string, string> parameters)
        {
            this.config.Update(parameters);
        }
    }
}

