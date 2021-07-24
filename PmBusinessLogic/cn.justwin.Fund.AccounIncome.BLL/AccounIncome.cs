namespace cn.justwin.Fund.AccounIncome.BLL
{
    using cn.justwin.Fund.AccounIncome.Dal;
    using cn.justwin.Fund.AccounIncome.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class AccounIncome
    {
        private readonly AccounIncomeServer dal = new AccounIncomeServer();

        public bool Add(AccounIncomeModel model)
        {
            return this.dal.Add(model);
        }

        public List<AccounIncomeModel> DataTableToList(DataTable dt)
        {
            List<AccounIncomeModel> list = new List<AccounIncomeModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    AccounIncomeModel item = new AccounIncomeModel();
                    if (dt.Rows[i]["InUid"].ToString() != "")
                    {
                        item.InUid = new Guid(dt.Rows[i]["InUid"].ToString());
                    }
                    item.PrjGuid = dt.Rows[i]["PrjGuid"].ToString();
                    item.InCode = dt.Rows[i]["InCode"].ToString();
                    item.PlanUid = dt.Rows[i]["PlanUid"].ToString();
                    item.ContractID = dt.Rows[i]["ContractID"].ToString();
                    item.Subject = dt.Rows[i]["Subject"].ToString();
                    if (dt.Rows[i]["GetDate"].ToString() != "")
                    {
                        item.GetDate = new DateTime?(DateTime.Parse(dt.Rows[i]["GetDate"].ToString()));
                    }
                    if (dt.Rows[i]["GetMoney"].ToString() != "")
                    {
                        item.GetMoney = new decimal?(decimal.Parse(dt.Rows[i]["GetMoney"].ToString()));
                    }
                    if (dt.Rows[i]["InMoney"].ToString() != "")
                    {
                        item.InMoney = new decimal?(decimal.Parse(dt.Rows[i]["InMoney"].ToString()));
                    }
                    item.InPeople = dt.Rows[i]["InPeople"].ToString();
                    if (dt.Rows[i]["InDate"].ToString() != "")
                    {
                        item.InDate = new DateTime?(DateTime.Parse(dt.Rows[i]["InDate"].ToString()));
                    }
                    item.Remark = dt.Rows[i]["Remark"].ToString();
                    if (dt.Rows[i]["FlowState"].ToString() != "")
                    {
                        item.FlowState = new int?(int.Parse(dt.Rows[i]["FlowState"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public void Delete(Guid InUid)
        {
            this.dal.Delete(InUid);
        }

        public bool Exists(string code)
        {
            return this.dal.Exists(code);
        }

        public DataTable GetAllList()
        {
            return this.GetList("");
        }

        public DataTable GetAllNews(string PayGuid)
        {
            return this.dal.GetAllNews(PayGuid);
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public AccounIncomeModel GetModel(Guid InUid)
        {
            return this.dal.GetModel(InUid);
        }

        public List<AccounIncomeModel> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public bool Update(AccounIncomeModel model)
        {
            return this.dal.Update(model);
        }
    }
}

