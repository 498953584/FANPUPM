namespace cn.justwin.XPMBasicContactCorp
{
    using System;
    using System.Collections.Generic;

    internal class XPMBasicContactCorpBll
    {
        private BasicContactCorp bacc = new BasicContactCorp();

        public int Add(BasicContactCorpModel model)
        {
            return this.bacc.Add(model);
        }

        public int Delete(int COPID)
        {
            return this.bacc.Delete(COPID);
        }

        public int Delete(int COPID, string copname)
        {
            return this.bacc.Delete(COPID, copname);
        }

        public List<BasicContactCorpModel> GetListArray(string strWhere)
        {
            return this.bacc.GetListArray(strWhere);
        }

        public BasicContactCorpModel GetModel(int CorpID, string CorpName)
        {
            return this.bacc.GetModel(CorpID, CorpName);
        }

        public int Update(BasicContactCorpModel model)
        {
            return this.bacc.Update(model);
        }
    }
}

