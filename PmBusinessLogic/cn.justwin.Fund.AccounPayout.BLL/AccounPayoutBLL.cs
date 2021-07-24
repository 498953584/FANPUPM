namespace cn.justwin.Fund.AccounPayout.BLL
{
    using cn.justwin.Fund.AccounPayout.Dal;
    using cn.justwin.Fund.AccounPayout.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class AccounPayoutBLL
    {
        private readonly AccounPayoutDAL dal = new AccounPayoutDAL();

        public bool Add(AccounPayoutModel model)
        {
            return this.dal.Add(model);
        }

        public List<AccounPayoutModel> DataTableToList(DataTable dt)
        {
            List<AccounPayoutModel> list = new List<AccounPayoutModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    AccounPayoutModel item = new AccounPayoutModel();
                    if (dt.Rows[i]["PayOutGuid"].ToString() != "")
                    {
                        item.PayOutGuid = new Guid(dt.Rows[i]["PayOutGuid"].ToString());
                    }
                    item.PayOutCode = dt.Rows[i]["PayOutCode"].ToString();
                    item.prjGuid = dt.Rows[i]["prjGuid"].ToString();
                    item.RPGuid = dt.Rows[i]["RPGuid"].ToString();
                    if (dt.Rows[i]["PayOutMoney"].ToString() != "")
                    {
                        item.PayOutMoney = new decimal?(decimal.Parse(dt.Rows[i]["PayOutMoney"].ToString()));
                    }
                    item.PayOutPeople = dt.Rows[i]["PayOutPeople"].ToString();
                    if (dt.Rows[i]["PayOutTime"].ToString() != "")
                    {
                        item.PayOutTime = new DateTime?(DateTime.Parse(dt.Rows[i]["PayOutTime"].ToString()));
                    }
                    item.Handler = dt.Rows[i]["Handler"].ToString();
                    if (dt.Rows[i]["FloeState"].ToString() != "")
                    {
                        item.FloeState = new int?(int.Parse(dt.Rows[i]["FloeState"].ToString()));
                    }
                    item.Remark = dt.Rows[i]["Remark"].ToString();
                    item.UpdateUser = dt.Rows[i]["UpdateUser"].ToString();
                    if (dt.Rows[i]["UpdateTime"].ToString() != "")
                    {
                        item.UpdateTime = new DateTime?(DateTime.Parse(dt.Rows[i]["UpdateTime"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(Guid PayOutGuid)
        {
            return this.dal.Delete(PayOutGuid);
        }

        public bool Exists(string PayOutCode, Guid PayOutGuid)
        {
            return this.dal.Exists(PayOutCode, PayOutGuid);
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

        public AccounPayoutModel GetModel(Guid PayOutGuid)
        {
            return this.dal.GetModel(PayOutGuid);
        }

        public List<AccounPayoutModel> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public decimal getMoneyByPayCode(string PayCode)
        {
            return this.dal.getMoneyByPayCode(PayCode);
        }

        public DataTable GetPaymentMoney(string paymentId)
        {
            return this.dal.GetPaymentMoney(paymentId);
        }

        public DataTable getReportByWhere(string Typestring, string strwhere)
        {
            return this.dal.getReportByWhere(Typestring, strwhere);
        }

        public bool Update(AccounPayoutModel model)
        {
            return this.dal.Update(model);
        }
    }
}

