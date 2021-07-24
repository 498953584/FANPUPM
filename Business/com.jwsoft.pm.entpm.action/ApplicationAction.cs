namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ApplicationAction
    {
        public int Add(HRLeaveApplication model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Leave_Application(");
            builder.Append("RecordID,AuditState,UserCode,RecordDate,LeaveType,BeginDate,Days,Reason,IsApply");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RecordID + "',");
            builder.Append(model.AuditState + ",");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append(model.LeaveType + ",");
            builder.Append("'" + model.BeginDate + "',");
            builder.Append(model.Days + ",");
            builder.Append("'" + model.Reason + "',");
            builder.Append("'" + model.IsApply + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int AnnulConfirmUpdate(HRLeaveApplication model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Leave_Application set ");
            builder.Append("LeaveDays=" + model.LeaveDays + ",");
            builder.Append("ConfirmUser='" + model.ConfirmUser + "',");
            builder.Append("backdate='" + DateTime.Now + "',");
            builder.Append("IsConfirm='" + model.IsConfirm + "'");
            builder.Append(" where RecordID='" + model.RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int ConfirmUpdate(HRLeaveApplication model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Leave_Application set ");
            builder.Append("IsApply='" + model.IsApply + "',");
            builder.Append("BackDate='" + model.BackDate + "',");
            builder.Append("LeaveDays=" + model.LeaveDays + ",");
            builder.Append("IsConfirm='" + model.IsConfirm + "'");
            builder.Append(" where RecordID='" + model.RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Leave_Application ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int GetAttendanceCount(string startDate, string endDate, int? depId)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (!depId.HasValue)
            {
                depId = 0;
            }
            string cmdText = "\r\n                    --DECLARE @depId int,@beginDate datetime,@endDate datetime;\r\n                    --SET @depId = 1\r\n                    --SET @beginDate = '2013-09-01'\r\n                    --SET @endDate = '2013-10-01';\r\n                    WITH cte_LeaveDays AS\r\n                    (\r\n\t                    SELECT L.BeginDate, L.BeginDate + L.LeaveDays - 0.5 AS EndDate, L.LeaveDays, L.UserCode\r\n                        FROM HR_Leave_Application AS L\r\n                        WHERE L.AuditState = 1 AND L.IsConfirm = 1\r\n                            AND L.BeginDate >= @beginDate AND L.BeginDate < @endDate\r\n                    ), cte_TheLeaveDay AS \r\n                    (\r\n\t                    SELECT DISTINCT L.BeginDate + n - 1 AS BeginDate, DAY(L.BeginDate + n - 1) AS day , L.UserCode, Y.v_xm\r\n\t                    FROM cte_LeaveDays L\r\n\t                    JOIN Nums ON L.BeginDate + n - 1 <= L.EndDate \r\n\t                    JOIN PT_Yhmc AS Y ON Y.v_yhdm = L.UserCode\r\n\t                    WHERE n <= 30 AND Y.i_bmdm = @depId\r\n                    )\r\n                    ,cte_WorkDays AS\r\n                    (\r\n\t                    SELECT BeginDate,day,PT_Yhmc.v_xm,v_yhdm AS UserCode FROM PT_Yhmc  LEFT JOIN cte_TheLeaveDay\r\n\t                    ON cte_TheLeaveDay.UserCode = PT_Yhmc.v_yhdm \r\n\t                    WHERE i_bmdm = @depId\r\n                    ), cte_MonthDays AS\r\n                    (\r\n                        SELECT * FROM cte_WorkDays\r\n                        PIVOT \r\n                        (\r\n                            COUNT(BeginDate) FOR day IN(\t\t\r\n                                [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],\r\n                                [16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31]\r\n                            )\r\n                        ) AS PVT\r\n                    )\r\n                    SELECT MD.*, \r\n                    (SELECT COUNT(*) FROM cte_TheLeaveDay AS TD WHERE TD.UserCode = MD.UserCode) AS TotalLeaveDays\r\n                    FROM cte_MonthDays AS MD";
            list.Add(new SqlParameter("@beginDate", startDate));
            list.Add(new SqlParameter("@endDate", endDate));
            list.Add(new SqlParameter("@depId", depId));
            SqlDataReader reader = publicDbOpClass.ExecuteReader(CommandType.Text, cmdText, list.ToArray());
            DataTable table = new DataTable();
            table.Load(reader);
            return (table.Rows.Count + 1);
        }

        public DataTable GetAttendanceTable(string startDate, string endDate, int? depId, int pageSize, int pageNo)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetAttendanceCount(startDate, endDate, depId);
            }
            if (pageNo == 0)
            {
                pageNo = 1;
            }
            List<SqlParameter> list = new List<SqlParameter>();
            if (!depId.HasValue)
            {
                depId = 0;
            }
            string cmdText = "\r\n                    --DECLARE @depId int,@beginDate datetime,@endDate datetime,@pageSize int,@pageNo int;\r\n                    --SET @depId = 1\r\n                    --SET @beginDate = '2013-09-01'\r\n                    --SET @endDate = '2013-10-01';\r\n                    --SET @pageSize = 15;\r\n                    --SET @pageNo = 1;\r\n                    WITH cte_LeaveDays AS\r\n                    (\r\n\t                    SELECT L.BeginDate, L.BeginDate + L.LeaveDays - 0.5 AS EndDate, L.LeaveDays, L.UserCode\r\n                        FROM HR_Leave_Application AS L\r\n                        WHERE L.AuditState = 1 AND L.IsConfirm = 1\r\n                            AND L.BeginDate >= @beginDate AND L.BeginDate < @endDate\r\n                    ), cte_TheLeaveDay AS \r\n                    (\r\n\t                    SELECT DISTINCT L.BeginDate + n - 1 AS BeginDate, DAY(L.BeginDate + n - 1) AS day , L.UserCode, Y.v_xm\r\n\t                    FROM cte_LeaveDays L\r\n\t                    JOIN Nums ON L.BeginDate + n - 1 <= L.EndDate \r\n\t                    JOIN PT_Yhmc AS Y ON Y.v_yhdm = L.UserCode\r\n\t                    WHERE n <= 30 AND Y.i_bmdm = @depId\r\n                    )\r\n                    ,cte_WorkDays AS\r\n                    (\r\n\t                    SELECT BeginDate,day,PT_Yhmc.v_xm,v_yhdm AS UserCode FROM PT_Yhmc  LEFT JOIN cte_TheLeaveDay\r\n\t                    ON cte_TheLeaveDay.UserCode = PT_Yhmc.v_yhdm \r\n\t                    WHERE i_bmdm = @depId\r\n                    ), cte_MonthDays AS\r\n                    (\r\n                        SELECT * FROM cte_WorkDays\r\n                        PIVOT \r\n                        (\r\n                            COUNT(BeginDate) FOR day IN(\t\t\r\n                                [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],\r\n                                [16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31]\r\n                            )\r\n                        ) AS PVT\r\n                    )\r\n                    SELECT TOP(@pageSize) tab.*\r\n                    FROM\r\n                    (\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY UserCode) AS Num, MD.*, \r\n                        (SELECT COUNT(*) FROM cte_TheLeaveDay AS TD WHERE TD.UserCode = MD.UserCode) AS TotalLeaveDays\r\n                        FROM cte_MonthDays AS MD\r\n                    )tab WHERE Num > (@pageNo - 1 ) * @pageSize";
            list.Add(new SqlParameter("@beginDate", startDate));
            list.Add(new SqlParameter("@endDate", endDate));
            list.Add(new SqlParameter("@depId", depId));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageNo", pageNo));
            SqlDataReader reader = publicDbOpClass.ExecuteReader(CommandType.Text, cmdText, list.ToArray());
            DataTable table = new DataTable();
            table.Load(reader);
            return table;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,AuditState,UserCode,RecordDate,LeaveType,BeginDate,Days,Reason,IsApply,BackDate,LeaveDays,ConfirmUser,IsConfirm ");
            builder.Append(" FROM HR_Leave_Application ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public HRLeaveApplication GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" RecordID,AuditState,UserCode,RecordDate,LeaveType,BeginDate,Days,Reason,IsApply,BackDate,LeaveDays,ConfirmUser,IsConfirm ");
            builder.Append(" from HR_Leave_Application ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            HRLeaveApplication application = new HRLeaveApplication();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                application.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                application.AuditState = int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString());
            }
            application.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                application.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["LeaveType"].ToString() != "")
            {
                application.LeaveType = int.Parse(set.Tables[0].Rows[0]["LeaveType"].ToString());
            }
            if (set.Tables[0].Rows[0]["BeginDate"].ToString() != "")
            {
                application.BeginDate = DateTime.Parse(set.Tables[0].Rows[0]["BeginDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["Days"].ToString() != "")
            {
                application.Days = decimal.Parse(set.Tables[0].Rows[0]["Days"].ToString());
            }
            application.Reason = set.Tables[0].Rows[0]["Reason"].ToString();
            application.IsApply = set.Tables[0].Rows[0]["IsApply"].ToString();
            if (set.Tables[0].Rows[0]["BackDate"].ToString() != "")
            {
                application.BackDate = DateTime.Parse(set.Tables[0].Rows[0]["BackDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["LeaveDays"].ToString() != "")
            {
                application.LeaveDays = decimal.Parse(set.Tables[0].Rows[0]["LeaveDays"].ToString());
            }
            application.ConfirmUser = set.Tables[0].Rows[0]["ConfirmUser"].ToString();
            application.IsConfirm = set.Tables[0].Rows[0]["IsConfirm"].ToString();
            return application;
        }

        public int Update(HRLeaveApplication model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Leave_Application set ");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("LeaveType=" + model.LeaveType + ",");
            builder.Append("BeginDate='" + model.BeginDate + "',");
            builder.Append("Days=" + model.Days + ",");
            builder.Append("Reason='" + model.Reason + "',");
            builder.Append("IsApply='" + model.IsApply + "'");
            builder.Append(" where RecordID='" + model.RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

