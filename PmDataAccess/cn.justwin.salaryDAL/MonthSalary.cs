namespace cn.justwin.salaryDAL
{
    using cn.justwin.DAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class MonthSalary
    {
        public int Add(SqlTransaction trans, MonthSalaryModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sam_MonthSalary(");
            builder.Append("Id,UserCode,CurrentMonth,Days,ActualDays,Award,DeductElse,LaborProtection,PreTax,Tax,Paid)");
            builder.Append(" values (");
            builder.Append("@Id,@UserCode,@CurrentMonth,@Days,@ActualDays,@Award,@DeductElse,@LaborProtection,@PreTax,@Tax,@Paid)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 0x40), new SqlParameter("@UserCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@CurrentMonth", SqlDbType.NVarChar, 10), new SqlParameter("@Days", SqlDbType.Decimal, 9), new SqlParameter("@ActualDays", SqlDbType.Decimal, 9), new SqlParameter("@Award", SqlDbType.Decimal, 9), new SqlParameter("@DeductElse", SqlDbType.Decimal, 9), new SqlParameter("@LaborProtection", SqlDbType.Decimal, 9), new SqlParameter("@PreTax", SqlDbType.Decimal, 9), new SqlParameter("@Tax", SqlDbType.Decimal, 9), new SqlParameter("@Paid", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.UserCode;
            commandParameters[2].Value = model.CurrentMonth;
            if (string.IsNullOrEmpty(model.Days.ToString()))
            {
                commandParameters[3].Value = DBNull.Value;
            }
            else
            {
                commandParameters[3].Value = model.Days;
            }
            if (string.IsNullOrEmpty(model.ActualDays.ToString()))
            {
                commandParameters[4].Value = DBNull.Value;
            }
            else
            {
                commandParameters[4].Value = model.ActualDays;
            }
            if (string.IsNullOrEmpty(model.Award.ToString()))
            {
                commandParameters[5].Value = DBNull.Value;
            }
            else
            {
                commandParameters[5].Value = model.Award;
            }
            if (string.IsNullOrEmpty(model.DeductElse.ToString()))
            {
                commandParameters[6].Value = DBNull.Value;
            }
            else
            {
                commandParameters[6].Value = model.DeductElse;
            }
            if (string.IsNullOrEmpty(model.LaborProtection.ToString()))
            {
                commandParameters[7].Value = DBNull.Value;
            }
            else
            {
                commandParameters[7].Value = model.LaborProtection;
            }
            if (string.IsNullOrEmpty(model.PreTax.ToString()))
            {
                commandParameters[8].Value = DBNull.Value;
            }
            else
            {
                commandParameters[8].Value = model.PreTax;
            }
            if (string.IsNullOrEmpty(model.Tax.ToString()))
            {
                commandParameters[9].Value = DBNull.Value;
            }
            else
            {
                commandParameters[9].Value = model.Tax;
            }
            if (string.IsNullOrEmpty(model.Paid.ToString()))
            {
                commandParameters[10].Value = DBNull.Value;
            }
            else
            {
                commandParameters[10].Value = model.Paid;
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sam_MonthSalary ");
            builder.Append(" where Id=@Id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = Id;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByUserCode(SqlTransaction trans, string userCode, string month)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sam_MonthSalary ");
            builder.Append(" where userCode in (" + userCode + ") and CurrentMonth='" + month + "'");
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public List<MonthSalaryModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Id,UserCode,CurrentMonth,Days,ActualDays,Award,DeductElse,LaborProtection,PreTax,Tax,Paid ");
            builder.Append(" FROM Sam_MonthSalary ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<MonthSalaryModel> list = new List<MonthSalaryModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public MonthSalaryModel GetModel(string Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Id,UserCode,CurrentMonth,Days,ActualDays,Award,DeductElse,LaborProtection,PreTax,Tax,Paid from Sam_MonthSalary ");
            builder.Append(" where Id=@Id ");
            MonthSalaryModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@Id", Id) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetSalaryTable(string bmdm, string month)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select p1.v_yhdm,p1.EnterCorpDate,p1.v_xm,p1.IDCard,p2.Account,");
            builder.Append(" p2.DayLaborersPrice,p3.ActualDays,p2.BasicWage,p3.Award,p2.other,p2.PhoneSubsidies,");
            builder.Append(" p3.LaborProtection,p3.PreTax,p3.Tax,p3.DeductElse,p3.Paid,p3.CurrentMonth,");
            builder.Append(" p2.LowestSalaryDay,p2.HoursDay,p3.Days,p2.LivingSubsidy,p2.TemplateID from pt_yhmc as p1");
            builder.Append(" left join dbo.SaM_SetSalary as p2 on p1.v_yhdm=p2.userCode ");
            builder.Append(" left join dbo.Sam_MonthSalary as p3 on p1.v_yhdm=p3.userCode ");
            builder.Append(" where currentMonth='" + month + "' and p1.i_bmdm in (select i_bmdm from f_cid('" + bmdm + "'))");
            builder.Append(" order by p1.v_yhdm asc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public DataTable GetTableByBmdm(string bmdm, string month)
        {
            StringBuilder builder = new StringBuilder();
            if (this.GetListArray(" where CurrentMonth='" + month + "'").Count != 0)
            {
                builder.Append("select p1.v_yhdm,p1.v_xm,p1.EnterCorpDate,p3.templateId,p2.* from pt_yhmc as p1");
                builder.Append(" left join dbo.Sam_MonthSalary as p2 on p1.v_yhdm=p2.usercode ");
                builder.Append(" left join dbo.SaM_SetSalary as p3 on p1.v_yhdm=p3.usercode ");
                builder.Append(" where p1.i_bmdm in (select i_bmdm from f_cid('" + bmdm + "'))");
                builder.Append(" and p2.CurrentMonth='" + month + "'");
            }
            else
            {
                builder.Append("select v_yhdm,v_xm,EnterCorpDate,null as templateId,null as Days,");
                builder.Append(" null as ActualDays,null as Award,null as DeductElse,");
                builder.Append(" null as LaborProtection,null as PreTax,null as Tax,null as Paid");
                builder.Append(" from pt_yhmc as p1 where p1.i_bmdm in (select i_bmdm from f_cid('" + bmdm + "'))");
            }
            builder.Append(" order by p1.v_yhdm asc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public MonthSalaryModel ReaderBind(IDataReader dataReader)
        {
            MonthSalaryModel model = new MonthSalaryModel {
                Id = dataReader["Id"].ToString(),
                UserCode = dataReader["UserCode"].ToString(),
                CurrentMonth = dataReader["CurrentMonth"].ToString()
            };
            object obj2 = dataReader["Days"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Days = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["ActualDays"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ActualDays = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["Award"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Award = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["DeductElse"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.DeductElse = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["LaborProtection"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.LaborProtection = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["PreTax"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.PreTax = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["Tax"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Tax = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["Paid"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Paid = new decimal?((decimal) obj2);
            }
            return model;
        }

        public int Update(MonthSalaryModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sam_MonthSalary set ");
            builder.Append("UserCode=@UserCode,");
            builder.Append("CurrentMonth=@CurrentMonth,");
            builder.Append("Days=@Days,");
            builder.Append("ActualDays=@ActualDays,");
            builder.Append("Award=@Award,");
            builder.Append("DeductElse=@DeductElse,");
            builder.Append("LaborProtection=@LaborProtection,");
            builder.Append("PreTax=@PreTax,");
            builder.Append("Tax=@Tax,");
            builder.Append("Paid=@Paid");
            builder.Append(" where Id=@Id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 0x40), new SqlParameter("@UserCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@CurrentMonth", SqlDbType.NVarChar, 10), new SqlParameter("@Days", SqlDbType.Decimal, 9), new SqlParameter("@ActualDays", SqlDbType.Decimal, 9), new SqlParameter("@Award", SqlDbType.Decimal, 9), new SqlParameter("@DeductElse", SqlDbType.Decimal, 9), new SqlParameter("@LaborProtection", SqlDbType.Decimal, 9), new SqlParameter("@PreTax", SqlDbType.Decimal, 9), new SqlParameter("@Tax", SqlDbType.Decimal, 9), new SqlParameter("@Paid", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.UserCode;
            commandParameters[2].Value = model.CurrentMonth;
            commandParameters[3].Value = model.Days;
            commandParameters[4].Value = model.ActualDays;
            commandParameters[5].Value = model.Award;
            commandParameters[6].Value = model.DeductElse;
            commandParameters[7].Value = model.LaborProtection;
            commandParameters[8].Value = model.PreTax;
            commandParameters[9].Value = model.Tax;
            commandParameters[10].Value = model.Paid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

