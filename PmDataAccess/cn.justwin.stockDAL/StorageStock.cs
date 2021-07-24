namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class StorageStock
    {
        public int Add(SqlTransaction trans, StorageStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Storage_Stock(");
            builder.Append("ssid,scode,stcode,number,sprice,corp,intype,linkcode,checkCondition)");
            builder.Append(" values (");
            builder.Append("@ssid,@scode,@stcode,@number,@sprice,@corp,@intype,@linkcode,@checkCondition)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ssid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@stcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@intype", SqlDbType.NVarChar, 0x40), new SqlParameter("@linkcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@checkCondition", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = model.ssid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.stcode;
            commandParameters[3].Value = model.number;
            commandParameters[4].Value = model.sprice;
            commandParameters[5].Value = model.corp;
            commandParameters[6].Value = model.intype;
            commandParameters[7].Value = model.linkcode;
            if (model.CheckCondition != null)
            {
                commandParameters[8].Value = model.CheckCondition;
            }
            else
            {
                commandParameters[8].Value = "";
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string ssid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Storage_Stock ");
            builder.Append(" where ssid=@ssid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ssid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ssid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByStorageCode(SqlTransaction trans, string storageCode)
        {
            string cmdText = "delete from Sm_Storage_Stock where stcode = @stcode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@stcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = storageCode;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, commandParameters);
        }

        public List<StorageStockModel> GetGenericList(string condition)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Sm_Storage_Stock ");
            if (!string.IsNullOrEmpty(condition))
            {
                builder.Append("WHERE ");
                builder.Append(condition);
            }
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                List<StorageStockModel> list = new List<StorageStockModel>();
                while (reader.Read())
                {
                    list.Add(this.GetModel(reader));
                }
                return list;
            }
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ssid,scode,stcode,number,sprice,corp,intype,linkcode,checkCondition ");
            builder.Append(" FROM Sm_Storage_Stock ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetListByStcode(string stcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ssid,scode,stcode,number,sprice,corp,intype,linkcode,checkCondition ");
            builder.Append("FROM Sm_Storage_Stock ");
            builder.Append("where stcode = @stcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@stcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = stcode;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public StorageStockModel GetModel(IDataReader dr)
        {
            return new StorageStockModel { ssid = DBHelper.GetString(dr["ssid"]), scode = DBHelper.GetString(dr["scode"]), stcode = DBHelper.GetString(dr["stcode"]), number = DBHelper.GetDecimal(dr["number"]), sprice = DBHelper.GetDecimal(dr["sprice"]), corp = DBHelper.GetString(dr["corp"]), intype = DBHelper.GetString(dr["intype"]), linkcode = DBHelper.GetString(dr["linkcode"]), CheckCondition = DBHelper.GetString(dr["checkCondition"]) };
        }

        public StorageStockModel GetModel(string storageStockID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Sm_Storage_Stock ");
            builder.Append(" WHERE ssid=@in_ssid");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ssid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = storageStockID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public DataTable GetReportDataSource(string condition)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select row_number() over(order by intime desc) as RowNumber, sid, ssid, Sm_Storage_Stock.scode as ResourceCode,ResourceName,Specification,Name as UnitName, number, sprice,").AppendLine();
            builder.Append("Convert(decimal(18,3),number*sprice) as total,corp,CorpName, intype, linkcode,project,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) AS PrjName,convert(nvarchar(10),intime,121) as intime,tname,Convert(nvarchar(64),Sm_Storage.scode) as StorageCode ").AppendLine();
            builder.Append(",Res_Resource.TechnicalParameter,Res_Resource.ModelNumber,Res_Resource.Brand,Res_ResourceType.ResourceTypeName ").AppendLine();
            builder.Append("from Sm_Storage_Stock ").AppendLine();
            builder.Append("join Sm_Storage on Sm_Storage.scode = Sm_Storage_Stock.stcode and Sm_Storage.inflag=1 ").AppendLine();
            builder.Append("join Res_Resource on Sm_Storage_Stock.scode = Res_Resource.ResourceCode ").AppendLine();
            builder.Append("left join Res_Unit on Res_Resource.Unit = Res_Unit.UnitID  ").AppendLine();
            builder.Append("left join PT_PrjInfo on Sm_Storage.Project = Convert(nvarchar(64),PT_PrjInfo.PrjGuid) ").AppendLine();
            builder.Append("left join PT_PrjInfo_ZTB on Sm_Storage.Project = Convert(nvarchar(64),PT_PrjInfo_ZTB.PrjGuid) ").AppendLine();
            builder.Append("left join XPM_Basic_ContactCorp on Sm_Storage_Stock.corp = XPM_Basic_ContactCorp.CorpID ").AppendLine();
            builder.Append("join Sm_Treasury on Sm_Treasury.tcode = Sm_Storage.tcode ").AppendLine();
            builder.Append("join Res_ResourceType on Res_ResourceType.ResourceTypeId = Res_Resource.ResourceType ").AppendLine();
            builder.Append("where Sm_Storage.flowstate = 1 ").AppendLine();
            builder.Append(condition).AppendLine();
            builder.Append("order by RowNumber ").AppendLine();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetStorageStockDataSource(string storageCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("--入库单资源信息\r\n                                WITH cteStorageStock AS\r\n                                (\r\n\t                                SELECT scode,sprice,corp,linkCode,number FROM Sm_Storage_Stock \r\n\t                                WHERE stcode = @stcode\r\n                                ), ctePurchaseStock AS\r\n                                (\r\n\t                                --采购单信息\r\n                                    SELECT scode,pscode,SUM(number) ContractNumber,sprice,corp FROM\r\n\t                                (\r\n\t\t                                SELECT purchaseStock.* FROM dbo.Sm_Purchase_Stock purchaseStock\r\n\t\t                                INNER JOIN cteStorageStock ON pscode=cteStorageStock.linkCode\r\n\t\t                                AND purchaseStock.Scode=cteStorageStock.Scode \r\n\t\t                                AND purchaseStock.sprice=cteStorageStock.sprice \r\n\t\t                                AND purchaseStock.corp=cteStorageStock.corp \r\n\t                                ) PurchaseStock GROUP BY scode,pscode,sprice,corp\r\n                                ), cteAllInStorgeStock AS\r\n                                (\r\n\t                                --采购但材料已经入库的信息\r\n                                    SELECT scode,SUM(number) AllInNumber,sprice,corp FROM\r\n\t                                (\r\n\t\t                                SELECT storgeStock.* FROM cteStorageStock INNER JOIN Sm_Storage_Stock storgeStock \r\n\t\t                                ON storgeStock.linkCode=cteStorageStock.linkCode\r\n\t\t                                AND storgeStock.Scode=cteStorageStock.Scode \r\n\t\t                                AND storgeStock.sprice=cteStorageStock.sprice \r\n\t\t                                AND storgeStock.corp=cteStorageStock.corp\r\n\t\t                                LEFT JOIN Sm_Storage storage ON storgeStock.stCode=storage.sCode\r\n\t\t                                WHERE FlowState=1 AND Inflag=1\r\n\t                                ) allInStorgeStock GROUP BY scode,sprice,corp\r\n                                )");
            builder.AppendLine("SELECT currentStorageStock.ssid, ResourceId, ResourceCode, ResourceName,Specification,Name AS UnitName, ISNULL(ContractNumber,0) ContractNumber,");
            builder.AppendLine("ISNULL(AllInNumber,0) AllInNumber,currentStorageStock.number, currentStorageStock.sprice,(currentStorageStock.number*currentStorageStock.sprice) Total,");
            builder.AppendLine("currentStorageStock.corp, CorpName,intype, linkcode,checkCondition,");
            builder.AppendLine("Res_Resource.brand,ModelNumber,TechnicalParameter FROM Res_Resource ");
            builder.AppendLine("INNER JOIN Sm_Storage_Stock currentStorageStock ON currentStorageStock.scode = Res_Resource.ResourceCode ");
            builder.AppendLine("LEFT JOIN Res_Unit on Res_Unit.UnitId = Res_Resource.Unit  ");
            builder.AppendLine("LEFT JOIN XPM_Basic_ContactCorp on currentStorageStock.corp = XPM_Basic_ContactCorp.CorpID  ");
            builder.AppendLine("LEFT JOIN ctePurchaseStock ON ResourceCode=ctePurchaseStock.scode \r\n                                AND currentStorageStock.sprice=ctePurchaseStock.sprice \r\n                                AND currentStorageStock.corp=ctePurchaseStock.corp \r\n                                LEFT JOIN cteAllInStorgeStock ON ResourceCode=cteAllInStorgeStock.scode \r\n                                AND currentStorageStock.sprice=cteAllInStorgeStock.sprice \r\n                                AND currentStorageStock.corp=cteAllInStorgeStock.corp ");
            builder.AppendLine("where stcode = @stcode  ORDER BY ResourceCode DESC ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@stcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = storageCode;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetTableByStcode(string stcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select p2.scode,p1.ResourceName,");
            builder.Append("p1.Specification,p4.UnitName,sum(p2.number) as number");
            builder.Append(" from dbo.Res_Resource as p1 ");
            builder.Append(" join dbo.Sm_Storage_Stock as p2 on p1.resourceCode=p2.scode");
            builder.Append(" join Res_Unit as p4 on p1.Unit=p4.UnitID");
            builder.Append(" where p2.stcode in(" + stcode + ")");
            builder.Append(" group by p2.scode,p1.ResourceName,p1.Specification,p4.Name");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public int Update(SqlTransaction trans, StorageStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Storage_Stock set ");
            builder.Append("scode=@scode,");
            builder.Append("stcode=@stcode,");
            builder.Append("number=@number,");
            builder.Append("sprice=@sprice,");
            builder.Append("corp=@corp,");
            builder.Append("intype=@intype,");
            builder.Append("linkcode=@linkcode");
            builder.Append("checkCondition=@checkCondition");
            builder.Append(" where ssid=@ssid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ssid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@stcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@intype", SqlDbType.NVarChar, 0x40), new SqlParameter("@linkcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@checkCondition", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = model.ssid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.stcode;
            commandParameters[3].Value = model.number;
            commandParameters[4].Value = model.sprice;
            commandParameters[5].Value = model.corp;
            commandParameters[6].Value = model.intype;
            commandParameters[7].Value = model.linkcode;
            commandParameters[8].Value = model.CheckCondition;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

