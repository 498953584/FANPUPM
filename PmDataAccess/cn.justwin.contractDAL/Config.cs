namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Config
    {
        public void Add(ConfigModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Config(");
            builder.Append("ID,ParaName,ParaValue,Notes)");
            builder.Append(" values (");
            builder.Append("@ID,@ParaName,@ParaValue,@Notes)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ParaName", SqlDbType.NVarChar, 200), new SqlParameter("@ParaValue", SqlDbType.NVarChar, 200), new SqlParameter("@Notes", SqlDbType.NVarChar) };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.ParaName;
            commandParameters[2].Value = model.ParaValue;
            commandParameters[3].Value = model.Notes;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public void Delete(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Config ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        private List<ConfigModel> GetList(IDataReader dr)
        {
            List<ConfigModel> list = new List<ConfigModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<ConfigModel> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder("SELECT * FROM Con_Config ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append("WHERE ").Append(strWhere);
            }
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        private ConfigModel GetModel(IDataReader dr)
        {
            return new ConfigModel { ID = DBHelper.GetString(dr["ID"]), ParaName = DBHelper.GetString(dr["ParaName"]), ParaValue = DBHelper.GetString(dr["ParaValue"]), Notes = DBHelper.GetString(dr["Notes"]) };
        }

        public ConfigModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Con_Config ");
            builder.Append(" WHERE ID=@ID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            ConfigModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    model = this.GetModel(reader);
                }
                return model;
            }
        }

        public void Update(Dictionary<string, string> parameters)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in parameters.Keys)
                    {
                        this.Update(str, parameters[str], trans);
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw new Exception("更新失败");
                }
            }
        }

        public void Update(string name, string value, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Con_Config SET ");
            builder.Append("ParaValue = @ParaValue ");
            builder.Append("WHERE ParaName = @ParaName ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ParaName", SqlDbType.NVarChar, 200), new SqlParameter("@ParaValue", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = name;
            commandParameters[1].Value = value;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }
    }
}

