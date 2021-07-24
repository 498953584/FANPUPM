namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;

    public class PTDbsjBll
    {
        private PTDbsj dal = new PTDbsj();

        public int Add(PTDbsjModel model)
        {
            return this.dal.Add(model);
        }

        public int Delete(int I_DBSJ_ID)
        {
            return this.dal.Delete(I_DBSJ_ID);
        }

        public void DelPastDueData(string xgid)
        {
            this.dal.DelPastDueData(xgid);
        }

        public bool Exists(string guid)
        {
            return this.dal.Exists(guid);
        }

        public List<PTDbsjModel> GetListArray(string strWhere)
        {
            return this.dal.GetListArray(strWhere);
        }

        public PTDbsjModel GetModel(int I_DBSJ_ID)
        {
            return this.dal.GetModel(I_DBSJ_ID);
        }

        public PTDbsjModel GetModelByGID(string I_XGID)
        {
            return this.dal.GetModelByGID(I_XGID);
        }

        public int Update(PTDbsjModel model)
        {
            return this.dal.Update(model);
        }
    }
}

