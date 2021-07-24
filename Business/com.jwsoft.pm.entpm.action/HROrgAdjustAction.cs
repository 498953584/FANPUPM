namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class HROrgAdjustAction
    {
        public int Add(HROrgAdjust model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Org_Adjust(");
            builder.Append("RecordID,AuditState,CorpCode,UserCode,RecordDate,AdjustContent,AdjustReason,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RecordID + "',");
            builder.Append(model.AuditState + ",");
            builder.Append("'" + model.CorpCode + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.AdjustContent + "',");
            builder.Append("'" + model.AdjustReason + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Org_Adjust ");
            builder.Append(" where RecordID='" + RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select count(1) from HR_Org_Adjust where RecordID='" + RecordID + "' ");
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,AuditState,CorpCode,UserCode,RecordDate,AdjustContent,AdjustReason,Remark ");
            builder.Append(" FROM HR_Org_Adjust ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int Update(HROrgAdjust model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Org_Adjust set ");
            builder.Append("AuditState=" + model.AuditState + ",");
            builder.Append("CorpCode='" + model.CorpCode + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("AdjustContent='" + model.AdjustContent + "',");
            builder.Append("AdjustReason='" + model.AdjustReason + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where RecordID='" + model.RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

