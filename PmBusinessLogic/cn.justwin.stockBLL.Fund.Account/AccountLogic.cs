namespace cn.justwin.stockBLL.Fund.Account
{
    using cn.justwin.DAL;
    using cn.justwin.Fund.Account;
    using cn.justwin.Serialize;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AccountLogic
    {
        private readonly AccountService dal = new AccountService();

        public int Add(AccounModel model)
        {
            return this.dal.Add(model);
        }

        public List<AccounModel> DataTableToList(DataTable dt)
        {
            List<AccounModel> list = new List<AccounModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    AccounModel item = new AccounModel();
                    if ((dt.Rows[i]["AccountID"] != null) && (dt.Rows[i]["AccountID"].ToString() != ""))
                    {
                        item.AccountID = new Guid(dt.Rows[i]["AccountID"].ToString());
                    }
                    if ((dt.Rows[i]["PrjGuid"] != null) && (dt.Rows[i]["PrjGuid"].ToString() != ""))
                    {
                        item.PrjGuid = dt.Rows[i]["PrjGuid"].ToString();
                    }
                    if ((dt.Rows[i]["accountNum"] != null) && (dt.Rows[i]["accountNum"].ToString() != ""))
                    {
                        item.accountNum = dt.Rows[i]["accountNum"].ToString();
                    }
                    if ((dt.Rows[i]["acountName"] != null) && (dt.Rows[i]["acountName"].ToString() != ""))
                    {
                        item.acountName = dt.Rows[i]["acountName"].ToString();
                    }
                    if ((dt.Rows[i]["creatData"] != null) && (dt.Rows[i]["creatData"].ToString() != ""))
                    {
                        item.creatDate = new DateTime?(DateTime.Parse(dt.Rows[i]["creatData"].ToString()));
                    }
                    if ((dt.Rows[i]["createMan"] != null) && (dt.Rows[i]["createMan"].ToString() != ""))
                    {
                        item.createMan = dt.Rows[i]["createMan"].ToString();
                    }
                    if ((dt.Rows[i]["activeData"] != null) && (dt.Rows[i]["activeData"].ToString() != ""))
                    {
                        item.activeDate = new DateTime?(DateTime.Parse(dt.Rows[i]["activeData"].ToString()));
                    }
                    if ((dt.Rows[i]["activeMan"] != null) && (dt.Rows[i]["activeMan"].ToString() != ""))
                    {
                        item.activeMan = dt.Rows[i]["activeMan"].ToString();
                    }
                    if ((dt.Rows[i]["authorizer"] != null) && (dt.Rows[i]["authorizer"].ToString() != ""))
                    {
                        item.authorizer = dt.Rows[i]["authorizer"].ToString();
                    }
                    if ((dt.Rows[i]["closeData"] != null) && (dt.Rows[i]["closeData"].ToString() != ""))
                    {
                        item.closeDate = new DateTime?(DateTime.Parse(dt.Rows[i]["closeData"].ToString()));
                    }
                    if ((dt.Rows[i]["closeMan"] != null) && (dt.Rows[i]["closeMan"].ToString() != ""))
                    {
                        item.closeMan = dt.Rows[i]["closeMan"].ToString();
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
                    }
                    if ((dt.Rows[i]["initialFund"] != null) && (dt.Rows[i]["initialFund"].ToString() != ""))
                    {
                        item.initialFund = new decimal?(decimal.Parse(dt.Rows[i]["initialFund"].ToString()));
                    }
                    if ((dt.Rows[i]["AccountState"] != null) && (dt.Rows[i]["AccountState"].ToString() != ""))
                    {
                        item.AccountState = new int?(int.Parse(dt.Rows[i]["AccountState"].ToString()));
                    }
                    if ((dt.Rows[i]["FlowState"] != null) && (dt.Rows[i]["FlowState"].ToString() != ""))
                    {
                        item.FlowState = new int?(int.Parse(dt.Rows[i]["FlowState"].ToString()));
                    }
                    if ((dt.Rows[i]["moneyRate"] != null) && (dt.Rows[i]["moneyRate"].ToString() != ""))
                    {
                        item.moneyRate = new decimal?(decimal.Parse(dt.Rows[i]["moneyRate"].ToString()));
                    }
                    if ((dt.Rows[i]["IncomeFund"] != null) && (dt.Rows[i]["IncomeFund"].ToString() != ""))
                    {
                        item.IncomeFund = new decimal?(decimal.Parse(dt.Rows[i]["IncomeFund"].ToString()));
                    }
                    if ((dt.Rows[i]["PayoutFund"] != null) && (dt.Rows[i]["PayoutFund"].ToString() != ""))
                    {
                        item.PayoutFund = new decimal?(decimal.Parse(dt.Rows[i]["PayoutFund"].ToString()));
                    }
                    if ((dt.Rows[i]["CurrentFund"] != null) && (dt.Rows[i]["CurrentFund"].ToString() != ""))
                    {
                        item.CurrentFund = new decimal?(decimal.Parse(dt.Rows[i]["CurrentFund"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(string accountID)
        {
            return this.dal.Delete(accountID);
        }

        public bool DeleteList(string idlist)
        {
            return this.dal.DeleteList(idlist);
        }

        public bool Exists(string accountCode, Guid accountID)
        {
            return this.dal.Exists(accountCode, accountID);
        }

        public static string getAccountState(string _AccountState)
        {
            StringBuilder builder = new StringBuilder();
            string str = _AccountState;
            if (str != null)
            {
                if (!(str == "0"))
                {
                    if (str == "1")
                    {
                        builder.Append("激活");
                        goto Label_006A;
                    }
                    if (str == "2")
                    {
                        builder.Append("注销");
                        goto Label_006A;
                    }
                }
                else
                {
                    builder.Append("未激活");
                    goto Label_006A;
                }
            }
            builder.Append("未激活");
        Label_006A:
            return builder.ToString();
        }

        public DataTable getByList(string strwhere)
        {
            return this.dal.getByList(strwhere);
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public AccounModel GetModel(string AccountID)
        {
            return this.dal.GetModel(AccountID);
        }

        public List<AccounModel> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public string getPrjName(string jsonPrjGuid)
        {
            string[] strArray;
            string str = "";
            ISerializable serializable = new JsonSerializer();
            if (jsonPrjGuid.StartsWith("'"))
            {
                strArray = jsonPrjGuid.Split(new char[] { ',' });
                for (int j = 0; j < strArray.Length; j++)
                {
                    strArray[j] = strArray[j].ToString().Substring(1, strArray[j].ToString().Length - 2);
                }
            }
            else if (jsonPrjGuid.StartsWith("["))
            {
                strArray = serializable.Deserialize<string[]>(jsonPrjGuid);
            }
            else
            {
                strArray = new string[] { jsonPrjGuid };
            }
            DataTable table = this.getTableByjson(strArray);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i < (table.Rows.Count - 1))
                {
                    str = str + table.Rows[i]["PrjName"] + ",";
                }
                else
                {
                    str = str + table.Rows[i]["PrjName"];
                }
            }
            return str;
        }

        public DataTable getTableByjson(string[] PrjGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select * from Pt_Prjinfo where PrjGuid IN({0})", DBHelper.GetInParameterSql(PrjGuid));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public string GetUserNameByUserCode(string code)
        {
            return this.dal.GetUserNameByUserCode(code);
        }

        public DataTable QueryAccount(string accountNum, string acountName, string authorizer)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(authorizer))
            {
                builder.Append(" AND ");
                builder.Append("authorizer like '%").Append(authorizer).Append("%'");
            }
            if (!string.IsNullOrEmpty(accountNum))
            {
                builder.Append(" AND ");
                builder.Append("accountNum like '%").Append(accountNum).Append("%'");
            }
            if (!string.IsNullOrEmpty(acountName))
            {
                builder.Append(" AND ");
                builder.Append("acountName like '%").Append(acountName).Append("%'");
            }
            return this.dal.GetList(builder.ToString());
        }

        public bool Update(AccounModel model)
        {
            return this.dal.Update(model);
        }

        public bool Update(string _authorizer, string _AccountID)
        {
            return this.dal.Update(_authorizer, _AccountID);
        }

        public bool UpdateisActivity(ActivityModel model, SqlTransaction trans)
        {
            return this.dal.UpdateisActivity(model, trans);
        }
    }
}

