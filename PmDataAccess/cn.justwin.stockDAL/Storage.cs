namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Storage
    {
        public int Add(SqlTransaction trans, StorageModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Storage(");
            builder.Append("sid,scode,tcode,flowstate,person,intime,inflag,annx,explain,project,isfirst,trustee,supervisor,isintime)");
            builder.Append(" values (");
            builder.Append("@sid,@scode,@tcode,@flowstate,@person,@intime,@inflag,@annx,@explain,@project,@isfirst,@trustee,@supervisor,@isintime)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@sid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.NVarChar, 0x40), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@inflag", SqlDbType.Bit, 1), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@project", SqlDbType.NVarChar, 0x40), new SqlParameter("@isfirst", SqlDbType.Bit, 1), new SqlParameter("@trustee", SqlDbType.NVarChar, 50), new SqlParameter("@supervisor", SqlDbType.NVarChar, 50), new SqlParameter("@isintime", SqlDbType.SmallDateTime) };
            commandParameters[0].Value = model.sid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.tcode;
            commandParameters[3].Value = model.flowstate;
            commandParameters[4].Value = model.person;
            commandParameters[5].Value = model.intime;
            commandParameters[6].Value = model.inflag;
            commandParameters[7].Value = model.annx;
            commandParameters[8].Value = model.explain;
            commandParameters[9].Value = model.project;
            commandParameters[10].Value = model.isfirst;
            commandParameters[11].Value = model.Trustee;
            commandParameters[12].Value = model.Supervisor;
            commandParameters[13].Value = model.IsInTime;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DelByTrans(SqlTransaction trans, List<string> lstStorageCode)
        {
            int num = 0;
            foreach (string str in lstStorageCode)
            {
                this.Delete(trans, str);
                num++;
            }
            return num;
        }

        public int Delete(string scode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Storage ");
            builder.Append(" where scode=@scode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@scode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = scode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(List<string> lstStorageCode)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in lstStorageCode)
                    {
                        this.Delete(trans, str);
                        num++;
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    num = 0;
                }
            }
            return num;
        }

        private int Delete(SqlTransaction trans, string storageCode)
        {
            string cmdText = "delete from Sm_Storage_Stock where stcode =@scode";
            string str2 = "delete from Sm_Storage where scode =@scode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@scode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = storageCode;
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, commandParameters);
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str2, commandParameters);
            return 1;
        }

        public string GetCodeFromGuid(string guid)
        {
            string cmdText = "select scode from Sm_Storage where sid = @sid";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@sid", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = guid;
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        private StorageModel GetModel(IDataReader dr)
        {
            StorageModel model = new StorageModel {
                sid = DBHelper.GetString(dr["sid"]),
                scode = DBHelper.GetString(dr["scode"]),
                tcode = DBHelper.GetString(dr["tcode"]),
                flowstate = DBHelper.GetInt(dr["flowstate"]),
                person = DBHelper.GetString(dr["person"]),
                intime = DBHelper.GetDateTime(dr["intime"]),
                inflag = DBHelper.GetBool(dr["inflag"]),
                annx = DBHelper.GetString(dr["annx"]),
                explain = DBHelper.GetString(dr["explain"]),
                project = DBHelper.GetString(dr["project"]),
                isfirst = DBHelper.GetBool(dr["isfirst"]),
                Trustee = DBHelper.GetString(dr["trustee"]),
                Supervisor = DBHelper.GetString(dr["supervisor"])
            };
            if (!string.IsNullOrEmpty(dr["isintime"].ToString()))
            {
                model.IsInTime = DBHelper.GetSqlDateTime(dr["isintime"]);
            }
            return model;
        }

        public StorageModel GetModel(string scode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Sm_Storage ");
            builder.Append(" where scode=@scode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@scode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = scode;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public StorageModel GetModelBySid(string sid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Sm_Storage ");
            builder.Append(" where sid=@sid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@sid", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = sid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public DataTable GetTable(string condition)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sid,Sm_Storage.scode,Sm_Storage.tcode,tname,flowstate,person,isintime,");
            builder.Append("Convert(varchar(10),intime,120) as intime,inflag,annx,explain,project,isfirst,trustee,supervisor ");
            builder.Append("from Sm_Storage ");
            builder.Append("join Sm_Treasury on Sm_Storage.tcode = Sm_Treasury.tcode ");
            if (!string.IsNullOrEmpty(condition))
            {
                builder.Append("where 1=1 ").Append(condition);
            }
            builder.Append("order by intime desc,scode desc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        private void PrepareUpdateCommand(StorageModel model, out string cmdText, out SqlParameter[] parameters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Storage set ");
            builder.Append("sid=@sid,");
            builder.Append("tcode=@tcode,");
            builder.Append("flowstate=@flowstate,");
            builder.Append("person=@person,");
            builder.Append("intime=@intime,");
            builder.Append("inflag=@inflag,");
            builder.Append("annx=@annx,");
            builder.Append("explain=@explain,");
            builder.Append("project=@project,");
            builder.Append("isfirst=@isfirst, ");
            builder.Append("trustee=@trustee, ");
            builder.Append("supervisor=@supervisor, ");
            builder.Append("isintime=@isintime ");
            builder.Append(" where scode=@scode ");
            cmdText = builder.ToString();
            parameters = new SqlParameter[] { new SqlParameter("@sid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.NVarChar, 0x40), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@inflag", SqlDbType.Bit, 1), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@project", SqlDbType.NVarChar, 0x40), new SqlParameter("@isfirst", SqlDbType.Bit, 1), new SqlParameter("@trustee", SqlDbType.NVarChar, 50), new SqlParameter("@supervisor", SqlDbType.NVarChar, 50), new SqlParameter("@isintime", SqlDateTime.MinValue) };
            parameters[0].Value = model.sid;
            parameters[1].Value = model.scode;
            parameters[2].Value = model.tcode;
            parameters[3].Value = model.flowstate;
            parameters[4].Value = model.person;
            parameters[5].Value = model.intime;
            parameters[6].Value = model.inflag;
            parameters[7].Value = model.annx;
            parameters[8].Value = model.explain;
            parameters[9].Value = model.project;
            parameters[10].Value = model.isfirst;
            parameters[11].Value = model.Trustee;
            parameters[12].Value = model.Supervisor;
            parameters[13].Value = model.IsInTime;
        }

        public int Update(StorageModel model)
        {
            SqlParameter[] parameterArray;
            string cmdText = string.Empty;
            this.PrepareUpdateCommand(model, out cmdText, out parameterArray);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText.ToString(), parameterArray);
        }

        public int Update(SqlTransaction trans, StorageModel model)
        {
            SqlParameter[] parameterArray;
            string cmdText = string.Empty;
            this.PrepareUpdateCommand(model, out cmdText, out parameterArray);
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, parameterArray);
        }
    }
}

