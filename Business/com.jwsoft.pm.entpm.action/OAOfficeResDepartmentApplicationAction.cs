namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAOfficeResDepartmentApplicationAction
    {
        public int Add(OAOfficeResDepartmentApplication model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_DepartmentApplication(");
            builder.Append("DARecordID,AuditState,CorpCode,ApplyDate,ApplyDepartment,ApplyMan,IsSubmit,IsComplete,UserCode,RecordDate,IsAbove,ACRecordID,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.DARecordID + "',");
            builder.Append(model.AuditState + ",");
            builder.Append("'" + model.CorpCode + "',");
            builder.Append("'" + model.ApplyDate + "',");
            builder.Append(model.ApplyDepartment + ",");
            builder.Append("'" + model.ApplyMan + "',");
            builder.Append("'" + model.IsSubmit + "',");
            builder.Append("'" + model.IsComplete + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.IsAbove + "',");
            builder.Append("'" + model.ACRecordID + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid DARecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete OA_OfficeRes_DepartmentApplicationDetail ");
            builder.Append(" where DARecordID='" + DARecordID + "' ");
            builder.Append(" delete OA_OfficeRes_DepartmentApplication ");
            builder.Append(" where DARecordID='" + DARecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 ID from OA_OfficeRes_DepartmentApplication where ID=" + ID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select DARecordID,AuditState,CorpCode,ApplyDate,ApplyDepartment,ApplyMan,IsSubmit,IsComplete,UserCode,RecordDate,IsAbove,ACRecordID,Remark ");
            builder.Append(" FROM OA_OfficeRes_DepartmentApplication ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere + " order by ApplyDate desc");
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAOfficeResDepartmentApplication GetModel(Guid DARecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select DARecordID,AuditState,CorpCode,ApplyDate,ApplyDepartment,ApplyMan,IsSubmit,IsComplete,UserCode,RecordDate,IsAbove,ACRecordID ");
            builder.Append(" from OA_OfficeRes_DepartmentApplication ");
            builder.Append(" where DARecordID='" + DARecordID + "'");
            OAOfficeResDepartmentApplication application = new OAOfficeResDepartmentApplication();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["DARecordID"].ToString() != "")
            {
                application.DARecordID = new Guid(set.Tables[0].Rows[0]["DARecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                application.AuditState = int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString());
            }
            application.CorpCode = set.Tables[0].Rows[0]["CorpCode"].ToString();
            if (set.Tables[0].Rows[0]["ApplyDate"].ToString() != "")
            {
                application.ApplyDate = DateTime.Parse(set.Tables[0].Rows[0]["ApplyDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["ApplyDepartment"].ToString() != "")
            {
                application.ApplyDepartment = int.Parse(set.Tables[0].Rows[0]["ApplyDepartment"].ToString());
            }
            application.ApplyMan = set.Tables[0].Rows[0]["ApplyMan"].ToString();
            application.IsSubmit = set.Tables[0].Rows[0]["IsSubmit"].ToString();
            application.IsComplete = set.Tables[0].Rows[0]["IsComplete"].ToString();
            application.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            application.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                application.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            application.IsAbove = set.Tables[0].Rows[0]["IsAbove"].ToString();
            if (set.Tables[0].Rows[0]["ACRecordID"].ToString() != "")
            {
                application.ACRecordID = new Guid(set.Tables[0].Rows[0]["ACRecordID"].ToString());
            }
            return application;
        }

        public int Update(OAOfficeResDepartmentApplication model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_DepartmentApplication set ");
            builder.Append("ApplyDate='" + model.ApplyDate + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append("where DARecordID='" + model.DARecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Update(string DARecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_DepartmentApplication set ");
            builder.Append("IsHaveDo='1'");
            builder.Append("where DARecordID='" + DARecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateTransact(Guid DARecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_DepartmentApplication set ");
            builder.Append("IsSubmit='1'");
            builder.Append("where DARecordID='" + DARecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

