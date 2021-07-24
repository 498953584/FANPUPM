namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class StocktakeDetail
    {
        public void Add(SqlTransaction trans, List<StocktakeDetailModel> listStocktakeModel)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                if (listStocktakeModel.Count > 0)
                {
                    Delete(trans, listStocktakeModel[0].StocktakeId);
                    foreach (StocktakeDetailModel model in listStocktakeModel)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append("INSERT INTO Sm_Stocktake_Detail (DetailId,StocktakeId,ResourceCode,ResourceName,Specification,Unit,Price, ");
                        builder.Append("SupplierId,Supplier,LastMonthNum,StorageNum,FirstStorageNum,OutReserveNum,TransferringInNum,TransferringOutNum, ");
                        builder.Append("RefundingNum,BookNum,StocktakeNum,InputDate,Note,WastageNum) values (@DetailId,@StocktakeId,@ResourceCode, ");
                        builder.Append("@ResourceName,@Specification,@Unit,@Price,@SupplierId,@Supplier,@LastMonthNum,@StorageNum,@FirstStorageNum, ");
                        builder.Append("@OutReserveNum,@TransferringInNum,@TransferringOutNum,@RefundingNum,@BookNum,@StocktakeNum,@InputDate,@Note,@WastageNum) ");
                        SqlParameter[] commandParameters = new SqlParameter[] { 
                            new SqlParameter("@DetailId", SqlDbType.NVarChar, 500), new SqlParameter("@StocktakeId", SqlDbType.NVarChar, 500), new SqlParameter("@ResourceCode", SqlDbType.NVarChar, 500), new SqlParameter("@ResourceName", SqlDbType.NVarChar, 500), new SqlParameter("@Specification", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Unit", SqlDbType.NVarChar, 500), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@SupplierId", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Supplier", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@LastMonthNum", SqlDbType.Decimal, 9), new SqlParameter("@StorageNum", SqlDbType.Decimal, 9), new SqlParameter("@FirstStorageNum", SqlDbType.Decimal, 9), new SqlParameter("@OutReserveNum", SqlDbType.Decimal, 9), new SqlParameter("@TransferringInNum", SqlDbType.Decimal, 9), new SqlParameter("@TransferringOutNum", SqlDbType.Decimal, 9), new SqlParameter("@RefundingNum", SqlDbType.Decimal, 9), 
                            new SqlParameter("@BookNum", SqlDbType.Decimal, 9), new SqlParameter("@StocktakeNum", SqlDbType.Decimal, 9), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@Note", SqlDbType.Text), new SqlParameter("@WastageNum", SqlDbType.NVarChar, 500)
                         };
                        commandParameters[0].Value = Guid.NewGuid().ToString();
                        commandParameters[1].Value = model.StocktakeId;
                        commandParameters[2].Value = model.ResourceCode;
                        commandParameters[3].Value = model.ResourceName;
                        commandParameters[4].Value = model.Specification;
                        commandParameters[5].Value = model.Unit;
                        commandParameters[6].Value = model.Price;
                        commandParameters[7].Value = model.SupplierId;
                        commandParameters[8].Value = model.Supplier;
                        commandParameters[9].Value = model.LastMonthNum;
                        commandParameters[10].Value = model.StorageNum;
                        commandParameters[11].Value = model.FirstStorageNum;
                        commandParameters[12].Value = model.OutReserveNum;
                        commandParameters[13].Value = model.TransferringInNum;
                        commandParameters[14].Value = model.TransferringOutNum;
                        commandParameters[15].Value = model.RefundingNum;
                        commandParameters[0x10].Value = model.BookNum;
                        commandParameters[0x11].Value = model.StocktakeNum;
                        commandParameters[0x12].Value = model.InputDate;
                        commandParameters[0x13].Value = model.Note;
                        commandParameters[20].Value = model.WastageNum;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
                    }
                }
            }
        }

        public void AddTreasuryStock(SqlTransaction trans, TreasuryStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Treasury_Stock(");
            builder.Append("tsid,scode,tcode,sprice,snumber,isfirst,corp,incode,intime,intype,type)");
            builder.Append(" values (");
            builder.Append("@tsid,@scode,@tcode,@sprice,@snumber,@isfirst,@corp,@incode,@intime,@intype,@type)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tsid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@snumber", SqlDbType.Decimal, 9), new SqlParameter("@isfirst", SqlDbType.Bit, 1), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@incode", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@intype", SqlDbType.Int, 4), new SqlParameter("@type", SqlDbType.Char, 1) };
            commandParameters[0].Value = model.tsid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.tcode;
            commandParameters[3].Value = model.sprice;
            commandParameters[4].Value = model.snumber;
            commandParameters[5].Value = model.isfirst;
            commandParameters[6].Value = model.corp;
            commandParameters[7].Value = model.incode;
            commandParameters[8].Value = model.intime;
            commandParameters[9].Value = model.intype;
            commandParameters[10].Value = 'T';
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        private static void Delete(SqlTransaction trans, string stocktakeId)
        {
            string cmdText = "DELETE Sm_Stocktake_Detail WHERE StocktakeId=@StocktakeId";
            SqlParameter parameter = new SqlParameter("@StocktakeId", SqlDbType.NVarChar, 500) {
                Value = stocktakeId
            };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public int DeleteTreasuryStock(SqlTransaction trans, string tsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Treasury_Stock ");
            builder.Append(" where tsid=@tsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tsid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = tsid;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<StocktakeDetailModel> GetByStocktakeId(string StocktakeId)
        {
            List<StocktakeDetailModel> list = new List<StocktakeDetailModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT DetailId,StocktakeId,ResourceCode,ResourceName,Specification,Unit,Price,SupplierId,Supplier,LastMonthNum,StorageNum,");
            builder.Append("FirstStorageNum,OutReserveNum,TransferringInNum,TransferringOutNum,WastageNum,RefundingNum,BookNum,StocktakeNum,InputDate,Note");
            builder.Append(" FROM Sm_Stocktake_Detail WHERE StocktakeId=@StocktakeId ORDER BY ResourceCode");
            SqlParameter parameter = new SqlParameter("@StocktakeId", SqlDbType.NVarChar, 500) {
                Value = StocktakeId
            };
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter }))
            {
                while (reader.Read())
                {
                    StocktakeDetailModel item = new StocktakeDetailModel {
                        Id = reader["DetailId"].ToString(),
                        StocktakeId = reader["StocktakeId"].ToString(),
                        ResourceCode = reader["ResourceCode"].ToString(),
                        ResourceName = reader["ResourceName"].ToString(),
                        Specification = reader["Specification"].ToString(),
                        Unit = reader["Unit"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        SupplierId = reader["SupplierId"].ToString(),
                        Supplier = reader["Supplier"].ToString(),
                        LastMonthNum = Convert.ToDecimal(reader["LastMonthNum"]),
                        StorageNum = Convert.ToDecimal(reader["StorageNum"]),
                        FirstStorageNum = Convert.ToDecimal(reader["FirstStorageNum"]),
                        OutReserveNum = Convert.ToDecimal(reader["OutReserveNum"]),
                        TransferringInNum = Convert.ToDecimal(reader["TransferringInNum"]),
                        TransferringOutNum = Convert.ToDecimal(reader["TransferringOutNum"]),
                        RefundingNum = Convert.ToDecimal(reader["RefundingNum"]),
                        BookNum = Convert.ToDecimal(reader["BookNum"]),
                        StocktakeNum = Convert.ToDecimal(reader["StocktakeNum"]),
                        WastageNum = Convert.ToDecimal(reader["WastageNum"]),
                        InputDate = Convert.ToDateTime(reader["InputDate"]),
                        Note = reader["Note"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public List<StocktakeDetailModel> GetByTreasuryCode(string treasuryCode, bool isFirst, DateTime endTime)
        {
            DateTime now = DateTime.Now;
            string id = "";
            Stocktake stocktake = new Stocktake();
            StocktakeModel lastStocktakeModel = new StocktakeModel();
            if (!isFirst)
            {
                lastStocktakeModel = stocktake.GetLastStocktakeModel(treasuryCode);
                now = lastStocktakeModel.EndDate;
                id = lastStocktakeModel.Id;
            }
            List<StocktakeDetailModel> list = new List<StocktakeDetailModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append(" WITH SCTE AS  --库存物资").AppendLine();
            builder.Append("(").AppendLine();
            builder.Append(" SELECT scode, sprice, corp, SUM(snumber) AS num ").AppendLine();
            builder.Append(" FROM Sm_Treasury_Stock").AppendLine();
            builder.Append(" WHERE tcode =@treasuryCode  AND intime < @EndTime").AppendLine();
            builder.Append(" GROUP BY scode, sprice, corp").AppendLine();
            builder.Append(" ), SSCTE AS   -- 入库").AppendLine();
            builder.Append(" (").AppendLine();
            builder.Append(" SELECT ST.scode, ST.sprice, ST.corp, SUM(ST.number) AS num").AppendLine();
            builder.Append(" FROM Sm_Storage_Stock AS ST").AppendLine();
            builder.Append(" INNER JOIN Sm_Storage AS S ON S.scode =  ST.stcode").AppendLine();
            builder.Append(" WHERE tcode =@treasuryCode  ").AppendLine();
            builder.Append(" AND ST.number != 0  AND IsFirst=0 ").AppendLine();
            if (!isFirst)
            {
                builder.Append(" AND intime BETWEEN @BeginTime AND @EndTime").AppendLine();
            }
            else
            {
                builder.Append(" AND intime < @EndTime").AppendLine();
            }
            builder.Append(" GROUP BY ST.scode, ST.sprice, ST.corp").AppendLine();
            builder.Append(" ), SOCTE AS  --出库").AppendLine();
            builder.Append(" (").AppendLine();
            builder.Append(" SELECT OS.scode, OS.sprice, OS.corp, SUM(OS.number) AS num").AppendLine();
            builder.Append(" FROM Sm_out_Stock AS OS").AppendLine();
            builder.Append(" INNER JOIN Sm_OutReserve AS O ON O.orcode = OS.orcode").AppendLine();
            builder.Append(" WHERE tcode =@treasuryCode ").AppendLine();
            if (!isFirst)
            {
                builder.Append(" AND IsOutTime BETWEEN @BeginTime AND @EndTime").AppendLine();
            }
            else
            {
                builder.Append(" AND IsOutTime < @EndTime").AppendLine();
            }
            builder.Append(" GROUP BY OS.scode, OS.sprice, OS.corp").AppendLine();
            builder.Append(" ),FirstStorageInfo AS --甲供入库").AppendLine();
            builder.Append(" (").AppendLine();
            builder.Append(" SELECT ST.scode, ST.sprice, ST.corp, SUM(ST.number) AS num").AppendLine();
            builder.Append(" FROM Sm_Storage_Stock AS ST").AppendLine();
            builder.Append(" INNER JOIN Sm_Storage AS S ON S.scode =  ST.stcode").AppendLine();
            builder.Append(" WHERE tcode =@treasuryCode  ").AppendLine();
            builder.Append(" AND ST.number != 0  AND IsFirst=1 ").AppendLine();
            if (!isFirst)
            {
                builder.Append(" AND intime BETWEEN @BeginTime AND @EndTime").AppendLine();
            }
            else
            {
                builder.Append(" AND intime < @EndTime").AppendLine();
            }
            builder.Append(" GROUP BY ST.scode, ST.sprice, ST.corp").AppendLine();
            builder.Append(" ),TransferringInInfo AS  --调拨入库").AppendLine();
            builder.Append(" (").AppendLine();
            builder.Append(" SELECT SAS.scode, SAS.sprice, SAS.corp, SUM(SAS.number) AS num").AppendLine();
            builder.Append(" FROM Sm_Allocation_Stock AS  SAS").AppendLine();
            builder.Append(" INNER JOIN Sm_Allocation AS SA ON SA.Acode = SAS.Acode").AppendLine();
            builder.Append(" WHERE TcodeB =@treasuryCode AND Isinb='1'").AppendLine();
            if (!isFirst)
            {
                builder.Append(" AND Isintime BETWEEN @BeginTime AND @EndTime").AppendLine();
            }
            else
            {
                builder.Append(" AND Isintime < @EndTime").AppendLine();
            }
            builder.Append(" GROUP BY SAS.scode, SAS.sprice, SAS.corp").AppendLine();
            builder.Append(" ),TransferringOutInfo AS --调拨出库").AppendLine();
            builder.Append(" (").AppendLine();
            builder.Append(" SELECT SAS.scode, SAS.sprice, SAS.corp, SUM(SAS.number) AS num").AppendLine();
            builder.Append(" FROM Sm_Allocation_Stock AS  SAS").AppendLine();
            builder.Append(" INNER JOIN Sm_Allocation AS SA ON SA.Acode = SAS.Acode").AppendLine();
            builder.Append(" WHERE TcodeA =@treasuryCode AND IsOutA='1'").AppendLine();
            if (!isFirst)
            {
                builder.Append(" AND Isouttime BETWEEN @BeginTime AND @EndTime").AppendLine();
            }
            else
            {
                builder.Append(" AND Isouttime < @EndTime").AppendLine();
            }
            builder.Append(" GROUP BY SAS.scode, SAS.sprice, SAS.corp").AppendLine();
            builder.Append(" ),WastageInfo AS --报损出库").AppendLine();
            builder.Append(" (").AppendLine();
            builder.Append(" SELECT BS.ResourceCode scode, BS.sprice, BS.corp, SUM(BS.number) AS num").AppendLine();
            builder.Append("  FROM Sm_Wastage_Stock AS BS ").AppendLine();
            builder.Append("  INNER JOIN Sm_Wastage AS R ON R.WastageCode = BS.WastageCode ").AppendLine();
            builder.Append(" WHERE TreasuryCode =@treasuryCode AND IsOut='1'").AppendLine();
            if (!isFirst)
            {
                builder.Append(" AND Isouttime BETWEEN @BeginTime AND @EndTime").AppendLine();
            }
            else
            {
                builder.Append(" AND Isouttime < @EndTime").AppendLine();
            }
            builder.Append(" GROUP BY BS.ResourceCode, BS.sprice, BS.corp").AppendLine();
            builder.Append(" ),RefundingInfo AS --退库数量").AppendLine();
            builder.Append(" (").AppendLine();
            builder.Append(" SELECT BS.scode, BS.sprice, BS.corp, SUM(BS.number) AS num").AppendLine();
            builder.Append(" FROM Sm_back_Stock AS BS").AppendLine();
            builder.Append(" INNER JOIN Sm_Refunding AS R ON R.Rcode = BS.Rcode").AppendLine();
            builder.Append(" WHERE Tcode =@treasuryCode ").AppendLine();
            if (!isFirst)
            {
                builder.Append(" AND Isintime BETWEEN @BeginTime AND @EndTime").AppendLine();
            }
            else
            {
                builder.Append(" AND Isintime < @EndTime").AppendLine();
            }
            builder.Append(" GROUP BY BS.scode, BS.sprice, BS.corp").AppendLine();
            builder.Append(" )").AppendLine();
            builder.Append(" ,InitInfo AS --初始化").AppendLine();
            builder.Append(" (").AppendLine();
            if (!isFirst)
            {
                builder.Append(" SELECT ResourceCode scode,Price sprice,SupplierId corp,StocktakeNum num FROM Sm_Stocktake_Detail WHERE StocktakeId=@StocktakeId").AppendLine();
            }
            else
            {
                builder.Append(" SELECT scode, sprice, corp, SUM(snumber) AS num ").AppendLine();
                builder.Append(" FROM Sm_Treasury_Stock").AppendLine();
                builder.Append(" WHERE tcode =@treasuryCode and type='I'").AppendLine();
                builder.Append(" GROUP BY scode, sprice, corp").AppendLine();
            }
            builder.Append(" )").AppendLine();
            builder.Append(" SELECT Scode,ResourceName,Specification,UnitName,Brand,ModelNumber,TechnicalParameter,Sprice,Corp,CorpName,LastMonthNum,StorageNum,").AppendLine();
            builder.Append(" OutReserveNum,FirstStorageNum,TransferringInNum,TransferringOutNum,WastageNum,RefundingNum,").AppendLine();
            builder.Append(" (LastMonthNum+StorageNum-OutReserveNum+FirstStorageNum+TransferringInNum-TransferringOutNum-WastageNum+RefundingNum) ").AppendLine();
            builder.Append(" AS BookNum,").AppendLine();
            builder.Append(" (LastMonthNum+StorageNum-OutReserveNum+FirstStorageNum+TransferringInNum-TransferringOutNum-WastageNum+RefundingNum)").AppendLine();
            builder.Append(" AS StocktakeNum,'' AS Note").AppendLine();
            builder.Append(" FROM (").AppendLine();
            builder.Append(" SELECT S.Scode,ResourceName,Specification,[Name] AS UnitName, ").AppendLine();
            builder.Append(" ISNULL(Res_Resource.Brand,'') Brand, ISNULL(ModelNumber,'') ModelNumber, ISNULL(TechnicalParameter,'') TechnicalParameter, ").AppendLine();
            builder.Append(" S.Sprice,S.Corp,CorpName,ISNULL(SSCTE.num,0.000) AS StorageNum, ").AppendLine();
            builder.Append(" ISNULL(SOCTE.num,0.000) AS OutReserveNum,ISNULL(FirstStorageInfo.num,0.000) AS FirstStorageNum,").AppendLine();
            builder.Append(" ISNULL(TransferringInInfo.num,0.000) AS TransferringInNum,").AppendLine();
            builder.Append(" ISNULL(TransferringOutInfo.num,0.000) AS TransferringOutNum,ISNULL(RefundingInfo.num,0.000) AS RefundingNum,").AppendLine();
            builder.Append(" ISNULL(WastageInfo.num,0.000) AS WastageNum,").AppendLine();
            builder.Append(" ISNULL(InitInfo.num,0.000) AS LastMonthNum").AppendLine();
            builder.Append(" FROM").AppendLine();
            builder.Append(" (").AppendLine();
            builder.Append(" SELECT scode, sprice, corp FROM SCTE").AppendLine();
            builder.Append(" UNION ").AppendLine();
            builder.Append(" SELECT scode, sprice, corp FROM SSCTE").AppendLine();
            builder.Append(" UNION ").AppendLine();
            builder.Append(" SELECT scode, sprice, corp FROM SOCTE").AppendLine();
            builder.Append(" UNION ").AppendLine();
            builder.Append(" SELECT scode, sprice, corp FROM FirstStorageInfo").AppendLine();
            builder.Append(" UNION ").AppendLine();
            builder.Append(" SELECT scode, sprice, corp FROM TransferringInInfo").AppendLine();
            builder.Append(" UNION ").AppendLine();
            builder.Append(" SELECT scode, sprice, corp FROM TransferringOutInfo").AppendLine();
            builder.Append(" UNION ").AppendLine();
            builder.Append(" SELECT scode, sprice, corp FROM WastageInfo").AppendLine();
            builder.Append(" UNION ").AppendLine();
            builder.Append(" SELECT scode, sprice, corp FROM RefundingInfo").AppendLine();
            builder.Append(" UNION ").AppendLine();
            builder.Append(" SELECT scode, sprice, corp FROM InitInfo").AppendLine();
            builder.Append(" ) AS S").AppendLine();
            builder.Append(" LEFT JOIN SCTE ON S.scode = SCTE.scode AND S.sprice=SCTE.sprice AND S.corp=SCTE.corp").AppendLine();
            builder.Append(" LEFT JOIN SSCTE ON S.scode = SSCTE.scode AND S.sprice=SSCTE.sprice AND S.corp=SSCTE.corp").AppendLine();
            builder.Append(" LEFT JOIN SOCTE ON S.scode = SOCTE.scode AND S.sprice=SOCTE.sprice AND S.corp=SOCTE.corp").AppendLine();
            builder.Append(" LEFT JOIN FirstStorageInfo ON S.scode = FirstStorageInfo.scode AND S.sprice=FirstStorageInfo.sprice AND S.corp=FirstStorageInfo.corp").AppendLine();
            builder.Append(" LEFT JOIN TransferringInInfo ON S.scode = TransferringInInfo.scode AND S.sprice=TransferringInInfo.sprice AND S.corp=TransferringInInfo.corp").AppendLine();
            builder.Append(" LEFT JOIN TransferringOutInfo ON S.scode = TransferringOutInfo.scode AND S.sprice=TransferringOutInfo.sprice AND S.corp=TransferringOutInfo.corp").AppendLine();
            builder.Append(" LEFT JOIN WastageInfo ON S.scode = WastageInfo.scode AND S.sprice=WastageInfo.sprice AND S.corp=WastageInfo.corp").AppendLine();
            builder.Append(" LEFT JOIN RefundingInfo ON S.scode = RefundingInfo.scode AND S.sprice=RefundingInfo.sprice AND S.corp=RefundingInfo.corp").AppendLine();
            builder.Append(" LEFT JOIN InitInfo ON S.scode = InitInfo.scode AND S.sprice=InitInfo.sprice AND S.corp=InitInfo.corp").AppendLine();
            builder.Append(" LEFT JOIN  Res_Resource ON S.Scode=ResourceCode LEFT JOIN  ").AppendLine();
            builder.Append(" XPM_Basic_ContactCorp ON S.Corp=CorpId left join Res_Unit ON Unit=UnitId ) Tab").AppendLine();
            builder.Append(" ORDER BY SCODE").AppendLine();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@treasuryCode", SqlDbType.NVarChar, 0x200), new SqlParameter("@BeginTime", SqlDbType.DateTime), new SqlParameter("@EndTime", SqlDbType.DateTime), new SqlParameter("@StocktakeId", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = treasuryCode;
            commandParameters[1].Value = now.AddDays(1.0);
            commandParameters[2].Value = endTime.AddDays(1.0);
            commandParameters[3].Value = id;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    StocktakeDetailModel item = new StocktakeDetailModel {
                        Id = "",
                        StocktakeId = "",
                        ResourceCode = reader["Scode"].ToString(),
                        ResourceName = reader["ResourceName"].ToString(),
                        Specification = reader["Specification"].ToString(),
                        Unit = reader["UnitName"].ToString(),
                        Price = Convert.ToDecimal(reader["Sprice"]),
                        SupplierId = reader["Corp"].ToString(),
                        Supplier = reader["CorpName"].ToString(),
                        LastMonthNum = Convert.ToDecimal(reader["LastMonthNum"]),
                        StorageNum = Convert.ToDecimal(reader["StorageNum"]),
                        FirstStorageNum = Convert.ToDecimal(reader["FirstStorageNum"]),
                        OutReserveNum = Convert.ToDecimal(reader["OutReserveNum"]),
                        TransferringInNum = Convert.ToDecimal(reader["TransferringInNum"]),
                        TransferringOutNum = Convert.ToDecimal(reader["TransferringOutNum"]),
                        RefundingNum = Convert.ToDecimal(reader["RefundingNum"]),
                        BookNum = Convert.ToDecimal(reader["BookNum"]),
                        StocktakeNum = Convert.ToDecimal(reader["StocktakeNum"]),
                        WastageNum = Convert.ToDecimal(reader["WastageNum"]),
                        Note = reader["Note"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public int Update(SqlTransaction trans, TreasuryStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Treasury_Stock set ");
            builder.Append("scode=@scode,");
            builder.Append("tcode=@tcode,");
            builder.Append("sprice=@sprice,");
            builder.Append("snumber=@snumber,");
            builder.Append("isfirst=@isfirst,");
            builder.Append("corp=@corp,");
            builder.Append("incode=@incode,");
            builder.Append("intime=@intime,");
            builder.Append("intype=@intype");
            builder.Append(" where tsid=@tsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tsid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@snumber", SqlDbType.Decimal, 9), new SqlParameter("@isfirst", SqlDbType.Bit, 1), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@incode", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@intype", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.tsid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.tcode;
            commandParameters[3].Value = model.sprice;
            commandParameters[4].Value = model.snumber;
            commandParameters[5].Value = model.isfirst;
            commandParameters[6].Value = model.corp;
            commandParameters[7].Value = model.incode;
            commandParameters[8].Value = model.intime;
            commandParameters[9].Value = model.intype;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

