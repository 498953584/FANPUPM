namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class SalaryIPIAction
    {
        public int Add(SalaryIPIModel model, string bmbm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Salary_IssuePayInfo(");
            builder.Append("RecordID,AuditState,IssueYear,IssueMonth,CorpCode,ClassCode,TempletID,IsIssuePay,UserCode,RecordDate,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RecordID + "',");
            builder.Append(model.AuditState + ",");
            builder.Append(model.IssueYear + ",");
            builder.Append(model.IssueMonth + ",");
            builder.Append("'" + model.CorpCode + "',");
            builder.Append("'" + model.ClassCode + "',");
            builder.Append(model.TempletID + ",");
            builder.Append("'" + model.IsIssuePay + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            builder.Append(string.Concat(new object[] { " exec P_HR_Salary_CreateWage ", model.TempletID, ",", model.IssueYear, ",", model.IssueMonth, ",'", bmbm, "','", model.RecordID, "'" }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int IssueYear, int IssueMonth, string CorpCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Salary_IssuePayInfo ");
            builder.Append(string.Concat(new object[] { " where IssueYear=", IssueYear, " and IssueMonth=", IssueMonth, " and CorpCode='", CorpCode, "' " }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,AuditState,IssueYear,IssueMonth,CorpCode,ClassCode,TempletID,IsIssuePay,UserCode,RecordDate,Remark ");
            builder.Append(" FROM HR_Salary_IssuePayInfo ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public SalaryIPIModel GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" RecordID,AuditState,IssueYear,IssueMonth,CorpCode,ClassCode,TempletID,IsIssuePay,UserCode,RecordDate,Remark ");
            builder.Append(" from HR_Salary_IssuePayInfo ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            SalaryIPIModel model = new SalaryIPIModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                model.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                model.AuditState = new int?(int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString()));
            }
            if (set.Tables[0].Rows[0]["IssueYear"].ToString() != "")
            {
                model.IssueYear = int.Parse(set.Tables[0].Rows[0]["IssueYear"].ToString());
            }
            if (set.Tables[0].Rows[0]["IssueMonth"].ToString() != "")
            {
                model.IssueMonth = int.Parse(set.Tables[0].Rows[0]["IssueMonth"].ToString());
            }
            model.CorpCode = set.Tables[0].Rows[0]["CorpCode"].ToString();
            model.ClassCode = set.Tables[0].Rows[0]["ClassCode"].ToString();
            if (set.Tables[0].Rows[0]["TempletID"].ToString() != "")
            {
                model.TempletID = new int?(int.Parse(set.Tables[0].Rows[0]["TempletID"].ToString()));
            }
            model.IsIssuePay = set.Tables[0].Rows[0]["IsIssuePay"].ToString();
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                model.RecordDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString()));
            }
            model.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            return model;
        }

        public SalaryIPIModel GetModel(int IssueYear, int IssueMonth, string CorpCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" RecordID,AuditState,IssueYear,IssueMonth,CorpCode,ClassCode,TempletID,IsIssuePay,UserCode,RecordDate,Remark ");
            builder.Append(" from HR_Salary_IssuePayInfo ");
            builder.Append(string.Concat(new object[] { " where IssueYear=", IssueYear, " and IssueMonth=", IssueMonth, " and CorpCode='", CorpCode, "' " }));
            SalaryIPIModel model = new SalaryIPIModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                model.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                model.AuditState = new int?(int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString()));
            }
            if (set.Tables[0].Rows[0]["IssueYear"].ToString() != "")
            {
                model.IssueYear = int.Parse(set.Tables[0].Rows[0]["IssueYear"].ToString());
            }
            if (set.Tables[0].Rows[0]["IssueMonth"].ToString() != "")
            {
                model.IssueMonth = int.Parse(set.Tables[0].Rows[0]["IssueMonth"].ToString());
            }
            model.CorpCode = set.Tables[0].Rows[0]["CorpCode"].ToString();
            model.ClassCode = set.Tables[0].Rows[0]["ClassCode"].ToString();
            if (set.Tables[0].Rows[0]["TempletID"].ToString() != "")
            {
                model.TempletID = new int?(int.Parse(set.Tables[0].Rows[0]["TempletID"].ToString()));
            }
            model.IsIssuePay = set.Tables[0].Rows[0]["IsIssuePay"].ToString();
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                model.RecordDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString()));
            }
            model.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            return model;
        }

        public string strClassCode(int temid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select ClassCode from HR_Salary_Templet where TempletID = " + temid);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "000";
        }

        public string strCorpCode(string v_bmdm)
        {
            DataTable table = publicDbOpClass.DataTableQuary(" select CorpCode from pt_d_bm where v_bmbm = '" + v_bmdm + "' ");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "00";
        }

        public int Update(SalaryIPIModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Salary_IssuePayInfo set ");
            builder.Append("RecordID='" + model.RecordID + "',");
            builder.Append("AuditState=" + model.AuditState + ",");
            builder.Append("ClassCode='" + model.ClassCode + "',");
            builder.Append("TempletID=" + model.TempletID + ",");
            builder.Append("IsIssuePay='" + model.IsIssuePay + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(string.Concat(new object[] { " where IssueYear=", model.IssueYear, " and IssueMonth=", model.IssueMonth, " and CorpCode='", model.CorpCode, "' " }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateIsIssuePay(SalaryIPIModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Salary_IssuePayInfo set ");
            builder.Append("IsIssuePay='" + model.IsIssuePay + "'");
            builder.Append(string.Concat(new object[] { " where IssueYear=", model.IssueYear, " and IssueMonth=", model.IssueMonth, " and CorpCode='", model.CorpCode, "' " }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

