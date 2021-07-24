namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class StatAction
    {
        public int Add(HRLeaveStat model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Leave_Stat(");
            builder.Append("iYear,iMonth,UserCode,DeptCode,Later,LeaveEarly,Holiday1,Holiday2,Holiday3,Holiday4,Holiday5,Holiday6,Holiday7");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.iYear + ",");
            builder.Append(model.iMonth + ",");
            builder.Append("'" + model.UserCode + "',");
            builder.Append(model.DeptCode + ",");
            builder.Append(model.Later + ",");
            builder.Append(model.LeaveEarly + ",");
            builder.Append(model.Holiday1 + ",");
            builder.Append(model.Holiday2 + ",");
            builder.Append(model.Holiday3 + ",");
            builder.Append(model.Holiday4 + ",");
            builder.Append(model.Holiday5 + ",");
            builder.Append(model.Holiday6 + ",");
            builder.Append(model.Holiday7);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public void ApplicationCount(int DeptCode, DateTime CountDate)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DeptCode", SqlDbType.Int), new SqlParameter("@CountDate", SqlDbType.DateTime) };
            commandParameters[0].Value = DeptCode;
            commandParameters[1].Value = CountDate;
            publicDbOpClass.ExecuteProcedure("P_HR_Leave_ApplicationCount", commandParameters);
        }

        public int Delete(int iYear, int iMonth, string UserCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Leave_Stat ");
            builder.Append(string.Concat(new object[] { " where iYear=", iYear, " and iMonth=", iMonth, " and UserCode='", UserCode, "' " }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetBMList(string cc, string sj)
        {
            string sqlString = "";
            if (cc == "")
            {
                sqlString = "select * from PT_d_bm where i_sjdm = '" + sj + "' and c_sfyx = 'y' order by i_xh";
            }
            else
            {
                sqlString = "select * from PT_d_bm where CorpCode = '" + cc + "' and i_sjdm = '" + sj + "' and c_sfyx = 'y' order by i_xh";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select iYear,iMonth,UserCode,DeptCode,Later,LeaveEarly,Holiday1,Holiday2,Holiday3,Holiday4,Holiday5,Holiday6,Holiday7,Holiday8,FactDay  ");
            builder.Append(" FROM HR_Leave_Stat ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public HRLeaveStat GetModel(int iYear, int iMonth, string UserCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" iYear,iMonth,UserCode,DeptCode,Later,LeaveEarly,Holiday1,Holiday2,Holiday3,Holiday4,Holiday5,Holiday6,Holiday7 ");
            builder.Append(" from HR_Leave_Stat ");
            builder.Append(string.Concat(new object[] { " where iYear=", iYear, " and iMonth=", iMonth, " and UserCode='", UserCode, "' " }));
            HRLeaveStat stat = new HRLeaveStat();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["iYear"].ToString() != "")
            {
                stat.iYear = int.Parse(set.Tables[0].Rows[0]["iYear"].ToString());
            }
            if (set.Tables[0].Rows[0]["iMonth"].ToString() != "")
            {
                stat.iMonth = int.Parse(set.Tables[0].Rows[0]["iMonth"].ToString());
            }
            stat.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["DeptCode"].ToString() != "")
            {
                stat.DeptCode = int.Parse(set.Tables[0].Rows[0]["DeptCode"].ToString());
            }
            if (set.Tables[0].Rows[0]["Later"].ToString() != "")
            {
                stat.Later = decimal.Parse(set.Tables[0].Rows[0]["Later"].ToString());
            }
            if (set.Tables[0].Rows[0]["LeaveEarly"].ToString() != "")
            {
                stat.LeaveEarly = decimal.Parse(set.Tables[0].Rows[0]["LeaveEarly"].ToString());
            }
            if (set.Tables[0].Rows[0]["Holiday1"].ToString() != "")
            {
                stat.Holiday1 = decimal.Parse(set.Tables[0].Rows[0]["Holiday1"].ToString());
            }
            if (set.Tables[0].Rows[0]["Holiday2"].ToString() != "")
            {
                stat.Holiday2 = decimal.Parse(set.Tables[0].Rows[0]["Holiday2"].ToString());
            }
            if (set.Tables[0].Rows[0]["Holiday3"].ToString() != "")
            {
                stat.Holiday3 = decimal.Parse(set.Tables[0].Rows[0]["Holiday3"].ToString());
            }
            if (set.Tables[0].Rows[0]["Holiday4"].ToString() != "")
            {
                stat.Holiday4 = decimal.Parse(set.Tables[0].Rows[0]["Holiday4"].ToString());
            }
            if (set.Tables[0].Rows[0]["Holiday5"].ToString() != "")
            {
                stat.Holiday5 = decimal.Parse(set.Tables[0].Rows[0]["Holiday5"].ToString());
            }
            if (set.Tables[0].Rows[0]["Holiday6"].ToString() != "")
            {
                stat.Holiday6 = decimal.Parse(set.Tables[0].Rows[0]["Holiday6"].ToString());
            }
            if (set.Tables[0].Rows[0]["Holiday7"].ToString() != "")
            {
                stat.Holiday7 = decimal.Parse(set.Tables[0].Rows[0]["Holiday7"].ToString());
            }
            return stat;
        }

        public int Update(HRLeaveStat model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Leave_Stat set ");
            builder.Append("Later=" + model.Later + ",");
            builder.Append("LeaveEarly=" + model.LeaveEarly + ",");
            builder.Append("Holiday1=" + model.Holiday1 + ",");
            builder.Append("Holiday2=" + model.Holiday2 + ",");
            builder.Append("Holiday3=" + model.Holiday3 + ",");
            builder.Append("Holiday4=" + model.Holiday4 + ",");
            builder.Append("Holiday5=" + model.Holiday5 + ",");
            builder.Append("Holiday6=" + model.Holiday6 + ",");
            builder.Append("Holiday7=" + model.Holiday7);
            builder.Append(string.Concat(new object[] { " where iYear=", model.iYear, " and iMonth=", model.iMonth, " and UserCode='", model.UserCode, "' " }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable YearList()
        {
            string sqlString = "select DISTINCT year(RecordDate) as RecordDate from HR_Leave_Application";
            return publicDbOpClass.DataTableQuary(sqlString);
        }
    }
}

