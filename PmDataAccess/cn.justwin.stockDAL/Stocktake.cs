namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Stocktake
    {
        public int Add(SqlTransaction trans, StocktakeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Sm_Stocktake ");
            builder.Append("(StocktakeId,StocktakeCode,StocktakeName,TreasuryCode,StocktakeDate,InputUser,InputDate,BeginDate,EndDate,State,Note,FlowState) ");
            builder.Append("values (@StocktakeId,@StocktakeCode,@StocktakeName,@TreasuryCode,@StocktakeDate,@InputUser,@InputDate,@BeginDate,@EndDate,");
            builder.Append("@State,@Note,@FlowState)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@StocktakeId", SqlDbType.NVarChar, 500), new SqlParameter("@StocktakeCode", SqlDbType.NVarChar, 500), new SqlParameter("@StocktakeName", SqlDbType.NVarChar, 500), new SqlParameter("@TreasuryCode", SqlDbType.NVarChar, 0x200), new SqlParameter("@StocktakeDate", SqlDbType.NVarChar, 100), new SqlParameter("@InputUser", SqlDbType.NVarChar, 500), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@BeginDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@Note", SqlDbType.Text), new SqlParameter("@FlowState", SqlDbType.Int) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.Code;
            commandParameters[2].Value = model.Name;
            commandParameters[3].Value = model.TreasuryCode;
            commandParameters[4].Value = model.StocktakeDate;
            commandParameters[5].Value = model.InputUser;
            commandParameters[6].Value = model.InputDate;
            commandParameters[7].Value = model.BeginDate;
            commandParameters[8].Value = model.EndDate;
            commandParameters[9].Value = model.State;
            commandParameters[10].Value = model.Note;
            commandParameters[11].Value = model.FlowState;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public DateTime GetAllocationDate(string treasuryCode)
        {
            DateTime time = new DateTime();
            string cmdText = "SELECT TOP 1 IsinTime FROM dbo.Sm_Allocation WHERE TcodeB=@treasuryCode AND Isinb=1 AND Isintime is not null ORDER BY IsInTime ASC";
            SqlParameter parameter = new SqlParameter("@treasuryCode", SqlDbType.NVarChar, 500) {
                Value = treasuryCode
            };
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, new SqlParameter[] { parameter }))
            {
                if (reader.Read())
                {
                    return Convert.ToDateTime(reader["IsinTime"].ToString());
                }
                return DateTime.Now;
            }
        }

        public StocktakeModel GetById(string stocktakeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT StocktakeId,StocktakeCode,StocktakeName,TreasuryCode,Tname,StocktakeDate,InputUser,InputDate,BeginDate,EndDate,LockDate,State,Note ");
            builder.Append("FROM Sm_Stocktake LEFT JOIN dbo.Sm_Treasury ON TreasuryCode=Tcode WHERE StocktakeId=@StocktakeId ");
            SqlParameter parameter = new SqlParameter("@StocktakeId", SqlDbType.NVarChar, 500) {
                Value = stocktakeId
            };
            StocktakeModel model = new StocktakeModel();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter }))
            {
                if (!reader.Read())
                {
                    return model;
                }
                model.Id = stocktakeId;
                model.Code = reader["StocktakeCode"].ToString();
                model.Name = reader["StocktakeName"].ToString();
                model.TreasuryCode = reader["TreasuryCode"].ToString();
                model.TreasuryName = reader["Tname"].ToString();
                model.StocktakeDate = reader["StocktakeDate"].ToString();
                model.InputUser = reader["InputUser"].ToString();
                model.InputDate = Convert.ToDateTime(reader["InputDate"].ToString());
                model.BeginDate = Convert.ToDateTime(reader["BeginDate"].ToString());
                model.EndDate = Convert.ToDateTime(reader["EndDate"].ToString());
                if (reader["LockDate"].ToString() != "")
                {
                    model.LockDate = Convert.ToDateTime(reader["LockDate"].ToString());
                }
                model.State = Convert.ToInt32(reader["State"]);
                if (Convert.ToInt32(reader["State"]) == 0)
                {
                    model.StateName = "挂起";
                }
                else if (Convert.ToInt32(reader["State"]) == 1)
                {
                    model.StateName = "未锁定";
                }
                else if (Convert.ToInt32(reader["State"]) == 2)
                {
                    model.StateName = "已锁定";
                }
                model.Note = reader["Note"].ToString();
            }
            return model;
        }

        public List<StocktakeModel> GetByTreasuryCode(string treasuryCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT StocktakeId,StocktakeCode,StocktakeName,TreasuryCode,Tname,StocktakeDate,InputUser,InputDate,BeginDate,EndDate,LockDate,State,FlowState, ");
            builder.Append("Note FROM Sm_Stocktake LEFT JOIN dbo.Sm_Treasury ON TreasuryCode=Tcode WHERE TreasuryCode=@TreasuryCode ORDER BY InputDate DESC");
            SqlParameter parameter = new SqlParameter("@TreasuryCode", SqlDbType.NVarChar, 500) {
                Value = treasuryCode
            };
            List<StocktakeModel> list = new List<StocktakeModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter }))
            {
                while (reader.Read())
                {
                    StocktakeModel item = new StocktakeModel {
                        Id = reader["StocktakeId"].ToString(),
                        Code = reader["StocktakeCode"].ToString(),
                        Name = reader["StocktakeName"].ToString(),
                        TreasuryCode = reader["TreasuryCode"].ToString(),
                        TreasuryName = reader["Tname"].ToString(),
                        StocktakeDate = reader["StocktakeDate"].ToString(),
                        InputUser = reader["InputUser"].ToString(),
                        InputDate = DBHelper.GetDateTime(reader["InputDate"]),
                        BeginDate = DBHelper.GetDateTime(reader["BeginDate"]),
                        EndDate = DBHelper.GetDateTime(reader["EndDate"]),
                        FlowState = DBHelper.GetInt(reader["FlowState"])
                    };
                    if (reader["LockDate"].ToString() != "")
                    {
                        item.LockDate = DBHelper.GetDateTime(reader["LockDate"]);
                    }
                    item.State = DBHelper.GetInt(reader["State"]);
                    if (item.State == 0)
                    {
                        item.StateName = "挂起";
                    }
                    else if (item.State == 1)
                    {
                        item.StateName = "未锁定";
                    }
                    else if (item.State == 2)
                    {
                        item.StateName = "<span style='color:#008B45;' state=2>已锁定</span>";
                    }
                    item.Note = reader["Note"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public int GetCountByMoreStDate(string treasuryCode, string stocktakeDate)
        {
            string cmdText = "SELECT COUNT(*) FROM dbo.Sm_Stocktake WHERE TreasuryCode=@treasuryCode AND StocktakeDate>@stocktakeDate AND FlowState =1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@treasuryCode", SqlDbType.NVarChar, 500), new SqlParameter("@stocktakeDate", SqlDbType.NVarChar, 6) };
            commandParameters[0].Value = treasuryCode;
            commandParameters[1].Value = stocktakeDate;
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public int GetCountByStDate(string treasuryCode, string stocktakeDate)
        {
            string cmdText = "SELECT COUNT(*) FROM dbo.Sm_Stocktake WHERE TreasuryCode=@treasuryCode AND StocktakeDate=@stocktakeDate AND FlowState =1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@treasuryCode", SqlDbType.NVarChar, 500), new SqlParameter("@stocktakeDate", SqlDbType.NVarChar, 6) };
            commandParameters[0].Value = treasuryCode;
            commandParameters[1].Value = stocktakeDate;
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public StocktakeModel GetEditModel(string treasuryCode)
        {
            string cmdText = "\r\n\t\t\t\tSELECT StocktakeId,StocktakeCode,StocktakeName,TreasuryCode,\r\n\t\t\t\t\tStocktakeDate,InputUser,InputDate,BeginDate,EndDate,LockDate,State,Note,FlowState \r\n\t\t\t\tFROM dbo.Sm_Stocktake WHERE TreasuryCode=@treasuryCode AND (FlowState='-1' OR FlowState='0')\r\n\t\t\t";
            SqlParameter parameter = new SqlParameter("@treasuryCode", SqlDbType.NVarChar, 500) {
                Value = treasuryCode
            };
            StocktakeModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, new SqlParameter[] { parameter }))
            {
                if (reader.Read())
                {
                    model = new StocktakeModel {
                        Id = reader["stocktakeId"].ToString(),
                        Code = reader["StocktakeCode"].ToString(),
                        Name = reader["StocktakeName"].ToString(),
                        TreasuryCode = reader["TreasuryCode"].ToString(),
                        StocktakeDate = reader["StocktakeDate"].ToString(),
                        InputUser = reader["InputUser"].ToString(),
                        InputDate = Convert.ToDateTime(reader["InputDate"].ToString()),
                        BeginDate = Convert.ToDateTime(reader["BeginDate"].ToString()),
                        EndDate = Convert.ToDateTime(reader["EndDate"].ToString()),
                        State = Convert.ToInt32(reader["State"]),
                        FlowState = Convert.ToInt32(reader["FlowState"]),
                        Note = reader["Note"].ToString()
                    };
                }
            }
            return model;
        }

        public DateTime GetInitializeDate(string treasuryCode)
        {
            DateTime time = new DateTime();
            string cmdText = "SELECT TOP 1 InTime FROM dbo.Sm_Treasury_Stock WHERE Tcode=@treasuryCode AND Type='I'";
            SqlParameter parameter = new SqlParameter("@treasuryCode", SqlDbType.NVarChar, 500) {
                Value = treasuryCode
            };
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, new SqlParameter[] { parameter }))
            {
                if (reader.Read())
                {
                    return Convert.ToDateTime(reader["InTime"].ToString());
                }
                return DateTime.Now;
            }
        }

        public StocktakeModel GetLastStocktakeModel(string treasuryCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 StocktakeId,StocktakeCode,StocktakeName,TreasuryCode,StocktakeDate,InputUser,InputDate,BeginDate,EndDate,LockDate,State,Note ");
            builder.Append("FROM Sm_Stocktake WHERE TreasuryCode=@treasuryCode  ORDER BY LockDate DESC");
            SqlParameter parameter = new SqlParameter("@treasuryCode", SqlDbType.NVarChar, 500) {
                Value = treasuryCode
            };
            StocktakeModel model = new StocktakeModel();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter }))
            {
                if (!reader.Read())
                {
                    return model;
                }
                model.Id = reader["StocktakeId"].ToString();
                model.Code = reader["StocktakeCode"].ToString();
                model.Name = reader["StocktakeName"].ToString();
                model.TreasuryCode = reader["TreasuryCode"].ToString();
                model.StocktakeDate = reader["StocktakeDate"].ToString();
                model.InputUser = reader["InputUser"].ToString();
                model.InputDate = Convert.ToDateTime(reader["InputDate"].ToString());
                model.BeginDate = Convert.ToDateTime(reader["BeginDate"].ToString());
                model.EndDate = Convert.ToDateTime(reader["EndDate"].ToString());
                if (reader["LockDate"].ToString() != "")
                {
                    model.LockDate = Convert.ToDateTime(reader["LockDate"].ToString());
                }
                model.State = Convert.ToInt32(reader["State"]);
                model.Note = reader["Note"].ToString();
            }
            return model;
        }

        public DateTime GetStorageDate(string treasuryCode)
        {
            DateTime time = new DateTime();
            string cmdText = "SELECT TOP 1 Isintime FROM Sm_Storage WHERE Tcode=@treasuryCode AND Inflag='1' AND Isintime is not null ORDER BY Isintime ASC";
            SqlParameter parameter = new SqlParameter("@treasuryCode", SqlDbType.NVarChar, 500) {
                Value = treasuryCode
            };
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, new SqlParameter[] { parameter }))
            {
                if (reader.Read())
                {
                    return Convert.ToDateTime(reader["Isintime"].ToString());
                }
                return DateTime.Now;
            }
        }

        public bool IsAdd(string treasuryCode)
        {
            bool flag = true;
            string cmdText = "SELECT count(*) FROM dbo.Sm_Stocktake WHERE TreasuryCode=@treasuryCode and FlowState='-1' ";
            SqlParameter parameter = new SqlParameter("@treasuryCode", SqlDbType.NVarChar, 500) {
                Value = treasuryCode
            };
            if (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter })) > 0)
            {
                flag = false;
            }
            return flag;
        }

        public bool IsFirst(string treasuryCode)
        {
            bool flag = true;
            string cmdText = "SELECT count(*) FROM dbo.Sm_Stocktake WHERE TreasuryCode=@treasuryCode ";
            SqlParameter parameter = new SqlParameter("@treasuryCode", SqlDbType.NVarChar, 500) {
                Value = treasuryCode
            };
            if (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter })) > 0)
            {
                flag = false;
            }
            return flag;
        }

        public int LockStocktake(SqlTransaction trans, StocktakeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Sm_Stocktake SET LockDate=@LockDate,State=@State WHERE StocktakeId=@StocktakeId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@StocktakeId", SqlDbType.NVarChar, 500), new SqlParameter("@LockDate", SqlDbType.DateTime), new SqlParameter("@State", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.LockDate;
            commandParameters[2].Value = model.State;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int UpdateState(SqlTransaction trans, StocktakeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Sm_Stocktake SET StocktakeName=@StocktakeName,StocktakeDate=@StocktakeDate,EndDate=@EndDate,State=@State, ");
            builder.Append("Note=@Note WHERE StocktakeId=@StocktakeId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@StocktakeId", SqlDbType.NVarChar, 500), new SqlParameter("@StocktakeName", SqlDbType.NVarChar, 500), new SqlParameter("@StocktakeDate", SqlDbType.NVarChar, 100), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@Note", SqlDbType.Text) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.Name;
            commandParameters[2].Value = model.StocktakeDate;
            commandParameters[3].Value = model.EndDate;
            commandParameters[4].Value = model.State;
            commandParameters[5].Value = model.Note;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

