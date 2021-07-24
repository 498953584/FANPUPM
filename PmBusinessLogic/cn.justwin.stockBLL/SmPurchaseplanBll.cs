namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;

    public class SmPurchaseplanBll
    {
        private SmPurchaseplan smPurchaseplan = new SmPurchaseplan();

        public int Add(SqlTransaction trans, SmPurchaseplanModel model)
        {
            return this.smPurchaseplan.Add(trans, model);
        }

        public int Delete(SqlTransaction trans, string ppcode)
        {
            return this.smPurchaseplan.Delete(trans, ppcode);
        }

        public List<SmPurchaseplanModel> GetList(DateTime startDate, DateTime endDate, string ppcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" where flowstate = 1 ");
            builder.AppendFormat(" and intime between '{0}' and '{1}' ", startDate.ToShortDateString(), endDate.ToShortDateString());
            if (!string.IsNullOrEmpty(ppcode))
            {
                builder.AppendFormat(" and ppcode like '%{0}%' ", ppcode);
            }
            return this.smPurchaseplan.GetListArray(builder.ToString());
        }

        public List<SmPurchaseplanModel> GetListArray(string strWhere)
        {
            return this.smPurchaseplan.GetListArray(strWhere);
        }

        public List<SmPurchaseplanModel> GetListByTimeAndNum(string startTime, string endTime, string ppcode, string person, string project)
        {
            return this.smPurchaseplan.GetListByTimeAndNum(startTime, endTime, ppcode, person, project);
        }

        public SmPurchaseplanModel GetModel(string ppcode)
        {
            return this.smPurchaseplan.GetModel(ppcode);
        }

        public SmPurchaseplanModel GetModelByCid(string cid)
        {
            return this.smPurchaseplan.GetModelByCid(cid);
        }

        public int Update(SmPurchaseplanModel model)
        {
            return this.smPurchaseplan.Update(null, model);
        }

        public int Update(SqlTransaction trans, SmPurchaseplanModel model)
        {
            return this.smPurchaseplan.Update(trans, model);
        }

        public void UpdateAcceptState(string ppcode)
        {
            SmPurchaseplanModel model = this.GetModel(ppcode);
            model.acceptstate++;
            this.Update(model);
        }

        public void UpdateAcceptState(SqlTransaction trans, string ppcode)
        {
            SmPurchaseplanModel model = this.GetModel(ppcode);
            model.acceptstate++;
            this.Update(trans, model);
        }
    }
}

