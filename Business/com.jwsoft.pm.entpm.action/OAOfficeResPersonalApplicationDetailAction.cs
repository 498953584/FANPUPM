namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAOfficeResPersonalApplicationDetailAction
    {
        public int Add(OAOfficeResPersonalApplicationDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_PersonalApplicationDetail(");
            builder.Append("PARecordID,MaterialID,ApplyNumber,IsPass");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.PARecordID + ",");
            builder.Append(model.MaterialID + ",");
            builder.Append(model.ApplyNumber + ",");
            builder.Append("'" + model.IsPass + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int PADRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_PersonalApplicationDetail ");
            builder.Append(" where PADRecordID=" + PADRecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int PADRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 PADRecordID from OA_OfficeRes_PersonalApplicationDetail where PADRecordID=" + PADRecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public decimal GetApplyStat(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select sum(isnull(c.PlanFee,0)*isnull(b.ApplyNumber,0)) from OA_OfficeRes_PersonalApplication a ");
            builder.Append(" join OA_OfficeRes_PersonalApplicationDetail b on a.PARecordID=b.PARecordID and IsPass='1' ");
            builder.Append(" join OA_OfficeRes_Resources c on b.MaterialID=c.RecordID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if ((obj2 != null) && (obj2.ToString() != ""))
            {
                return (decimal) obj2;
            }
            return 0M;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select a.PADRecordID,a.PARecordID,a.MaterialID,a.ApplyNumber,a.IsPass,b.ResName,b.UseType,b.GetType,b.Unit,b.PlanFee from OA_OfficeRes_PersonalApplicationDetail a ");
            builder.Append(" join OA_OfficeRes_Resources b on a.MaterialID=b.RecordID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAOfficeResPersonalApplicationDetail GetModel(int PADRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" PADRecordID,PARecordID,MaterialID,ApplyNumber,IsPass ");
            builder.Append(" from OA_OfficeRes_PersonalApplicationDetail ");
            builder.Append(" where PADRecordID=" + PADRecordID);
            OAOfficeResPersonalApplicationDetail detail = new OAOfficeResPersonalApplicationDetail();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["PADRecordID"].ToString() != "")
            {
                detail.PADRecordID = int.Parse(set.Tables[0].Rows[0]["PADRecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["PARecordID"].ToString() != "")
            {
                detail.PARecordID = int.Parse(set.Tables[0].Rows[0]["PARecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["MaterialID"].ToString() != "")
            {
                detail.MaterialID = int.Parse(set.Tables[0].Rows[0]["MaterialID"].ToString());
            }
            if (set.Tables[0].Rows[0]["ApplyNumber"].ToString() != "")
            {
                detail.ApplyNumber = int.Parse(set.Tables[0].Rows[0]["ApplyNumber"].ToString());
            }
            detail.IsPass = set.Tables[0].Rows[0]["IsPass"].ToString();
            return detail;
        }

        public int Update(OAOfficeResPersonalApplicationDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_PersonalApplicationDetail set ");
            builder.Append("ApplyNumber=" + model.ApplyNumber);
            builder.Append(" where PADRecordID=" + model.PADRecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Update(int RecordID, Guid AcGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_OfficeRes_PersonalApplication set ");
            builder.Append(" IsComplete='1', ");
            builder.Append(" ACRecordID='" + AcGuid + "' ");
            builder.Append(" where PARecordID=" + RecordID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

