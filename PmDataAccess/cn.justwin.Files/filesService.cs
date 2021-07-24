namespace cn.justwin.Files
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class filesService
    {
        public int Add(filesModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Files(");
            builder.Append("ID,file_sid,file_name,file_info,file_mark,Original_table,sid_ColumnName,sid_ColumnType,prjcode,Content)");
            builder.Append(" values (");
            builder.Append("@ID,@file_sid,@file_name,@file_info,@file_mark,@Original_table,@sid_ColumnName,@sid_ColumnType,@prjcode,@Content)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@file_sid", SqlDbType.VarChar, 0x40), new SqlParameter("@file_name", SqlDbType.VarChar, 100), new SqlParameter("@file_info", SqlDbType.VarChar), new SqlParameter("@file_mark", SqlDbType.Int, 4), new SqlParameter("@Original_table", SqlDbType.VarChar, 50), new SqlParameter("@sid_ColumnName", SqlDbType.VarChar, 50), new SqlParameter("@prjcode", SqlDbType.VarChar, 0x40), new SqlParameter("@sid_ColumnType", SqlDbType.Int, 4), new SqlParameter("@Content", SqlDbType.NVarChar) };
            commandParameters[0].Value = Guid.NewGuid();
            commandParameters[1].Value = model.file_sid;
            commandParameters[2].Value = model.file_name;
            commandParameters[3].Value = model.file_info;
            commandParameters[4].Value = model.file_mark;
            commandParameters[5].Value = model.Original_table;
            commandParameters[6].Value = model.Sid_ColumnName;
            commandParameters[9].Value = model.Content;
            if ((model.Prjcode == null) || (model.Prjcode.ToString() == ""))
            {
                commandParameters[7].Value = DBNull.Value;
            }
            else
            {
                commandParameters[7].Value = model.Prjcode;
            }
            commandParameters[8].Value = model.Sid_ColumnType;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Delete(Guid ID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Files ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = ID;
            int num = 0;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
            return (num > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Files ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null) > 0);
        }

        public bool Exists(Guid ID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Files");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = ID;
            int num = 0;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
            return (num > 0);
        }

        public bool Exists(string sid, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Files");
            builder.Append(" where file_sid=@file_sid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@file_sid", SqlDbType.VarChar, 0x40) };
            commandParameters[0].Value = sid;
            int num = 0;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
            return (num > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,file_sid,file_name,file_info,file_mark,Original_table,sid_ColumnName,sid_ColumnType,prjcode,Content ");
            builder.Append(" FROM Files ");
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
            builder.Append(" ID,file_sid,file_name,file_info,file_mark,Original_table,sid_ColumnName,sid_ColumnType,prjcode,Content ");
            builder.Append(" FROM Files ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetList(string TABLENAME, string id, string rowName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM  ").Append(TABLENAME).Append(" WHERE ").Append(rowName).Append(" = ").Append(id);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public filesModel GetModel(Guid ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,file_sid,file_name,file_info,file_mark,Original_table ,sid_ColumnName,sid_ColumnType,prjcode,Content from Files ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = ID;
            filesModel model = new filesModel();
            new DataSet();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if ((table.Rows[0]["ID"] != null) && (table.Rows[0]["ID"].ToString() != ""))
            {
                model.ID = new Guid(table.Rows[0]["ID"].ToString());
            }
            if ((table.Rows[0]["file_sid"] != null) && (table.Rows[0]["file_sid"].ToString() != ""))
            {
                model.file_sid = table.Rows[0]["file_sid"].ToString();
            }
            if ((table.Rows[0]["file_name"] != null) && (table.Rows[0]["file_name"].ToString() != ""))
            {
                model.file_name = table.Rows[0]["file_name"].ToString();
            }
            if ((table.Rows[0]["file_info"] != null) && (table.Rows[0]["file_info"].ToString() != ""))
            {
                model.file_info = table.Rows[0]["file_info"].ToString();
            }
            if ((table.Rows[0]["file_mark"] != null) && (table.Rows[0]["file_mark"].ToString() != ""))
            {
                model.file_mark = new int?(int.Parse(table.Rows[0]["file_mark"].ToString()));
            }
            if ((table.Rows[0]["Original_table"] != null) && (table.Rows[0]["Original_table"].ToString() != ""))
            {
                model.Original_table = table.Rows[0]["Original_table"].ToString();
            }
            if ((table.Rows[0]["sid_ColumnName"] != null) && (table.Rows[0]["sid_ColumnName"].ToString() != ""))
            {
                model.Sid_ColumnName = table.Rows[0]["sid_ColumnName"].ToString();
            }
            if ((table.Rows[0]["prjcode"] != null) && (table.Rows[0]["prjcode"].ToString() != ""))
            {
                model.Prjcode = table.Rows[0]["prjcode"].ToString();
            }
            if ((table.Rows[0]["Content"] != null) && (table.Rows[0]["Content"].ToString() != ""))
            {
                model.Content = table.Rows[0]["Content"].ToString();
            }
            return model;
        }

        public bool Update(filesModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Files set ");
            builder.Append("file_sid=@file_sid,");
            builder.Append("file_name=@file_name,");
            builder.Append("file_info=@file_info,");
            builder.Append("file_mark=@file_mark,");
            builder.Append("Original_table=@Original_table");
            builder.Append("sid_ColumnName=@sid_ColumnName");
            builder.Append("sid_ColumnType=@sid_ColumnType");
            builder.Append("Content=@Content,");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@file_sid", SqlDbType.VarChar, 0x40), new SqlParameter("@file_name", SqlDbType.VarChar, 100), new SqlParameter("@file_info", SqlDbType.VarChar), new SqlParameter("@file_mark", SqlDbType.Int, 4), new SqlParameter("@Original_table", SqlDbType.VarChar, 50), new SqlParameter("@sid_ColumnName", SqlDbType.VarChar, 50), new SqlParameter("@sid_ColumnType", SqlDbType.Int, 4), new SqlParameter("@Content", SqlDbType.NVarChar), new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = model.file_sid;
            commandParameters[1].Value = model.file_name;
            commandParameters[2].Value = model.file_info;
            commandParameters[3].Value = model.file_mark;
            commandParameters[4].Value = model.Original_table;
            commandParameters[5].Value = model.Sid_ColumnName;
            commandParameters[6].Value = model.Sid_ColumnType;
            commandParameters[7].Value = model.Content;
            commandParameters[8].Value = model.ID;
            int num = 0;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
            return (num > 0);
        }

        public bool Update(string ids, int marks, string table_name, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Files set ");
            builder.Append("file_mark=@file_mark");
            builder.Append(" where file_sid in ('@file_sid') and Original_table=@Original_table ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@file_sid", SqlDbType.VarChar), new SqlParameter("@file_mark", SqlDbType.Int, 4), new SqlParameter("@Original_table", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = ids;
            commandParameters[1].Value = marks;
            commandParameters[2].Value = table_name;
            int num = 0;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
            return (num > 0);
        }

        public bool Update(string idvals, string tableName, string rowName, int rowType, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ");
            builder.Append(tableName).Append(" ");
            builder.Append(" set  mark=3").Append(" ");
            builder.Append(" where ");
            builder.Append(rowName).Append(" in ").Append("( ").Append(idvals).Append(" )");
            int num = 0;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), null);
            }
            return (num > 0);
        }
    }
}

