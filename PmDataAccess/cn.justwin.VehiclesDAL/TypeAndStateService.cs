namespace cn.justwin.VehiclesDAL
{
    using cn.justwin.DAL;
    using cn.justwin.VehiclesModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TypeAndStateService
    {
        public void Add(TypeAndState model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Vehicle_TypeAndState(");
            builder.Append("guid,code,Name,ParentGuid,Type)");
            builder.Append(" values (");
            builder.Append("@guid,@code,@Name,@ParentGuid,@Type)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@guid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@code", SqlDbType.NVarChar, 100), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@ParentGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Type", SqlDbType.Int, 4) };
            commandParameters[0].Value = Guid.NewGuid();
            commandParameters[1].Value = model.code;
            commandParameters[2].Value = model.Name;
            commandParameters[3].Value = model.ParentGuid;
            commandParameters[4].Value = model.Type;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Delete(Guid guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from OA_Vehicle_TypeAndState ");
            builder.Append(" where guid=@guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@guid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = guid;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public void Delete(List<string> Guidlist)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (string str in Guidlist)
                    {
                        Guid guid = new Guid(str);
                        this.Delete(guid);
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new Exception("删除失败！");
                }
            }
        }

        public bool Exists(Guid guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_Vehicle_TypeAndState");
            builder.Append(" where guid=@guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@guid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = guid;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public bool Exists(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_Vehicle_TypeAndState");
            builder.Append(" where " + where);
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null)) > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select guid,code,Name,ParentGuid,Type ");
            builder.Append(" FROM OA_Vehicle_TypeAndState ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" guid,code,Name,ParentGuid,Type ");
            builder.Append(" FROM OA_Vehicle_TypeAndState ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public TypeAndState GetModel(IDataReader dr)
        {
            return new TypeAndState { guid = new Guid(DBHelper.GetString(dr["guid"])), code = DBHelper.GetString(dr["code"]), Name = DBHelper.GetString(dr["Name"]), ParentGuid = new Guid(DBHelper.GetString(dr["ParentGuid"])), Type = new int?(DBHelper.GetInt(dr["Type"])) };
        }

        public TypeAndState GetModel(Guid guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 guid,code,Name,ParentGuid,Type from OA_Vehicle_TypeAndState ");
            builder.Append(" where guid=@guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@guid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = guid;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public bool Update(TypeAndState model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Vehicle_TypeAndState set ");
            builder.Append("code=@code,");
            builder.Append("Name=@Name,");
            builder.Append("ParentGuid=@ParentGuid,");
            builder.Append("Type=@Type");
            builder.Append(" where guid=@guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@code", SqlDbType.NVarChar, 100), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@ParentGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@guid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = model.code;
            commandParameters[1].Value = model.Name;
            commandParameters[2].Value = model.ParentGuid;
            commandParameters[3].Value = model.Type;
            commandParameters[4].Value = model.guid;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

