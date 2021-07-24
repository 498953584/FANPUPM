namespace cn.justwin.epm.datum
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DatumService
    {
        public int Add(DatumModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into EPM_Datum_All(");
            builder.Append("i_id,PrjCode,AffairClass,AffairTitle,AddDate,Remark,Context,DatumClass,CA)");
            builder.Append(" values (");
            builder.Append("@i_id,@PrjCode,@AffairClass,@AffairTitle,@AddDate,@Remark,@Context,@DatumClass,@CA)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@i_id", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PrjCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@AffairClass", SqlDbType.VarChar, 30), new SqlParameter("@AffairTitle", SqlDbType.VarChar, 200), new SqlParameter("@AddDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.VarChar, 300), new SqlParameter("@Context", SqlDbType.VarChar, 0x7d0), new SqlParameter("@DatumClass", SqlDbType.Int, 4), new SqlParameter("@CA", SqlDbType.Int, 4) };
            commandParameters[0].Value = Guid.NewGuid();
            commandParameters[1].Value = model.PrjCode;
            commandParameters[2].Value = model.AffairClass;
            commandParameters[3].Value = model.AffairTitle;
            commandParameters[4].Value = model.AddDate;
            commandParameters[5].Value = model.Remark;
            commandParameters[6].Value = model.Context;
            commandParameters[7].Value = model.DatumClass;
            commandParameters[8].Value = model.CA;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Delete(Guid i_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from EPM_Datum_All ");
            builder.Append(" where i_id=@i_id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@i_id", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = i_id;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool DeleteList(string i_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from EPM_Datum_Affair ");
            builder.Append(" where i_id in (" + i_idlist + ")  ");
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null) > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select i_id,PrjCode,AffairClass,AffairTitle,AddDate,Remark,Context,DatumClass,CA ");
            builder.Append(" FROM EPM_Datum_All ");
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
            builder.Append(" i_id,PrjCode,AffairClass,AffairTitle,AddDate,Remark,Context,DatumClass,CA ");
            builder.Append(" FROM EPM_Datum_All ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public int getMaxID(string table)
        {
            int num = 0;
            string cmdText = "SELECT IDENT_CURRENT('" + table + "')+(SELECT ident_incr('" + table + "'))";
            SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, null);
            if (reader.Read())
            {
                reader.GetValue(0);
                if ((reader.GetValue(0) != null) && (reader.GetValue(0).ToString() != ""))
                {
                    num = int.Parse(reader.GetValue(0).ToString());
                }
            }
            return num;
        }

        public int getMaxID(string table, string idClomname)
        {
            int num = 0;
            string cmdText = "SELECT max(" + idClomname + ") FROM " + table + " ";
            SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, null);
            if (reader.Read())
            {
                reader.GetValue(0);
                if ((reader.GetValue(0) != null) && (reader.GetValue(0).ToString() != ""))
                {
                    num = int.Parse(reader.GetValue(0).ToString());
                }
            }
            return num;
        }

        public DatumModel GetModel(Guid i_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 i_id,PrjCode,AffairClass,AffairTitle,AddDate,Remark,Context,DatumClass,CA from EPM_Datum_All ");
            builder.Append(" where i_id=@i_id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@i_id", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = i_id;
            DatumModel model = new DatumModel();
            DataSet set = new DataSet();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            set.Tables.Add(table);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["i_id"] != null) && (set.Tables[0].Rows[0]["i_id"].ToString() != ""))
            {
                model.i_id = new Guid(set.Tables[0].Rows[0]["i_id"].ToString());
            }
            if ((set.Tables[0].Rows[0]["PrjCode"] != null) && (set.Tables[0].Rows[0]["PrjCode"].ToString() != ""))
            {
                model.PrjCode = new Guid(set.Tables[0].Rows[0]["PrjCode"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AffairClass"] != null) && (set.Tables[0].Rows[0]["AffairClass"].ToString() != ""))
            {
                model.AffairClass = set.Tables[0].Rows[0]["AffairClass"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AffairTitle"] != null) && (set.Tables[0].Rows[0]["AffairTitle"].ToString() != ""))
            {
                model.AffairTitle = set.Tables[0].Rows[0]["AffairTitle"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AddDate"] != null) && (set.Tables[0].Rows[0]["AddDate"].ToString() != ""))
            {
                model.AddDate = DateTime.Parse(set.Tables[0].Rows[0]["AddDate"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Remark"] != null) && (set.Tables[0].Rows[0]["Remark"].ToString() != ""))
            {
                model.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Context"] != null) && (set.Tables[0].Rows[0]["Context"].ToString() != ""))
            {
                model.Context = set.Tables[0].Rows[0]["Context"].ToString();
            }
            if ((set.Tables[0].Rows[0]["DatumClass"] != null) && (set.Tables[0].Rows[0]["DatumClass"].ToString() != ""))
            {
                model.DatumClass = int.Parse(set.Tables[0].Rows[0]["DatumClass"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CA"] != null) && (set.Tables[0].Rows[0]["CA"].ToString() != ""))
            {
                model.CA = new int?(int.Parse(set.Tables[0].Rows[0]["CA"].ToString()));
            }
            return model;
        }

        public bool Update(DatumModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update EPM_Datum_All set ");
            builder.Append("i_id=@i_id,");
            builder.Append("PrjCode=@PrjCode,");
            builder.Append("AffairClass=@AffairClass,");
            builder.Append("AffairTitle=@AffairTitle,");
            builder.Append("AddDate=@AddDate,");
            builder.Append("Remark=@Remark,");
            builder.Append("Context=@Context,");
            builder.Append("DatumClass=@DatumClass,");
            builder.Append("CA=@CA");
            builder.Append(" where i_id=@i_id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@i_id", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PrjCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@AffairClass", SqlDbType.VarChar, 30), new SqlParameter("@AffairTitle", SqlDbType.VarChar, 200), new SqlParameter("@AddDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.VarChar, 300), new SqlParameter("@Context", SqlDbType.VarChar, 0x7d0), new SqlParameter("@DatumClass", SqlDbType.Int, 4), new SqlParameter("@CA", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.i_id;
            commandParameters[1].Value = model.PrjCode;
            commandParameters[2].Value = model.AffairClass;
            commandParameters[3].Value = model.AffairTitle;
            commandParameters[4].Value = model.AddDate;
            commandParameters[5].Value = model.Remark;
            commandParameters[6].Value = model.Context;
            commandParameters[7].Value = model.DatumClass;
            commandParameters[8].Value = model.CA;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool UpdateByID(string tablename, string liename, string id, SqlTransaction trans)
        {
            if (!(id != ""))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE ").Append(tablename);
            builder.Append(" SET  [mark] = 1  WHERE  ").Append(liename).Append(" in(" + id + ")");
            return (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), null) > 0);
        }

        public bool UpdateByID(string tablename, int markVal, string idname, string idval, SqlTransaction trans)
        {
            if (!(idval != ""))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE " + tablename);
            builder.Append(" SET  [mark] = ").Append(markVal).Append(" ");
            builder.Append(" WHERE ").Append(idname).Append(" in ( ").Append(idval).Append(")");
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

        public bool UpdateByID(string tablename, string setName_val, string idname, string idval, SqlTransaction trans)
        {
            if (!(idval != ""))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE " + tablename);
            builder.Append(" SET ").Append(setName_val).Append(" ");
            builder.Append(" WHERE ").Append(idname).Append(" in ( ").Append(idval).Append(")");
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

