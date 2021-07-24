namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;

    public class Config
    {
        private readonly cn.justwin.contractDAL.Config dal = new cn.justwin.contractDAL.Config();

        public void Add(ConfigModel model)
        {
            this.dal.Add(model);
        }

        public void Delete(string ConfigID)
        {
            this.dal.Delete(ConfigID);
        }

        public List<ConfigModel> GetAllList()
        {
            return this.GetList("");
        }

        public List<ConfigModel> GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public ConfigModel GetModel(string ConfigID)
        {
            return this.dal.GetModel(ConfigID);
        }

        internal void Update(Dictionary<string, string> parameters)
        {
            this.dal.Update(parameters);
        }

        internal void Update(string name, string value)
        {
            this.dal.Update(name, value, null);
        }
    }
}

