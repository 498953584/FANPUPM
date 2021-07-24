namespace cn.justwin.VehiclesDAL
{
    using cn.justwin.DAL;
    using cn.justwin.VehiclesModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class MainService
    {
        public void Add(MainModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Vehicle_Main(");
            builder.Append("guid,VehicleCode,VehicleName,VehicleIdentify,EngineCode,Specification,VehicleType,PurchaseDate,OnHouserDate,InspectionDate,InsuranceDate,Address,Sparekey,Ability,Fatfare,Recordedprice,ManufactureDate,DepreciationRate,State,Remark,Purchaser,IsShare)");
            builder.Append(" values (");
            builder.Append("@guid,@VehicleCode,@VehicleName,@VehicleIdentify,@EngineCode,@Specification,@VehicleType,@PurchaseDate,@OnHouserDate,@InspectionDate,@InsuranceDate,@Address,@Sparekey,@Ability,@Fatfare,@Recordedprice,@ManufactureDate,@DepreciationRate,@State,@Remark,@Purchaser,@IsShare)");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@guid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@VehicleCode", SqlDbType.NVarChar, 100), new SqlParameter("@VehicleName", SqlDbType.NVarChar, 100), new SqlParameter("@VehicleIdentify", SqlDbType.NVarChar, 200), new SqlParameter("@EngineCode", SqlDbType.NVarChar, 100), new SqlParameter("@Specification", SqlDbType.NVarChar), new SqlParameter("@VehicleType", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PurchaseDate", SqlDbType.DateTime), new SqlParameter("@OnHouserDate", SqlDbType.DateTime), new SqlParameter("@InspectionDate", SqlDbType.DateTime), new SqlParameter("@InsuranceDate", SqlDbType.DateTime), new SqlParameter("@Address", SqlDbType.NVarChar), new SqlParameter("@Sparekey", SqlDbType.NVarChar, 100), new SqlParameter("@Ability", SqlDbType.NVarChar, 100), new SqlParameter("@Fatfare", SqlDbType.Decimal, 9), new SqlParameter("@Recordedprice", SqlDbType.Decimal, 9), 
                new SqlParameter("@ManufactureDate", SqlDbType.DateTime), new SqlParameter("@DepreciationRate", SqlDbType.Decimal, 9), new SqlParameter("@State", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@Purchaser", SqlDbType.NVarChar, 100), new SqlParameter("@IsShare", SqlDbType.Int, 4)
             };
            commandParameters[0].Value = Guid.NewGuid();
            commandParameters[1].Value = model.VehicleCode;
            commandParameters[2].Value = model.VehicleName;
            commandParameters[3].Value = model.VehicleIdentify;
            commandParameters[4].Value = model.EngineCode;
            commandParameters[5].Value = model.Specification;
            commandParameters[6].Value = model.VehicleType;
            if (!model.PurchaseDate.HasValue)
            {
                commandParameters[7].Value = DBNull.Value;
            }
            else
            {
                commandParameters[7].Value = model.PurchaseDate;
            }
            if (!model.OnHouserDate.HasValue)
            {
                commandParameters[8].Value = DBNull.Value;
            }
            else
            {
                commandParameters[8].Value = model.OnHouserDate;
            }
            if (!model.InspectionDate.HasValue)
            {
                commandParameters[9].Value = DBNull.Value;
            }
            else
            {
                commandParameters[9].Value = model.InspectionDate;
            }
            if (!model.InsuranceDate.HasValue)
            {
                commandParameters[10].Value = DBNull.Value;
            }
            else
            {
                commandParameters[10].Value = model.InsuranceDate;
            }
            commandParameters[11].Value = model.Address;
            commandParameters[12].Value = model.Sparekey;
            commandParameters[13].Value = model.Ability;
            commandParameters[14].Value = model.Fatfare;
            commandParameters[15].Value = model.Recordedprice;
            if ((model.ManufactureDate.ToString() == "") || (model.ManufactureDate == null))
            {
                commandParameters[0x10].Value = DBNull.Value;
            }
            else
            {
                commandParameters[0x10].Value = DateTime.Parse(model.ManufactureDate);
            }
            commandParameters[0x11].Value = model.DepreciationRate;
            commandParameters[0x12].Value = model.State;
            commandParameters[0x13].Value = model.Remark;
            commandParameters[20].Value = model.Purchaser;
            commandParameters[0x15].Value = model.IsShare;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Delete(Guid guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from OA_Vehicle_Main ");
            builder.Append(" where guid=@guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@guid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = guid;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool DeleteList(string guidlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from OA_Vehicle_Main ");
            builder.Append(" where guid in (" + guidlist + ")  ");
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null) > 0);
        }

        public bool Exists(Guid guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_Vehicle_Main");
            builder.Append(" where guid=@guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@guid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = guid;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public bool Exists(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_Vehicle_Main");
            builder.Append(" where " + where);
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null)) > 0);
        }

        public string GetCodeByguid(Guid guid)
        {
            string str = "";
            string cmdText = "select VehicleCode from OA_Vehicle_Main where guid='" + guid + "'";
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0][0].ToString();
            }
            return str;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select guid,VehicleCode,VehicleName,VehicleIdentify,EngineCode,Specification,VehicleType,PurchaseDate,OnHouserDate,InspectionDate,InsuranceDate,Address,Sparekey,Ability,Fatfare,Recordedprice,ManufactureDate,DepreciationRate,State,Remark,Purchaser,IsShare ");
            builder.Append(" FROM OA_Vehicle_Main ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" guid,VehicleCode,VehicleName,VehicleIdentify,EngineCode,Specification,VehicleType,PurchaseDate,OnHouserDate,InspectionDate,InsuranceDate,Address,Sparekey,Ability,Fatfare,Recordedprice,ManufactureDate,DepreciationRate,State,Remark,Purchaser,IsShare ");
            builder.Append(" FROM OA_Vehicle_Main ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public MainModel GetModel(Guid guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 guid,VehicleCode,VehicleName,VehicleIdentify,EngineCode,Specification,VehicleType,PurchaseDate,OnHouserDate,InspectionDate,InsuranceDate,Address,Sparekey,Ability,Fatfare,Recordedprice,ManufactureDate,DepreciationRate,State,Remark,Purchaser,IsShare from OA_Vehicle_Main ");
            builder.Append(" where guid=@guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@guid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = guid;
            MainModel model = new MainModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if ((table.Rows[0]["guid"] != null) && (table.Rows[0]["guid"].ToString() != ""))
            {
                model.guid = new Guid(table.Rows[0]["guid"].ToString());
            }
            if ((table.Rows[0]["VehicleCode"] != null) && (table.Rows[0]["VehicleCode"].ToString() != ""))
            {
                model.VehicleCode = table.Rows[0]["VehicleCode"].ToString();
            }
            if ((table.Rows[0]["VehicleName"] != null) && (table.Rows[0]["VehicleName"].ToString() != ""))
            {
                model.VehicleName = table.Rows[0]["VehicleName"].ToString();
            }
            if ((table.Rows[0]["VehicleIdentify"] != null) && (table.Rows[0]["VehicleIdentify"].ToString() != ""))
            {
                model.VehicleIdentify = table.Rows[0]["VehicleIdentify"].ToString();
            }
            if ((table.Rows[0]["EngineCode"] != null) && (table.Rows[0]["EngineCode"].ToString() != ""))
            {
                model.EngineCode = table.Rows[0]["EngineCode"].ToString();
            }
            if ((table.Rows[0]["Specification"] != null) && (table.Rows[0]["Specification"].ToString() != ""))
            {
                model.Specification = table.Rows[0]["Specification"].ToString();
            }
            if ((table.Rows[0]["VehicleType"] != null) && (table.Rows[0]["VehicleType"].ToString() != ""))
            {
                model.VehicleType = new Guid(table.Rows[0]["VehicleType"].ToString());
            }
            if ((table.Rows[0]["PurchaseDate"] != null) && (table.Rows[0]["PurchaseDate"].ToString() != ""))
            {
                model.PurchaseDate = new DateTime?(DateTime.Parse(table.Rows[0]["PurchaseDate"].ToString()));
            }
            if ((table.Rows[0]["OnHouserDate"] != null) && (table.Rows[0]["OnHouserDate"].ToString() != ""))
            {
                model.OnHouserDate = new DateTime?(DateTime.Parse(table.Rows[0]["OnHouserDate"].ToString()));
            }
            if ((table.Rows[0]["InspectionDate"] != null) && (table.Rows[0]["InspectionDate"].ToString() != ""))
            {
                model.InspectionDate = new DateTime?(DateTime.Parse(table.Rows[0]["InspectionDate"].ToString()));
            }
            if ((table.Rows[0]["InsuranceDate"] != null) && (table.Rows[0]["InsuranceDate"].ToString() != ""))
            {
                model.InsuranceDate = new DateTime?(DateTime.Parse(table.Rows[0]["InsuranceDate"].ToString()));
            }
            if ((table.Rows[0]["Address"] != null) && (table.Rows[0]["Address"].ToString() != ""))
            {
                model.Address = table.Rows[0]["Address"].ToString();
            }
            if ((table.Rows[0]["Sparekey"] != null) && (table.Rows[0]["Sparekey"].ToString() != ""))
            {
                model.Sparekey = table.Rows[0]["Sparekey"].ToString();
            }
            if ((table.Rows[0]["Ability"] != null) && (table.Rows[0]["Ability"].ToString() != ""))
            {
                model.Ability = table.Rows[0]["Ability"].ToString();
            }
            if ((table.Rows[0]["Fatfare"] != null) && (table.Rows[0]["Fatfare"].ToString() != ""))
            {
                model.Fatfare = new decimal?(decimal.Parse(table.Rows[0]["Fatfare"].ToString()));
            }
            if ((table.Rows[0]["Recordedprice"] != null) && (table.Rows[0]["Recordedprice"].ToString() != ""))
            {
                model.Recordedprice = new decimal?(decimal.Parse(table.Rows[0]["Recordedprice"].ToString()));
            }
            if ((table.Rows[0]["ManufactureDate"] != null) && (table.Rows[0]["ManufactureDate"].ToString() != ""))
            {
                model.ManufactureDate = table.Rows[0]["ManufactureDate"].ToString();
            }
            if ((table.Rows[0]["DepreciationRate"] != null) && (table.Rows[0]["DepreciationRate"].ToString() != ""))
            {
                model.DepreciationRate = new decimal?(decimal.Parse(table.Rows[0]["DepreciationRate"].ToString()));
            }
            if ((table.Rows[0]["State"] != null) && (table.Rows[0]["State"].ToString() != ""))
            {
                model.State = new Guid(table.Rows[0]["State"].ToString());
            }
            if ((table.Rows[0]["Remark"] != null) && (table.Rows[0]["Remark"].ToString() != ""))
            {
                model.Remark = table.Rows[0]["Remark"].ToString();
            }
            if ((table.Rows[0]["Purchaser"] != null) && (table.Rows[0]["Purchaser"].ToString() != ""))
            {
                model.Purchaser = table.Rows[0]["Purchaser"].ToString();
            }
            if ((table.Rows[0]["IsShare"] != null) && (table.Rows[0]["IsShare"].ToString() != ""))
            {
                model.IsShare = new int?(int.Parse(table.Rows[0]["IsShare"].ToString()));
            }
            return model;
        }

        public MainModel GetModel(string VehicleCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 guid,VehicleCode,VehicleName,VehicleIdentify,EngineCode,Specification,VehicleType,PurchaseDate,OnHouserDate,InspectionDate,InsuranceDate,Address,Sparekey,Ability,Fatfare,Recordedprice,ManufactureDate,DepreciationRate,State,Remark,Purchaser,IsShare from OA_Vehicle_Main ");
            builder.Append(" where guid=@guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@VehicleCode", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = VehicleCode;
            MainModel model = new MainModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if ((table.Rows[0]["guid"] != null) && (table.Rows[0]["guid"].ToString() != ""))
            {
                model.guid = new Guid(table.Rows[0]["guid"].ToString());
            }
            if ((table.Rows[0]["VehicleCode"] != null) && (table.Rows[0]["VehicleCode"].ToString() != ""))
            {
                model.VehicleCode = table.Rows[0]["VehicleCode"].ToString();
            }
            if ((table.Rows[0]["VehicleName"] != null) && (table.Rows[0]["VehicleName"].ToString() != ""))
            {
                model.VehicleName = table.Rows[0]["VehicleName"].ToString();
            }
            if ((table.Rows[0]["VehicleIdentify"] != null) && (table.Rows[0]["VehicleIdentify"].ToString() != ""))
            {
                model.VehicleIdentify = table.Rows[0]["VehicleIdentify"].ToString();
            }
            if ((table.Rows[0]["EngineCode"] != null) && (table.Rows[0]["EngineCode"].ToString() != ""))
            {
                model.EngineCode = table.Rows[0]["EngineCode"].ToString();
            }
            if ((table.Rows[0]["Specification"] != null) && (table.Rows[0]["Specification"].ToString() != ""))
            {
                model.Specification = table.Rows[0]["Specification"].ToString();
            }
            if ((table.Rows[0]["VehicleType"] != null) && (table.Rows[0]["VehicleType"].ToString() != ""))
            {
                model.VehicleType = new Guid(table.Rows[0]["VehicleType"].ToString());
            }
            if ((table.Rows[0]["PurchaseDate"] != null) && (table.Rows[0]["PurchaseDate"].ToString() != ""))
            {
                model.PurchaseDate = new DateTime?(DateTime.Parse(table.Rows[0]["PurchaseDate"].ToString()));
            }
            if ((table.Rows[0]["OnHouserDate"] != null) && (table.Rows[0]["OnHouserDate"].ToString() != ""))
            {
                model.OnHouserDate = new DateTime?(DateTime.Parse(table.Rows[0]["OnHouserDate"].ToString()));
            }
            if ((table.Rows[0]["InspectionDate"] != null) && (table.Rows[0]["InspectionDate"].ToString() != ""))
            {
                model.InspectionDate = new DateTime?(DateTime.Parse(table.Rows[0]["InspectionDate"].ToString()));
            }
            if ((table.Rows[0]["InsuranceDate"] != null) && (table.Rows[0]["InsuranceDate"].ToString() != ""))
            {
                model.InsuranceDate = new DateTime?(DateTime.Parse(table.Rows[0]["InsuranceDate"].ToString()));
            }
            if ((table.Rows[0]["Address"] != null) && (table.Rows[0]["Address"].ToString() != ""))
            {
                model.Address = table.Rows[0]["Address"].ToString();
            }
            if ((table.Rows[0]["Sparekey"] != null) && (table.Rows[0]["Sparekey"].ToString() != ""))
            {
                model.Sparekey = table.Rows[0]["Sparekey"].ToString();
            }
            if ((table.Rows[0]["Ability"] != null) && (table.Rows[0]["Ability"].ToString() != ""))
            {
                model.Ability = table.Rows[0]["Ability"].ToString();
            }
            if ((table.Rows[0]["Fatfare"] != null) && (table.Rows[0]["Fatfare"].ToString() != ""))
            {
                model.Fatfare = new decimal?(decimal.Parse(table.Rows[0]["Fatfare"].ToString()));
            }
            if ((table.Rows[0]["Recordedprice"] != null) && (table.Rows[0]["Recordedprice"].ToString() != ""))
            {
                model.Recordedprice = new decimal?(decimal.Parse(table.Rows[0]["Recordedprice"].ToString()));
            }
            if ((table.Rows[0]["ManufactureDate"] != null) && (table.Rows[0]["ManufactureDate"].ToString() != ""))
            {
                model.ManufactureDate = table.Rows[0]["ManufactureDate"].ToString();
            }
            if ((table.Rows[0]["DepreciationRate"] != null) && (table.Rows[0]["DepreciationRate"].ToString() != ""))
            {
                model.DepreciationRate = new decimal?(decimal.Parse(table.Rows[0]["DepreciationRate"].ToString()));
            }
            if ((table.Rows[0]["State"] != null) && (table.Rows[0]["State"].ToString() != ""))
            {
                model.State = new Guid(table.Rows[0]["State"].ToString());
            }
            if ((table.Rows[0]["Remark"] != null) && (table.Rows[0]["Remark"].ToString() != ""))
            {
                model.Remark = table.Rows[0]["Remark"].ToString();
            }
            if ((table.Rows[0]["Purchaser"] != null) && (table.Rows[0]["Purchaser"].ToString() != ""))
            {
                model.Purchaser = table.Rows[0]["Purchaser"].ToString();
            }
            if ((table.Rows[0]["IsShare"] != null) && (table.Rows[0]["IsShare"].ToString() != ""))
            {
                model.IsShare = new int?(int.Parse(table.Rows[0]["IsShare"].ToString()));
            }
            return model;
        }

        public DataTable getSelect(string strWhere)
        {
            string cmdText = "select a.guid,a.VehicleCode,a.VehicleName,a.EngineCode,a.Specification,(select name from OA_Vehicle_TypeAndState where guid=a.VehicleType )as VehicleType,a.purchaseDate,a.onHouserDate,a.InspectionDate,a.Address,a.sparekey,a.Ability,a.Fatfare,a.recordedprice,a.ManufactureDate,a.depreciationrate,";
            cmdText = cmdText + "(select name from OA_Vehicle_TypeAndState where guid=a.state )as state,a.remark,a.purchaser,a.isshare from OA_Vehicle_Main as a ";
            if (!string.IsNullOrEmpty(strWhere))
            {
                cmdText = cmdText + " where 1=1 " + strWhere;
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public bool Update(MainModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Vehicle_Main set ");
            builder.Append("VehicleCode=@VehicleCode,");
            builder.Append("VehicleName=@VehicleName,");
            builder.Append("VehicleIdentify=@VehicleIdentify,");
            builder.Append("EngineCode=@EngineCode,");
            builder.Append("Specification=@Specification,");
            builder.Append("VehicleType=@VehicleType,");
            builder.Append("PurchaseDate=@PurchaseDate,");
            builder.Append("OnHouserDate=@OnHouserDate,");
            builder.Append("InspectionDate=@InspectionDate,");
            builder.Append("InsuranceDate=@InsuranceDate,");
            builder.Append("Address=@Address,");
            builder.Append("Sparekey=@Sparekey,");
            builder.Append("Ability=@Ability,");
            builder.Append("Fatfare=@Fatfare,");
            builder.Append("Recordedprice=@Recordedprice,");
            builder.Append("ManufactureDate=@ManufactureDate,");
            builder.Append("DepreciationRate=@DepreciationRate,");
            builder.Append("State=@State,");
            builder.Append("Remark=@Remark,");
            builder.Append("Purchaser=@Purchaser,");
            builder.Append("IsShare=@IsShare");
            builder.Append(" where guid=@guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@VehicleCode", SqlDbType.NVarChar, 100), new SqlParameter("@VehicleName", SqlDbType.NVarChar, 100), new SqlParameter("@VehicleIdentify", SqlDbType.NVarChar, 200), new SqlParameter("@EngineCode", SqlDbType.NVarChar, 100), new SqlParameter("@Specification", SqlDbType.NVarChar), new SqlParameter("@VehicleType", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PurchaseDate", SqlDbType.DateTime), new SqlParameter("@OnHouserDate", SqlDbType.DateTime), new SqlParameter("@InspectionDate", SqlDbType.DateTime), new SqlParameter("@InsuranceDate", SqlDbType.DateTime), new SqlParameter("@Address", SqlDbType.NVarChar), new SqlParameter("@Sparekey", SqlDbType.NVarChar, 100), new SqlParameter("@Ability", SqlDbType.NVarChar, 100), new SqlParameter("@Fatfare", SqlDbType.Decimal, 9), new SqlParameter("@Recordedprice", SqlDbType.Decimal, 9), new SqlParameter("@ManufactureDate", SqlDbType.DateTime), 
                new SqlParameter("@DepreciationRate", SqlDbType.Decimal, 9), new SqlParameter("@State", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@Purchaser", SqlDbType.NVarChar, 100), new SqlParameter("@IsShare", SqlDbType.Int, 4), new SqlParameter("@guid", SqlDbType.UniqueIdentifier, 0x10)
             };
            commandParameters[0].Value = model.VehicleCode;
            commandParameters[1].Value = model.VehicleName;
            commandParameters[2].Value = model.VehicleIdentify;
            commandParameters[3].Value = model.EngineCode;
            commandParameters[4].Value = model.Specification;
            commandParameters[5].Value = model.VehicleType;
            if (!model.PurchaseDate.HasValue)
            {
                commandParameters[6].Value = DBNull.Value;
            }
            else
            {
                commandParameters[6].Value = model.PurchaseDate;
            }
            if (!model.OnHouserDate.HasValue)
            {
                commandParameters[7].Value = DBNull.Value;
            }
            else
            {
                commandParameters[7].Value = model.OnHouserDate;
            }
            if (!model.InspectionDate.HasValue)
            {
                commandParameters[8].Value = DBNull.Value;
            }
            else
            {
                commandParameters[8].Value = model.InspectionDate;
            }
            if (!model.InsuranceDate.HasValue)
            {
                commandParameters[9].Value = DBNull.Value;
            }
            else
            {
                commandParameters[9].Value = model.InsuranceDate;
            }
            commandParameters[10].Value = model.Address;
            commandParameters[11].Value = model.Sparekey;
            commandParameters[12].Value = model.Ability;
            commandParameters[13].Value = model.Fatfare;
            commandParameters[14].Value = model.Recordedprice;
            if ((model.ManufactureDate.ToString() == "") || (model.ManufactureDate == null))
            {
                commandParameters[15].Value = DBNull.Value;
            }
            else
            {
                commandParameters[15].Value = DateTime.Parse(model.ManufactureDate);
            }
            commandParameters[0x10].Value = model.DepreciationRate;
            commandParameters[0x11].Value = model.State;
            commandParameters[0x12].Value = model.Remark;
            commandParameters[0x13].Value = model.Purchaser;
            commandParameters[20].Value = model.IsShare;
            commandParameters[0x15].Value = model.guid;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

