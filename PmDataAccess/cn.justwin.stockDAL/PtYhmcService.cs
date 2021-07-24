namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PtYhmcService
    {
        public int Add(PtYhmc model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_yhmc(");
            builder.Append("v_yhdm,v_xm,i_bmdm,I_DUTYID,OtherDepts,OtherDutyIDs,i_xh,c_sfyx,MobilePhoneCode,RelationCorp,EnterCorpDate,Age,State,SuperordinateDuty,Sex,Nation,EducationalBackground,Stature,ClassID,PositionLevel,PostAndRank,RegisteredPlace,Address,ComputeLevel,EnglishLevel,DriveLevel,Marriage,Tel,ExpectationSalary,CommunicateAddress,Postcode,UserPhoto,Birthday,IDCard,FormalDate,PoliticsFace,JoinPartyDate,JoinCorpMode,Introducer,Specialty,GraduateSchool,Level,BeginWorkDate,PostAndCompetency,EndowmentInsurance,MedicareInsurance,UnemploymentInsurance,InjuryInsurance,HousingAccumulationFund,PersonSuddennessInsurance)");
            builder.Append(" values (");
            builder.Append("@v_yhdm,@v_xm,@i_bmdm,@I_DUTYID,@OtherDepts,@OtherDutyIDs,@i_xh,@c_sfyx,@MobilePhoneCode,@RelationCorp,@EnterCorpDate,@Age,@State,@SuperordinateDuty,@Sex,@Nation,@EducationalBackground,@Stature,@ClassID,@PositionLevel,@PostAndRank,@RegisteredPlace,@Address,@ComputeLevel,@EnglishLevel,@DriveLevel,@Marriage,@Tel,@ExpectationSalary,@CommunicateAddress,@Postcode,@UserPhoto,@Birthday,@IDCard,@FormalDate,@PoliticsFace,@JoinPartyDate,@JoinCorpMode,@Introducer,@Specialty,@GraduateSchool,@Level,@BeginWorkDate,@PostAndCompetency,@EndowmentInsurance,@MedicareInsurance,@UnemploymentInsurance,@InjuryInsurance,@HousingAccumulationFund,@PersonSuddennessInsurance)");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@v_yhdm", SqlDbType.VarChar, 8), new SqlParameter("@v_xm", SqlDbType.VarChar, 50), new SqlParameter("@i_bmdm", SqlDbType.Int, 4), new SqlParameter("@I_DUTYID", SqlDbType.Int, 4), new SqlParameter("@OtherDepts", SqlDbType.VarChar, 100), new SqlParameter("@OtherDutyIDs", SqlDbType.VarChar, 100), new SqlParameter("@i_xh", SqlDbType.Int, 4), new SqlParameter("@c_sfyx", SqlDbType.Char, 1), new SqlParameter("@MobilePhoneCode", SqlDbType.VarChar, 13), new SqlParameter("@RelationCorp", SqlDbType.VarChar, 2), new SqlParameter("@EnterCorpDate", SqlDbType.DateTime), new SqlParameter("@Age", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@SuperordinateDuty", SqlDbType.Int, 4), new SqlParameter("@Sex", SqlDbType.Char, 1), new SqlParameter("@Nation", SqlDbType.VarChar, 50), 
                new SqlParameter("@EducationalBackground", SqlDbType.VarChar, 50), new SqlParameter("@Stature", SqlDbType.VarChar, 50), new SqlParameter("@ClassID", SqlDbType.Int, 4), new SqlParameter("@PositionLevel", SqlDbType.Int, 4), new SqlParameter("@PostAndRank", SqlDbType.VarChar, 50), new SqlParameter("@RegisteredPlace", SqlDbType.VarChar, 300), new SqlParameter("@Address", SqlDbType.VarChar, 300), new SqlParameter("@ComputeLevel", SqlDbType.VarChar, 50), new SqlParameter("@EnglishLevel", SqlDbType.VarChar, 50), new SqlParameter("@DriveLevel", SqlDbType.VarChar, 50), new SqlParameter("@Marriage", SqlDbType.VarChar, 50), new SqlParameter("@Tel", SqlDbType.VarChar, 20), new SqlParameter("@ExpectationSalary", SqlDbType.Decimal, 5), new SqlParameter("@CommunicateAddress", SqlDbType.VarChar, 200), new SqlParameter("@Postcode", SqlDbType.VarChar, 6), new SqlParameter("@UserPhoto", SqlDbType.VarChar, 100), 
                new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@IDCard", SqlDbType.VarChar, 20), new SqlParameter("@FormalDate", SqlDbType.DateTime), new SqlParameter("@PoliticsFace", SqlDbType.VarChar, 50), new SqlParameter("@JoinPartyDate", SqlDbType.DateTime), new SqlParameter("@JoinCorpMode", SqlDbType.VarChar, 50), new SqlParameter("@Introducer", SqlDbType.VarChar, 20), new SqlParameter("@Specialty", SqlDbType.VarChar, 50), new SqlParameter("@GraduateSchool", SqlDbType.VarChar, 100), new SqlParameter("@Level", SqlDbType.VarChar, 50), new SqlParameter("@BeginWorkDate", SqlDbType.DateTime), new SqlParameter("@PostAndCompetency", SqlDbType.VarChar, 50), new SqlParameter("@EndowmentInsurance", SqlDbType.Char, 1), new SqlParameter("@MedicareInsurance", SqlDbType.Char, 1), new SqlParameter("@UnemploymentInsurance", SqlDbType.Char, 1), new SqlParameter("@InjuryInsurance", SqlDbType.Char, 1), 
                new SqlParameter("@HousingAccumulationFund", SqlDbType.Char, 1), new SqlParameter("@PersonSuddennessInsurance", SqlDbType.Char, 1)
             };
            commandParameters[0].Value = model.v_yhdm;
            commandParameters[1].Value = model.v_xm;
            commandParameters[2].Value = model.i_bmdm;
            commandParameters[3].Value = model.I_DUTYID;
            commandParameters[4].Value = model.OtherDepts;
            commandParameters[5].Value = model.OtherDutyIDs;
            commandParameters[6].Value = model.i_xh;
            commandParameters[7].Value = model.c_sfyx;
            commandParameters[8].Value = model.MobilePhoneCode;
            commandParameters[9].Value = model.RelationCorp;
            commandParameters[10].Value = model.EnterCorpDate;
            commandParameters[11].Value = model.Age;
            commandParameters[12].Value = model.State;
            commandParameters[13].Value = model.SuperordinateDuty;
            commandParameters[14].Value = model.Sex;
            commandParameters[15].Value = model.Nation;
            commandParameters[0x10].Value = model.EducationalBackground;
            commandParameters[0x11].Value = model.Stature;
            commandParameters[0x12].Value = model.ClassID;
            commandParameters[0x13].Value = model.PositionLevel;
            commandParameters[20].Value = model.PostAndRank;
            commandParameters[0x15].Value = model.RegisteredPlace;
            commandParameters[0x16].Value = model.Address;
            commandParameters[0x17].Value = model.ComputeLevel;
            commandParameters[0x18].Value = model.EnglishLevel;
            commandParameters[0x19].Value = model.DriveLevel;
            commandParameters[0x1a].Value = model.Marriage;
            commandParameters[0x1b].Value = model.Tel;
            commandParameters[0x1c].Value = model.ExpectationSalary;
            commandParameters[0x1d].Value = model.CommunicateAddress;
            commandParameters[30].Value = model.Postcode;
            commandParameters[0x1f].Value = model.UserPhoto;
            commandParameters[0x20].Value = model.Birthday;
            commandParameters[0x21].Value = model.IDCard;
            commandParameters[0x22].Value = model.FormalDate;
            commandParameters[0x23].Value = model.PoliticsFace;
            commandParameters[0x24].Value = model.JoinPartyDate;
            commandParameters[0x25].Value = model.JoinCorpMode;
            commandParameters[0x26].Value = model.Introducer;
            commandParameters[0x27].Value = model.Specialty;
            commandParameters[40].Value = model.GraduateSchool;
            commandParameters[0x29].Value = model.Level;
            commandParameters[0x2a].Value = model.BeginWorkDate;
            commandParameters[0x2b].Value = model.PostAndCompetency;
            commandParameters[0x2c].Value = model.EndowmentInsurance;
            commandParameters[0x2d].Value = model.MedicareInsurance;
            commandParameters[0x2e].Value = model.UnemploymentInsurance;
            commandParameters[0x2f].Value = model.InjuryInsurance;
            commandParameters[0x30].Value = model.HousingAccumulationFund;
            commandParameters[0x31].Value = model.PersonSuddennessInsurance;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string v_yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PT_yhmc ");
            builder.Append(" where v_yhdm=@v_yhdm ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@v_yhdm", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = v_yhdm;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public IList<PtYhmc> GetAllModelByWhere(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                SELECT * FROM (\r\n                select b.v_yhdm,b.v_xm,b.i_bmdm,b.I_DUTYID,b.OtherDepts,b.OtherDutyIDs,b.i_xh,b.c_sfyx,b.MobilePhoneCode,\r\n                b.RelationCorp,b.EnterCorpDate,b.Age,b.State,b.SuperordinateDuty,b.Sex,b.Nation,b.EducationalBackground,\r\n                b.Stature,b.ClassID,b.PositionLevel,b.PostAndRank,b.RegisteredPlace,b.Address,b.ComputeLevel,b.EnglishLevel,\r\n                b.DriveLevel,b.Marriage,b.Tel,b.ExpectationSalary,b.CommunicateAddress,b.Postcode,b.UserPhoto,b.Birthday,b.IDCard,\r\n                b.FormalDate,b.PoliticsFace,b.JoinPartyDate,b.JoinCorpMode,b.Introducer,b.Specialty,b.GraduateSchool,b.Level,\r\n                b.BeginWorkDate,b.PostAndCompetency,b.EndowmentInsurance,b.MedicareInsurance,b.UnemploymentInsurance,b.InjuryInsurance,\r\n                b.HousingAccumulationFund,b.PersonSuddennessInsurance,b.IsChargeMan from PT_yhmc b \r\n                where b.v_yhdm  IN (SELECT V_YHDM FROM PT_LOGIN WHERE c_sfyx='y') \r\n                ) AS TResult ");
            builder.Append(where);
            builder.Append(" order by IsChargeMan desc");
            IList<PtYhmc> list = new List<PtYhmc>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.GetModelByReader(reader));
                }
            }
            return list;
        }

        public PtYhmc GetModelById(string v_yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 v_yhdm,v_xm,i_bmdm,I_DUTYID,OtherDepts,OtherDutyIDs,i_xh,c_sfyx,MobilePhoneCode,RelationCorp,EnterCorpDate,Age,State,SuperordinateDuty,Sex,Nation,EducationalBackground,Stature,ClassID,PositionLevel,PostAndRank,RegisteredPlace,Address,ComputeLevel,EnglishLevel,DriveLevel,Marriage,Tel,ExpectationSalary,CommunicateAddress,Postcode,UserPhoto,Birthday,IDCard,FormalDate,PoliticsFace,JoinPartyDate,JoinCorpMode,Introducer,Specialty,GraduateSchool,Level,BeginWorkDate,PostAndCompetency,EndowmentInsurance,MedicareInsurance,UnemploymentInsurance,InjuryInsurance,HousingAccumulationFund,PersonSuddennessInsurance from PT_yhmc ");
            builder.Append(" where v_yhdm=@v_yhdm ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@v_yhdm", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = v_yhdm;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    return this.GetModelByReader(reader);
                }
            }
            return null;
        }

        private PtYhmc GetModelByReader(SqlDataReader dataReader)
        {
            PtYhmc yhmc = new PtYhmc {
                v_yhdm = dataReader["v_yhdm"].ToString(),
                v_xm = dataReader["v_xm"].ToString()
            };
            object obj2 = dataReader["i_bmdm"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.i_bmdm = (int) obj2;
            }
            obj2 = dataReader["I_DUTYID"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.I_DUTYID = (int) obj2;
            }
            yhmc.OtherDepts = dataReader["OtherDepts"].ToString();
            yhmc.OtherDutyIDs = dataReader["OtherDutyIDs"].ToString();
            obj2 = dataReader["i_xh"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.i_xh = (int) obj2;
            }
            yhmc.c_sfyx = dataReader["c_sfyx"].ToString();
            yhmc.MobilePhoneCode = dataReader["MobilePhoneCode"].ToString();
            yhmc.RelationCorp = dataReader["RelationCorp"].ToString();
            obj2 = dataReader["EnterCorpDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.EnterCorpDate = (DateTime) obj2;
            }
            obj2 = dataReader["Age"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.Age = (int) obj2;
            }
            obj2 = dataReader["State"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.State = (int) obj2;
            }
            obj2 = dataReader["SuperordinateDuty"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.SuperordinateDuty = (int) obj2;
            }
            yhmc.Sex = dataReader["Sex"].ToString();
            yhmc.Nation = dataReader["Nation"].ToString();
            yhmc.EducationalBackground = dataReader["EducationalBackground"].ToString();
            yhmc.Stature = dataReader["Stature"].ToString();
            obj2 = dataReader["ClassID"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.ClassID = (int) obj2;
            }
            obj2 = dataReader["PositionLevel"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.PositionLevel = (int) obj2;
            }
            yhmc.PostAndRank = dataReader["PostAndRank"].ToString();
            yhmc.RegisteredPlace = dataReader["RegisteredPlace"].ToString();
            yhmc.Address = dataReader["Address"].ToString();
            yhmc.ComputeLevel = dataReader["ComputeLevel"].ToString();
            yhmc.EnglishLevel = dataReader["EnglishLevel"].ToString();
            yhmc.DriveLevel = dataReader["DriveLevel"].ToString();
            yhmc.Marriage = dataReader["Marriage"].ToString();
            yhmc.Tel = dataReader["Tel"].ToString();
            obj2 = dataReader["ExpectationSalary"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.ExpectationSalary = (decimal) obj2;
            }
            yhmc.CommunicateAddress = dataReader["CommunicateAddress"].ToString();
            yhmc.Postcode = dataReader["Postcode"].ToString();
            yhmc.UserPhoto = dataReader["UserPhoto"].ToString();
            obj2 = dataReader["Birthday"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.Birthday = (DateTime) obj2;
            }
            yhmc.IDCard = dataReader["IDCard"].ToString();
            obj2 = dataReader["FormalDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.FormalDate = (DateTime) obj2;
            }
            yhmc.PoliticsFace = dataReader["PoliticsFace"].ToString();
            obj2 = dataReader["JoinPartyDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.JoinPartyDate = (DateTime) obj2;
            }
            yhmc.JoinCorpMode = dataReader["JoinCorpMode"].ToString();
            yhmc.Introducer = dataReader["Introducer"].ToString();
            yhmc.Specialty = dataReader["Specialty"].ToString();
            yhmc.GraduateSchool = dataReader["GraduateSchool"].ToString();
            yhmc.Level = dataReader["Level"].ToString();
            obj2 = dataReader["BeginWorkDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                yhmc.BeginWorkDate = (DateTime) obj2;
            }
            yhmc.PostAndCompetency = dataReader["PostAndCompetency"].ToString();
            yhmc.EndowmentInsurance = dataReader["EndowmentInsurance"].ToString();
            yhmc.MedicareInsurance = dataReader["MedicareInsurance"].ToString();
            yhmc.UnemploymentInsurance = dataReader["UnemploymentInsurance"].ToString();
            yhmc.InjuryInsurance = dataReader["InjuryInsurance"].ToString();
            yhmc.HousingAccumulationFund = dataReader["HousingAccumulationFund"].ToString();
            yhmc.PersonSuddennessInsurance = dataReader["PersonSuddennessInsurance"].ToString();
            return yhmc;
        }

        public string GetRTXID(string userCode)
        {
            string cmdText = string.Format("SELECT RTXID FROM PT_yhmc WHERE v_yhdm = '{0}'", userCode);
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]));
        }

        public DataTable GetSaItems(string saBooksId, int year, int month)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                        SELECT Id,Name FROM (\r\n                        SELECT ItemId FROM SA_MonthSalary saMonth\r\n                        INNER JOIN SA_UserSalaryBooks saUserBooks ON saMonth.UserCode=saUserBooks.UserCode\r\n                        WHERE  saUserBooks.BooksId=@saBooksId AND year=@year and month=@month\r\n                        group by ItemId) saItemIds left join SA_SalaryItem saItem ON saItemIds.ItemId=saItem.Id\r\n                        ORDER BY No ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@saBooksId", saBooksId),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetSaItems(string saBooksId, int year, int month, string departmentId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                        --\tDECLARE @saBooksId NVARCHAR(50),@year INT,@month INT, @departmentId INT;\r\n                        --\tSET @saBooksId='a4e1b731-00b6-4922-a326-c90fc0849f39'\r\n                        --\tSET @year=2012\r\n                        --\tSET @month=12\r\n                        --\tSET @departmentId=123\r\n\t                        SELECT Id,Name FROM (\r\n\t\t                        SELECT ItemId FROM SA_MonthSalary saMonth\r\n\t\t                        INNER JOIN SA_UserSalaryBooks saUserBooks ON saMonth.UserCode=saUserBooks.UserCode\r\n\t\t                        INNER JOIN PT_yhmc yhmc ON saUserBooks.UserCode=v_yhdm\r\n\t\t                        LEFT JOIN SA_Payoff Payoff ON saMonth.UserCode=Payoff.UserCode \r\n\t\t                        AND saMonth.year=Payoff.year AND saMonth.Month=Payoff.Month\r\n\t\t                        WHERE saUserBooks.BooksId=@saBooksId AND saMonth.year=@year and saMonth.month=@month\r\n\t\t                        AND (IsPayoff IS NULL OR IsPayoff = 0) \r\n\r\n\t\t                    ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@saBooksId", saBooksId),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };
            if (departmentId != "1")
            {
                builder.AppendLine(" AND [i_bmdm] = @departmentId ");
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            builder.Append("    \r\n                            GROUP BY ItemId\r\n\t                        ) saItemIds left join SA_SalaryItem saItem ON saItemIds.ItemId=saItem.Id\r\n                            ORDER BY No  ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        private string GetSaItemsCSNoPayoff(string saBooksId, int year, int month, string departmentId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                        DECLARE @ItemName AS nvarchar(4000);\r\n                --\t\tDECLARE @saBooksId NVARCHAR(50),@year INT,@month INT, @departmentId INT;\r\n                --\t\tSET @saBooksId='a4e1b731-00b6-4922-a326-c90fc0849f39'\r\n                --        SET @year=2012\r\n                --\t\tSET @month=12\r\n                --\t\tSET @departmentId=123\r\n\t\t                SET @ItemName=''\r\n                        SELECT @ItemName= Name+'],['+@ItemName\r\n                        FROM (\r\n\t\t\t                SELECT Name,No FROM (\r\n\t\t\t\t                SELECT ItemId FROM SA_MonthSalary saMonth\r\n\t\t\t\t                INNER JOIN SA_UserSalaryBooks saUserBooks ON saMonth.UserCode=saUserBooks.UserCode\r\n\t\t\t\t                INNER JOIN PT_yhmc yhmc ON saUserBooks.UserCode=v_yhdm\r\n\t\t\t\t                LEFT JOIN SA_Payoff Payoff ON saMonth.UserCode=Payoff.UserCode \r\n\t\t\t\t                AND saMonth.year=Payoff.year AND saMonth.Month=Payoff.Month\r\n\t\t\t\t                WHERE saUserBooks.BooksId=@saBooksId AND saMonth.year=@year and saMonth.month=@month\r\n\t\t\t\t                 ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@saBooksId", saBooksId),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };
            if (departmentId != "1")
            {
                builder.AppendLine(" AND [i_bmdm] = @departmentId ");
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            builder.Append("\r\n                        GROUP BY ItemId\r\n\t\t\t                ) saItemIds LEFT JOIN SA_SalaryItem saItem ON saItemIds.ItemId=saItem.Id\r\n                        )  AS T ORDER BY No DESC\r\n                        IF(@itemName IS NULL or @itemName='')\r\n                        BEGIN\r\n                            SET @ItemName=''\r\n                        END \r\n                        ELSE \r\n                        BEGIN\r\n                            SET @itemName = '[' + @itemName\r\n                            SET @itemName = SUBSTRING(@itemName,1,LEN(@itemName) - 2)\r\n                        END\r\n                        SELECT @ItemName \r\n            ");
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()).ToString();
        }

        public string GetSaItemsCSV()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                        DECLARE @ItemName AS nvarchar(4000);\r\n                        SET @ItemName=''\r\n                        SELECT @ItemName= Name+'],['+@ItemName\r\n\t                        FROM SA_SalaryItem ORDER BY No DESC\r\n                        IF(@itemName IS NULL or @itemName='')\r\n                        BEGIN\r\n                            SET @ItemName=''\r\n                        END \r\n                        ELSE \r\n                        BEGIN\r\n                            SET @itemName = '[' + @itemName\r\n                            SET @itemName = SUBSTRING(@itemName,1,LEN(@itemName) - 2)\r\n                        END\r\n                        SELECT @ItemName \r\n\t\t                    ");
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null).ToString();
        }

        private string GetSaItemsCSV(string saBooksId, int year, int month)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                        DECLARE @ItemName AS nvarchar(4000);\r\n                        SET @ItemName=''\r\n                        SELECT @ItemName= Name+'],['+@ItemName\r\n                        FROM (\r\n                        SELECT Name,No FROM (\r\n                        SELECT ItemId FROM SA_MonthSalary saMonth\r\n                        INNER JOIN SA_UserSalaryBooks saUserBooks ON saMonth.UserCode=saUserBooks.UserCode\r\n                        WHERE  saUserBooks.BooksId=@saBooksId AND year=@year and month=@month\r\n                        GROUP BY ItemId) saItemIds LEFT JOIN SA_SalaryItem saItem ON saItemIds.ItemId=saItem.Id\r\n                        )  AS T ORDER BY No DESC\r\n                        IF(@itemName IS NULL or @itemName='')\r\n                        BEGIN\r\n                            SET @ItemName=''\r\n                        END \r\n                        ELSE \r\n                        BEGIN\r\n                            SET @itemName = '[' + @itemName\r\n                            SET @itemName = SUBSTRING(@itemName,1,LEN(@itemName) - 2)\r\n                        END\r\n                        SELECT @ItemName ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@saBooksId", saBooksId),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()).ToString();
        }

        public int GetSaMonthCount(string departmentId, string saBooksId, string userCode, string userName, int year, int month, string isGenerate)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("\r\n                            --DECLARE @year INT,@month INT,@saBooksId NVARCHAR(500),@departmentId INT;\r\n                            --SET @year=2012;\r\n                            --SET @month=12;\r\n                            --SET @saBooksId='a4e1b731-00b6-4922-a326-c90fc0849f39';\r\n                            --SET @departmentId=123;\r\n                            select COUNT(*) from \r\n                            (\r\n                            select Row_Number() OVER (ORDER BY v_yhdm DESC) as Num,* from\r\n                            (\r\n                            SELECT a.UserCode,[v_yhdm],[v_xm],RoleTypeName,SaMonth.Month,SaMonth.year\r\n                            ,Name,CONVERT(DECIMAL(18,3),Cost) Cost ,ISNULL(IsPayoff,0) IsPayoff\r\n\t                        FROM [PT_yhmc] a \r\n                            LEFT JOIN (SELECT RoleTypeName, i_dutyId FROM pt_d_role\r\n                            INNER JOIN pt_duty ON  pt_d_role.RoleTypeCode=pt_duty.DutyCode) post ON a.i_dutyId=post.i_dutyId\r\n\t                        INNER JOIN  SA_UserSalaryBooks saUserBooks ON a.v_yhdm=saUserBooks.UserCode \r\n                            LEFT JOIN SA_MonthSalary SaMonth ON saUserBooks.UserCode=SaMonth.UserCode AND (saMonth.year=@year and saMonth.month=@month)\r\n                            LEFT JOIN SA_SalaryItem saItem ON SaMonth.ItemId=saItem.Id\r\n\t                        LEFT JOIN SA_Payoff payoff ON saMonth.Month=payoff.Month AND saMonth.Year=payoff.Year\r\n\t                        AND SaMonth.UserCode=payoff.UserCode AND (saMonth.year=@year and saMonth.month=@month)\r\n                            WHERE 1=1 ");
            list.Add(new SqlParameter("@year", year));
            list.Add(new SqlParameter("@month", month));
            string saItemsCSV = this.GetSaItemsCSV(saBooksId, year, month);
            if (departmentId != "1")
            {
                builder.AppendFormat(" AND [i_bmdm] = @departmentId ", new object[0]);
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.Append(" AND a.UserCode LIKE '%'+@userCode+'%' ");
                list.Add(new SqlParameter("@userCode", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.Append(" AND v_xm LIKE '%'+@userName+'%' ");
                list.Add(new SqlParameter("@userName", userName));
            }
            if (!string.IsNullOrEmpty(saBooksId))
            {
                builder.AppendFormat(" and ((saMonth.year=@year and saMonth.month=@month) or (saMonth.year is null and saMonth.month is null )) AND  saUserBooks.BooksId=@saBooksId ", new object[0]);
                list.Add(new SqlParameter("@saBooksId", saBooksId));
            }
            else
            {
                saItemsCSV = this.GetSaItemsCSV();
            }
            if (!string.IsNullOrEmpty(isGenerate))
            {
                if (isGenerate == "0")
                {
                    builder.AppendFormat(" AND saItem.Name IS NULL ", new object[0]);
                }
                else
                {
                    builder.AppendFormat(" AND saItem.Name IS NOT NULL ", new object[0]);
                }
            }
            builder.Append(") T ").AppendLine();
            if (!string.IsNullOrEmpty(saItemsCSV))
            {
                builder.Append(" PIVOT ").AppendLine();
                builder.Append("(").AppendLine();
                builder.AppendFormat("\tMAX(Cost) FOR Name in  ({0})", saItemsCSV).AppendLine();
                builder.Append(") AS pvt").AppendLine();
            }
            builder.Append(" ) as saMonth ").AppendLine();
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()));
        }

        public int GetSaMonthCountPayoff(string departmentId, string saBooksId, string userCode, string userName, int year, int month, string isPayoff)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                            select COUNT(*) from \r\n                            (\r\n                                select Row_Number() OVER (ORDER BY v_yhdm DESC) as Num,* from\r\n                                (\r\n                                SELECT a.UserCode,[v_yhdm],[v_xm],RoleTypeName,Month,year,\r\n\t                            Name,Cost FROM [PT_yhmc] a \r\n\t                            LEFT JOIN (SELECT RoleTypeName, i_dutyId FROM pt_d_role\r\n\t                            INNER JOIN pt_duty ON  pt_d_role.RoleTypeCode=pt_duty.DutyCode) post ON a.i_dutyId=post.i_dutyId\r\n\t                            LEFT JOIN SA_MonthSalary SaMonth ON a.[v_yhdm]=SaMonth.UserCode\r\n\t                            INNER JOIN SA_SalaryItem saItem ON SaMonth.ItemId=saItem.Id\r\n                                INNER JOIN  SA_UserSalaryBooks saUserBooks ON a.v_yhdm=saUserBooks.UserCode \r\n\t                            WHERE year=@year and month=@month  ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };
            string saItemsCSV = string.Empty;
            if (departmentId != "1")
            {
                builder.AppendFormat(" AND [i_bmdm] = @departmentId ", new object[0]);
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.Append(" AND a.UserCode LIKE '%'+@userCode+'%' ");
                list.Add(new SqlParameter("@userCode", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.Append(" AND v_xm LIKE '%'+@userName+'%' ");
                list.Add(new SqlParameter("@userName", userName));
            }
            if (!string.IsNullOrEmpty(saBooksId))
            {
                builder.AppendFormat(" AND  saUserBooks.BooksId=@saBooksId ", new object[0]);
                list.Add(new SqlParameter("@saBooksId", saBooksId));
                saItemsCSV = this.GetSaItemsCSV(saBooksId, year, month);
            }
            else
            {
                saItemsCSV = this.GetSaItemsCSV();
            }
            builder.Append(") T ").AppendLine();
            if (!string.IsNullOrEmpty(saItemsCSV))
            {
                builder.Append(" PIVOT ").AppendLine();
                builder.Append("(").AppendLine();
                builder.AppendFormat("\tMAX(Cost) FOR Name in  ({0})", saItemsCSV).AppendLine();
                builder.Append(") AS pvt").AppendLine();
            }
            builder.Append(" ) as saMonth ").AppendLine();
            builder.Append("\r\n                    LEFT JOIN SA_Payoff payoff ON saMonth.Month=payoff.Month AND saMonth.Year=payoff.Year\r\n                    AND saMonth.v_yhdm=payoff.UserCode \r\n                    where 1=1 ");
            if (!string.IsNullOrEmpty(isPayoff))
            {
                if (isPayoff == "1")
                {
                    builder.Append(" AND IsPayOff = 1 ");
                }
                else
                {
                    builder.Append(" AND (IsPayoff IS NULL OR IsPayoff = 0) ");
                }
            }
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()));
        }

        public int GetSaMonthCountReport(string departmentId, string userCode, string userName, int year, int month, string isPayoff)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                            select COUNT(*) from \r\n                            (\r\n                                select Row_Number() OVER (ORDER BY v_yhdm DESC) as Num,* from\r\n                                (\r\n                                SELECT a.UserCode,[v_yhdm],[v_xm],RoleTypeName,Month,year,\r\n\t                            Name,Cost FROM [PT_yhmc] a \r\n\t                            LEFT JOIN (SELECT RoleTypeName, i_dutyId FROM pt_d_role\r\n\t                            INNER JOIN pt_duty ON  pt_d_role.RoleTypeCode=pt_duty.DutyCode) post ON a.i_dutyId=post.i_dutyId\r\n\t                            LEFT JOIN SA_MonthSalary SaMonth ON a.[v_yhdm]=SaMonth.UserCode\r\n\t                            INNER JOIN SA_SalaryItem saItem ON SaMonth.ItemId=saItem.Id\r\n\t                            WHERE year=@year and month=@month  ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };
            string saItemsCSV = string.Empty;
            if (departmentId != "1")
            {
                builder.AppendFormat(" AND [i_bmdm] = @departmentId ", new object[0]);
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.Append(" AND a.UserCode LIKE '%'+@userCode+'%' ");
                list.Add(new SqlParameter("@userCode", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.Append(" AND v_xm LIKE '%'+@userName+'%' ");
                list.Add(new SqlParameter("@userName", userName));
            }
            saItemsCSV = this.GetSaItemsCSV();
            builder.Append(") T ").AppendLine();
            if (!string.IsNullOrEmpty(saItemsCSV))
            {
                builder.Append(" PIVOT ").AppendLine();
                builder.Append("(").AppendLine();
                builder.AppendFormat("\tMAX(Cost) FOR Name in  ({0})", saItemsCSV).AppendLine();
                builder.Append(") AS pvt").AppendLine();
            }
            builder.Append(" ) as saMonth ").AppendLine();
            builder.Append("\r\n                    LEFT JOIN SA_Payoff payoff ON saMonth.Month=payoff.Month AND saMonth.Year=payoff.Year\r\n                    AND saMonth.v_yhdm=payoff.UserCode \r\n                    where 1=1 ");
            if (!string.IsNullOrEmpty(isPayoff))
            {
                if (isPayoff == "1")
                {
                    builder.Append(" AND IsPayOff = 1 ");
                }
                else
                {
                    builder.Append(" AND (IsPayoff IS NULL OR IsPayoff = 0) ");
                }
            }
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()));
        }

        public DataTable GetSaMonthInfo(string departmentId, string saBooksId, string userCode, string userName, int year, int month, string isGenerate, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = this.GetSaMonthCount(departmentId, saBooksId, userCode, userName, year, month, isGenerate);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                            --DECLARE @pageSize INT,@pageIndex INT,@year INT,@month INT,@saBooksId NVARCHAR(500),@departmentId INT;\r\n                            --SET @pageSize=10000;\r\n                            --SET @pageIndex=1;\r\n                            --SET @year=2012;\r\n                            --SET @month=12;\r\n                            --SET @saBooksId='a4e1b731-00b6-4922-a326-c90fc0849f39';\r\n                            --SET @departmentId=123;\r\n                            select top(@pageSize) * from \r\n                            (\r\n                            SELECT Row_Number() OVER (ORDER BY v_yhdm DESC) as Num,saMonth.*,\r\n                            case IsPayoff when '1' then '是' else '否' end AS IsPayoffName FROM (\r\n                            select * from\r\n                            (\r\n                            SELECT a.UserCode,[v_yhdm],[v_xm],RoleTypeName,saBooks.Name BooksName,SaMonth.Month,SaMonth.year\r\n                            ,case ISNULL(saItem.Name,'') when '' then '否' else '是' end AS IsGenerate,saItem.Name,CONVERT(DECIMAL(18,3),Cost) Cost \r\n\t                        ,ISNULL(IsPayoff,0) IsPayoff FROM [PT_yhmc] a \r\n                            LEFT JOIN (SELECT RoleTypeName, i_dutyId FROM pt_d_role\r\n                            INNER JOIN pt_duty ON  pt_d_role.RoleTypeCode=pt_duty.DutyCode) post ON a.i_dutyId=post.i_dutyId\r\n\t                        INNER JOIN  SA_UserSalaryBooks saUserBooks ON a.v_yhdm=saUserBooks.UserCode \r\n                            LEFT JOIN SA_SalaryBooks saBooks ON saUserBooks.BooksId=saBooks.Id\r\n                            LEFT JOIN SA_MonthSalary SaMonth ON saUserBooks.UserCode=SaMonth.UserCode AND (saMonth.year=@year and saMonth.month=@month)\r\n                            LEFT JOIN SA_SalaryItem saItem ON SaMonth.ItemId=saItem.Id\r\n\t                        LEFT JOIN SA_Payoff payoff ON saMonth.Month=payoff.Month AND saMonth.Year=payoff.Year\r\n\t                        AND SaMonth.UserCode=payoff.UserCode AND (saMonth.year=@year and saMonth.month=@month)\r\n                            WHERE 1=1  ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@pageIndex", pageIndex),
                new SqlParameter("@pageSize", pageSize),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };
            string saItemsCSV = this.GetSaItemsCSNoPayoff(saBooksId, year, month, departmentId);
            if (departmentId != "1")
            {
                builder.AppendFormat(" AND [i_bmdm] = @departmentId ", new object[0]);
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.Append(" AND a.UserCode LIKE '%'+@userCode+'%' ");
                list.Add(new SqlParameter("@userCode", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.Append(" AND v_xm LIKE '%'+@userName+'%' ");
                list.Add(new SqlParameter("@userName", userName));
            }
            if (!string.IsNullOrEmpty(saBooksId))
            {
                builder.AppendFormat(" and ((saMonth.year=@year and saMonth.month=@month) or (saMonth.year is null and saMonth.month is null )) AND  saUserBooks.BooksId=@saBooksId ", new object[0]);
                list.Add(new SqlParameter("@saBooksId", saBooksId));
            }
            else
            {
                saItemsCSV = this.GetSaItemsCSV();
            }
            if (!string.IsNullOrEmpty(isGenerate))
            {
                if (isGenerate == "0")
                {
                    builder.AppendFormat(" AND saItem.Name IS NULL ", new object[0]);
                }
                else
                {
                    builder.AppendFormat(" AND saItem.Name IS NOT NULL ", new object[0]);
                }
            }
            builder.Append(") T ").AppendLine();
            if (!string.IsNullOrEmpty(saItemsCSV))
            {
                builder.Append(" PIVOT ").AppendLine();
                builder.Append("(").AppendLine();
                builder.AppendFormat("\tMAX(Cost) FOR Name in  ({0})", saItemsCSV).AppendLine();
                builder.Append(") AS pvt").AppendLine();
            }
            builder.Append(" ) as saMonth ").AppendLine();
            builder.Append(" ) as tab where Num>@pageSize*(@pageIndex-1) ").AppendLine();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetSaMonthInfoPayoff(string departmentId, string saBooksId, string userCode, string userName, int year, int month, string isPayoff, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = this.GetSaMonthCountPayoff(departmentId, saBooksId, userCode, userName, year, month, isPayoff);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                            --DECLARE @pageSize INT,@pageIndex INT,@departmentId NVARCHAR(50),@year INT,@month INT,@saBooksId  NVARCHAR(50);\r\n                            --DECLARE @isPayoff NVARCHAR(50);\r\n                            --set @pageSize=1000;\r\n                            --set @pageIndex=1\r\n                            --set @departmentId='1'\r\n                            --set @year=2012\r\n                            --set @month=12\r\n                            --set @saBooksId=''\r\n                            --SET @isPayoff='1'\r\n                            select top(@pageSize) * from \r\n                            (\r\n\t                            SELECT Row_Number() OVER (ORDER BY v_yhdm DESC) as Num,\r\n\t                            saMonth.* FROM (\r\n\t                            select * from\r\n\t                            (\r\n\t\t                            SELECT a.UserCode,[v_yhdm],[v_xm],case IsPayoff when '1' then '是' else '否' end AS IsPayoffName,\r\n\t\t                            ISNULL(IsPayoff,0) IsPayoff,saMonth.Month,saMonth.year,Name,CONVERT(DECIMAL(18,3),Cost) Cost FROM [PT_yhmc] a \r\n\t\t                            LEFT JOIN SA_MonthSalary SaMonth ON a.[v_yhdm]=SaMonth.UserCode\r\n\t\t                            INNER JOIN SA_SalaryItem saItem ON SaMonth.ItemId=saItem.Id\r\n\t\t                            INNER JOIN  SA_UserSalaryBooks saUserBooks ON a.v_yhdm=saUserBooks.UserCode \r\n\t\t                            LEFT JOIN SA_Payoff payoff ON saMonth.Month=payoff.Month AND saMonth.Year=payoff.Year\r\n\t\t                            AND a.v_yhdm=payoff.UserCode \r\n\t\t                            WHERE saMonth.year=@year and saMonth.month=@month   ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };
            string saItemsCSV = string.Empty;
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            list.Add(new SqlParameter("@pageSize", pageSize));
            if (departmentId != "1")
            {
                builder.AppendFormat(" AND [i_bmdm] = @departmentId ", new object[0]);
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.Append(" AND a.UserCode LIKE '%'+@userCode+'%' ");
                list.Add(new SqlParameter("@userCode", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.Append(" AND v_xm LIKE '%'+@userName+'%' ");
                list.Add(new SqlParameter("@userName", userName));
            }
            if (!string.IsNullOrEmpty(isPayoff))
            {
                if (isPayoff == "1")
                {
                    builder.Append(" AND IsPayOff = 1 ");
                }
                else
                {
                    builder.Append(" AND (IsPayoff IS NULL OR IsPayoff = 0) ");
                }
            }
            if (!string.IsNullOrEmpty(saBooksId))
            {
                builder.AppendFormat(" AND  saUserBooks.BooksId=@saBooksId ", new object[0]);
                list.Add(new SqlParameter("@saBooksId", saBooksId));
                saItemsCSV = this.GetSaItemsCSV(saBooksId, year, month);
            }
            else
            {
                saItemsCSV = this.GetSaItemsCSV();
            }
            builder.Append(") T ").AppendLine();
            if (!string.IsNullOrEmpty(saItemsCSV))
            {
                builder.Append(" PIVOT ").AppendLine();
                builder.Append("(").AppendLine();
                builder.AppendFormat("\tMAX(Cost) FOR Name in  ({0})", saItemsCSV).AppendLine();
                builder.Append(") AS pvt").AppendLine();
            }
            builder.Append(" ) as saMonth ").AppendLine();
            builder.Append(" ) as tab where Num>@pageSize*(@pageIndex-1) ").AppendLine();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetSaMonthInfoReport(string departmentId, string userCode, string userName, int year, int month, string isPayoff, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = this.GetSaMonthCountReport(departmentId, userCode, userName, year, month, isPayoff);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                            --DECLARE @pageSize INT,@pageIndex INT,@departmentId NVARCHAR(50),@year INT,@month INT,@saBooksId  NVARCHAR(50);\r\n                            --DECLARE @isPayoff NVARCHAR(50);\r\n                            --set @pageSize=1000;\r\n                            --set @pageIndex=1\r\n                            --set @departmentId='1'\r\n                            --set @year=2012\r\n                            --set @month=12\r\n                            --set @saBooksId=''\r\n                            --SET @isPayoff='1'\r\n                            select top(@pageSize) * from \r\n                            (\r\n\t                            SELECT Row_Number() OVER (ORDER BY v_yhdm DESC) as Num,\r\n\t                            saMonth.* FROM (\r\n\t                            select * from\r\n\t                            (\r\n\t\t                            SELECT a.UserCode,[v_yhdm],[v_xm],case IsPayoff when '1' then '是' else '否' end AS IsPayoffName,\r\n\t\t                            ISNULL(IsPayoff,0) IsPayoff,saMonth.Month,saMonth.year,Name,CONVERT(DECIMAL(18,3),Cost) Cost FROM [PT_yhmc] a \r\n\t\t                            LEFT JOIN SA_MonthSalary SaMonth ON a.[v_yhdm]=SaMonth.UserCode\r\n\t\t                            INNER JOIN SA_SalaryItem saItem ON SaMonth.ItemId=saItem.Id\r\n\t\t                            LEFT JOIN SA_Payoff payoff ON saMonth.Month=payoff.Month AND saMonth.Year=payoff.Year\r\n\t\t                            AND a.v_yhdm=payoff.UserCode \r\n\t\t                            WHERE saMonth.year=@year and saMonth.month=@month   ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };
            string saItemsCSV = string.Empty;
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            list.Add(new SqlParameter("@pageSize", pageSize));
            if (departmentId != "1")
            {
                builder.AppendFormat(" AND [i_bmdm] = @departmentId ", new object[0]);
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.Append(" AND a.UserCode LIKE '%'+@userCode+'%' ");
                list.Add(new SqlParameter("@userCode", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.Append(" AND v_xm LIKE '%'+@userName+'%' ");
                list.Add(new SqlParameter("@userName", userName));
            }
            if (!string.IsNullOrEmpty(isPayoff))
            {
                if (isPayoff == "1")
                {
                    builder.Append(" AND IsPayOff = 1 ");
                }
                else
                {
                    builder.Append(" AND (IsPayoff IS NULL OR IsPayoff = 0) ");
                }
            }
            saItemsCSV = this.GetSaItemsCSV();
            builder.Append(") T ").AppendLine();
            if (!string.IsNullOrEmpty(saItemsCSV))
            {
                builder.Append(" PIVOT ").AppendLine();
                builder.Append("(").AppendLine();
                builder.AppendFormat("\tMAX(Cost) FOR Name in  ({0})", saItemsCSV).AppendLine();
                builder.Append(") AS pvt").AppendLine();
            }
            builder.Append(" ) as saMonth ").AppendLine();
            builder.Append(" ) as tab where Num>@pageSize*(@pageIndex-1) ").AppendLine();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetUserInfoByDepartBooks(string departmentId, string booksId, int year, int month)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                            SELECT Row_Number() OVER (ORDER BY v_yhdm DESC) as Num, [v_yhdm], UserSaBooks.BooksId FROM [PT_yhmc] a\r\n                            INNER JOIN SA_UserSalaryBooks UserSaBooks ON a.v_yhdm= UserSaBooks.UserCode\r\n                            LEFT JOIN (SELECT Id,UserCode FROM SA_Payoff WHERE Year=@year AND Month=@month AND IsPayoff=1) payoff ON a.v_yhdm=payoff.UserCode \r\n                            WHERE UserSaBooks.BooksId=@booksId AND payoff.Id IS NULL ");
            List<SqlParameter> list = new List<SqlParameter>();
            if (departmentId != "1")
            {
                builder.AppendFormat(" AND [i_bmdm] = @departmentId ", new object[0]);
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            list.Add(new SqlParameter("@year", year));
            list.Add(new SqlParameter("@month", month));
            list.Add(new SqlParameter("@booksId", booksId));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetUserInfoSaBooks(string departmentId, string userCode, string userName, string postName, string isAssignSaBooks, string state, string saBooksId, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = this.GetUserSaBooksCount(departmentId, userCode, userName, postName, isAssignSaBooks, state, saBooksId);
            }
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                        select top(@pageSize) * from( SELECT Row_Number() OVER (ORDER BY v_yhdm DESC) as Num,a.UserCode, [v_yhdm], [v_xm], [i_bmdm],\r\n                        (select b.v_bmmc from pt_d_bm b where b.i_bmdm = a.i_bmdm) as bmmc,A.[I_DUTYID], RoleTypeName,\r\n                        [OtherDepts], [OtherDutyIDs], [i_xh], [c_sfyx], [MobilePhoneCode], [RelationCorp], [EnterCorpDate], [Age], [State], \r\n                        (case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end ) as StateName,[IsChargeMan],\r\n                        (case IsChargeMan when 'False' then '否' when 'True' then '是' end) IsChargeManName,\r\n                        UserSaBooks.Id,UserSaBooks.BooksId FROM [PT_yhmc] a \r\n                        LEFT JOIN (SELECT RoleTypeName, i_dutyId FROM pt_d_role\t\t--岗位\r\n                        INNER JOIN pt_duty ON  pt_d_role.RoleTypeCode=pt_duty.DutyCode) post ON a.i_dutyId=post.i_dutyId\r\n                        LEFT JOIN SA_UserSalaryBooks UserSaBooks ON a.v_yhdm= UserSaBooks.UserCode\r\n                        WHERE 1=1 ");
            StringBuilder builder2 = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@pageSize", pageSize),
                new SqlParameter("@pageIndex", pageIndex)
            };
            if (departmentId != "1")
            {
                builder2.AppendFormat(" AND [i_bmdm] = @departmentId ", new object[0]);
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder2.AppendFormat(" AND a.UserCode LIKE '%'+@userCode+'%' ", new object[0]);
                list.Add(new SqlParameter("@userCode", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder2.AppendFormat(" AND v_xm LIKE '%'+@userName+'%' ", new object[0]);
                list.Add(new SqlParameter("@userName", userName));
            }
            if (!string.IsNullOrEmpty(postName))
            {
                builder2.AppendFormat(" AND RoleTypeName LIKE '%'+@postName+'%' ", new object[0]);
                list.Add(new SqlParameter("@postName", postName));
            }
            if (!string.IsNullOrEmpty(state))
            {
                builder2.AppendFormat(" AND a.State = @state  ", new object[0]);
                list.Add(new SqlParameter("@state", state));
            }
            if (!string.IsNullOrEmpty(isAssignSaBooks))
            {
                if (isAssignSaBooks == "true")
                {
                    builder2.AppendFormat(" AND UserSaBooks.Id IS NOT NULL ", new object[0]);
                }
                else
                {
                    builder2.AppendFormat(" AND UserSaBooks.Id IS NULL ", new object[0]);
                }
            }
            if (!string.IsNullOrEmpty(saBooksId))
            {
                builder2.AppendFormat(" AND BooksId = @saBooksId  ", new object[0]);
                list.Add(new SqlParameter("@saBooksId", saBooksId));
            }
            builder.Append(builder2.ToString());
            builder.Append(" ) Tab  WHERE Num >@pageSize * (@pageIndex-1) ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public int GetUserSaBooksCount(string departmentId, string userCode, string userName, string postName, string isAssignSaBooks, string state, string saBooksId)
        {
            new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append(" \r\n                    SELECT COUNT(*) FROM (SELECT Row_Number() OVER (ORDER BY v_yhdm DESC) as Num,a.UserCode, [v_yhdm], [v_xm], [i_bmdm],\r\n                    (select b.v_bmmc from pt_d_bm b where b.i_bmdm = a.i_bmdm) as bmmc,A.[I_DUTYID], RoleTypeName,\r\n                        [OtherDepts], [OtherDutyIDs], [i_xh], [c_sfyx], [MobilePhoneCode], [RelationCorp], [EnterCorpDate], [Age], [State], \r\n                        (case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end ) as StateName,[IsChargeMan],\r\n                    (case IsChargeMan when 'False' then '否' when 'True' then '是' end) IsChargeManName,\r\n                    UserSaBooks.Id,UserSaBooks.BooksId FROM [PT_yhmc] a \r\n                    LEFT JOIN (SELECT RoleTypeName, i_dutyId FROM pt_d_role\t\t--岗位\r\n                    INNER JOIN pt_duty ON  pt_d_role.RoleTypeCode=pt_duty.DutyCode) post ON a.i_dutyId=post.i_dutyId\r\n                    LEFT JOIN SA_UserSalaryBooks UserSaBooks ON a.v_yhdm= UserSaBooks.UserCode\r\n                    WHERE 1=1 ");
            StringBuilder builder2 = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (departmentId != "1")
            {
                builder2.AppendFormat(" AND [i_bmdm] = @departmentId ", new object[0]);
                list.Add(new SqlParameter("@departmentId", departmentId));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder2.AppendFormat(" AND a.UserCode LIKE '%'+@userCode+'%' ", new object[0]);
                list.Add(new SqlParameter("@userCode", userCode));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder2.AppendFormat(" AND v_xm LIKE '%'+@userName+'%' ", new object[0]);
                list.Add(new SqlParameter("@userName", userName));
            }
            if (!string.IsNullOrEmpty(postName))
            {
                builder2.AppendFormat(" AND RoleTypeName LIKE '%'+@postName+'%' ", new object[0]);
                list.Add(new SqlParameter("@postName", postName));
            }
            if (!string.IsNullOrEmpty(state))
            {
                builder2.AppendFormat(" AND a.State = @state  ", new object[0]);
                list.Add(new SqlParameter("@state", state));
            }
            if (!string.IsNullOrEmpty(isAssignSaBooks))
            {
                if (isAssignSaBooks == "true")
                {
                    builder2.AppendFormat(" AND UserSaBooks.Id IS NOT NULL ", new object[0]);
                }
                else
                {
                    builder2.AppendFormat(" AND UserSaBooks.Id IS NULL ", new object[0]);
                }
            }
            if (!string.IsNullOrEmpty(saBooksId))
            {
                builder2.AppendFormat(" AND BooksId = @saBooksId  ", new object[0]);
                list.Add(new SqlParameter("@saBooksId", saBooksId));
            }
            builder.Append(builder2.ToString());
            builder.Append(" ) Tab ");
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()));
        }

        public int Update(PtYhmc model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_yhmc set ");
            builder.Append("v_xm=@v_xm,");
            builder.Append("i_bmdm=@i_bmdm,");
            builder.Append("I_DUTYID=@I_DUTYID,");
            builder.Append("OtherDepts=@OtherDepts,");
            builder.Append("OtherDutyIDs=@OtherDutyIDs,");
            builder.Append("i_xh=@i_xh,");
            builder.Append("c_sfyx=@c_sfyx,");
            builder.Append("MobilePhoneCode=@MobilePhoneCode,");
            builder.Append("RelationCorp=@RelationCorp,");
            builder.Append("EnterCorpDate=@EnterCorpDate,");
            builder.Append("Age=@Age,");
            builder.Append("State=@State,");
            builder.Append("SuperordinateDuty=@SuperordinateDuty,");
            builder.Append("Sex=@Sex,");
            builder.Append("Nation=@Nation,");
            builder.Append("EducationalBackground=@EducationalBackground,");
            builder.Append("Stature=@Stature,");
            builder.Append("ClassID=@ClassID,");
            builder.Append("PositionLevel=@PositionLevel,");
            builder.Append("PostAndRank=@PostAndRank,");
            builder.Append("RegisteredPlace=@RegisteredPlace,");
            builder.Append("Address=@Address,");
            builder.Append("ComputeLevel=@ComputeLevel,");
            builder.Append("EnglishLevel=@EnglishLevel,");
            builder.Append("DriveLevel=@DriveLevel,");
            builder.Append("Marriage=@Marriage,");
            builder.Append("Tel=@Tel,");
            builder.Append("ExpectationSalary=@ExpectationSalary,");
            builder.Append("CommunicateAddress=@CommunicateAddress,");
            builder.Append("Postcode=@Postcode,");
            builder.Append("UserPhoto=@UserPhoto,");
            builder.Append("Birthday=@Birthday,");
            builder.Append("IDCard=@IDCard,");
            builder.Append("FormalDate=@FormalDate,");
            builder.Append("PoliticsFace=@PoliticsFace,");
            builder.Append("JoinPartyDate=@JoinPartyDate,");
            builder.Append("JoinCorpMode=@JoinCorpMode,");
            builder.Append("Introducer=@Introducer,");
            builder.Append("Specialty=@Specialty,");
            builder.Append("GraduateSchool=@GraduateSchool,");
            builder.Append("Level=@Level,");
            builder.Append("BeginWorkDate=@BeginWorkDate,");
            builder.Append("PostAndCompetency=@PostAndCompetency,");
            builder.Append("EndowmentInsurance=@EndowmentInsurance,");
            builder.Append("MedicareInsurance=@MedicareInsurance,");
            builder.Append("UnemploymentInsurance=@UnemploymentInsurance,");
            builder.Append("InjuryInsurance=@InjuryInsurance,");
            builder.Append("HousingAccumulationFund=@HousingAccumulationFund,");
            builder.Append("PersonSuddennessInsurance=@PersonSuddennessInsurance");
            builder.Append(" where v_yhdm=@v_yhdm ");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@v_yhdm", SqlDbType.VarChar, 8), new SqlParameter("@v_xm", SqlDbType.VarChar, 50), new SqlParameter("@i_bmdm", SqlDbType.Int, 4), new SqlParameter("@I_DUTYID", SqlDbType.Int, 4), new SqlParameter("@OtherDepts", SqlDbType.VarChar, 100), new SqlParameter("@OtherDutyIDs", SqlDbType.VarChar, 100), new SqlParameter("@i_xh", SqlDbType.Int, 4), new SqlParameter("@c_sfyx", SqlDbType.Char, 1), new SqlParameter("@MobilePhoneCode", SqlDbType.VarChar, 13), new SqlParameter("@RelationCorp", SqlDbType.VarChar, 2), new SqlParameter("@EnterCorpDate", SqlDbType.DateTime), new SqlParameter("@Age", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@SuperordinateDuty", SqlDbType.Int, 4), new SqlParameter("@Sex", SqlDbType.Char, 1), new SqlParameter("@Nation", SqlDbType.VarChar, 50), 
                new SqlParameter("@EducationalBackground", SqlDbType.VarChar, 50), new SqlParameter("@Stature", SqlDbType.VarChar, 50), new SqlParameter("@ClassID", SqlDbType.Int, 4), new SqlParameter("@PositionLevel", SqlDbType.Int, 4), new SqlParameter("@PostAndRank", SqlDbType.VarChar, 50), new SqlParameter("@RegisteredPlace", SqlDbType.VarChar, 300), new SqlParameter("@Address", SqlDbType.VarChar, 300), new SqlParameter("@ComputeLevel", SqlDbType.VarChar, 50), new SqlParameter("@EnglishLevel", SqlDbType.VarChar, 50), new SqlParameter("@DriveLevel", SqlDbType.VarChar, 50), new SqlParameter("@Marriage", SqlDbType.VarChar, 50), new SqlParameter("@Tel", SqlDbType.VarChar, 20), new SqlParameter("@ExpectationSalary", SqlDbType.Decimal, 5), new SqlParameter("@CommunicateAddress", SqlDbType.VarChar, 200), new SqlParameter("@Postcode", SqlDbType.VarChar, 6), new SqlParameter("@UserPhoto", SqlDbType.VarChar, 100), 
                new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@IDCard", SqlDbType.VarChar, 20), new SqlParameter("@FormalDate", SqlDbType.DateTime), new SqlParameter("@PoliticsFace", SqlDbType.VarChar, 50), new SqlParameter("@JoinPartyDate", SqlDbType.DateTime), new SqlParameter("@JoinCorpMode", SqlDbType.VarChar, 50), new SqlParameter("@Introducer", SqlDbType.VarChar, 20), new SqlParameter("@Specialty", SqlDbType.VarChar, 50), new SqlParameter("@GraduateSchool", SqlDbType.VarChar, 100), new SqlParameter("@Level", SqlDbType.VarChar, 50), new SqlParameter("@BeginWorkDate", SqlDbType.DateTime), new SqlParameter("@PostAndCompetency", SqlDbType.VarChar, 50), new SqlParameter("@EndowmentInsurance", SqlDbType.Char, 1), new SqlParameter("@MedicareInsurance", SqlDbType.Char, 1), new SqlParameter("@UnemploymentInsurance", SqlDbType.Char, 1), new SqlParameter("@InjuryInsurance", SqlDbType.Char, 1), 
                new SqlParameter("@HousingAccumulationFund", SqlDbType.Char, 1), new SqlParameter("@PersonSuddennessInsurance", SqlDbType.Char, 1)
             };
            commandParameters[0].Value = model.v_yhdm;
            commandParameters[1].Value = model.v_xm;
            commandParameters[2].Value = model.i_bmdm;
            commandParameters[3].Value = model.I_DUTYID;
            commandParameters[4].Value = model.OtherDepts;
            commandParameters[5].Value = model.OtherDutyIDs;
            commandParameters[6].Value = model.i_xh;
            commandParameters[7].Value = model.c_sfyx;
            commandParameters[8].Value = model.MobilePhoneCode;
            commandParameters[9].Value = model.RelationCorp;
            commandParameters[10].Value = model.EnterCorpDate;
            commandParameters[11].Value = model.Age;
            commandParameters[12].Value = model.State;
            commandParameters[13].Value = model.SuperordinateDuty;
            commandParameters[14].Value = model.Sex;
            commandParameters[15].Value = model.Nation;
            commandParameters[0x10].Value = model.EducationalBackground;
            commandParameters[0x11].Value = model.Stature;
            commandParameters[0x12].Value = model.ClassID;
            commandParameters[0x13].Value = model.PositionLevel;
            commandParameters[20].Value = model.PostAndRank;
            commandParameters[0x15].Value = model.RegisteredPlace;
            commandParameters[0x16].Value = model.Address;
            commandParameters[0x17].Value = model.ComputeLevel;
            commandParameters[0x18].Value = model.EnglishLevel;
            commandParameters[0x19].Value = model.DriveLevel;
            commandParameters[0x1a].Value = model.Marriage;
            commandParameters[0x1b].Value = model.Tel;
            commandParameters[0x1c].Value = model.ExpectationSalary;
            commandParameters[0x1d].Value = model.CommunicateAddress;
            commandParameters[30].Value = model.Postcode;
            commandParameters[0x1f].Value = model.UserPhoto;
            commandParameters[0x20].Value = model.Birthday;
            commandParameters[0x21].Value = model.IDCard;
            commandParameters[0x22].Value = model.FormalDate;
            commandParameters[0x23].Value = model.PoliticsFace;
            commandParameters[0x24].Value = model.JoinPartyDate;
            commandParameters[0x25].Value = model.JoinCorpMode;
            commandParameters[0x26].Value = model.Introducer;
            commandParameters[0x27].Value = model.Specialty;
            commandParameters[40].Value = model.GraduateSchool;
            commandParameters[0x29].Value = model.Level;
            commandParameters[0x2a].Value = model.BeginWorkDate;
            commandParameters[0x2b].Value = model.PostAndCompetency;
            commandParameters[0x2c].Value = model.EndowmentInsurance;
            commandParameters[0x2d].Value = model.MedicareInsurance;
            commandParameters[0x2e].Value = model.UnemploymentInsurance;
            commandParameters[0x2f].Value = model.InjuryInsurance;
            commandParameters[0x30].Value = model.HousingAccumulationFund;
            commandParameters[0x31].Value = model.PersonSuddennessInsurance;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

