namespace cn.justwin.stockBLL.TableTopBLL
{
    using cn.justwin.DAL;
    using cn.justwin.TableTopDAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;

    public class DesktopBLL
    {
        private DesktopDAL desktop = new DesktopDAL();

        public int Add(DeskTopDalModel model, SqlTransaction trans)
        {
            return this.desktop.Add(model, trans);
        }

        public int AddMenu(menuModel model, SqlTransaction trans)
        {
            return this.desktop.AddMenu(model, trans);
        }

        public DataTable checkModel(string userCode)
        {
            return this.desktop.getCheckModel(userCode);
        }

        public int Delete(string userCode, SqlTransaction trans)
        {
            return this.desktop.Delete(userCode, trans);
        }

        public int DeleteModel(string userCode, string modelID, SqlTransaction trans)
        {
            return this.desktop.DeleteModel(userCode, modelID, trans);
        }

        public int DeleteWebAddr(string userCode, string LinkID, SqlTransaction trans)
        {
            return this.desktop.DeleteWebAddr(userCode, LinkID, trans);
        }

        public int DelMenu(string userCode, string modelId, SqlTransaction trans)
        {
            return this.desktop.DelMenu(userCode, modelId, trans);
        }

        public DataTable GetAllModel(string userCode, string code, string name)
        {
            return this.desktop.allModel(userCode, code, name);
        }

        public DataTable GetDBInfo(string userCode, int rowCount)
        {
            string cmdText = string.Format("\r\n\t\t\t\t--DECLARE @UserCode nvarchar(8);\r\n\t\t\t\t--DECLARE @RowCount int;\r\n\t\t\t\t--SET @UserCode = '00000000';\r\n\t\t\t\t--SET @RowCount = 6;\r\n\t\t\t\tSELECT TOP(@RowCount) V_Content, CONVERT(varchar(10), DTM_DBSJ, 20) AS DTM_DBSJ, I_DBSJ_ID, V_DBLJ, I_XGID\r\n\t\t\t\tFROM\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT [I_DBSJ_ID], [I_XGID], [V_LXBM], [V_YHDM], [DTM_DBSJ], [C_OpenFlag], [V_DBLJ], [V_TPLJ], [V_Content] \r\n\t\t\t\t\tFROM [PT_DBSJ] \r\n\t\t\t\t\tWHERE [V_YHDM] = @UserCode AND IsOpened = 0\r\n\t\t\t\t\tUNION\r\n\t\t\t\t\tSELECT [I_DBSJ_ID], [I_XGID], [V_LXBM], [V_YHDM], [DTM_DBSJ], [C_OpenFlag], [V_DBLJ], [V_TPLJ], [V_Content] \r\n\t\t\t\t\tFROM [PT_DBSJ_Today] \r\n\t\t\t\t\tWHERE [V_YHDM] = @UserCode AND DTM_DBSJ <= GetDate() AND IsOpened = 0\r\n\t\t\t\t) AS T\r\n\t\t\t\tORDER BY DTM_DBSJ DESC\r\n\t\t\t\t", rowCount, userCode);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RowCount", rowCount), new SqlParameter("@UserCode", userCode) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetMenuLinkInfo(string userCode, int _FixRowCount, string sequence)
        {
            string cmdText = string.Concat(new object[] { "select top ", _FixRowCount, " f.Id, f.MNewName, CONVERT(varchar(10),f.addTime, 120) AS addTime, f.userCode,f.ModelId, f.orderId, p.v_mkmc,p.v_cdlj from frame_Desktop_Menulink as f inner join pt_mk as p on f.modelid=p.c_mkdm where f.userCode='", userCode, "' and f.sequence='", sequence, "' order by orderid" });
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public DataTable GetModelInfo(string userCode)
        {
            return this.desktop.getModel(userCode);
        }

        public DataTable GetNews(int rowCount = 6)
        {
            string cmdText = "\r\n\t\t\t\tSELECT TOP(@RowCount) i_xw_id AS Id, v_xwbt AS Content, CONVERT(varchar(10),dtm_fbsj, 120) AS Date ,\r\n\t\t\t\t\t('/WEB/WebSel.aspx?cd=99&nid=' + CAST( i_xw_id as nvarchar(10))) AS Url\r\n\t\t\t\tFROM Web_News WHERE c_xwlxdm = 99 AND c_sfyx ='y' ORDER BY dtm_fbsj DESC\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RowCount", rowCount) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetRegime(int rowCount = 6)
        {
            string cmdText = "\r\n\t\t\t\tSELECT TOP(@RowCount) InsCode AS Id, InsName AS Content, CONVERT(varchar(10),writedate, 120) AS Date,\r\n\t\t\t\t\t('/oa/System/Institution/InstitutionView.aspx?ic=' + CAST( InsCode as nvarchar(50))) AS Url\r\n\t\t\t\tFROM Institutions WHERE status=1 and isvalid=1 ORDER BY writedate DESC\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RowCount", rowCount) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public int GetRowId(string userCode)
        {
            string cmdText = "select * from frame_Desktop_UserModel where userCode='" + userCode + "'";
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
            return table.Rows.Count;
        }

        public int GetRowMenuId(string userCode, string sequence)
        {
            string cmdText = "select * from frame_Desktop_Menulink where userCode='" + userCode + "' and sequence='" + sequence + "' order by orderId desc";
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
            int num = 0;
            if (table.Rows.Count <= 0)
            {
                return num;
            }
            try
            {
                return Convert.ToInt32(table.Rows[0]["orderId"]);
            }
            catch
            {
                return 0;
            }
        }

        public DataTable GetShowInfo(string colName, string colTime, string tbName, string selWhere, string coldId, int rowCount)
        {
            string str = string.Concat(new object[] { " select top ", rowCount, " ", colName, ",", colTime, ",", coldId, " from ", tbName, " ", selWhere });
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public DataTable GetUserModelInfo(string userCode)
        {
            return this.desktop.GetUserModelInfo(userCode);
        }

        public DataTable GetUserModelInfo(string userCode, string ModelId)
        {
            return this.desktop.GetUserModelInfo(userCode, ModelId);
        }

        public DataTable GetUserSet(string userCode, string ault)
        {
            return this.desktop.GetUserSet(userCode, ault);
        }

        public DataTable GetWarningInfo(string userCode, int rowCount)
        {
            string cmdText = "\r\n\t\t\t\t--DECLARE @UserCode nvarchar(8);\r\n\t\t\t\t--DECLARE @RowCount int;\r\n\t\t\t\t--SET @UserCode = '00000000';\r\n\t\t\t\t--SET @RowCount = 6;\r\n\t\t\t\tSELECT TOP(@RowCount) WarningTitle,CONVERT(varchar(10), InputDate, 20) AS InputDate,URI,WarningId,WarningContent \r\n\t\t\t\tFROM PT_Warning  \r\n\t\t\t\tWHERE UserCode = @UserCode AND IsOpened = 0 \r\n\t\t\t\tORDER BY InputDate DESC \r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserCode", userCode), new SqlParameter("@RowCount", rowCount) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetWebLinkInfo(string userCode, int _FixRowCount)
        {
            string cmdText = string.Concat(new object[] { "select top ", _FixRowCount, " webName,CONVERT(varchar(10),addTime, 120) as addTime,WebAddr,orderID,linkId from dbo.frame_Desktop_Weblink where userCode='", userCode, "' order by orderId,addTime desc " });
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public DataTable GetWebLinkList(string userCode)
        {
            return this.desktop.getWebLinkList(userCode);
        }

        public DataTable GetWFInfo(string userCode, int rowCount)
        {
            string cmdText = "\r\n\t\t\t\t--DECLARE @userCode nvarchar(10)\r\n\t\t\t\t--DECLARE @count int\r\n\t\t\t\t--SET @userCode = '00000000'\r\n\t\t\t\t--SET @count = 6\r\n                SELECT TOP(@count) ISNULL(d.BusinessClassName, c.TemplateName) AS BusinessClassName,\r\n                \tCONVERT (varchar(10), a.StartTime, 20) as rq, a.BusinessCode, \r\n                \tDATEDIFF(hh, b.OutOfTime, GETDATE()) AS cs,a.BusinessClass, \r\n                \tb.NoteID, b.IsAllPass, a.TemplateID, c.TemplateName, \r\n                \tb.NodeID, b.NodeName, PT_yhmc.v_xm,a.StartTime, a.InstanceCode, \r\n                \tdbo.GetBusinessName(a.BusinessCode) AS BusinessName ,b.ArriveTime ,b.During \r\n                FROM WF_Instance_Main AS a INNER JOIN WF_Instance AS b ON a.ID = b.ID \r\n                LEFT JOIN WF_Business_Class AS d ON d.BusinessCode = a.BusinessCode AND d.BusinessClass = a.BusinessClass\r\n                LEFT JOIN WF_Template AS c ON c.TemplateID = a.TemplateID\r\n                LEFT JOIN PT_yhmc ON PT_yhmc.v_yhdm = a.Organiger\r\n                WHERE ((b.Sing = 0) AND (b.Operator = @userCode)\r\n                ) order by a.StartTime desc\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode), new SqlParameter("@count", rowCount) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable meunList(string userCode, string sequence)
        {
            string cmdText = "select f.*,p.v_mkmc from frame_Desktop_Menulink as f inner join pt_mk as p on f.modelid=p.c_mkdm where f.userCode='" + userCode + "' and Sequence='" + sequence + "' order by  f.orderid ASC";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public int Update(DeskTopDalModel model, SqlTransaction trans)
        {
            return this.desktop.Update(model, trans);
        }

        public int updateMenuOrderID(string orderID)
        {
            return this.desktop.updateMenuOrderID(orderID);
        }

        public int updateOrderID(string orderID, string usercode)
        {
            return this.desktop.updateOrderID(orderID, usercode);
        }

        public int UpdateWeblink(Desktop_Weblink model)
        {
            return this.desktop.UpdateWeblink(model);
        }

        public int UpdateWeblinkByLinkID(string _orderId, string usercode)
        {
            return this.desktop.UpdateWeblinkByLinkID(_orderId, usercode);
        }

        public int UpdMenu(menuModel model, SqlTransaction trans)
        {
            return this.desktop.UpdMenu(model, trans);
        }
    }
}

