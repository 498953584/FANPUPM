namespace cn.justwin.Fund.RequestPayment.BLL
{
    using cn.justwin.Fund.RequestPayment.DAL;
    using cn.justwin.Fund.RequestPayment.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class RequestPayMain
    {
        private readonly RequestPayMainService dal = new RequestPayMainService();

        public void Add(RequestPayMainModel model)
        {
            this.dal.Add(model);
        }

        public List<RequestPayMainModel> DataTableToList(DataTable dt)
        {
            List<RequestPayMainModel> list = new List<RequestPayMainModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    RequestPayMainModel item = new RequestPayMainModel();
                    if (dt.Rows[i]["RPayMainID"].ToString() != "")
                    {
                        item.RPayMainID = new Guid(dt.Rows[i]["RPayMainID"].ToString());
                    }
                    item.RPayCode = dt.Rows[i]["RPayCode"].ToString();
                    if (dt.Rows[i]["PrjGuid"].ToString() != "")
                    {
                        item.PrjGuid = new Guid(dt.Rows[i]["PrjGuid"].ToString());
                    }
                    if (dt.Rows[i]["FlowState"].ToString() != "")
                    {
                        item.FlowState = new int?(int.Parse(dt.Rows[i]["FlowState"].ToString()));
                    }
                    item.RPayUserCode = dt.Rows[i]["RPayUserCode"].ToString();
                    if (dt.Rows[i]["RPayDate"].ToString() != "")
                    {
                        item.RPayDate = new DateTime?(DateTime.Parse(dt.Rows[i]["RPayDate"].ToString()));
                    }
                    item.ReMark = dt.Rows[i]["ReMark"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(Guid RPayMainID)
        {
            return this.dal.Delete(RPayMainID);
        }

        public bool Exists(string RPayCode)
        {
            return this.dal.Exists(RPayCode);
        }

        public DataTable GetList(string strWhere, string filedOrder)
        {
            return this.dal.GetList(strWhere, filedOrder);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public RequestPayMainModel GetModel(Guid RPayMainID)
        {
            return this.dal.GetModel(RPayMainID);
        }

        public List<RequestPayMainModel> GetModelList(string strWhere, string filedOrder)
        {
            DataTable list = this.dal.GetList(strWhere, filedOrder);
            return this.DataTableToList(list);
        }

        public void Update(RequestPayMainModel model)
        {
            this.dal.Update(model);
        }
    }
}

