namespace cn.justwin.stockDAL.PTBULLETINMAIN
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PTBulletinMain
    {
        public int Add(PTBulletinMainModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_BULLETIN_MAIN(");
            builder.Append("I_BULLETINID,CorpCode,V_RELUSERCODE,V_RELEASEUSER,V_TITLE,V_CONTENT,URL,DTM_RELEASETIME,DTM_EXPRIESDATE,I_RELEASEBOUND,DeptRange,AuditState)");
            builder.Append(" values (");
            builder.Append("@I_BULLETINID,@CorpCode,@V_RELUSERCODE,@V_RELEASEUSER,@V_TITLE,@V_CONTENT,@URL,@DTM_RELEASETIME,@DTM_EXPRIESDATE,@I_RELEASEBOUND,@DeptRange,@AuditState)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@I_BULLETINID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@CorpCode", SqlDbType.VarChar, 20), new SqlParameter("@V_RELUSERCODE", SqlDbType.VarChar, 8), new SqlParameter("@V_RELEASEUSER", SqlDbType.VarChar, 50), new SqlParameter("@V_TITLE", SqlDbType.VarChar, 200), new SqlParameter("@V_CONTENT", SqlDbType.VarChar), new SqlParameter("@URL", SqlDbType.VarChar, 100), new SqlParameter("@DTM_RELEASETIME", SqlDbType.DateTime), new SqlParameter("@DTM_EXPRIESDATE", SqlDbType.DateTime), new SqlParameter("@I_RELEASEBOUND", SqlDbType.Int, 4), new SqlParameter("@DeptRange", SqlDbType.VarChar, 200), new SqlParameter("@AuditState", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.I_BULLETINID;
            commandParameters[1].Value = model.CorpCode;
            commandParameters[2].Value = model.V_RELUSERCODE;
            commandParameters[3].Value = model.V_RELEASEUSER;
            commandParameters[4].Value = model.V_TITLE;
            commandParameters[5].Value = model.V_CONTENT;
            commandParameters[6].Value = model.URL;
            commandParameters[7].Value = model.DTM_RELEASETIME;
            commandParameters[8].Value = model.DTM_EXPRIESDATE;
            commandParameters[9].Value = model.I_RELEASEBOUND;
            commandParameters[10].Value = model.DeptRange;
            commandParameters[11].Value = model.AuditState;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string I_BULLETINID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PT_BULLETIN_MAIN ");
            builder.Append(" where I_BULLETINID=@I_BULLETINID ");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@I_BULLETINID", I_BULLETINID) });
        }

        public List<PTBulletinMainModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select I_BULLETINID,CorpCode,V_RELUSERCODE,V_RELEASEUSER,V_TITLE,V_CONTENT,URL,DTM_RELEASETIME,DTM_EXPRIESDATE,I_RELEASEBOUND,DeptRange,AuditState ");
            builder.Append(" FROM PT_BULLETIN_MAIN ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<PTBulletinMainModel> list = new List<PTBulletinMainModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public PTBulletinMainModel GetModel(string I_BULLETINID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select I_BULLETINID,CorpCode,V_RELUSERCODE,V_RELEASEUSER,V_TITLE,V_CONTENT,URL,DTM_RELEASETIME,DTM_EXPRIESDATE,I_RELEASEBOUND,DeptRange,AuditState from PT_BULLETIN_MAIN ");
            builder.Append(" where I_BULLETINID=@I_BULLETINID ");
            PTBulletinMainModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@I_BULLETINID", I_BULLETINID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public PTBulletinMainModel ReaderBind(IDataReader dataReader)
        {
            PTBulletinMainModel model = new PTBulletinMainModel();
            object obj2 = dataReader["I_BULLETINID"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.I_BULLETINID = new Guid(obj2.ToString());
            }
            model.CorpCode = dataReader["CorpCode"].ToString();
            model.V_RELUSERCODE = dataReader["V_RELUSERCODE"].ToString();
            model.V_RELEASEUSER = dataReader["V_RELEASEUSER"].ToString();
            model.V_TITLE = dataReader["V_TITLE"].ToString();
            model.V_CONTENT = dataReader["V_CONTENT"].ToString();
            model.URL = dataReader["URL"].ToString();
            obj2 = dataReader["DTM_RELEASETIME"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.DTM_RELEASETIME = (DateTime) obj2;
            }
            obj2 = dataReader["DTM_EXPRIESDATE"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.DTM_EXPRIESDATE = (DateTime) obj2;
            }
            obj2 = dataReader["I_RELEASEBOUND"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.I_RELEASEBOUND = (int) obj2;
            }
            model.DeptRange = dataReader["DeptRange"].ToString();
            obj2 = dataReader["AuditState"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.AuditState = (int) obj2;
            }
            return model;
        }

        public int Update(PTBulletinMainModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_BULLETIN_MAIN set ");
            builder.Append("CorpCode=@CorpCode,");
            builder.Append("V_RELUSERCODE=@V_RELUSERCODE,");
            builder.Append("V_RELEASEUSER=@V_RELEASEUSER,");
            builder.Append("V_TITLE=@V_TITLE,");
            builder.Append("V_CONTENT=@V_CONTENT,");
            builder.Append("URL=@URL,");
            builder.Append("DTM_RELEASETIME=@DTM_RELEASETIME,");
            builder.Append("DTM_EXPRIESDATE=@DTM_EXPRIESDATE,");
            builder.Append("I_RELEASEBOUND=@I_RELEASEBOUND,");
            builder.Append("DeptRange=@DeptRange,");
            builder.Append("AuditState=@AuditState");
            builder.Append(" where I_BULLETINID=@I_BULLETINID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@I_BULLETINID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@CorpCode", SqlDbType.VarChar, 20), new SqlParameter("@V_RELUSERCODE", SqlDbType.VarChar, 8), new SqlParameter("@V_RELEASEUSER", SqlDbType.VarChar, 50), new SqlParameter("@V_TITLE", SqlDbType.VarChar, 200), new SqlParameter("@V_CONTENT", SqlDbType.VarChar), new SqlParameter("@URL", SqlDbType.VarChar, 100), new SqlParameter("@DTM_RELEASETIME", SqlDbType.DateTime), new SqlParameter("@DTM_EXPRIESDATE", SqlDbType.DateTime), new SqlParameter("@I_RELEASEBOUND", SqlDbType.Int, 4), new SqlParameter("@DeptRange", SqlDbType.VarChar, 200), new SqlParameter("@AuditState", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.I_BULLETINID;
            commandParameters[1].Value = model.CorpCode;
            commandParameters[2].Value = model.V_RELUSERCODE;
            commandParameters[3].Value = model.V_RELEASEUSER;
            commandParameters[4].Value = model.V_TITLE;
            commandParameters[5].Value = model.V_CONTENT;
            commandParameters[6].Value = model.URL;
            commandParameters[7].Value = model.DTM_RELEASETIME;
            commandParameters[8].Value = model.DTM_EXPRIESDATE;
            commandParameters[9].Value = model.I_RELEASEBOUND;
            commandParameters[10].Value = model.DeptRange;
            commandParameters[11].Value = model.AuditState;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

