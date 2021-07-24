namespace cn.justwin.Fund.PlanSummary.BLL
{
    using cn.justwin.Fund.PlanSummary.DAL;
    using cn.justwin.Fund.PlanSummary.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PlanSummaryMainBLL
    {
        private readonly PlanSummaryMainDAL dal = new PlanSummaryMainDAL();

        public void Add(PlanSummaryMain model)
        {
            this.dal.Add(model);
        }

        public List<PlanSummaryMain> DataTableToList(DataTable dt)
        {
            List<PlanSummaryMain> list = new List<PlanSummaryMain>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    PlanSummaryMain item = new PlanSummaryMain();
                    if (dt.Rows[i]["MID"].ToString() != "")
                    {
                        item.MID = new Guid(dt.Rows[i]["MID"].ToString());
                    }
                    item.PlanName = dt.Rows[i]["PlanName"].ToString();
                    item.Reporter = dt.Rows[i]["Reporter"].ToString();
                    if (dt.Rows[i]["ReportTime"].ToString() != "")
                    {
                        item.ReportTime = new DateTime?(DateTime.Parse(dt.Rows[i]["ReportTime"].ToString()));
                    }
                    if (dt.Rows[i]["FlowState"].ToString() != "")
                    {
                        item.FlowState = new int?(int.Parse(dt.Rows[i]["FlowState"].ToString()));
                    }
                    item.remark = dt.Rows[i]["remark"].ToString();
                    item.PlanType = dt.Rows[i]["PlanType"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(Guid MID)
        {
            return this.dal.Delete(MID);
        }

        public bool Exists(string PlanName, string PlanType)
        {
            return this.dal.Exists(PlanName, PlanType);
        }

        public DataTable GetAllList()
        {
            return this.GetList("");
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public PlanSummaryMain GetModel(Guid MID)
        {
            return this.dal.GetModel(MID);
        }

        public List<PlanSummaryMain> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public bool Update(PlanSummaryMain model)
        {
            return this.dal.Update(model);
        }

        public void UpdateFlow(string PlanGuid)
        {
            this.dal.UpdateFlow(PlanGuid);
        }
    }
}

