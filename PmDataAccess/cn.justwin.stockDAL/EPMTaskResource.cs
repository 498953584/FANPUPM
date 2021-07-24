namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class EPMTaskResource
    {
        public int Add(EPMTaskResourceModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into EPM_Task_Resource(");
            builder.Append("ProjectCode,TaskCode,VersionCode,RationItem,ResourceCode,Wastage,UnitPrice,Fee,Fee1,ResourceStyle,StepCode,Quantity,WbsType,ResourceName,ResourceUnit,Content)");
            builder.Append(" values (");
            builder.Append("@ProjectCode,@TaskCode,@VersionCode,@RationItem,@ResourceCode,@Wastage,@UnitPrice,@Fee,@Fee1,@ResourceStyle,@StepCode,@Quantity,@WbsType,@ResourceName,@ResourceUnit,@Content)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@TaskCode", SqlDbType.VarChar, 50), new SqlParameter("@VersionCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@RationItem", SqlDbType.VarChar, 100), new SqlParameter("@ResourceCode", SqlDbType.VarChar, 50), new SqlParameter("@Wastage", SqlDbType.Decimal, 9), new SqlParameter("@UnitPrice", SqlDbType.Decimal, 9), new SqlParameter("@Fee", SqlDbType.Decimal, 9), new SqlParameter("@Fee1", SqlDbType.Decimal, 9), new SqlParameter("@ResourceStyle", SqlDbType.Int, 4), new SqlParameter("@StepCode", SqlDbType.VarChar, 50), new SqlParameter("@Quantity", SqlDbType.Decimal, 9), new SqlParameter("@WbsType", SqlDbType.Int, 4), new SqlParameter("@ResourceName", SqlDbType.VarChar, 500), new SqlParameter("@ResourceUnit", SqlDbType.VarChar, 50), new SqlParameter("@Content", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = model.ProjectCode;
            commandParameters[1].Value = model.TaskCode;
            commandParameters[2].Value = model.VersionCode;
            commandParameters[3].Value = model.RationItem;
            commandParameters[4].Value = model.ResourceCode;
            commandParameters[5].Value = model.Wastage;
            commandParameters[6].Value = model.UnitPrice;
            commandParameters[7].Value = model.Fee;
            commandParameters[8].Value = model.Fee1;
            commandParameters[9].Value = model.ResourceStyle;
            commandParameters[10].Value = model.StepCode;
            commandParameters[11].Value = model.Quantity;
            commandParameters[12].Value = model.WbsType;
            commandParameters[13].Value = model.ResourceName;
            commandParameters[14].Value = model.ResourceUnit;
            commandParameters[15].Value = model.Content;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public Dictionary<string, decimal> GetDictionary(string projectCode)
        {
            Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceCode,Sum(Quantity) as Quantity from EPM_Task_Resource ");
            builder.Append("where ProjectCode = @ProjectCode ");
            builder.Append("group by ResourceCode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = new Guid(projectCode);
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    string str = reader.GetString(0);
                    reader.GetDecimal(1);
                    if (!string.IsNullOrEmpty(str))
                    {
                        dictionary.Add(reader.GetString(0), reader.GetDecimal(1));
                    }
                }
            }
            return dictionary;
        }

        public string GetPrjUsers(string projectGuid)
        {
            string cmdText = "SELECT Podepom FROM PT_PrjInfo WHERE PrjGuid = @PrjGuid";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjGuid", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = projectGuid;
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public decimal GetQuantity(string projectCode, string resourceCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Sum(Quantity) as Quantity from EPM_Task_Resource ");
            builder.Append("where ProjectCode = @ProjectCode ");
            builder.Append("and ResourceCode = @ResourceCode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", SqlDbType.UniqueIdentifier), new SqlParameter("@ResourceCode", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = new Guid(projectCode);
            commandParameters[1].Value = resourceCode;
            return DBHelper.GetDecimal(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters));
        }

        public string GetResourceNameByCode(string resourceCode)
        {
            string str = "select ResourceName from Res_Resource where ResourceCode = @ResourceCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ResourceCode", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = resourceCode;
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, str.ToString(), commandParameters));
        }
    }
}

