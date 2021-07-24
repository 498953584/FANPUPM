namespace cn.justwin.salaryDAL
{
    using cn.justwin.DAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SalaryTemplate
    {
        public int Add(SalaryTemplateModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO SaM_SalaryTemplate(");
            builder.Append("TemplateID,TemplateName,Formula,Enabled,BeginDate,EndDate,TemplateType)");
            builder.Append(" VALUES (");
            builder.Append("@TemplateID,@TemplateName,@Formula,@Enabled,@BeginDate,@EndDate,@TemplateType)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TemplateID", SqlDbType.NVarChar, 0x40), new SqlParameter("@TemplateName", SqlDbType.NVarChar, 0x20), new SqlParameter("@Formula", SqlDbType.NVarChar, 0x80), new SqlParameter("@Enabled", SqlDbType.Bit, 1), new SqlParameter("@BeginDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@TemplateType", SqlDbType.Char, 2) };
            commandParameters[0].Value = model.TemplateID;
            commandParameters[1].Value = model.TemplateName;
            commandParameters[2].Value = model.Formula;
            commandParameters[3].Value = model.Enabled;
            commandParameters[4].Value = model.BeginDate;
            commandParameters[5].Value = model.EndDate;
            commandParameters[6].Value = model.TemplateType;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string TemplateID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM SaM_SalaryTemplate ");
            builder.Append(" WHERE TemplateID=@TemplateID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TemplateID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = TemplateID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        private List<SalaryTemplateModel> GetList(IDataReader dr)
        {
            List<SalaryTemplateModel> list = new List<SalaryTemplateModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<SalaryTemplateModel> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder("SELECT * FROM SaM_SalaryTemplate " + strWhere);
            builder.Append(strWhere);
            builder.Append(" ORDER BY TemplateType ");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        private SalaryTemplateModel GetModel(IDataReader dr)
        {
            return new SalaryTemplateModel { TemplateID = DBHelper.GetString(dr["TemplateID"]), TemplateName = DBHelper.GetString(dr["TemplateName"]), Formula = DBHelper.GetString(dr["Formula"]), Enabled = DBHelper.GetBool(dr["Enabled"]), BeginDate = new DateTime?(DBHelper.GetDateTime(dr["BeginDate"])), EndDate = new DateTime?(DBHelper.GetDateTime(dr["EndDate"])), TemplateType = DBHelper.GetString(dr["TemplateType"]) };
        }

        public SalaryTemplateModel GetModel(string TemplateID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM SaM_SalaryTemplate ");
            builder.Append(" WHERE TemplateID=@TemplateID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TemplateID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = TemplateID;
            SalaryTemplateModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    model = this.GetModel(reader);
                }
                return model;
            }
        }

        public int Update(SalaryTemplateModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SaM_SalaryTemplate SET ");
            builder.Append("TemplateName=@TemplateName,");
            builder.Append("Formula=@Formula,");
            builder.Append("Enabled=@Enabled,");
            builder.Append("BeginDate=@BeginDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("TemplateType=@TemplateType");
            builder.Append(" WHERE TemplateID=@TemplateID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TemplateID", SqlDbType.NVarChar, 0x40), new SqlParameter("@TemplateName", SqlDbType.NVarChar, 0x20), new SqlParameter("@Formula", SqlDbType.NVarChar, 0x80), new SqlParameter("@Enabled", SqlDbType.Bit, 1), new SqlParameter("@BeginDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@TemplateType", SqlDbType.Char, 2) };
            commandParameters[0].Value = model.TemplateID;
            commandParameters[1].Value = model.TemplateName;
            commandParameters[2].Value = model.Formula;
            commandParameters[3].Value = model.Enabled;
            commandParameters[4].Value = model.BeginDate;
            commandParameters[5].Value = model.EndDate;
            commandParameters[6].Value = model.TemplateType;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

