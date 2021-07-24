namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAOfficeResApplicationCollectAction
    {
        public int Add(OAOfficeResApplicationCollect model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_ApplicationCollect(");
            builder.Append("ACRecordID,AuditState,iYear,iMonth,ApplyType,CorpCode,SumMoney,IsSubmit,UserCode,RecordDate,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.ACRecordID + "',");
            builder.Append(model.AuditState + ",");
            builder.Append(model.iYear + ",");
            builder.Append(model.iMonth + ",");
            builder.Append("'" + model.ApplyType + "',");
            builder.Append("'" + model.CorpCode + "',");
            builder.Append(model.SumMoney + ",");
            builder.Append("'" + model.IsSubmit + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid ACRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete OA_OfficeRes_ApplicationCollectDetail ");
            builder.Append(" where ACRecordID='" + ACRecordID + "' ");
            builder.Append(" delete OA_OfficeRes_ApplicationCollect ");
            builder.Append(" where ACRecordID='" + ACRecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(Guid ACRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 ID from OA_OfficeRes_ApplicationCollect where ACRecordID='" + ACRecordID + "'");
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ACRecordID,AuditState,iYear,iMonth,ApplyType,CorpCode,(select isnull(sum(a.ApplyNumber*b.PlanFee),0) from OA_OfficeRes_ApplicationCollectDetail a join OA_OfficeRes_Resources b on a.MaterialID=b.RecordID where a.ACRecordID=n.ACRecordID) as SumMoney,IsSubmit,UserCode,RecordDate,Remark ");
            builder.Append(" FROM OA_OfficeRes_ApplicationCollect n");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAOfficeResApplicationCollect GetModel(Guid ACRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" ACRecordID,AuditState,iYear,iMonth,ApplyType,CorpCode,SumMoney,IsSubmit,UserCode,RecordDate ");
            builder.Append(" from OA_OfficeRes_ApplicationCollect ");
            builder.Append(" where ACRecordID='" + ACRecordID + "' ");
            OAOfficeResApplicationCollect collect = new OAOfficeResApplicationCollect();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ACRecordID"].ToString() != "")
            {
                collect.ACRecordID = new Guid(set.Tables[0].Rows[0]["ACRecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                collect.AuditState = int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString());
            }
            if (set.Tables[0].Rows[0]["iYear"].ToString() != "")
            {
                collect.iYear = int.Parse(set.Tables[0].Rows[0]["iYear"].ToString());
            }
            if (set.Tables[0].Rows[0]["iMonth"].ToString() != "")
            {
                collect.iMonth = int.Parse(set.Tables[0].Rows[0]["iMonth"].ToString());
            }
            collect.ApplyType = set.Tables[0].Rows[0]["ApplyType"].ToString();
            collect.CorpCode = set.Tables[0].Rows[0]["CorpCode"].ToString();
            if (set.Tables[0].Rows[0]["SumMoney"].ToString() != "")
            {
                collect.SumMoney = decimal.Parse(set.Tables[0].Rows[0]["SumMoney"].ToString());
            }
            collect.IsSubmit = set.Tables[0].Rows[0]["IsSubmit"].ToString();
            collect.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                collect.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            collect.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            return collect;
        }

        public int Update(OAOfficeResApplicationCollect model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_ApplicationCollect set ");
            builder.Append("iMonth=" + model.iMonth + ",");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where ACRecordID='" + model.ACRecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

