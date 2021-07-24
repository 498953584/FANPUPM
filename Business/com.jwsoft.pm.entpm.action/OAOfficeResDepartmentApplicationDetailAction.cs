namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAOfficeResDepartmentApplicationDetailAction
    {
        public int Add(OAOfficeResDepartmentApplicationDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_DepartmentApplicationDetail(");
            builder.Append("DARecordID,MaterialID,ApplyNumber,IsPass");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.DARecordID + "',");
            builder.Append(model.MaterialID + ",");
            builder.Append(model.ApplyNumber + ",");
            builder.Append("'" + model.IsPass + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int DADRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_DepartmentApplicationDetail ");
            builder.Append(" where DADRecordID=" + DADRecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int DADRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_OfficeRes_DepartmentApplicationDetail where DADRecordID=" + DADRecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public decimal GetApplyStat(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select sum(isnull(c.PlanFee,0)*isnull(b.ApplyNumber,0)) from OA_OfficeRes_DepartmentApplication a ");
            builder.Append(" join OA_OfficeRes_DepartmentApplicationDetail b on a.DARecordID=b.DARecordID and IsPass='1' ");
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
            builder.Append(" select a.DADRecordID,a.DARecordID,a.MaterialID,a.ApplyNumber,a.IsPass,b.ResName,b.UseType,b.GetType,b.Unit,b.PlanFee from OA_OfficeRes_DepartmentApplicationDetail a ");
            builder.Append(" join OA_OfficeRes_Resources b on a.MaterialID=b.RecordID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAOfficeResDepartmentApplicationDetail GetModel(int DADRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" DADRecordID,DARecordID,MaterialID,ApplyNumber,IsPass ");
            builder.Append(" from OA_OfficeRes_DepartmentApplicationDetail ");
            builder.Append(" where DADRecordID=" + DADRecordID);
            OAOfficeResDepartmentApplicationDetail detail = new OAOfficeResDepartmentApplicationDetail();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["DADRecordID"].ToString() != "")
            {
                detail.DADRecordID = int.Parse(set.Tables[0].Rows[0]["DADRecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["DARecordID"].ToString() != "")
            {
                detail.DARecordID = new Guid(set.Tables[0].Rows[0]["DARecordID"].ToString());
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

        public int Update(OAOfficeResDepartmentApplicationDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_DepartmentApplicationDetail set ");
            builder.Append("MaterialID=" + model.MaterialID + ",");
            builder.Append("ApplyNumber=" + model.ApplyNumber);
            builder.Append(" where DADRecordID=" + model.DADRecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Update(Guid RecordID, Guid AcGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_OfficeRes_DepartmentApplication set ");
            builder.Append(" IsComplete='1', ");
            builder.Append(" ACRecordID='" + AcGuid + "' ");
            builder.Append(" where DARecordID='" + RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateAudit(Guid InStorageID, string IsAbove)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_OfficeRes_DepartmentApplication set ");
            builder.Append(" IsAbove='" + IsAbove + "' ");
            builder.Append(" where DARecordID='" + InStorageID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

