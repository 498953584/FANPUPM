namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Text;

    public class Refunding
    {
        public int Add(SqlTransaction trans, RefundingModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Refunding(");
            builder.Append("rid,rcode,procode,tcode,flowstate,isin,person,intime,annx,explain)");
            builder.Append(" values (");
            builder.Append("@rid,@rcode,@procode,@tcode,@flowstate,@isin,@person,@intime,@annx,@explain)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rid", SqlDbType.NVarChar, 50), new SqlParameter("@rcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@procode", SqlDbType.NVarChar, 0x40), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@isin", SqlDbType.Bit, 1), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800) };
            commandParameters[0].Value = model.rid;
            commandParameters[1].Value = model.rcode;
            commandParameters[2].Value = model.procode;
            commandParameters[3].Value = model.tcode;
            commandParameters[4].Value = model.flowstate;
            commandParameters[5].Value = model.isin;
            commandParameters[6].Value = model.person;
            commandParameters[7].Value = model.intime;
            commandParameters[8].Value = model.annx;
            commandParameters[9].Value = model.explain;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string rcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Refunding ");
            builder.Append(" where rcode=@rcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rcode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = rcode;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<RefundingModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select rid,rcode,procode,tcode,flowstate,isin,person,intime,annx,explain ");
            builder.Append(" FROM Sm_Refunding ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<RefundingModel> list = new List<RefundingModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public DataTable GetListByTimeAndNum(string startTime, string endTime, string rcode, string person, string procode, string tcode, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select p1.rid,p1.rcode,p1.procode,p1.tcode,p1.flowstate,p1.isin,p1.person,p1.intime,p1.annx,p1.explain,p2.tname,p3.prjName ");
            builder.Append(" FROM Sm_Refunding as p1");
            builder.Append(" inner join dbo.Sm_Treasury as p2 on p1.tcode=p2.tcode");
            builder.Append(" inner join dbo.PT_PrjInfo as p3 on p1.procode=p3.prjGuid");
            builder.Append("  where p1.rcode like @rcode ");
            builder.Append("  and p1.person like @person ");
            builder.Append(strWhere);
            list.Add(new SqlParameter("@rcode", '%' + rcode + '%'));
            list.Add(new SqlParameter("@person", '%' + person + '%'));
            if (!string.IsNullOrEmpty(procode))
            {
                builder.Append(" and p1.procode=@procode");
                list.Add(new SqlParameter("@procode", procode));
            }
            if (!string.IsNullOrEmpty(tcode))
            {
                builder.Append(" and p2.tname LIKE @tname");
                list.Add(new SqlParameter("@tname", '%' + tcode + '%'));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and p1.intime>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and p1.intime<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            builder.Append(" order by intime desc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public RefundingModel GetModel(string rcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select rid,rcode,procode,tcode,flowstate,isin,person,intime,annx,explain from Sm_Refunding ");
            builder.Append(" where rcode=@rcode ");
            RefundingModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@rcode", rcode) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public RefundingModel GetModelByIc(string ic)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select rid,rcode,procode,tcode,flowstate,isin,person,intime,annx,explain from Sm_Refunding ");
            builder.Append(" where rid=@rid ");
            RefundingModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@rid", ic) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public RefundingModel ReaderBind(IDataReader dataReader)
        {
            RefundingModel model = new RefundingModel {
                rid = dataReader["rid"].ToString(),
                rcode = dataReader["rcode"].ToString(),
                procode = dataReader["procode"].ToString(),
                tcode = dataReader["tcode"].ToString()
            };
            object obj2 = dataReader["flowstate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.flowstate = (int) obj2;
            }
            obj2 = dataReader["isin"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.isin = (bool) obj2;
            }
            model.person = dataReader["person"].ToString();
            obj2 = dataReader["intime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.intime = (DateTime) obj2;
            }
            model.annx = dataReader["annx"].ToString();
            model.explain = dataReader["explain"].ToString();
            return model;
        }

        public int Update(SqlTransaction trans, RefundingModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Refunding set ");
            builder.Append("rid=@rid,");
            builder.Append("procode=@procode,");
            builder.Append("tcode=@tcode,");
            builder.Append("flowstate=@flowstate,");
            builder.Append("isin=@isin,");
            builder.Append("person=@person,");
            builder.Append("intime=@intime,");
            builder.Append("annx=@annx,");
            builder.Append("explain=@explain, ");
            builder.Append(" isintime=@isintime ");
            builder.Append(" where rcode=@rcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rid", SqlDbType.NVarChar, 50), new SqlParameter("@rcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@procode", SqlDbType.NVarChar, 0x40), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@isin", SqlDbType.Bit, 1), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@isintime", SqlDateTime.MinValue) };
            commandParameters[0].Value = model.rid;
            commandParameters[1].Value = model.rcode;
            commandParameters[2].Value = model.procode;
            commandParameters[3].Value = model.tcode;
            commandParameters[4].Value = model.flowstate;
            commandParameters[5].Value = model.isin;
            commandParameters[6].Value = model.person;
            commandParameters[7].Value = model.intime;
            commandParameters[8].Value = model.annx;
            commandParameters[9].Value = model.explain;
            commandParameters[10].Value = model.IsInTime;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

