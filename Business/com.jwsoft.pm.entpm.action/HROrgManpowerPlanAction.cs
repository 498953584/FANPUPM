namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class HROrgManpowerPlanAction
    {
        public int Add(HROrgManpowerPlan model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into HR_Org_ManpowerPlan( ");
            builder.Append(" RecordID,PlanName,UserCode,RecordDate,AuditState,CorpCode,Remark ");
            builder.Append(" )");
            builder.Append(" values ( ");
            builder.Append(" '" + model.RecordID + "', ");
            builder.Append(" '" + model.PlanName + "', ");
            builder.Append(" '" + model.UserCode + "', ");
            builder.Append(" '" + model.RecordDate + "', ");
            builder.Append(" '" + model.AuditState + "', ");
            builder.Append(" '" + model.CorpCode + "', ");
            builder.Append(" '" + model.Remark + "' ");
            builder.Append(" ) ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete HR_Org_ManpowerPlan ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select top 1 RecordID from HR_Org_ManpowerPlan where RecordID='" + RecordID + "' ");
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetCorpDepartment(string corpCode, bool isAll)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from PT_d_CorpCode ");
            if (!isAll)
            {
                builder.Append(" where CorpCode='" + corpCode + "' ");
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select RecordID,PlanName,UserCode,RecordDate,AuditState,CorpCode,Remark ");
            builder.Append(" FROM HR_Org_ManpowerPlan ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public HROrgManpowerPlan GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select RecordID,PlanName,UserCode,RecordDate,AuditState,CorpCode,Remark ");
            builder.Append(" from HR_Org_ManpowerPlan ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            HROrgManpowerPlan plan = new HROrgManpowerPlan();
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = table.Rows[0];
            if (row["RecordID"].ToString() != "")
            {
                plan.RecordID = new Guid(row["RecordID"].ToString());
            }
            plan.PlanName = row["PlanName"].ToString();
            plan.UserCode = row["UserCode"].ToString();
            if (row["RecordDate"].ToString() != "")
            {
                plan.RecordDate = DateTime.Parse(row["RecordDate"].ToString());
            }
            plan.AuditState = int.Parse(row["AuditState"].ToString());
            plan.CorpCode = row["CorpCode"].ToString();
            plan.Remark = row["Remark"].ToString();
            return plan;
        }

        public int Update(HROrgManpowerPlan model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update HR_Org_ManpowerPlan set ");
            builder.Append(" PlanName='" + model.PlanName + "', ");
            builder.Append(" RecordDate='" + model.RecordDate + "', ");
            builder.Append(" CorpCode='" + model.CorpCode + "', ");
            builder.Append(" Remark='" + model.Remark + "' ");
            builder.Append(" where RecordID='" + model.RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

