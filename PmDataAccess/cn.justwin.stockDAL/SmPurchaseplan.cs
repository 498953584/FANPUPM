namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SmPurchaseplan
    {
        public int Add(SqlTransaction trans, SmPurchaseplanModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Purchaseplan(");
            builder.Append("ppid,ppcode,flowstate,person,intime,acceptstate,annx,explain,Project)");
            builder.Append(" values (");
            builder.Append("@ppid,@ppcode,@flowstate,@person,@intime,@acceptstate,@annx,@explain,@Project)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ppid", SqlDbType.NVarChar, 50), new SqlParameter("@ppcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@acceptstate", SqlDbType.Int, 4), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@Project", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = model.ppid;
            commandParameters[1].Value = model.ppcode;
            commandParameters[2].Value = model.flowstate;
            commandParameters[3].Value = model.person;
            commandParameters[4].Value = model.intime;
            commandParameters[5].Value = model.acceptstate;
            commandParameters[6].Value = model.annx;
            commandParameters[7].Value = model.explain;
            commandParameters[8].Value = model.Project;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string ppcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Purchaseplan ");
            builder.Append(" where ppcode=@ppcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ppcode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ppcode;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<SmPurchaseplanModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Sm_Purchaseplan ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            builder.AppendFormat(" order by intime desc ", new object[0]);
            List<SmPurchaseplanModel> list = new List<SmPurchaseplanModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public List<SmPurchaseplanModel> GetListByTimeAndNum(string startTime, string endTime, string ppcode, string person, string project)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select p1.ppid,p1.ppcode,p1.flowstate,p1.person,");
            builder.Append("p1.intime,p1.acceptstate,p1.annx,p1.explain,p1.Project ");
            builder.Append(" FROM Sm_Purchaseplan as p1");
            builder.Append("  where ppcode like @ppcode ");
            list.Add(new SqlParameter("@ppcode", '%' + ppcode + '%'));
            if (!string.IsNullOrEmpty(person))
            {
                builder.Append(" and person=@person");
                list.Add(new SqlParameter("@person", person));
            }
            if (!string.IsNullOrEmpty(project))
            {
                builder.Append(" and project=@project");
                list.Add(new SqlParameter("@project", project));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and intime>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and intime<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            builder.AppendFormat(" order by intime desc ", new object[0]);
            List<SmPurchaseplanModel> list2 = new List<SmPurchaseplanModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), list.ToArray()))
            {
                while (reader.Read())
                {
                    list2.Add(this.ReaderBind(reader));
                }
            }
            return list2;
        }

        public SmPurchaseplanModel GetModel(string ppcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ppid,ppcode,flowstate,person,intime,acceptstate,annx,explain,Project from Sm_Purchaseplan ");
            builder.Append(" where ppcode=@ppcode ");
            SmPurchaseplanModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ppcode", ppcode) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public SmPurchaseplanModel GetModelByCid(string cid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ppid,ppcode,flowstate,person,intime,acceptstate,annx,explain,Project from Sm_Purchaseplan ");
            builder.Append(" where ppid=@ppid ");
            SmPurchaseplanModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ppid", cid) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public SmPurchaseplanModel ReaderBind(IDataReader dataReader)
        {
            SmPurchaseplanModel model = new SmPurchaseplanModel {
                ppid = dataReader["ppid"].ToString(),
                ppcode = dataReader["ppcode"].ToString()
            };
            object obj2 = dataReader["flowstate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.flowstate = (int) obj2;
            }
            model.person = dataReader["person"].ToString();
            obj2 = dataReader["intime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.intime = (DateTime) obj2;
            }
            obj2 = dataReader["acceptstate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.acceptstate = (int) obj2;
            }
            model.annx = dataReader["annx"].ToString();
            model.explain = dataReader["explain"].ToString();
            model.Project = dataReader["Project"].ToString();
            return model;
        }

        public int Update(SqlTransaction trans, SmPurchaseplanModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Purchaseplan set ");
            builder.Append("ppid=@ppid,");
            builder.Append("flowstate=@flowstate,");
            builder.Append("person=@person,");
            builder.Append("intime=@intime,");
            builder.Append("acceptstate=@acceptstate,");
            builder.Append("annx=@annx,");
            builder.Append("explain=@explain,");
            builder.Append("Project=@Project");
            builder.Append(" where ppcode=@ppcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ppid", SqlDbType.NVarChar, 50), new SqlParameter("@ppcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@acceptstate", SqlDbType.Int, 4), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@Project", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = model.ppid;
            commandParameters[1].Value = model.ppcode;
            commandParameters[2].Value = model.flowstate;
            commandParameters[3].Value = model.person;
            commandParameters[4].Value = model.intime;
            commandParameters[5].Value = model.acceptstate;
            commandParameters[6].Value = model.annx;
            commandParameters[7].Value = model.explain;
            commandParameters[8].Value = model.Project;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

