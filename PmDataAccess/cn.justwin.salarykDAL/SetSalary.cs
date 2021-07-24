namespace cn.justwin.salarykDAL
{
    using cn.justwin.DAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SetSalary
    {
        public int Add(SqlTransaction trans, SetSalaryModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SaM_SetSalary(");
            builder.Append("ID,TemplateID,UserCode,BasicWage,DayLaborersPrice,PhoneSubsidies,LivingSubsidy,Account,Other,AddTime,HoursDay,LowestSalaryDay)");
            builder.Append(" values (");
            builder.Append("@ID,@TemplateID,@UserCode,@BasicWage,@DayLaborersPrice,@PhoneSubsidies,@LivingSubsidy,@Account,@Other,@AddTime,@HoursDay,@LowestSalaryDay)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@TemplateID", SqlDbType.NVarChar, 0x40), new SqlParameter("@UserCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@BasicWage", SqlDbType.Decimal, 9), new SqlParameter("@DayLaborersPrice", SqlDbType.Decimal, 9), new SqlParameter("@PhoneSubsidies", SqlDbType.Decimal, 9), new SqlParameter("@LivingSubsidy", SqlDbType.Decimal, 9), new SqlParameter("@Account", SqlDbType.NVarChar, 0x20), new SqlParameter("@Other", SqlDbType.Decimal, 9), new SqlParameter("@AddTime", SqlDbType.DateTime), new SqlParameter("@HoursDay", SqlDbType.Decimal, 9), new SqlParameter("@LowestSalaryDay", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = model.ID;
            if (string.IsNullOrEmpty(model.TemplateID))
            {
                commandParameters[1].Value = DBNull.Value;
            }
            else
            {
                commandParameters[1].Value = model.TemplateID;
            }
            if (string.IsNullOrEmpty(model.UserCode))
            {
                commandParameters[2].Value = DBNull.Value;
            }
            else
            {
                commandParameters[2].Value = model.UserCode;
            }
            if (string.IsNullOrEmpty(model.BasicWage.ToString()))
            {
                commandParameters[3].Value = DBNull.Value;
            }
            else
            {
                commandParameters[3].Value = model.BasicWage;
            }
            if (string.IsNullOrEmpty(model.DayLaborersPrice.ToString()))
            {
                commandParameters[4].Value = DBNull.Value;
            }
            else
            {
                commandParameters[4].Value = model.DayLaborersPrice;
            }
            if (string.IsNullOrEmpty(model.PhoneSubsidies.ToString()))
            {
                commandParameters[5].Value = DBNull.Value;
            }
            else
            {
                commandParameters[5].Value = model.PhoneSubsidies;
            }
            if (string.IsNullOrEmpty(model.LivingSubsidy.ToString()))
            {
                commandParameters[6].Value = DBNull.Value;
            }
            else
            {
                commandParameters[6].Value = model.LivingSubsidy;
            }
            if (string.IsNullOrEmpty(model.Account))
            {
                commandParameters[7].Value = DBNull.Value;
            }
            else
            {
                commandParameters[7].Value = model.Account;
            }
            if (string.IsNullOrEmpty(model.Other.ToString()))
            {
                commandParameters[8].Value = DBNull.Value;
            }
            else
            {
                commandParameters[8].Value = model.Other;
            }
            commandParameters[9].Value = model.AddTime;
            if (string.IsNullOrEmpty(model.HoursDay.ToString()))
            {
                commandParameters[10].Value = DBNull.Value;
            }
            else
            {
                commandParameters[10].Value = model.HoursDay;
            }
            if (string.IsNullOrEmpty(model.LowestSalaryDay.ToString()))
            {
                commandParameters[11].Value = DBNull.Value;
            }
            else
            {
                commandParameters[11].Value = model.LowestSalaryDay;
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SaM_SetSalary ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByUserCode(SqlTransaction trans, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SaM_SetSalary ");
            builder.Append(" where userCode in (" + userCode + ") ");
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public List<SetSalaryModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,TemplateID,UserCode,BasicWage,DayLaborersPrice,PhoneSubsidies,LivingSubsidy,Account,Other,AddTime,HoursDay,LowestSalaryDay ");
            builder.Append(" FROM SaM_SetSalary ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<SetSalaryModel> list = new List<SetSalaryModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public SetSalaryModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,TemplateID,UserCode,BasicWage,DayLaborersPrice,PhoneSubsidies,LivingSubsidy,Account,Other,AddTime,HoursDay,LowestSalaryDay from SaM_SetSalary ");
            builder.Append(" where ID=@ID ");
            SetSalaryModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ID", ID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public SetSalaryModel GetModelByUserCode(string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,TemplateID,UserCode,BasicWage,DayLaborersPrice,PhoneSubsidies,LivingSubsidy,Account,Other,AddTime,HoursDay,LowestSalaryDay from SaM_SetSalary ");
            builder.Append(" where userCode=@userCode ");
            SetSalaryModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@userCode", userCode) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetTableByBmdm(string bmdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select p1.v_yhdm,p1.v_xm,p1.EnterCorpDate,p2.* from pt_yhmc as p1");
            builder.Append(" left join Sam_SetSalary as p2 on p1.v_yhdm=p2.usercode where i_bmdm in (select i_bmdm from f_cid('" + bmdm + "'))");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public SetSalaryModel ReaderBind(IDataReader dataReader)
        {
            SetSalaryModel model = new SetSalaryModel {
                ID = dataReader["ID"].ToString(),
                TemplateID = dataReader["TemplateID"].ToString(),
                UserCode = dataReader["UserCode"].ToString()
            };
            object obj2 = dataReader["BasicWage"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.BasicWage = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["DayLaborersPrice"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.DayLaborersPrice = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["PhoneSubsidies"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.PhoneSubsidies = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["LivingSubsidy"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.LivingSubsidy = new decimal?((decimal) obj2);
            }
            model.Account = dataReader["Account"].ToString();
            obj2 = dataReader["Other"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Other = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["AddTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.AddTime = (DateTime) obj2;
            }
            obj2 = dataReader["HoursDay"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.HoursDay = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["LowestSalaryDay"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.LowestSalaryDay = new decimal?((decimal) obj2);
            }
            return model;
        }

        public int Update(SetSalaryModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SaM_SetSalary set ");
            builder.Append("TemplateID=@TemplateID,");
            builder.Append("UserCode=@UserCode,");
            builder.Append("BasicWage=@BasicWage,");
            builder.Append("DayLaborersPrice=@DayLaborersPrice,");
            builder.Append("PhoneSubsidies=@PhoneSubsidies,");
            builder.Append("LivingSubsidy=@LivingSubsidy,");
            builder.Append("Account=@Account,");
            builder.Append("Other=@Other,");
            builder.Append("AddTime=@AddTime");
            builder.Append("HoursDay=@HoursDay");
            builder.Append("LowestSalaryDay=@LowestSalaryDay");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@TemplateID", SqlDbType.NVarChar, 0x40), new SqlParameter("@UserCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@BasicWage", SqlDbType.Decimal, 9), new SqlParameter("@DayLaborersPrice", SqlDbType.Decimal, 9), new SqlParameter("@PhoneSubsidies", SqlDbType.Decimal, 9), new SqlParameter("@LivingSubsidy", SqlDbType.Decimal, 9), new SqlParameter("@Account", SqlDbType.NVarChar, 0x20), new SqlParameter("@Other", SqlDbType.Decimal, 9), new SqlParameter("@AddTime", SqlDbType.DateTime), new SqlParameter("@HoursDay", SqlDbType.Decimal, 9), new SqlParameter("@LowestSalaryDay", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.TemplateID;
            commandParameters[2].Value = model.UserCode;
            commandParameters[3].Value = model.BasicWage;
            commandParameters[4].Value = model.DayLaborersPrice;
            commandParameters[5].Value = model.PhoneSubsidies;
            commandParameters[6].Value = model.LivingSubsidy;
            commandParameters[7].Value = model.Account;
            commandParameters[8].Value = model.Other;
            commandParameters[9].Value = model.AddTime;
            commandParameters[10].Value = model.HoursDay;
            commandParameters[11].Value = model.LowestSalaryDay;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

