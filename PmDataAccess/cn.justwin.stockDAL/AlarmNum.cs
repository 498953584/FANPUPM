namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AlarmNum
    {
        public int Add(AlarmNumModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_AlarmNum(");
            builder.Append("said,scode,AlarmNum,settime,tcode,InfoCode)");
            builder.Append(" values (");
            builder.Append("@said,@scode,@AlarmNum,@settime,@tcode,@InfoCode)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@said", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.NVarChar, 0x40), new SqlParameter("@AlarmNum", SqlDbType.Decimal, 9), new SqlParameter("@settime", SqlDbType.SmallDateTime), new SqlParameter("@tcode", SqlDbType.NVarChar, 50), new SqlParameter("@InfoCode", SqlDbType.NVarChar, 10) };
            commandParameters[0].Value = model.said;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.AlarmNum;
            commandParameters[3].Value = model.settime;
            commandParameters[4].Value = model.tcode;
            commandParameters[5].Value = model.InfoCode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public object AlarmMethod(string tcode, string scode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sum(AlarmNum) as number ");
            builder.Append(" from dbo.Sm_AlarmNum ");
            builder.Append("where tcode=@tcode and scode=@scode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tcode", tcode), new SqlParameter("@scode", scode) };
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string scode, string tcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_AlarmNum ");
            builder.Append(" where scode=@scode and tcode=@tcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@scode", SqlDbType.NVarChar, 50), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = scode;
            commandParameters[1].Value = tcode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<AlarmNumModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select said,scode,AlarmNum,settime,tcode,InfoCode ");
            builder.Append(" FROM Sm_AlarmNum ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<AlarmNumModel> list = new List<AlarmNumModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public AlarmNumModel GetModel(string scode, string tcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select said,scode,AlarmNum,settime,tcode,InfoCode from Sm_AlarmNum ");
            builder.Append(" where scode=@scode  and tcode=@tcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@scode", scode), new SqlParameter("@tcode", tcode) };
            AlarmNumModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetTableList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select p2.resourceCode,p2.ResourceName,p2.Specification,");
            builder.Append("p4.Name,p1.AlarmNum,p1.InfoCode from Sm_AlarmNum as p1");
            builder.Append(" inner join Res_Resource as p2 on p1.scode=p2.resourceCode");
            builder.Append(" inner join Res_Unit as p4 on p2.Unit=p4.UnitID");
            builder.Append(strWhere);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public AlarmNumModel ReaderBind(IDataReader dataReader)
        {
            AlarmNumModel model = new AlarmNumModel {
                said = dataReader["said"].ToString(),
                scode = dataReader["scode"].ToString()
            };
            object obj2 = dataReader["AlarmNum"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.AlarmNum = (decimal) obj2;
            }
            obj2 = dataReader["settime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.settime = (DateTime) obj2;
            }
            model.tcode = dataReader["tcode"].ToString();
            model.InfoCode = dataReader["InfoCode"].ToString();
            return model;
        }

        public int Update(AlarmNumModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_AlarmNum set ");
            builder.Append("said=@said,");
            builder.Append("AlarmNum=@AlarmNum,");
            builder.Append("settime=@settime,");
            builder.Append("InfoCode=@InfoCode");
            builder.Append(" where scode=@scode  and tcode=@tcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@said", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.NVarChar, 0x40), new SqlParameter("@AlarmNum", SqlDbType.Decimal, 9), new SqlParameter("@settime", SqlDbType.SmallDateTime), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@InfoCode", SqlDbType.NVarChar, 10) };
            commandParameters[0].Value = model.said;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.AlarmNum;
            commandParameters[3].Value = model.settime;
            commandParameters[4].Value = model.tcode;
            commandParameters[5].Value = model.InfoCode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int UpdateNum(string scode, string tcode, decimal AlarmNum)
        {
            string cmdText = "update  Sm_AlarmNum set AlarmNum=@AlarmNum,InfoCode=1 where scode=@scode  and tcode=@tcode ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@scode", scode), new SqlParameter("@tcode", tcode), new SqlParameter("@AlarmNum", AlarmNum) };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }
    }
}

