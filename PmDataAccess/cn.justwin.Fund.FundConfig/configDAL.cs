namespace cn.justwin.Fund.FundConfig
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class configDAL
    {
        public void Delete(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Config ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        private List<configModel> GetList(IDataReader dr)
        {
            List<configModel> list = new List<configModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<configModel> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder("SELECT * FROM Fund_Config ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append("WHERE ").Append(strWhere);
            }
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        private configModel GetModel(IDataReader dr)
        {
            return new configModel { ID = DBHelper.GetString(dr["ID"]), ParaName = DBHelper.GetString(dr["ParaName"]), ParaValue = DBHelper.GetString(dr["ParaValue"]), Notes = DBHelper.GetString(dr["Notes"]) };
        }

        public configModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,ParaName,ParaValue,Notes from Fund_Config ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            configModel model = null;
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
                catch
                {
                    trans.Rollback();
                    throw new Exception("更新失败");
                }
            }
        }

        public void Update(string name, string value, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Config set ");
            builder.Append("ParaValue=@ParaValue");
            builder.Append(" where  ParaName=@ParaName");
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

