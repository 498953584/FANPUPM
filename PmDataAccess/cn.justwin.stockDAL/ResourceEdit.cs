namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ResourceEdit
    {
        public static int DeleteNewPrice(string ResourceCode)
        {
            string cmdText = "delete sm_resource_price where rcode=@rcode";
            SqlParameter parameter = new SqlParameter("@rcode", SqlDbType.NVarChar, 0x80) {
                Value = ResourceCode
            };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public static int GetIsExistsSameResourceCode(string ResourceCode)
        {
            string cmdText = "select count(*) from EPM_Res_Resource where ResourceCode=@ResourceCode";
            SqlParameter parameter = new SqlParameter("@ResourceCode", SqlDbType.NVarChar, 0x200) {
                Value = ResourceCode
            };
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter }).ToString());
        }

        public static int GetIsShowCount()
        {
            string cmdText = "select count(*) from Sm_Resource_PriceType where rptIsShow=1 and IsDefault=0";
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, null).ToString());
        }

        public static string GetNewPriceTypeName(string rptCode)
        {
            string cmdText = "select rptname from sm_resource_pricetype where rptcode=@rptcode";
            SqlParameter parameter = new SqlParameter("@rptcode", SqlDbType.NVarChar, 0x80) {
                Value = rptCode
            };
            return SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter }).ToString();
        }

        public static object GetNewResPrice(string resCode, string priceTypeCode)
        {
            string cmdText = "select price from Sm_resource_price where rcode=@rcode and rptcode=@rptcode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rcode", SqlDbType.NVarChar, 0x80), new SqlParameter("@rptcode", SqlDbType.NVarChar, 0x80) };
            commandParameters[0].Value = resCode;
            commandParameters[1].Value = priceTypeCode;
            return SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetPriceShowItem()
        {
            string cmdText = "select * from Sm_Resource_PriceType where rptIsShow=1 and IsDefault=0";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public static string GetResPrice(string resCode)
        {
            string cmdText = "select price from EPM_Res_PriceRelations where ResourceCode=@ResourceCode";
            SqlParameter parameter = new SqlParameter("@ResourceCode", SqlDbType.NVarChar, 0x200) {
                Value = resCode
            };
            return SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter }).ToString();
        }

        public static DataTable GetResrouceList(string ResCode)
        {
            string cmdText = "select * from EPM_Res_Resource where ResourceCode=@ResourceCode";
            SqlParameter parameter = new SqlParameter("@ResourceCode", SqlDbType.NVarChar, 0x200) {
                Value = ResCode
            };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public static string GetUnitId(string resCode)
        {
            string cmdText = "select Unit from Res_Resource where ResourceCode=@ResourceCode";
            SqlParameter parameter = new SqlParameter("@ResourceCode", SqlDbType.NVarChar, 0x200) {
                Value = resCode
            };
            return SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter }).ToString();
        }

        public static int GetUnitID(string UnitName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("begin ");
            builder.Append(" if (select top 1 UnitName from EPM_Res_Unit where UnitName =@UnitName)!='' ");
            builder.Append("begin ");
            builder.Append(" select UnitID from EPM_Res_Unit where UnitName =@UnitName ");
            builder.Append(" end ");
            builder.Append(" else ");
            builder.Append(" begin ");
            builder.Append(" declare @Ui int ");
            builder.Append(" select @Ui = Max(UnitID) +1 from EPM_Res_Unit ");
            builder.Append(" insert into EPM_Res_Unit (UnitID,UnitName,IsValid,Owner,VersionTime) ");
            builder.Append(" values(@Ui,@UnitName,1,'000000',getdate()) ");
            builder.Append(" select UnitID from EPM_Res_Unit where UnitName = @UnitName end end ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnitName", SqlDbType.NVarChar, 0xd7) };
            commandParameters[0].Value = UnitName;
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters).ToString());
        }

        public static DataTable GetUnitNameList()
        {
            string cmdText = "select distinct UnitName from EPM_Res_Unit ";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public static string GetUnitNameList(string UnitId)
        {
            string cmdText = "select name from Res_Unit where unitid=@unitid";
            SqlParameter parameter = new SqlParameter("@unitid", SqlDbType.NVarChar, 0x200) {
                Value = UnitId
            };
            return SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter }).ToString();
        }

        public static int Insert(ResourceNewPriceModel resNewPriceModel)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Resource_Price(");
            builder.Append("rpid,rcode,rptcode,price)");
            builder.Append(" values (");
            builder.Append("@rpid,@rcode,@rptcode,@price)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rpid", SqlDbType.NVarChar, 50), new SqlParameter("@rcode", SqlDbType.VarChar, 50), new SqlParameter("@rptcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@price", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = resNewPriceModel.rptId;
            commandParameters[1].Value = resNewPriceModel.rCode;
            commandParameters[2].Value = resNewPriceModel.rptCode;
            commandParameters[3].Value = resNewPriceModel.Price;
            return int.Parse(SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters).ToString());
        }

        public static int Insert(ResourceModel resModel, ResourceGaugeModel resGaugeModel, ResourcePriceModel resPriceModel)
        {
            SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString);
            connection.Open();
            SqlTransaction trans = connection.BeginTransaction();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            try
            {
                builder.Append(" begin insert into EPM_Res_Resource(VersionCode,ResourceCode,CategoryCode,ResourceName,Specification,ResourceStyle,ResourceType,IsValid,Owner,VersionTime,imgurl) ");
                builder.Append(" values(@VersionCode,@ResourceCode,@CategoryCode,@ResourceName,@Specification,@ResourceStyle,@ResourceType,@IsValid,@Owner,@VersionTime,@imgurl) ");
                builder.Append(" insert into  EPM_Res_Gauge(VersionCode,ResourceCode,UnitID,Quotiety,IsDefault,IsValid,Owner,VersionTime) ");
                builder.Append(" values(@VersionCode,@ResourceCode,@UnitID,@Quotiety,@IsDefault,@IsValid,@Owner,@VersionTime) ");
                builder.Append(" insert into EPM_Res_PriceRelations (VersionCode,ResourceCode,PriceItemID,Price,Owner,VersionTime) ");
                builder.Append(" values(@VersionCode,@ResourceCode,@PriceItemID,@Price,@Owner,@VersionTime) end ");
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@VersionCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@ResourceCode", SqlDbType.VarChar, 50), new SqlParameter("@CategoryCode", SqlDbType.VarChar, 50), new SqlParameter("@ResourceName", SqlDbType.VarChar, 200), new SqlParameter("@Specification", SqlDbType.VarChar, 200), new SqlParameter("@ResourceStyle", SqlDbType.Int, 4), new SqlParameter("@ResourceType", SqlDbType.Int, 4), new SqlParameter("@IsValid", SqlDbType.Bit, 1), new SqlParameter("@Owner", SqlDbType.VarChar, 10), new SqlParameter("@VersionTime", SqlDbType.DateTime), new SqlParameter("@UnitID", SqlDbType.Int, 4), new SqlParameter("@Quotiety", SqlDbType.Decimal, 9), new SqlParameter("@IsDefault", SqlDbType.Bit, 1), new SqlParameter("@PriceItemID", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@imgurl", SqlDbType.VarChar, 0xd7) };
                commandParameters[0].Value = new Guid(resModel.VersionCode);
                commandParameters[1].Value = resModel.ResourceCode;
                commandParameters[2].Value = resModel.CategoryCode;
                commandParameters[3].Value = resModel.ResourceName;
                commandParameters[4].Value = resModel.Specification;
                commandParameters[5].Value = int.Parse(resModel.ResourceStyle);
                commandParameters[6].Value = int.Parse(resModel.ResourceType);
                commandParameters[7].Value = Convert.ToBoolean(int.Parse(resModel.IsValid));
                commandParameters[8].Value = resModel.Owner;
                commandParameters[9].Value = resModel.VersionTime;
                commandParameters[10].Value = int.Parse(resGaugeModel.UnitId);
                commandParameters[11].Value = decimal.Parse(resGaugeModel.Quantity);
                commandParameters[12].Value = Convert.ToBoolean(int.Parse(resGaugeModel.IsDefault));
                commandParameters[13].Value = int.Parse(resPriceModel.PriceItem);
                commandParameters[14].Value = decimal.Parse(resPriceModel.Price);
                commandParameters[15].Value = resModel.ResourceImageUrl;
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                connection.Close();
                connection.Dispose();
            }
            connection.Close();
            connection.Dispose();
            return num;
        }

        protected static SqlParameter OrganizationParameter(string parameterName, ParameterDirection direction, SqlDbType dataType, string value, int size)
        {
            SqlParameter parameter = new SqlParameter {
                ParameterName = parameterName,
                SqlDbType = dataType,
                Size = size
            };
            if (direction != ParameterDirection.Output)
            {
                parameter.Value = value;
            }
            return parameter;
        }

        public static int Update(ResourceNewPriceModel resNewPriceModel)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin if(select count(*) from Sm_Resource_Price where rptcode=@rptcode and rcode=@rcode)>0 ");
            builder.Append(" begin ");
            builder.Append(" update Sm_Resource_Price set ");
            builder.Append(" price=@price ");
            builder.Append(" where rptcode=@rptcode and rcode=@rcode");
            builder.Append(" end ");
            builder.Append(" else ");
            builder.Append(" begin ");
            builder.Append(" insert into Sm_Resource_Price(rpid,rcode,rptcode,price) ");
            builder.Append(" values(@rpid,@rcode,@rptcode,@price) ");
            builder.Append(" end ");
            builder.Append(" end ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rcode", SqlDbType.NVarChar, 0x80), new SqlParameter("@rptcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@price", SqlDbType.Decimal), new SqlParameter("@rpid", SqlDbType.NVarChar, 0x80) };
            commandParameters[0].Value = resNewPriceModel.rCode;
            commandParameters[1].Value = resNewPriceModel.rptCode;
            commandParameters[2].Value = resNewPriceModel.Price;
            commandParameters[3].Value = resNewPriceModel.rptId;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int Update(ResourceModel resModel, ResourceGaugeModel resGaugeModel, ResourcePriceModel resPriceModel)
        {
            SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString);
            connection.Open();
            SqlTransaction trans = connection.BeginTransaction();
            int num = 0;
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append("begin update EPM_Res_Resource set ");
                builder.Append("VersionCode=@VersionCode,CategoryCode=@CategoryCode");
                builder.Append(",ResourceName=@ResourceName,Specification=@Specification,ResourceStyle=@ResourceStyle");
                builder.Append(",ResourceType=@ResourceType,IsValid=@IsValid,Owner=@Owner,VersionTime=@VersionTime,imgurl=@imgurl ");
                builder.Append(" where ResourceCode=@ResourceCode ");
                builder.Append(" update EPM_Res_Gauge set ");
                builder.Append(" VersionCode=@VersionCode,UnitID=@UnitID,Quotiety=@Quotiety,IsDefault=@IsDefault,IsValid=@IsValid,Owner=@Owner,VersionTime=@VersionTime ");
                builder.Append(" where ResourceCode=@ResourceCode ");
                builder.Append(" update EPM_Res_PriceRelations set ");
                builder.Append(" VersionCode=@VersionCode,PriceItemID=@PriceItemID,Price=@Price,Owner=@Owner,VersionTime=@VersionTime ");
                builder.Append(" where ResourceCode=@ResourceCode end ");
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@VersionCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@ResourceCode", SqlDbType.VarChar, 50), new SqlParameter("@CategoryCode", SqlDbType.VarChar, 50), new SqlParameter("@ResourceName", SqlDbType.VarChar, 200), new SqlParameter("@Specification", SqlDbType.VarChar, 200), new SqlParameter("@ResourceStyle", SqlDbType.Int, 4), new SqlParameter("@ResourceType", SqlDbType.Int, 4), new SqlParameter("@IsValid", SqlDbType.Bit, 1), new SqlParameter("@Owner", SqlDbType.VarChar, 10), new SqlParameter("@VersionTime", SqlDbType.DateTime), new SqlParameter("@UnitID", SqlDbType.Int, 4), new SqlParameter("@Quotiety", SqlDbType.Decimal, 9), new SqlParameter("@IsDefault", SqlDbType.Bit, 1), new SqlParameter("@PriceItemID", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@imgurl", SqlDbType.VarChar, 0xd7) };
                commandParameters[0].Value = new Guid(resModel.VersionCode);
                commandParameters[1].Value = resModel.ResourceCode;
                commandParameters[2].Value = resModel.CategoryCode;
                commandParameters[3].Value = resModel.ResourceName;
                commandParameters[4].Value = resModel.Specification;
                commandParameters[5].Value = int.Parse(resModel.ResourceStyle);
                commandParameters[6].Value = int.Parse(resModel.ResourceType);
                commandParameters[7].Value = Convert.ToBoolean(int.Parse(resModel.IsValid));
                commandParameters[8].Value = resModel.Owner;
                commandParameters[9].Value = resModel.VersionTime;
                commandParameters[10].Value = int.Parse(resGaugeModel.UnitId);
                commandParameters[11].Value = decimal.Parse(resGaugeModel.Quantity);
                commandParameters[12].Value = Convert.ToBoolean(int.Parse(resGaugeModel.IsDefault));
                commandParameters[13].Value = int.Parse(resPriceModel.PriceItem);
                commandParameters[14].Value = decimal.Parse(resPriceModel.Price);
                commandParameters[15].Value = resModel.ResourceImageUrl;
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                connection.Close();
                connection.Dispose();
            }
            connection.Close();
            connection.Dispose();
            return num;
        }
    }
}

