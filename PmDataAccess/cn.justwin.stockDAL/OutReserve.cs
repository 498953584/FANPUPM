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

    public class OutReserve
    {
        public int Add(SqlTransaction trans, OutReserveModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_OutReserve(");
            builder.Append("orid,orcode,procode,tcode,flowstate,isout,person,intime,annx,explain,PickingPeople,PickingSector,isouttime,EquipmentId)");
            builder.Append(" values (");
            builder.Append("@orid,@orcode,@procode,@tcode,@flowstate,@isout,@person,@intime,@annx,@explain,@PickingPeople,@PickingSector,@isouttime,@EquipmentId)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@orid", SqlDbType.NVarChar, 50), new SqlParameter("@orcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@procode", SqlDbType.NVarChar, 0x40), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@isout", SqlDbType.Bit, 1), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@PickingPeople", SqlDbType.NVarChar, 0x200), new SqlParameter("@PickingSector", SqlDbType.NVarChar, 0x200), new SqlParameter("@isouttime", SqlDbType.SmallDateTime), new SqlParameter("@EquipmentId", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = model.orid;
            commandParameters[1].Value = model.orcode;
            commandParameters[2].Value = model.procode;
            commandParameters[3].Value = model.tcode;
            commandParameters[4].Value = model.flowstate;
            commandParameters[5].Value = model.isout;
            commandParameters[6].Value = model.person;
            commandParameters[7].Value = model.intime;
            commandParameters[8].Value = model.annx;
            commandParameters[9].Value = model.explain;
            commandParameters[10].Value = model.PickingPeople;
            commandParameters[11].Value = model.PickingSector;
            commandParameters[12].Value = model.IsOutTime;
            if (!string.IsNullOrEmpty(model.EquipmentId))
            {
                commandParameters[13].Value = model.EquipmentId;
            }
            else
            {
                commandParameters[13].Value = DBNull.Value;
            }
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string orcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_OutReserve ");
            builder.Append(" where orcode=@orcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@orcode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = orcode;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<OutReserveModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select orid,orcode,procode,tcode,flowstate,isout,person,intime,annx,explain,PickingPeople,PickingSector,EquipmentId ");
            builder.Append(" FROM Sm_OutReserve ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<OutReserveModel> list = new List<OutReserveModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public DataTable GetListByParm(string orcode, string startTime, string endTime, string person, string procode, string tname, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select p1.orid,p1.orcode,p1.procode,p1.tcode,p1.flowstate,p1.isout,p1.person,p1.intime,p1.annx,p1.PickingPeople,p1.PickingSector,p1.explain,p2.tname,p3.prjName ");
            builder.Append(" FROM Sm_OutReserve as p1");
            builder.Append(" inner join dbo.Sm_Treasury as p2 on p1.tcode=p2.tcode");
            builder.Append(" inner join dbo.PT_PrjInfo as p3 on p1.procode=p3.prjGuid");
            builder.Append("  where p1.orcode like @orcode ");
            builder.Append("  and p1.person like @person ");
            builder.Append(strWhere);
            list.Add(new SqlParameter("@orcode", '%' + orcode + '%'));
            list.Add(new SqlParameter("@person", '%' + person + '%'));
            if (!string.IsNullOrEmpty(procode))
            {
                builder.Append(" and p1.procode=@procode");
                list.Add(new SqlParameter("@procode", procode));
            }
            if (!string.IsNullOrEmpty(tname))
            {
                builder.Append(" and p2.tname LIKE @tname");
                list.Add(new SqlParameter("@tname", '%' + tname + '%'));
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

        public OutReserveModel GetModel(string orcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select orid,orcode,procode,tcode,flowstate,isout,person,intime,annx,explain,PickingPeople,PickingSector,EquipmentId from Sm_OutReserve ");
            builder.Append(" where orcode=@orcode ");
            OutReserveModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@orcode", orcode) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public OutReserveModel GetModelByIc(string ic)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select orid,orcode,procode,tcode,flowstate,isout,person,intime,annx,explain,PickingPeople,PickingSector,EquipmentId from Sm_OutReserve ");
            builder.Append(" where orid=@orid ");
            OutReserveModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@orid", ic) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public OutReserveModel ReaderBind(IDataReader dataReader)
        {
            OutReserveModel model = new OutReserveModel {
                orid = dataReader["orid"].ToString(),
                orcode = dataReader["orcode"].ToString(),
                procode = dataReader["procode"].ToString(),
                tcode = dataReader["tcode"].ToString()
            };
            object obj2 = dataReader["flowstate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.flowstate = (int) obj2;
            }
            obj2 = dataReader["isout"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.isout = (bool) obj2;
            }
            model.person = dataReader["person"].ToString();
            obj2 = dataReader["intime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.intime = (DateTime) obj2;
            }
            model.annx = dataReader["annx"].ToString();
            model.explain = dataReader["explain"].ToString();
            model.PickingPeople = dataReader["PickingPeople"].ToString();
            model.PickingSector = dataReader["PickingSector"].ToString();
            model.EquipmentId = dataReader["EquipmentId"].ToString();
            return model;
        }

        public int Update(SqlTransaction trans, OutReserveModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_OutReserve set ");
            builder.Append("orid=@orid,");
            builder.Append("procode=@procode,");
            builder.Append("tcode=@tcode,");
            builder.Append("flowstate=@flowstate,");
            builder.Append("isout=@isout,");
            builder.Append("person=@person,");
            builder.Append("intime=@intime,");
            builder.Append("annx=@annx,");
            builder.Append("explain=@explain,");
            builder.Append("PickingPeople=@PickingPeople,");
            builder.Append("PickingSector=@PickingSector,");
            builder.Append("isouttime=@isouttime,");
            builder.Append("EquipmentId=@EquipmentId ");
            builder.Append(" where orcode=@orcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@orid", SqlDbType.NVarChar, 50), new SqlParameter("@orcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@procode", SqlDbType.NVarChar, 0x40), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@isout", SqlDbType.Bit, 1), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@PickingPeople", SqlDbType.NVarChar, 0x200), new SqlParameter("@PickingSector", SqlDbType.NVarChar, 0x200), new SqlParameter("@isouttime", SqlDateTime.MinValue), new SqlParameter("@EquipmentId", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = model.orid;
            commandParameters[1].Value = model.orcode;
            commandParameters[2].Value = model.procode;
            commandParameters[3].Value = model.tcode;
            commandParameters[4].Value = model.flowstate;
            commandParameters[5].Value = model.isout;
            commandParameters[6].Value = model.person;
            commandParameters[7].Value = model.intime;
            commandParameters[8].Value = model.annx;
            commandParameters[9].Value = model.explain;
            commandParameters[10].Value = model.PickingPeople;
            commandParameters[11].Value = model.PickingSector;
            commandParameters[12].Value = model.IsOutTime;
            if (!string.IsNullOrEmpty(model.EquipmentId))
            {
                commandParameters[13].Value = model.EquipmentId;
            }
            else
            {
                commandParameters[13].Value = DBNull.Value;
            }
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

