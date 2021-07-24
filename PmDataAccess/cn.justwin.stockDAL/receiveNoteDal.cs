namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class receiveNoteDal
    {
        public int Add(SqlTransaction trans, sm_receiveNote model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into sm_receiveNote(");
            builder.Append("rnId,rnCode,snId,rnTime,rnUser,remark,stId,soID,explain)");
            builder.Append(" values (");
            builder.Append("@rnId,@rnCode,@snId,@rnTime,@rnUser,@remark,@stId,@soID,@explain)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rnId", SqlDbType.NVarChar, 0x40), new SqlParameter("@rnCode", SqlDbType.NVarChar, 50), new SqlParameter("@snId", SqlDbType.NVarChar, 0x40), new SqlParameter("@rnTime", SqlDbType.DateTime), new SqlParameter("@rnUser", SqlDbType.NVarChar, 50), new SqlParameter("@remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@stId", SqlDbType.NVarChar, 0x40), new SqlParameter("@soID", SqlDbType.NVarChar, 0x40), new SqlParameter("@explain", SqlDbType.VarChar, 0x3e8) };
            commandParameters[0].Value = model.rnId;
            commandParameters[1].Value = model.rnCode;
            commandParameters[2].Value = model.snId;
            commandParameters[3].Value = model.rnTime;
            commandParameters[4].Value = model.rnUser;
            commandParameters[5].Value = model.remark;
            commandParameters[6].Value = model.stId;
            commandParameters[7].Value = model.soId;
            commandParameters[8].Value = model.Explain;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string rnId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from sm_receiveNote ");
            builder.Append(" where rnId=@rnId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rnId", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = rnId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteList(SqlTransaction trans, string rnIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from sm_receiveNote ");
            builder.Append(" where rnId in (" + rnIdlist + ")  ");
            return DBHelper.ExecuteNoQuery(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select rnId,rnCode,snId,rnTime,rnUser,remark,stId,soID,explain ");
            builder.Append(" FROM sm_receiveNote ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DBHelper.GetTable(builder.ToString());
        }

        public List<sm_receiveNote> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM sm_receiveNote ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            builder.AppendFormat(" order by rnTime desc ", new object[0]);
            List<sm_receiveNote> list = new List<sm_receiveNote>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<sm_receiveNote> GetListByTimeAndNum(string startTime, string endTime, string rnCode, string rnUser)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select rnId,rnCode,snId,rnTime,rnUser,remark,stId,soID,explain,sAllocationId ");
            builder.Append(" FROM sm_receiveNote ");
            builder.Append("  where rnCode like @rnCode ");
            list.Add(new SqlParameter("@rnCode", '%' + rnCode + '%'));
            if (!string.IsNullOrEmpty(rnUser))
            {
                builder.Append(" and rnUser=@rnUser");
                list.Add(new SqlParameter("@rnUser", rnUser));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and rnTime>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and rnTime<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            builder.AppendFormat(" order by rnTime desc ", new object[0]);
            List<sm_receiveNote> list2 = new List<sm_receiveNote>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), list.ToArray()))
            {
                while (reader.Read())
                {
                    list2.Add(this.ReaderBind(reader));
                }
            }
            return list2;
        }

        public List<sm_receiveNote> GetListByTimeAndNum(string startTime, string endTime, string rnCode, string rnUser, string prjId)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select rNote.rnId,rNote.rnCode,rNote.snId,rNote.rnTime,rNote.rnUser,rNote.remark,rNote.stId,rNote.soID,rNote.explain,sAllocationId ");
            builder.Append(" FROM sm_receiveNote rNote LEFT JOIN PT_yhmc ON rNote.rnUser=PT_yhmc.v_yhdm ");
            builder.Append(" LEFT JOIN sm_SendNote sNote ON  rNote.snId=sNote.snId ");
            if (!string.IsNullOrEmpty(prjId))
            {
                builder.Append(" where prjCode=@prjCode");
                list.Add(new SqlParameter("@prjCode", prjId));
            }
            else
            {
                builder.Append(" where prjCode is null ");
            }
            if (!string.IsNullOrEmpty(rnCode))
            {
                builder.Append("  and rnCode like @rnCode ");
                list.Add(new SqlParameter("@rnCode", '%' + rnCode + '%'));
            }
            if (!string.IsNullOrEmpty(rnUser))
            {
                builder.Append(" and v_xm LIKE @rnUser");
                list.Add(new SqlParameter("@rnUser", '%' + rnUser + '%'));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and rNote.rnTime>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and rNote.rnTime<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            builder.AppendFormat(" order by rNote.rnTime desc ", new object[0]);
            List<sm_receiveNote> list2 = new List<sm_receiveNote>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), list.ToArray()))
            {
                while (reader.Read())
                {
                    list2.Add(this.ReaderBind(reader));
                }
            }
            return list2;
        }

        public sm_receiveNote GetModel(string rnId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 rnId,rnCode,snId,rnTime,rnUser,remark,stId,soID,explain,sAllocationId from sm_receiveNote ");
            builder.Append(" where rnId=@rnId ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@rnId", SqlDbType.NVarChar, 50) };
            parameterArray[0].Value = rnId;
            sm_receiveNote note = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@rnId", rnId) }))
            {
                if (reader.Read())
                {
                    note = this.ReaderBind(reader);
                }
            }
            return note;
        }

        public DataTable GetNode(string sqlWhe)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select r.*, p.prjName,p.prjCode,s.snCode,s.sendState ");
            builder.Append("from dbo.sm_receiveNote as r inner join sm_SendNote as s on r.snid=s.snid ");
            builder.Append(" inner join pt_prjInfo as p on s.prjCode=p.prjGuid ");
            builder.Append(" inner join dbo.PT_yhmc on r.rnUser=PT_yhmc.v_yhdm ");
            builder.Append(sqlWhe);
            return DBHelper.GetTable(builder.ToString());
        }

        public sm_receiveNote GetRNBySnid(string snid)
        {
            string cmdText = "select * from sm_receiveNote where snId=@snid ";
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@snid", SqlDbType.NVarChar, 0x40) };
            parameterArray[0].Value = snid;
            sm_receiveNote note = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, new SqlParameter[] { new SqlParameter("@snid", snid) }))
            {
                if (reader.Read())
                {
                    note = this.ReaderBind(reader);
                }
            }
            return note;
        }

        public sm_receiveNote ReaderBind(IDataReader dataReader)
        {
            sm_receiveNote note = new sm_receiveNote {
                rnId = dataReader["rnId"].ToString(),
                rnCode = dataReader["rnCode"].ToString(),
                snId = dataReader["snId"].ToString()
            };
            if (dataReader["rnTime"].ToString() != "")
            {
                note.rnTime = new DateTime?(DateTime.Parse(dataReader["rnTime"].ToString()));
            }
            note.rnUser = dataReader["rnUser"].ToString();
            note.remark = dataReader["remark"].ToString();
            note.stId = dataReader["stId"].ToString();
            note.soId = dataReader["soID"].ToString();
            note.Explain = dataReader["explain"].ToString();
            note.SAllocationId = dataReader["sAllocationId"].ToString();
            return note;
        }

        public int Update(SqlTransaction trans, sm_receiveNote model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update sm_receiveNote set ");
            builder.Append("rnCode=@rnCode,");
            builder.Append("snId=@snId,");
            builder.Append("rnTime=@rnTime,");
            builder.Append("rnUser=@rnUser,");
            builder.Append("remark=@remark,");
            builder.Append("explain=@explain,");
            builder.Append("stId=@stId,");
            builder.Append("soId=@soId,");
            builder.Append("sAllocationId=@sAllocationId");
            builder.Append(" where rnId=@rnId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rnCode", SqlDbType.NVarChar, 50), new SqlParameter("@snId", SqlDbType.NVarChar, 0x40), new SqlParameter("@rnTime", SqlDbType.DateTime), new SqlParameter("@rnUser", SqlDbType.NVarChar, 50), new SqlParameter("@remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@rnId", SqlDbType.NVarChar, 0x40), new SqlParameter("@explain", SqlDbType.VarChar, 0x3e8), new SqlParameter("@stId", SqlDbType.VarChar, 0x40), new SqlParameter("@soId", SqlDbType.VarChar, 0x40), new SqlParameter("@sAllocationId", SqlDbType.VarChar, 0x40) };
            commandParameters[0].Value = model.rnCode;
            commandParameters[1].Value = model.snId;
            commandParameters[2].Value = model.rnTime;
            commandParameters[3].Value = model.rnUser;
            commandParameters[4].Value = model.remark;
            commandParameters[5].Value = model.rnId;
            commandParameters[6].Value = model.Explain;
            commandParameters[7].Value = model.stId;
            commandParameters[8].Value = model.soId;
            if (!string.IsNullOrEmpty(model.SAllocationId))
            {
                commandParameters[9].Value = model.SAllocationId;
            }
            else
            {
                commandParameters[9].Value = DBNull.Value;
            }
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

