namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PtdutyService
    {
        public int AddPtduty(Ptduty model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_DUTY(");
            builder.Append("i_bmdm,DutyCode,c_sfyx,RoleTypeCode,DutyNumber,Remark)");
            builder.Append(" values (");
            builder.Append("@i_bmdm,@DutyCode,@c_sfyx,@RoleTypeCode,@DutyNumber,@Remark)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@i_bmdm", SqlDbType.Int, 4), new SqlParameter("@DutyCode", SqlDbType.VarChar, 9), new SqlParameter("@c_sfyx", SqlDbType.Char, 1), new SqlParameter("@RoleTypeCode", SqlDbType.VarChar, 3), new SqlParameter("@DutyNumber", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8) };
            commandParameters[0].Value = model.i_bmdm;
            commandParameters[1].Value = model.DutyCode;
            commandParameters[2].Value = model.c_sfyx;
            commandParameters[3].Value = model.RoleTypeCode;
            commandParameters[4].Value = model.DutyNumber;
            commandParameters[5].Value = model.Remark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(int I_DUTYID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PT_DUTY ");
            builder.Append(" where I_DUTYID=@I_DUTYID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@I_DUTYID", SqlDbType.Int, 4) };
            commandParameters[0].Value = I_DUTYID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public IList<Ptduty> GetAllPtdutyByWhere(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT I_DUTYID,i_bmdm,DutyCode,DutyName,c_sfyx,RoleTypeCode,DutyNumber,Remark FROM PT_DUTY ");
            builder.Append(where);
            IList<Ptduty> list = new List<Ptduty>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.GetModelByReader(reader));
                }
            }
            return list;
        }

        private Ptduty GetModelByReader(SqlDataReader reader)
        {
            return new Ptduty { c_sfyx = Convert.ToString(reader["c_sfyx"]), DutyCode = Convert.ToString(reader["dutyCode"]), DutyNumber = Convert.ToInt32(reader["dutyNumber"]), i_bmdm = Convert.ToInt32(reader["i_bmdm"]), I_DUTYID = Convert.ToInt32(reader["i_dutyid"]), Remark = Convert.ToString(reader["remark"]), RoleTypeCode = Convert.ToString(reader["roleTypeCode"]), DutyName = DBHelper.GetString(reader["DutyName"]) };
        }

        public Ptduty GetPtDutyById(int I_DUTYID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 I_DUTYID,i_bmdm,DutyCode,c_sfyx,RoleTypeCode,DutyNumber,Remark,DUTYName from PT_DUTY ");
            builder.Append(" where I_DUTYID=@I_DUTYID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@I_DUTYID", SqlDbType.Int, 4) };
            commandParameters[0].Value = I_DUTYID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    return this.GetModelByReader(reader);
                }
            }
            return null;
        }

        public int Update(Ptduty model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_DUTY set ");
            builder.Append("i_bmdm=@i_bmdm,");
            builder.Append("DutyCode=@DutyCode,");
            builder.Append("c_sfyx=@c_sfyx,");
            builder.Append("RoleTypeCode=@RoleTypeCode,");
            builder.Append("DutyNumber=@DutyNumber,");
            builder.Append("Remark=@Remark");
            builder.Append(" where I_DUTYID=@I_DUTYID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@I_DUTYID", SqlDbType.Int, 4), new SqlParameter("@i_bmdm", SqlDbType.Int, 4), new SqlParameter("@DutyCode", SqlDbType.VarChar, 9), new SqlParameter("@c_sfyx", SqlDbType.Char, 1), new SqlParameter("@RoleTypeCode", SqlDbType.VarChar, 3), new SqlParameter("@DutyNumber", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8) };
            commandParameters[0].Value = model.I_DUTYID;
            commandParameters[1].Value = model.i_bmdm;
            commandParameters[2].Value = model.DutyCode;
            commandParameters[3].Value = model.c_sfyx;
            commandParameters[4].Value = model.RoleTypeCode;
            commandParameters[5].Value = model.DutyNumber;
            commandParameters[6].Value = model.Remark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

