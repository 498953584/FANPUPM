namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PTDbsj
    {
        public int Add(PTDbsjModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_DBSJ(");
            builder.Append("I_XGID,V_LXBM,V_YHDM,DTM_DBSJ,V_Content,V_TPLJ,V_DBLJ,C_OpenFlag)");
            builder.Append(" values (");
            builder.Append("@I_XGID,@V_LXBM,@V_YHDM,@DTM_DBSJ,@V_Content,@V_TPLJ,@V_DBLJ,@C_OpenFlag)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@I_XGID", SqlDbType.VarChar, 50), new SqlParameter("@V_LXBM", SqlDbType.VarChar, 10), new SqlParameter("@V_YHDM", SqlDbType.VarChar, 8), new SqlParameter("@DTM_DBSJ", SqlDbType.DateTime), new SqlParameter("@V_Content", SqlDbType.VarChar, 200), new SqlParameter("@V_TPLJ", SqlDbType.VarChar, 50), new SqlParameter("@V_DBLJ", SqlDbType.VarChar, 200), new SqlParameter("@C_OpenFlag", SqlDbType.Char, 1) };
            commandParameters[0].Value = model.I_XGID;
            commandParameters[1].Value = model.V_LXBM;
            commandParameters[2].Value = model.V_YHDM;
            commandParameters[3].Value = model.DTM_DBSJ;
            commandParameters[4].Value = model.V_Content;
            commandParameters[5].Value = model.V_TPLJ;
            commandParameters[6].Value = model.V_DBLJ;
            commandParameters[7].Value = model.C_OpenFlag;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters));
        }

        public int Delete(int I_DBSJ_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PT_DBSJ ");
            builder.Append(" where I_DBSJ_ID=@I_DBSJ_ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@I_DBSJ_ID", SqlDbType.Int, 4) };
            commandParameters[0].Value = I_DBSJ_ID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public void DelPastDueData(string xgid)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("DELETE FROM PT_DBSJ  WHERE I_XGID='{0}'", xgid);
            builder.AppendLine();
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public bool Exists(string guid)
        {
            string cmdText = string.Format("SELECT COUNT(1) FROM PT_DBSJ WHERE I_XGID = '{0}' ", guid);
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, null)) > 0);
        }

        public List<PTDbsjModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select I_DBSJ_ID,I_XGID,V_LXBM,V_YHDM,DTM_DBSJ,V_Content,V_TPLJ,V_DBLJ,C_OpenFlag ");
            builder.Append(" FROM PT_DBSJ ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<PTDbsjModel> list = new List<PTDbsjModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public PTDbsjModel GetModel(int I_DBSJ_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select I_DBSJ_ID,I_XGID,V_LXBM,V_YHDM,DTM_DBSJ,V_Content,V_TPLJ,V_DBLJ,C_OpenFlag from PT_DBSJ ");
            builder.Append(" where I_DBSJ_ID=@I_DBSJ_ID ");
            PTDbsjModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@I_DBSJ_ID", I_DBSJ_ID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public PTDbsjModel GetModelByGID(string I_XGID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select I_DBSJ_ID,I_XGID,V_LXBM,V_YHDM,DTM_DBSJ,V_Content,V_TPLJ,V_DBLJ,C_OpenFlag from PT_DBSJ ");
            builder.Append(" where I_XGID=@I_XGID ");
            PTDbsjModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@I_XGID", I_XGID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public PTDbsjModel ReaderBind(IDataReader dataReader)
        {
            PTDbsjModel model = new PTDbsjModel();
            object obj2 = dataReader["I_DBSJ_ID"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.I_DBSJ_ID = (int) obj2;
            }
            model.I_XGID = dataReader["I_XGID"].ToString();
            model.V_LXBM = dataReader["V_LXBM"].ToString();
            model.V_YHDM = dataReader["V_YHDM"].ToString();
            obj2 = dataReader["DTM_DBSJ"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.DTM_DBSJ = (DateTime) obj2;
            }
            model.V_Content = dataReader["V_Content"].ToString();
            model.V_TPLJ = dataReader["V_TPLJ"].ToString();
            model.V_DBLJ = dataReader["V_DBLJ"].ToString();
            model.C_OpenFlag = dataReader["C_OpenFlag"].ToString();
            return model;
        }

        public int Update(PTDbsjModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_DBSJ set ");
            builder.Append("I_XGID=@I_XGID,");
            builder.Append("V_LXBM=@V_LXBM,");
            builder.Append("V_YHDM=@V_YHDM,");
            builder.Append("DTM_DBSJ=@DTM_DBSJ,");
            builder.Append("V_Content=@V_Content,");
            builder.Append("V_TPLJ=@V_TPLJ,");
            builder.Append("V_DBLJ=@V_DBLJ,");
            builder.Append("C_OpenFlag=@C_OpenFlag");
            builder.Append(" where I_DBSJ_ID=@I_DBSJ_ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@I_DBSJ_ID", SqlDbType.Int, 4), new SqlParameter("@I_XGID", SqlDbType.VarChar, 50), new SqlParameter("@V_LXBM", SqlDbType.VarChar, 10), new SqlParameter("@V_YHDM", SqlDbType.VarChar, 8), new SqlParameter("@DTM_DBSJ", SqlDbType.DateTime), new SqlParameter("@V_Content", SqlDbType.VarChar, 200), new SqlParameter("@V_TPLJ", SqlDbType.VarChar, 50), new SqlParameter("@V_DBLJ", SqlDbType.VarChar, 200), new SqlParameter("@C_OpenFlag", SqlDbType.Char, 1) };
            commandParameters[0].Value = model.I_DBSJ_ID;
            commandParameters[1].Value = model.I_XGID;
            commandParameters[2].Value = model.V_LXBM;
            commandParameters[3].Value = model.V_YHDM;
            commandParameters[4].Value = model.DTM_DBSJ;
            commandParameters[5].Value = model.V_Content;
            commandParameters[6].Value = model.V_TPLJ;
            commandParameters[7].Value = model.V_DBLJ;
            commandParameters[8].Value = model.C_OpenFlag;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

