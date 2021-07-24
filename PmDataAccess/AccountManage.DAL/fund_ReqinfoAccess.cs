namespace AccountManage.DAL
{
    using AccountManage.Model;
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class fund_ReqinfoAccess
    {
        public int Add(fund_ReqinfoModle model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into fund_Reqinfo(");
            builder.Append("reqNum,PrjNum,amount,reqType,reqPeopNum,reqCause,reqDate,useDate,isInterest,isDefault,InterestNum,auditState,remark,IsContr)");
            builder.Append(" values (");
            builder.Append("@reqNum,@PrjNum,@amount,@reqType,@reqPeopNum,@reqCause,@reqDate,@useDate,@isInterest,@isDefault,@InterestNum,@auditState,@remark,@IsContr)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@reqNum", SqlDbType.NVarChar, 50), new SqlParameter("@PrjNum", SqlDbType.NVarChar, 50), new SqlParameter("@amount", SqlDbType.Decimal, 0x12), new SqlParameter("@reqType", SqlDbType.Int, 4), new SqlParameter("@reqPeopNum", SqlDbType.NVarChar, 50), new SqlParameter("@reqCause", SqlDbType.Text), new SqlParameter("@reqDate", SqlDbType.DateTime), new SqlParameter("@useDate", SqlDbType.DateTime), new SqlParameter("@isInterest", SqlDbType.Bit, 1), new SqlParameter("@isDefault", SqlDbType.Bit, 1), new SqlParameter("@InterestNum", SqlDbType.Decimal, 0x12), new SqlParameter("@auditState", SqlDbType.Int, 4), new SqlParameter("@remark", SqlDbType.Text), new SqlParameter("@IsContr", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.reqNum;
            commandParameters[1].Value = model.PrjNum;
            commandParameters[2].Value = model.amount;
            commandParameters[3].Value = model.reqType;
            commandParameters[4].Value = model.reqPeopNum;
            commandParameters[5].Value = model.reqCause;
            commandParameters[6].Value = model.reqDate;
            commandParameters[7].Value = model.useDate;
            commandParameters[8].Value = model.isInterest;
            commandParameters[9].Value = model.isDefault;
            commandParameters[10].Value = model.InterestNum;
            commandParameters[11].Value = model.auditState;
            commandParameters[12].Value = model.remark;
            commandParameters[13].Value = model.IsContr;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Delete(string reqNum)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_Reqinfo ");
            builder.Append(" where reqNum=@reqNum ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@reqNum", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = reqNum;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Exists(fund_ReqinfoModle model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 *from fund_Reqinfo ");
            builder.Append("where reqNum='" + model.reqNum + "'");
            return (SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null).Rows.Count > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,reqNum,PrjNum,amount,reqType,reqPeopNum,reqCause,reqDate,useDate,isInterest,isDefault,InterestNum,auditState,remark,IsContr ");
            builder.Append(" FROM fund_Reqinfo ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where 1=1 " + strWhere);
            }
            builder.Append(" order by reqdate desc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public fund_ReqinfoModle GetModel(string reqNum)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 id,reqNum,PrjNum,amount,reqType,reqPeopNum,reqCause,reqDate,useDate,isInterest,isDefault,InterestNum,auditState,remark ,IsContr from fund_Reqinfo ");
            builder.Append(" where reqNum=@reqNum ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@reqNum", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = reqNum;
            fund_ReqinfoModle modle = new fund_ReqinfoModle();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            modle.id = table.Rows[0]["id"].ToString();
            if (table.Rows[0]["reqNum"].ToString() != "")
            {
                modle.reqNum = table.Rows[0]["reqNum"].ToString();
            }
            if (table.Rows[0]["PrjNum"].ToString() != "")
            {
                modle.PrjNum = table.Rows[0]["PrjNum"].ToString();
            }
            if (table.Rows[0]["amount"].ToString() != "")
            {
                modle.amount = new decimal?(decimal.Parse(table.Rows[0]["amount"].ToString()));
            }
            if (table.Rows[0]["reqType"].ToString() != "")
            {
                modle.reqType = new int?(int.Parse(table.Rows[0]["reqType"].ToString()));
            }
            modle.reqPeopNum = table.Rows[0]["reqPeopNum"].ToString();
            modle.reqCause = table.Rows[0]["reqCause"].ToString();
            if (table.Rows[0]["reqDate"].ToString() != "")
            {
                modle.reqDate = new DateTime?(DateTime.Parse(table.Rows[0]["reqDate"].ToString()));
            }
            if (table.Rows[0]["useDate"].ToString() != "")
            {
                modle.useDate = new DateTime?(DateTime.Parse(table.Rows[0]["useDate"].ToString()));
            }
            if (table.Rows[0]["isInterest"].ToString() != "")
            {
                if ((table.Rows[0]["isInterest"].ToString() == "1") || (table.Rows[0]["isInterest"].ToString().ToLower() == "true"))
                {
                    modle.isInterest = true;
                }
                else
                {
                    modle.isInterest = false;
                }
            }
            if (table.Rows[0]["isDefault"].ToString() != "")
            {
                if ((table.Rows[0]["isDefault"].ToString() == "1") || (table.Rows[0]["isDefault"].ToString().ToLower() == "true"))
                {
                    modle.isDefault = true;
                }
                else
                {
                    modle.isDefault = false;
                }
            }
            if (table.Rows[0]["InterestNum"].ToString() != "")
            {
                modle.InterestNum = new decimal?(decimal.Parse(table.Rows[0]["InterestNum"].ToString()));
            }
            if (table.Rows[0]["auditState"].ToString() != "")
            {
                modle.auditState = new int?(int.Parse(table.Rows[0]["auditState"].ToString()));
            }
            modle.remark = table.Rows[0]["remark"].ToString();
            if (table.Rows[0]["IsContr"].ToString() != "")
            {
                modle.IsContr = new int?(int.Parse(table.Rows[0]["IsContr"].ToString()));
            }
            return modle;
        }

        public bool Update(fund_ReqinfoModle model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update fund_Reqinfo set ");
            builder.Append("PrjNum=@PrjNum,");
            builder.Append("amount=@amount,");
            builder.Append("reqType=@reqType,");
            builder.Append("reqPeopNum=@reqPeopNum,");
            builder.Append("reqCause=@reqCause,");
            builder.Append("reqDate=@reqDate,");
            builder.Append("useDate=@useDate,");
            builder.Append("isInterest=@isInterest,");
            builder.Append("isDefault=@isDefault,");
            builder.Append("InterestNum=@InterestNum,");
            builder.Append("auditState=@auditState,");
            builder.Append("remark=@remark,");
            builder.Append("IsContr=@IsContr");
            builder.Append(" where reqNum=@reqNum ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjNum", SqlDbType.NVarChar, 50), new SqlParameter("@amount", SqlDbType.NVarChar, 50), new SqlParameter("@reqType", SqlDbType.Int, 4), new SqlParameter("@reqPeopNum", SqlDbType.VarChar, 50), new SqlParameter("@reqCause", SqlDbType.Text), new SqlParameter("@reqDate", SqlDbType.DateTime), new SqlParameter("@useDate", SqlDbType.DateTime), new SqlParameter("@isInterest", SqlDbType.Bit, 1), new SqlParameter("@isDefault", SqlDbType.Bit, 1), new SqlParameter("@InterestNum", SqlDbType.Decimal, 0x12), new SqlParameter("@auditState", SqlDbType.Int, 4), new SqlParameter("@remark", SqlDbType.Text), new SqlParameter("@IsContr", SqlDbType.Int, 4), new SqlParameter("@reqNum", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = model.PrjNum;
            commandParameters[1].Value = model.amount;
            commandParameters[2].Value = model.reqType;
            commandParameters[3].Value = model.reqPeopNum;
            commandParameters[4].Value = model.reqCause;
            commandParameters[5].Value = model.reqDate;
            commandParameters[6].Value = model.useDate;
            commandParameters[7].Value = model.isInterest;
            commandParameters[8].Value = model.isDefault;
            commandParameters[9].Value = model.InterestNum;
            commandParameters[10].Value = model.auditState;
            commandParameters[11].Value = model.remark;
            commandParameters[12].Value = model.IsContr;
            commandParameters[13].Value = model.reqNum;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

