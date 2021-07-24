namespace cn.justwin.salaryDAL
{
    using cn.justwin.DAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Formula
    {
        public void Add(FormulaModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SaM_Formula(");
            builder.Append("FormulaID,FormulaName,Formula,AddDate)");
            builder.Append(" values (");
            builder.Append("@FormulaID,@FormulaName,@Formula,@AddDate)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@FormulaID", SqlDbType.NVarChar, 0x40), new SqlParameter("@FormulaName", SqlDbType.NVarChar, 0x20), new SqlParameter("@Formula", SqlDbType.NVarChar, 0x100), new SqlParameter("@AddDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.FormulaID;
            commandParameters[1].Value = model.FormulaName;
            commandParameters[2].Value = model.Formula;
            commandParameters[3].Value = model.AddDate;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Delete(List<string> formulaIDs)
        {
            bool flag = true;
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in formulaIDs)
                    {
                        this.Delete(str, trans);
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    flag = false;
                }
            }
            return flag;
        }

        public void Delete(string FormulaID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SaM_Formula ");
            builder.Append(" where FormulaID=@FormulaID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@FormulaID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = FormulaID;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        private List<FormulaModel> GetList(IDataReader dr)
        {
            List<FormulaModel> list = new List<FormulaModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<FormulaModel> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder("SELECT * FROM SaM_Formula ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append(strWhere);
            }
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        private FormulaModel GetModel(IDataReader dr)
        {
            return new FormulaModel { FormulaID = DBHelper.GetString(dr["FormulaID"]), FormulaName = DBHelper.GetString(dr["FormulaName"]), Formula = DBHelper.GetString(dr["Formula"]), AddDate = DBHelper.GetDateTime(dr["AddDate"]) };
        }

        public FormulaModel GetModel(string formulaID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM SaM_Formula ");
            builder.Append(" WHERE FormulaID=@in_FormulaID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@in_FormulaID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = formulaID;
            FormulaModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    model = this.GetModel(reader);
                }
                return model;
            }
        }

        public void Update(FormulaModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SaM_Formula set ");
            builder.Append("FormulaName=@FormulaName,");
            builder.Append("Formula=@Formula,");
            builder.Append("AddDate=@AddDate");
            builder.Append(" where FormulaID=@FormulaID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@FormulaID", SqlDbType.NVarChar, 0x40), new SqlParameter("@FormulaName", SqlDbType.NVarChar, 0x20), new SqlParameter("@Formula", SqlDbType.NVarChar, 0x100), new SqlParameter("@AddDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.FormulaID;
            commandParameters[1].Value = model.FormulaName;
            commandParameters[2].Value = model.Formula;
            commandParameters[3].Value = model.AddDate;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

