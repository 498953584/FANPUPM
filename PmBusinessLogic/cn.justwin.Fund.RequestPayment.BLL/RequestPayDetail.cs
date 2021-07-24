namespace cn.justwin.Fund.RequestPayment.BLL
{
    using cn.justwin.Fund.RequestPayment.DAL;
    using cn.justwin.Fund.RequestPayment.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class RequestPayDetail
    {
        private readonly RequestPayDetailService dal = new RequestPayDetailService();

        public bool Add(RequestPayDetailModel model)
        {
            return this.dal.Add(model);
        }

        public List<RequestPayDetailModel> DataTableToList(DataTable dt)
        {
            List<RequestPayDetailModel> list = new List<RequestPayDetailModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    RequestPayDetailModel item = new RequestPayDetailModel();
                    if (dt.Rows[i]["RPayMainID"].ToString() != "")
                    {
                        item.RPayMainID = new Guid(dt.Rows[i]["RPayMainID"].ToString());
                    }
                    if (dt.Rows[i]["RPayUID"].ToString() != "")
                    {
                        item.RPayUID = new Guid(dt.Rows[i]["RPayUID"].ToString());
                    }
                    if (dt.Rows[i]["PlanUID"].ToString() != "")
                    {
                        item.PlanUID = new Guid(dt.Rows[i]["PlanUID"].ToString());
                    }
                    item.ContractID = dt.Rows[i]["ContractID"].ToString();
                    item.RPaysubject = dt.Rows[i]["RPaysubject"].ToString();
                    if (dt.Rows[i]["RPayMoney"].ToString() != "")
                    {
                        item.RPayMoney = new decimal?(decimal.Parse(dt.Rows[i]["RPayMoney"].ToString()));
                    }
                    item.ReMark = dt.Rows[i]["ReMark"].ToString();
                    item.RPayUser = dt.Rows[i]["RPayUser"].ToString();
                    if (dt.Rows[i]["OrderID"].ToString() != "")
                    {
                        item.OrderID = int.Parse(dt.Rows[i]["OrderID"].ToString());
                    }
                    item.RPayCause = dt.Rows[i]["RPayCause"].ToString();
                    if (dt.Rows[i]["isInterest"].ToString() != "")
                    {
                        if ((dt.Rows[i]["isInterest"].ToString() == "1") || (dt.Rows[i]["isInterest"].ToString().ToLower() == "true"))
                        {
                            item.isInterest = true;
                        }
                        else
                        {
                            item.isInterest = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(Guid PlanUID)
        {
            return this.dal.Delete(PlanUID);
        }

        public bool DeleteBYUID(Guid RPayUID)
        {
            return this.dal.DeleteBYUID(RPayUID);
        }

        public bool Exists(Guid RPayUID)
        {
            return this.dal.Exists(RPayUID);
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

        public RequestPayDetailModel GetModel(Guid RPayUID)
        {
            return this.dal.GetModel(RPayUID);
        }

        public List<RequestPayDetailModel> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public bool Update(RequestPayDetailModel model)
        {
            return this.dal.Update(model);
        }
    }
}

