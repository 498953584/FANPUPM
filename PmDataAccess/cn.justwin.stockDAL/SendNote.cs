namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SendNote
    {
        public int Add(SqlTransaction trans, SendNodteModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into sm_SendNote(");
            builder.Append("snId,snCode,snAddTime,snAddUser,sendState,remark,prjCode,limits)");
            builder.Append(" values (");
            builder.Append("@snId,@snCode,@snAddTime,@snAddUser,@sendState,@remark,@prjCode,@limits)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@snId", SqlDbType.NVarChar, 0x40), new SqlParameter("@snCode", SqlDbType.VarChar, 100), new SqlParameter("@snAddTime", SqlDbType.DateTime), new SqlParameter("@snAddUser", SqlDbType.VarChar, 50), new SqlParameter("@sendState", SqlDbType.Int, 4), new SqlParameter("@remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@prjCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@limits", SqlDbType.VarChar, 0x3e8) };
            commandParameters[0].Value = model.snId;
            commandParameters[1].Value = model.snCode;
            commandParameters[2].Value = model.snAddTime;
            commandParameters[3].Value = model.snAddUser;
            commandParameters[4].Value = model.sendState;
            commandParameters[5].Value = model.remark;
            commandParameters[6].Value = model.prjCode;
            commandParameters[7].Value = model.Limits;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string snId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from sm_SendNote ");
            builder.Append(" where snId=@snId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@snId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = snId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteList(SqlTransaction trans, string snIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from sm_SendNote ");
            builder.Append(" where snId in (" + snIdlist + ")  ");
            return DBHelper.ExecuteNoQuery(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select snId,snCode,snAddTime,snAddUser,sendState,remark,prjCode,limits ");
            builder.Append(" FROM sm_SendNote ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DBHelper.GetTable(builder.ToString());
        }

        public List<SendNodteModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM sm_SendNote ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            builder.AppendFormat(" order by snAddTime desc ", new object[0]);
            List<SendNodteModel> list = new List<SendNodteModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<SendNodteModel> GetListByTimeAndNum(string startTime, string endTime, string snCode, string person, string project, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select snId,snCode,snAddTime,snAddUser,sendState,remark,prjCode,limits");
            builder.Append(" FROM sm_SendNote LEFT JOIN PT_yhmc ON sm_SendNote.snAddUser=PT_yhmc.v_yhdm  ");
            builder.Append("  where limits like '%'+@userCode+'%' and snCode like @snCode ");
            list.Add(new SqlParameter("@snCode", '%' + snCode + '%'));
            list.Add(new SqlParameter("@userCode", userCode));
            if (!string.IsNullOrEmpty(person))
            {
                builder.Append(" and v_xm LIKE @snAddUser");
                list.Add(new SqlParameter("@snAddUser", '%' + person + '%'));
            }
            if (!string.IsNullOrEmpty(project))
            {
                builder.Append(" and prjCode=@prjCode");
                list.Add(new SqlParameter("@prjCode", project));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and snAddTime>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and snAddTime<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            builder.AppendFormat(" order by snAddTime desc ", new object[0]);
            List<SendNodteModel> list2 = new List<SendNodteModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), list.ToArray()))
            {
                while (reader.Read())
                {
                    list2.Add(this.ReaderBind(reader));
                }
            }
            return list2;
        }

        public List<SendNodteModel> GetListByTimeAndNum1(string startTime, string endTime, string snCode, string person, string project)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select snId,snCode,snAddTime,snAddUser,sendState,remark,prjCode,limits");
            builder.Append(" FROM sm_SendNote  LEFT JOIN PT_yhmc ON sm_SendNote.snAddUser=PT_yhmc.v_yhdm ");
            builder.Append("  where snCode like @snCode ");
            list.Add(new SqlParameter("@snCode", '%' + snCode + '%'));
            if (!string.IsNullOrEmpty(person))
            {
                builder.Append(" and v_xm LIKE @snAddUser");
                list.Add(new SqlParameter("@snAddUser", '%' + person + '%'));
            }
            if (!string.IsNullOrEmpty(project))
            {
                builder.Append(" and prjCode=@prjCode");
                list.Add(new SqlParameter("@prjCode", project));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and snAddTime>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and snAddTime<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            builder.AppendFormat(" order by snAddTime desc ", new object[0]);
            List<SendNodteModel> list2 = new List<SendNodteModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), list.ToArray()))
            {
                while (reader.Read())
                {
                    list2.Add(this.ReaderBind(reader));
                }
            }
            return list2;
        }

        public SendNodteModel GetModel(string snId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select snId,snCode,snAddTime,snAddUser,sendState,remark,prjCode,limits from sm_SendNote ");
            builder.Append(" where snId=@snId ");
            SendNodteModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@snId", snId) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public SendNodteModel ReaderBind(IDataReader dataReader)
        {
            SendNodteModel model = new SendNodteModel {
                snId = dataReader["snId"].ToString(),
                snCode = dataReader["snCode"].ToString()
            };
            if (dataReader["snAddTime"].ToString() != "")
            {
                model.snAddTime = new DateTime?(DateTime.Parse(dataReader["snAddTime"].ToString()));
            }
            model.snAddUser = dataReader["snAddUser"].ToString();
            if (dataReader["sendState"].ToString() != "")
            {
                model.sendState = new int?(int.Parse(dataReader["sendState"].ToString()));
            }
            model.remark = dataReader["remark"].ToString();
            if (dataReader["prjCode"].ToString() != "")
            {
                model.prjCode = new Guid(dataReader["prjCode"].ToString());
            }
            model.Limits = dataReader["limits"].ToString();
            return model;
        }

        public int Update(SqlTransaction trans, SendNodteModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update sm_SendNote set ");
            builder.Append("snCode=@snCode,");
            builder.Append("snAddTime=@snAddTime,");
            builder.Append("snAddUser=@snAddUser,");
            builder.Append("sendState=@sendState,");
            builder.Append("remark=@remark,");
            builder.Append("prjCode=@prjCode,");
            builder.Append("limits=@limits");
            builder.Append(" where snId=@snId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@snCode", SqlDbType.VarChar, 100), new SqlParameter("@snAddTime", SqlDbType.DateTime), new SqlParameter("@snAddUser", SqlDbType.VarChar, 50), new SqlParameter("@sendState", SqlDbType.Int, 4), new SqlParameter("@remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@prjCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@snId", SqlDbType.NVarChar, 0x40), new SqlParameter("@limits", SqlDbType.VarChar, 0x3e8) };
            commandParameters[0].Value = model.snCode;
            commandParameters[1].Value = model.snAddTime;
            commandParameters[2].Value = model.snAddUser;
            commandParameters[3].Value = model.sendState;
            commandParameters[4].Value = model.remark;
            commandParameters[5].Value = model.prjCode;
            commandParameters[6].Value = model.snId;
            commandParameters[7].Value = model.Limits;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int UpdateState(SqlTransaction trans, string snId)
        {
            string cmdText = " update dbo.sm_SendNote set sendState=1 where snid='" + snId + "'";
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[0]);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public int UpdateStateNo(SqlTransaction trans, string snId)
        {
            string cmdText = " update dbo.sm_SendNote set sendState=0 where snid='" + snId + "'";
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[0]);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, new SqlParameter[0]);
        }
    }
}

