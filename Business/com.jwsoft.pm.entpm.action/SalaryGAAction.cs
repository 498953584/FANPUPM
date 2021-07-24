namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class SalaryGAAction
    {
        public int Add(SalaryGAModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Salary_GroupAdjust(");
            builder.Append("AuditState,ApplyDate,Remark,UserCode,RecordDate,IsConfirm");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.AuditState + ",");
            builder.Append("'" + model.ApplyDate + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.IsConfirm + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin ");
            builder.Append(" DELETE [HR_Salary_GroupAdjustDetail]  WHERE [RecordID] = '" + RecordID + "'");
            builder.Append("delete HR_Salary_GroupAdjust ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            builder.Append(" end");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,AuditState,ApplyDate,Remark,UserCode,RecordDate,IsConfirm ");
            builder.Append(" FROM HR_Salary_GroupAdjust ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public SalaryGAModel GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" RecordID,AuditState,ApplyDate,Remark,UserCode,RecordDate,IsConfirm ");
            builder.Append(" from HR_Salary_GroupAdjust ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            SalaryGAModel model = new SalaryGAModel();
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
            if (set.Tables[0].Rows[0]["ApplyDate"].ToString() != "")
            {
                model.ApplyDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["ApplyDate"].ToString()));
            }
            model.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                model.RecordDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString()));
            }
            model.IsConfirm = set.Tables[0].Rows[0]["IsConfirm"].ToString();
            return model;
        }

        public int Update(SalaryGAModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Salary_GroupAdjust set ");
            builder.Append("AuditState=" + model.AuditState + ",");
            builder.Append("ApplyDate='" + model.ApplyDate + "',");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("IsConfirm='" + model.IsConfirm + "'");
            builder.Append(" where RecordID='" + model.RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateAuditResult(int noteId)
        {
            return publicDbOpClass.ExecSqlString("update set AuditResult=1 where NoteId=" + noteId);
        }
    }
}

