namespace AccountManage.BLL
{
    using AccountManage.DAL;
    using AccountManage.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class fund_accountInfoBLL
    {
        private readonly fund_accountInfoAccess dal = new fund_accountInfoAccess();

        public int Add(fund_accountInfoModle model)
        {
            return this.dal.Add(model);
        }

        public List<fund_accountInfoModle> DataTableToList(DataTable dt)
        {
            List<fund_accountInfoModle> list = new List<fund_accountInfoModle>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    fund_accountInfoModle item = new fund_accountInfoModle();
                    if (dt.Rows[i]["id"].ToString() != "")
                    {
                        item.id = int.Parse(dt.Rows[i]["id"].ToString());
                    }
                    item.accountNum = dt.Rows[i]["accountNum"].ToString();
                    if (dt.Rows[i]["opMoney"].ToString() != "")
                    {
                        item.opMoney = new decimal?(decimal.Parse(dt.Rows[i]["opMoney"].ToString()));
                    }
                    if (dt.Rows[i]["opType"].ToString() != "")
                    {
                        item.opType = new int?(int.Parse(dt.Rows[i]["opType"].ToString()));
                    }
                    if (dt.Rows[i]["opTime"].ToString() != "")
                    {
                        item.opTime = new DateTime?(DateTime.Parse(dt.Rows[i]["opTime"].ToString()));
                    }
                    item.opMan = dt.Rows[i]["opMan"].ToString();
                    if (dt.Rows[i]["isValid"].ToString() != "")
                    {
                        item.isValid = new int?(int.Parse(dt.Rows[i]["isValid"].ToString()));
                    }
                    item.sysPeop = dt.Rows[i]["sysPeop"].ToString();
                    if (dt.Rows[i]["opClass"].ToString() != "")
                    {
                        item.opClass = new int?(int.Parse(dt.Rows[i]["opClass"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int id)
        {
            return this.dal.Delete(id);
        }

        public bool Exists(int id)
        {
            return this.dal.Exists(id);
        }

        public DataTable GetAllList()
        {
            return this.GetList("");
        }

        public DataTable GetDistinctNumber(string usercode, string where)
        {
            return this.dal.GetDistinctNumber(usercode, where);
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public fund_accountInfoModle GetModel(int id)
        {
            return this.dal.GetModel(id);
        }

        public List<fund_accountInfoModle> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public DataTable GetNumberTable(string usercode, string where)
        {
            return this.dal.GetNumberTable(usercode, where);
        }

        public string GetSum(string accouid)
        {
            return this.dal.GetMonye(accouid);
        }

        public bool Update(fund_accountInfoModle model)
        {
            return this.dal.Update(model);
        }
    }
}

