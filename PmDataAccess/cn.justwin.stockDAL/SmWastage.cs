namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SmWastage
    {
        public int Add(SqlTransaction trans, SmWastageModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SM_Wastage(");
            builder.Append("WastageId,WastageCode,Treasurycode,Flowstate,Isout,InputPerson,InputDate,Explain,IsOutTime)");
            builder.Append(" values (");
            builder.Append("@WastageId,@WastageCode,@Treasurycode,@Flowstate,@Isout,@InputPerson,@InputDate,@Explain,@IsOutTime)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@WastageId", SqlDbType.NVarChar, 50), new SqlParameter("@WastageCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@Treasurycode", SqlDbType.NVarChar, 0x200), new SqlParameter("@Flowstate", SqlDbType.Int, 4), new SqlParameter("@Isout", SqlDbType.Bit, 1), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@Explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@IsOutTime", SqlDbType.DateTime) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.Code;
            commandParameters[2].Value = model.Treasurycode;
            commandParameters[3].Value = model.Flowstate;
            commandParameters[4].Value = model.Isout;
            commandParameters[5].Value = model.InputPerson;
            commandParameters[6].Value = model.InputDate;
            commandParameters[7].Value = model.Explain;
            if (!model.IsOutTime.HasValue)
            {
                commandParameters[8].Value = DBNull.Value;
            }
            else
            {
                commandParameters[8].Value = model.IsOutTime;
            }
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string wastageCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SM_Wastage ");
            builder.Append(" where WastageCode=@WastageCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@wastageCode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = wastageCode;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<SmWastageModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select WastageId,WastageCode,Treasurycode,Flowstate,Isout,InputPerson,InputDate,Explain,IsOutTime ");
            builder.Append(" FROM SM_Wastage ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<SmWastageModel> list = new List<SmWastageModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public DataTable GetListByParm(string wastageCode, string startTime, string endTime, string person, string treasurycode, string strWhere, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetListCountByParm(wastageCode, startTime, endTime, person, treasurycode, strWhere);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.AppendFormat("select TOP {0} Num,WastageId,WastageCode,Treasurycode,Flowstate,Isout,InputPerson,InputDate,Explain,IsOutTime,TreasuryName from ( ", pageSize);
            builder.Append("select Row_Number()over(order by InputDate desc) as Num,WastageId,WastageCode,Treasurycode,Flowstate,Isout,InputPerson,InputDate,Explain,IsOutTime,p2.tname TreasuryName ");
            builder.Append(" FROM SM_Wastage as p1");
            builder.Append(" inner join dbo.Sm_Treasury as p2 on p1.Treasurycode=p2.tcode ");
            builder.Append("  where p1.WastageCode like @WastageCode ");
            builder.Append("  and p1.InputPerson like @InputPerson ");
            builder.Append(strWhere);
            list.Add(new SqlParameter("@WastageCode", '%' + wastageCode + '%'));
            list.Add(new SqlParameter("@InputPerson", '%' + person + '%'));
            if (!string.IsNullOrEmpty(treasurycode))
            {
                builder.Append(" and p2.tname like @Treasurycode");
                list.Add(new SqlParameter("@Treasurycode", '%' + treasurycode + '%'));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and p1.InputDate>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and p1.InputDate<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            builder.AppendFormat(" ) wastageInfo WHERE Num>{0}*({1}-1)  ", pageSize, pageIndex);
            builder.Append(" order by InputDate desc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public int GetListCountByParm(string wastageCode, string startTime, string endTime, string person, string treasurycode, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select count(*) ");
            builder.Append(" FROM SM_Wastage as p1");
            builder.Append(" inner join dbo.Sm_Treasury as p2 on p1.Treasurycode=p2.tcode ");
            builder.Append("  where p1.WastageCode like @WastageCode ");
            builder.Append("  and p1.InputPerson like @InputPerson ");
            builder.Append(strWhere);
            list.Add(new SqlParameter("@WastageCode", '%' + wastageCode + '%'));
            list.Add(new SqlParameter("@InputPerson", '%' + person + '%'));
            if (!string.IsNullOrEmpty(treasurycode))
            {
                builder.Append(" and p1.Treasurycode=@Treasurycode");
                list.Add(new SqlParameter("@Treasurycode", treasurycode));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and p1.InputDate>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and p1.InputDate<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()));
        }

        public SmWastageModel GetModel(string wastageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select WastageId,WastageCode,Treasurycode,Flowstate,Isout,InputPerson,InputDate,Explain,IsOutTime from SM_Wastage ");
            builder.Append(" where WastageId=@wastageId ");
            SmWastageModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@wastageId", wastageId) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public SmWastageModel GetModelByCode(string wastageCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select WastageId,WastageCode,Treasurycode,Flowstate,Isout,InputPerson,InputDate,Explain,IsOutTime from SM_Wastage ");
            builder.Append(" where WastageCode=@wastageCode ");
            SmWastageModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@wastageCode", wastageCode) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public SmWastageModel GetModelByIc(string ic)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select WastageId,WastageCode,Treasurycode,Flowstate,Isout,InputPerson,InputDate,Explain,IsOutTime from SM_Wastage ");
            builder.Append(" where WastageId=@WastageId ");
            SmWastageModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@WastageId", ic) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public SmWastageModel ReaderBind(IDataReader dataReader)
        {
            SmWastageModel model = new SmWastageModel {
                Id = dataReader["WastageId"].ToString(),
                Code = dataReader["WastageCode"].ToString(),
                Treasurycode = dataReader["Treasurycode"].ToString()
            };
            object obj2 = dataReader["Flowstate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Flowstate = (int) obj2;
            }
            obj2 = dataReader["Isout"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Isout = (bool) obj2;
            }
            model.InputPerson = dataReader["InputPerson"].ToString();
            obj2 = dataReader["InputDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.InputDate = (DateTime) obj2;
            }
            model.Explain = dataReader["Explain"].ToString();
            obj2 = dataReader["IsOutTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.IsOutTime = new DateTime?((DateTime) obj2);
            }
            return model;
        }

        public int Update(SqlTransaction trans, SmWastageModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SM_Wastage SET ");
            builder.Append("WastageId=@WastageId,");
            builder.Append("Treasurycode=@Treasurycode,");
            builder.Append("FlowState=@FlowState,");
            builder.Append("Isout=@Isout,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("InputDate=@InputDate,");
            builder.Append("Explain=@Explain,");
            builder.Append("IsOutTime=@IsOutTime");
            builder.Append(" WHERE WastageCode=@WastageCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@WastageId", SqlDbType.NVarChar, 50), new SqlParameter("@WastageCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@Treasurycode", SqlDbType.NVarChar, 0x200), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@Isout", SqlDbType.Bit, 1), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@Explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@IsOutTime", SqlDbType.DateTime) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.Code;
            commandParameters[2].Value = model.Treasurycode;
            commandParameters[3].Value = model.Flowstate;
            commandParameters[4].Value = model.Isout;
            commandParameters[5].Value = model.InputPerson;
            commandParameters[6].Value = model.InputDate;
            commandParameters[7].Value = model.Explain;
            if (!model.IsOutTime.HasValue)
            {
                commandParameters[8].Value = DBNull.Value;
            }
            else
            {
                commandParameters[8].Value = model.IsOutTime;
            }
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

