namespace cn.justwin.salaryDAL
{
    using cn.justwin.DAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SalaryDetails
    {
        public int Add(SqlTransaction trans, SalaryDetailsModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SaM_SalaryDetails(");
            builder.Append("ID,SalaryCode,UserCode,CurrentMonth,OperatorCode,PayMode,CalcMode,DayWage,ActualDays,BasicWage,HousingSubsidy,OvertimeWage,Award,OtherSubsidy,SocialSecurity,FullSalary,TaxFormerSalary,Tax,DeductElse,Paid,PrintTime,Days)");
            builder.Append(" values (");
            builder.Append("@ID,@SalaryCode,@UserCode,@CurrentMonth,@OperatorCode,@PayMode,@CalcMode,@DayWage,@ActualDays,@BasicWage,@HousingSubsidy,@OvertimeWage,@Award,@OtherSubsidy,@SocialSecurity,@FullSalary,@TaxFormerSalary,@Tax,@DeductElse,@Paid,@PrintTime,@Days)");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@SalaryCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@UserCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@CurrentMonth", SqlDbType.NVarChar, 10), new SqlParameter("@OperatorCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@PayMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@CalcMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@DayWage", SqlDbType.Decimal, 9), new SqlParameter("@ActualDays", SqlDbType.Decimal, 9), new SqlParameter("@BasicWage", SqlDbType.Decimal, 9), new SqlParameter("@HousingSubsidy", SqlDbType.Decimal, 9), new SqlParameter("@OvertimeWage", SqlDbType.Decimal, 9), new SqlParameter("@Award", SqlDbType.Decimal, 9), new SqlParameter("@OtherSubsidy", SqlDbType.Decimal, 9), new SqlParameter("@SocialSecurity", SqlDbType.Decimal, 9), new SqlParameter("@FullSalary", SqlDbType.Decimal, 9), 
                new SqlParameter("@TaxFormerSalary", SqlDbType.Decimal, 9), new SqlParameter("@Tax", SqlDbType.Decimal, 9), new SqlParameter("@DeductElse", SqlDbType.Decimal, 9), new SqlParameter("@Paid", SqlDbType.Decimal, 9), new SqlParameter("@PrintTime", SqlDbType.DateTime), new SqlParameter("@Days", SqlDbType.Decimal, 9)
             };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.SalaryCode;
            commandParameters[2].Value = model.UserCode;
            commandParameters[3].Value = model.CurrentMonth;
            commandParameters[4].Value = model.OperatorCode;
            commandParameters[5].Value = model.PayMode;
            commandParameters[6].Value = model.CalcMode;
            if (!string.IsNullOrEmpty(model.DayWage.ToString()))
            {
                commandParameters[7].Value = model.DayWage;
            }
            else
            {
                commandParameters[7].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.ActualDays.ToString()))
            {
                commandParameters[8].Value = model.ActualDays;
            }
            else
            {
                commandParameters[8].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.BasicWage.ToString()))
            {
                commandParameters[9].Value = model.BasicWage;
            }
            else
            {
                commandParameters[9].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.HousingSubsidy.ToString()))
            {
                commandParameters[10].Value = model.HousingSubsidy;
            }
            else
            {
                commandParameters[10].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.OvertimeWage.ToString()))
            {
                commandParameters[11].Value = model.OvertimeWage;
            }
            else
            {
                commandParameters[11].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.Award.ToString()))
            {
                commandParameters[12].Value = model.Award;
            }
            else
            {
                commandParameters[12].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.OtherSubsidy.ToString()))
            {
                commandParameters[13].Value = model.OtherSubsidy;
            }
            else
            {
                commandParameters[13].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.SocialSecurity.ToString()))
            {
                commandParameters[14].Value = model.SocialSecurity;
            }
            else
            {
                commandParameters[14].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.FullSalary.ToString()))
            {
                commandParameters[15].Value = model.FullSalary;
            }
            else
            {
                commandParameters[15].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.TaxFormerSalary.ToString()))
            {
                commandParameters[0x10].Value = model.TaxFormerSalary;
            }
            else
            {
                commandParameters[0x10].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.Tax.ToString()))
            {
                commandParameters[0x11].Value = model.Tax;
            }
            else
            {
                commandParameters[0x11].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.DeductElse.ToString()))
            {
                commandParameters[0x12].Value = model.DeductElse;
            }
            else
            {
                commandParameters[0x12].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.Paid.ToString()))
            {
                commandParameters[0x13].Value = model.Paid;
            }
            else
            {
                commandParameters[0x13].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.PrintTime.ToString()))
            {
                commandParameters[20].Value = model.PrintTime;
            }
            else
            {
                commandParameters[20].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.Days.ToString()))
            {
                commandParameters[0x15].Value = model.Days;
            }
            else
            {
                commandParameters[0x15].Value = DBNull.Value;
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public string CreateSalaryCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DECLARE @TheDay NVARCHAR(8),@MaxCode NVARCHAR(12),@Num int ");
            builder.Append("SET @TheDay = CONVERT(NVARCHAR(8),GETDATE(),112) ");
            builder.Append("SET @MaxCode = ");
            builder.Append("( ");
            builder.Append("    SELECT TOP (1) (SalaryCode) FROM Sam_SalaryDetails ");
            builder.Append("    WHERE SalaryCode LIKE ");
            builder.Append("\t   (@TheDay+'%') ");
            builder.Append("    ORDER BY SalaryCode DESC ");
            builder.Append(") ");
            builder.Append("SET @Num = CONVERT(INT,SUBSTRING(@MaxCode,9,12)) + 1 ");
            builder.Append("SELECT ISNULL(@TheDay + RIGHT('0000' + CONVERT(NVARCHAR(4), @Num), 4),@TheDay + '0001') AS SalaryCode ");
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null));
        }

        public int Delete(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SaM_SalaryDetails ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByUserCode(SqlTransaction trans, string userCode, string month)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SaM_SalaryDetails ");
            builder.Append(" where userCode in (" + userCode + ") and CurrentMonth='" + month + "'");
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public List<SalaryDetailsModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,SalaryCode,UserCode,CurrentMonth,OperatorCode,PayMode,CalcMode,DayWage,ActualDays,BasicWage,HousingSubsidy,OvertimeWage,Award,OtherSubsidy,SocialSecurity,FullSalary,TaxFormerSalary,Tax,DeductElse,Paid,PrintTime,Days ");
            builder.Append(" FROM SaM_SalaryDetails ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<SalaryDetailsModel> list = new List<SalaryDetailsModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public SalaryDetailsModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,SalaryCode,UserCode,CurrentMonth,OperatorCode,PayMode,CalcMode,DayWage,ActualDays,BasicWage,HousingSubsidy,OvertimeWage,Award,OtherSubsidy,SocialSecurity,FullSalary,TaxFormerSalary,Tax,DeductElse,Paid,PrintTime,Days from SaM_SalaryDetails ");
            builder.Append(" where ID=@ID ");
            SalaryDetailsModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ID", ID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetTbByIds(string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select p1.v_yhdm,p1.EnterCorpDate,p1.v_xm,p1.i_bmdm,p1.IDCard,p2.* from pt_yhmc as p1");
            builder.Append(" left join dbo.SaM_SalaryDetails as p2 on p1.v_yhdm=p2.userCode where p2.id in(" + id + ")");
            builder.Append(" order by p1.v_yhdm asc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public DataTable GetTbByQuery(string userCode, string currentMonth, string bmdm, string userName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select p1.v_yhdm,p1.EnterCorpDate,p1.v_xm,p1.IDCard,p2.* from pt_yhmc as p1");
            builder.Append(" left join dbo.SaM_SalaryDetails as p2 on p1.v_yhdm=p2.userCode where 1=1");
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.Append(" and p2.userCode='" + userCode + "'");
            }
            if (!string.IsNullOrEmpty(currentMonth))
            {
                builder.Append(" and p2.currentMonth='" + currentMonth + "'");
            }
            if (!string.IsNullOrEmpty(bmdm))
            {
                builder.Append(" and p1.i_bmdm in (select i_bmdm from f_cid('" + bmdm + "'))");
            }
            builder.Append(" and p1.v_xm like @userName");
            builder.Append(" order by p1.v_yhdm asc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@userName", "%" + userName + "%") });
        }

        public SalaryDetailsModel ReaderBind(IDataReader dataReader)
        {
            SalaryDetailsModel model = new SalaryDetailsModel {
                ID = dataReader["ID"].ToString(),
                SalaryCode = dataReader["SalaryCode"].ToString(),
                UserCode = dataReader["UserCode"].ToString(),
                CurrentMonth = dataReader["CurrentMonth"].ToString(),
                OperatorCode = dataReader["OperatorCode"].ToString(),
                PayMode = dataReader["PayMode"].ToString(),
                CalcMode = dataReader["CalcMode"].ToString()
            };
            object obj2 = dataReader["DayWage"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.DayWage = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["ActualDays"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ActualDays = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["BasicWage"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.BasicWage = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["HousingSubsidy"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.HousingSubsidy = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["OvertimeWage"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.OvertimeWage = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["Award"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Award = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["OtherSubsidy"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.OtherSubsidy = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["SocialSecurity"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.SocialSecurity = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["FullSalary"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.FullSalary = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["TaxFormerSalary"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.TaxFormerSalary = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["Tax"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Tax = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["DeductElse"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.DeductElse = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["Paid"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Paid = new decimal?((decimal) obj2);
            }
            obj2 = dataReader["PrintTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.PrintTime = new DateTime?((DateTime) obj2);
            }
            obj2 = dataReader["Days"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Days = new decimal?((decimal) obj2);
            }
            return model;
        }

        public int Update(SalaryDetailsModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SaM_SalaryDetails set ");
            builder.Append("SalaryCode=@SalaryCode,");
            builder.Append("UserCode=@UserCode,");
            builder.Append("CurrentMonth=@CurrentMonth,");
            builder.Append("OperatorCode=@OperatorCode,");
            builder.Append("PayMode=@PayMode,");
            builder.Append("CalcMode=@CalcMode,");
            builder.Append("DayWage=@DayWage,");
            builder.Append("ActualDays=@ActualDays,");
            builder.Append("BasicWage=@BasicWage,");
            builder.Append("HousingSubsidy=@HousingSubsidy,");
            builder.Append("OvertimeWage=@OvertimeWage,");
            builder.Append("Award=@Award,");
            builder.Append("OtherSubsidy=@OtherSubsidy,");
            builder.Append("SocialSecurity=@SocialSecurity,");
            builder.Append("FullSalary=@FullSalary,");
            builder.Append("TaxFormerSalary=@TaxFormerSalary,");
            builder.Append("Tax=@Tax,");
            builder.Append("DeductElse=@DeductElse,");
            builder.Append("Paid=@Paid,");
            builder.Append("PrintTime=@PrintTime,");
            builder.Append("Days=@Days");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@SalaryCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@UserCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@CurrentMonth", SqlDbType.NVarChar, 10), new SqlParameter("@OperatorCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@PayMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@CalcMode", SqlDbType.NVarChar, 0x40), new SqlParameter("@DayWage", SqlDbType.Decimal, 9), new SqlParameter("@ActualDays", SqlDbType.Decimal, 9), new SqlParameter("@BasicWage", SqlDbType.Decimal, 9), new SqlParameter("@HousingSubsidy", SqlDbType.Decimal, 9), new SqlParameter("@OvertimeWage", SqlDbType.Decimal, 9), new SqlParameter("@Award", SqlDbType.Decimal, 9), new SqlParameter("@OtherSubsidy", SqlDbType.Decimal, 9), new SqlParameter("@SocialSecurity", SqlDbType.Decimal, 9), new SqlParameter("@FullSalary", SqlDbType.Decimal, 9), 
                new SqlParameter("@TaxFormerSalary", SqlDbType.Decimal, 9), new SqlParameter("@Tax", SqlDbType.Decimal, 9), new SqlParameter("@DeductElse", SqlDbType.Decimal, 9), new SqlParameter("@Paid", SqlDbType.Decimal, 9), new SqlParameter("@PrintTime", SqlDbType.DateTime), new SqlParameter("@Days", SqlDbType.Decimal, 9)
             };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.SalaryCode;
            commandParameters[2].Value = model.UserCode;
            commandParameters[3].Value = model.CurrentMonth;
            commandParameters[4].Value = model.OperatorCode;
            commandParameters[5].Value = model.PayMode;
            commandParameters[6].Value = model.CalcMode;
            commandParameters[7].Value = model.DayWage;
            commandParameters[8].Value = model.ActualDays;
            commandParameters[9].Value = model.BasicWage;
            commandParameters[10].Value = model.HousingSubsidy;
            commandParameters[11].Value = model.OvertimeWage;
            commandParameters[12].Value = model.Award;
            commandParameters[13].Value = model.OtherSubsidy;
            commandParameters[14].Value = model.SocialSecurity;
            commandParameters[15].Value = model.FullSalary;
            commandParameters[0x10].Value = model.TaxFormerSalary;
            commandParameters[0x11].Value = model.Tax;
            commandParameters[0x12].Value = model.DeductElse;
            commandParameters[0x13].Value = model.Paid;
            commandParameters[20].Value = model.PrintTime;
            commandParameters[0x15].Value = model.Days;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

