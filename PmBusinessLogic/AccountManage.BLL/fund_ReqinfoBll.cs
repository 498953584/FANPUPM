namespace AccountManage.BLL
{
    using AccountManage.DAL;
    using AccountManage.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class fund_ReqinfoBll
    {
        private readonly fund_ReqinfoAccess dal = new fund_ReqinfoAccess();

        public void Add(fund_ReqinfoModle model)
        {
            this.dal.Add(model);
        }

        public List<fund_ReqinfoModle> DataTableToList(DataTable dt)
        {
            List<fund_ReqinfoModle> list = new List<fund_ReqinfoModle>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    fund_ReqinfoModle item = new fund_ReqinfoModle {
                        id = dt.Rows[i]["id"].ToString()
                    };
                    if (dt.Rows[i]["reqNum"].ToString() != "")
                    {
                        item.reqNum = dt.Rows[i]["reqNum"].ToString();
                    }
                    if (dt.Rows[i]["PrjNum"].ToString() != "")
                    {
                        item.PrjNum = dt.Rows[i]["PrjNum"].ToString();
                    }
                    if (dt.Rows[i]["amount"].ToString() != "")
                    {
                        item.amount = new decimal?(decimal.Parse(dt.Rows[i]["amount"].ToString()));
                    }
                    if (dt.Rows[i]["reqType"].ToString() != "")
                    {
                        item.reqType = new int?(int.Parse(dt.Rows[i]["reqType"].ToString()));
                    }
                    item.reqPeopNum = dt.Rows[i]["reqPeopNum"].ToString();
                    item.reqCause = dt.Rows[i]["reqCause"].ToString();
                    if (dt.Rows[i]["reqDate"].ToString() != "")
                    {
                        item.reqDate = new DateTime?(DateTime.Parse(dt.Rows[i]["reqDate"].ToString()));
                    }
                    if (dt.Rows[i]["useDate"].ToString() != "")
                    {
                        item.useDate = new DateTime?(DateTime.Parse(dt.Rows[i]["useDate"].ToString()));
                    }
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
                    if (dt.Rows[i]["isDefault"].ToString() != "")
                    {
                        if ((dt.Rows[i]["isDefault"].ToString() == "1") || (dt.Rows[i]["isDefault"].ToString().ToLower() == "true"))
                        {
                            item.isDefault = true;
                        }
                        else
                        {
                            item.isDefault = false;
                        }
                    }
                    if (dt.Rows[i]["InterestNum"].ToString() != "")
                    {
                        item.InterestNum = new decimal?(decimal.Parse(dt.Rows[i]["InterestNum"].ToString()));
                    }
                    if (dt.Rows[i]["auditState"].ToString() != "")
                    {
                        item.auditState = new int?(int.Parse(dt.Rows[i]["auditState"].ToString()));
                    }
                    if (dt.Rows[i]["IsContr"].ToString() != "")
                    {
                        item.IsContr = new int?(int.Parse(dt.Rows[i]["IsContr"].ToString()));
                    }
                    item.remark = dt.Rows[i]["remark"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(string reqNum)
        {
            return this.dal.Delete(reqNum);
        }

        public bool Exists(fund_ReqinfoModle model)
        {
            return this.dal.Exists(model);
        }

        public DataTable GetAllList()
        {
            return this.GetList("");
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetListWhere(string where)
        {
            return this.GetList(where);
        }

        public fund_ReqinfoModle GetModel(string reqNum)
        {
            return this.dal.GetModel(reqNum);
        }

        public List<fund_ReqinfoModle> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public bool Update(fund_ReqinfoModle model)
        {
            return this.dal.Update(model);
        }
    }
}

