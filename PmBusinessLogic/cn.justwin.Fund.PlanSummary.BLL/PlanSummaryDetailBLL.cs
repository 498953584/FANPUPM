namespace cn.justwin.Fund.PlanSummary.BLL
{
    using cn.justwin.Fund.PlanSummary.DAL;
    using cn.justwin.Fund.PlanSummary.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PlanSummaryDetailBLL
    {
        private readonly PlanSummaryDetailDAL dal = new PlanSummaryDetailDAL();

        public void Add(PlanSummaryDetail model)
        {
            this.dal.Add(model);
        }

        public List<PlanSummaryDetail> DataTableToList(DataTable dt)
        {
            List<PlanSummaryDetail> list = new List<PlanSummaryDetail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    PlanSummaryDetail item = new PlanSummaryDetail();
                    if (dt.Rows[i]["DID"].ToString() != "")
                    {
                        item.DID = new Guid(dt.Rows[i]["DID"].ToString());
                    }
                    item.MID = dt.Rows[i]["MID"].ToString();
                    item.PlanID = dt.Rows[i]["PlanID"].ToString();
                    item.PrjID = dt.Rows[i]["PrjID"].ToString();
                    if (dt.Rows[i]["LastPlanMoney"].ToString() != "")
                    {
                        item.LastPlanMoney = new decimal?(decimal.Parse(dt.Rows[i]["LastPlanMoney"].ToString()));
                    }
                    if (dt.Rows[i]["LastActualMoney"].ToString() != "")
                    {
                        item.LastActualMoney = new decimal?(decimal.Parse(dt.Rows[i]["LastActualMoney"].ToString()));
                    }
                    if (dt.Rows[i]["CurrPlanMoney"].ToString() != "")
                    {
                        item.CurrPlanMoney = new decimal?(decimal.Parse(dt.Rows[i]["CurrPlanMoney"].ToString()));
                    }
                    item.InputPeople = dt.Rows[i]["InputPeople"].ToString();
                    if (dt.Rows[i]["InputTime"].ToString() != "")
                    {
                        item.InputTime = new DateTime?(DateTime.Parse(dt.Rows[i]["InputTime"].ToString()));
                    }
                    item.Remark = dt.Rows[i]["Remark"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public void Delete(Guid DID)
        {
            this.dal.Delete(DID);
        }

        public bool DeleteByMain(string MID)
        {
            return this.dal.DeleteByMain(MID);
        }

        public bool Exists(Guid DID)
        {
            return this.dal.Exists(DID);
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

        public PlanSummaryDetail GetModel(Guid DID)
        {
            return this.dal.GetModel(DID);
        }

        public List<PlanSummaryDetail> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public DataTable getPlanSummary(string strWhere, string PlanType)
        {
            return this.dal.getPlanSummary(strWhere, PlanType);
        }

        public void Update(PlanSummaryDetail model)
        {
            this.dal.Update(model);
        }
    }
}

