namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAOfficeResPersonalApplicationAction
    {
        public int Add(OAOfficeResPersonalApplication model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_PersonalApplication(");
            builder.Append("CorpCode,ApplyDate,ApplyMan,UseMan,IsSubmit,IsComplete,UserCode,RecordDate,ACRecordID");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.CorpCode + "',");
            builder.Append("'" + model.ApplyDate + "',");
            builder.Append("'" + model.ApplyMan + "',");
            builder.Append("'" + model.UseMan + "',");
            builder.Append("'" + model.IsSubmit + "',");
            builder.Append("'" + model.IsComplete + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.ACRecordID + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int PARecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete OA_OfficeRes_PersonalApplicationDetail where PARecordID=" + PARecordID);
            builder.Append(" delete OA_OfficeRes_PersonalApplication ");
            builder.Append(" where PARecordID=" + PARecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int PARecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 PARecordID from OA_OfficeRes_PersonalApplication where PARecordID=" + PARecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PARecordID,CorpCode,ApplyDate,ApplyMan,UseMan,IsSubmit,IsComplete,UserCode,RecordDate,ACRecordID ");
            builder.Append(" FROM OA_OfficeRes_PersonalApplication ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere + " order by ApplyDate desc");
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAOfficeResPersonalApplication GetModel(int PARecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" PARecordID,CorpCode,ApplyDate,ApplyMan,UseMan,IsSubmit,IsComplete,UserCode,RecordDate,ACRecordID ");
            builder.Append(" from OA_OfficeRes_PersonalApplication ");
            builder.Append(" where PARecordID=" + PARecordID);
            OAOfficeResPersonalApplication application = new OAOfficeResPersonalApplication();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["PARecordID"].ToString() != "")
            {
                application.PARecordID = int.Parse(set.Tables[0].Rows[0]["PARecordID"].ToString());
            }
            application.CorpCode = set.Tables[0].Rows[0]["CorpCode"].ToString();
            if (set.Tables[0].Rows[0]["ApplyDate"].ToString() != "")
            {
                application.ApplyDate = DateTime.Parse(set.Tables[0].Rows[0]["ApplyDate"].ToString());
            }
            application.ApplyMan = set.Tables[0].Rows[0]["ApplyMan"].ToString();
            application.UseMan = set.Tables[0].Rows[0]["UseMan"].ToString();
            application.IsSubmit = set.Tables[0].Rows[0]["IsSubmit"].ToString();
            application.IsComplete = set.Tables[0].Rows[0]["IsComplete"].ToString();
            application.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                application.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["ACRecordID"].ToString() != "")
            {
                application.ACRecordID = new Guid(set.Tables[0].Rows[0]["ACRecordID"].ToString());
            }
            return application;
        }

        public int Update(OAOfficeResPersonalApplication model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_PersonalApplication set ");
            builder.Append("ApplyMan='" + model.ApplyMan + "',");
            builder.Append("UseMan='" + model.UseMan + "',");
            builder.Append("CorpCode='" + model.CorpCode + "'");
            builder.Append(" where PARecordID=" + model.PARecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Update(string PARecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_PersonalApplication set ");
            builder.Append("IsHaveDo='1'");
            builder.Append(" where PARecordID='" + PARecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateSubmit(int PARecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_PersonalApplication set ");
            builder.Append("IsSubmit='1'");
            builder.Append(" where PARecordID=" + PARecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

