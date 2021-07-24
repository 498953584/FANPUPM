namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class ResourceSelectAction
    {
        public static DataTable GetResCategoryList(int resStyle)
        {
            string cmdText = "select CategoryCode,CategoryParentCode,CategoryName,ResourceStyle,ResourceType,isvalid from EPM_Res_Category where resourcestyle=@resourcestyle and isvalid=1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@resourcestyle", resStyle) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetResMaterialList(string strWhere)
        {
            string cmdText = "select ResourceCode,ResourceName,Specification,imgurl from EPM_Res_Resource where  isvalid=1";
            if (strWhere != "")
            {
                cmdText = cmdText + " " + strWhere + " ";
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public static DataTable GetResMaterialList(int resStyle, string categoryCode)
        {
            string cmdText = "select ResourceCode,ResourceName,Specification,imgurl from EPM_Res_Resource where resourcestyle=@resourcestyle and categorycode=@categorycode and isvalid=1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@resourcestyle", resStyle), new SqlParameter("@categorycode", categoryCode) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetResMaterialListByResCode(string resCode)
        {
            string cmdText = "select ResourceCode,ResourceName,Specification,imgurl from EPM_Res_Resource where resourcecode in (" + resCode + ")";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }
    }
}

