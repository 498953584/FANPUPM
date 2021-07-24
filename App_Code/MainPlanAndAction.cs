using com.jwsoft.common.data;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class MainPlanAndAction
{
    public string BackDept(string WkpDeptId)
    {
        DataTable table = publicDbOpClass.DataTableQuary("select v_bmmc from pt_d_bm where i_bmdm='" + WkpDeptId + "'");
        if (table.Rows.Count > 0)
        {
            return table.Rows[0]["v_bmmc"].ToString();
        }
        return "";
    }

    public string[] BackDeptAndID(string UserName)
    {
        DataTable table = publicDbOpClass.DataTableQuary("select i_bmdm from pt_yhmc where v_xm='" + UserName + "'");
        string[] strArray = new string[2];
        if (table.Rows.Count > 0)
        {
            strArray[0] = table.Rows[0]["i_bmdm"].ToString();
            table = publicDbOpClass.DataTableQuary("select v_bmmc from pt_d_bm where i_bmdm='" + strArray[0] + "' ");
            strArray[1] = table.Rows[0]["v_bmmc"].ToString();
            return strArray;
        }
        strArray[0] = "";
        strArray[1] = "";
        return null;
    }

    public static string[] BackDeptOrID(string UserCode)
    {
        DataTable table = publicDbOpClass.DataTableQuary("select i_bmdm from pt_yhmc where v_yhdm='" + UserCode + "'");
        string[] strArray = new string[2];
        if (table.Rows.Count > 0)
        {
            strArray[0] = table.Rows[0]["i_bmdm"].ToString();
            table = publicDbOpClass.DataTableQuary("select v_bmmc from pt_d_bm where i_bmdm='" + strArray[0] + "' ");
            strArray[1] = table.Rows[0]["v_bmmc"].ToString();
            return strArray;
        }
        strArray[0] = "";
        strArray[1] = "";
        return null;
    }

    public string BackUserCode(string username)
    {
        DataTable table = publicDbOpClass.DataTableQuary("select v_yhdm from pt_yhmc where v_xm='" + username + "'");
        if (table.Rows.Count > 0)
        {
            return table.Rows[0]["v_yhdm"].ToString();
        }
        return "";
    }

    public string BackUserName(string usercode)
    {
        DataTable table = publicDbOpClass.DataTableQuary("select v_xm from pt_yhmc where v_yhdm='" + usercode + "'");
        if (table.Rows.Count > 0)
        {
            return table.Rows[0]["v_xm"].ToString();
        }
        return "";
    }

    public bool ExecuteResult(string sqlstr)
    {
        bool flag;
        SqlConnection connection = new Conn().SqlConnectionSystem();
        SqlCommand command = new SqlCommand();
        SqlTransaction transaction = connection.BeginTransaction();
        command.Connection = connection;
        command.Transaction = transaction;
        command.CommandText = sqlstr;
        command.ExecuteNonQuery();
        try
        {
            transaction.Commit();
            flag = true;
        }
        catch
        {
            transaction.Rollback();
            flag = false;
        }
        finally
        {
            connection.Close();
            command.Dispose();
        }
        return flag;
    }

    public static string GetInsertStr(MainPlan model)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("insert into Pm_WorkPlan_WeekPlan (", new object[0]);
        builder.AppendFormat("WkpId,Wkpusercode,wkpdeptid,wkprecorddate,wkpreportuser,wkpcheckeruser,wkpisreport,wkpreporttype,wkpistrue,wkpregisteruser) values ('", new object[0]);
        builder.AppendFormat(string.Concat(new object[] { model.WkpId, "','", model.WkpUserCode, "','", model.WkpDeptId, "','", model.WkpRecordDate, "','", model.WkpReportUser, "','" }), new object[0]);
        builder.AppendFormat(string.Concat(new object[] { model.WkpCheckerUser, "',", model.WkpIsReport, ",", model.WkpReportType, ",", model.WkpIstrue, ",'", model.WkpRegisterUser, "')" }), new object[0]);
        return builder.ToString();
    }

    public MainPlan GetModel(Guid WkpId)
    {
        DataTable table = this.GetTable(WkpId);
        MainPlan plan = new MainPlan();
        if (table.Rows.Count > 0)
        {
            plan.WkpRegisterUser = table.Rows[0]["WkpRegisterUser"].ToString();
            plan.WkpId = (Guid) table.Rows[0]["WkpId"];
            plan.WkpUserCode = table.Rows[0]["WkpUserCode"].ToString();
            plan.WkpDeptId = table.Rows[0]["WkpDeptId"].ToString();
            plan.WkpReportUser = table.Rows[0]["WkpReportUser"].ToString();
            plan.WkpReportType = Convert.ToInt32(table.Rows[0]["WkpReportType"]);
            plan.WkpRecordDate = DateTime.Parse(table.Rows[0]["WkpRecordDate"].ToString());
            plan.WkpIstrue = Convert.ToInt32(table.Rows[0]["WkpIstrue"]);
            plan.WkpIsReport = Convert.ToInt32(table.Rows[0]["WkpIsReport"]);
            plan.WkpCheckerUser = table.Rows[0]["WkpCheckerUser"].ToString();
            return plan;
        }
        return null;
    }

    public MainPlan GetModel(string WkpUserCode)
    {
        DataTable table = this.GetTable(WkpUserCode);
        MainPlan plan = new MainPlan();
        if (table.Rows.Count > 0)
        {
            plan.WkpRegisterUser = table.Rows[0]["WkpRegisterUser"].ToString();
            plan.WkpId = (Guid) table.Rows[0]["WkpId"];
            plan.WkpUserCode = table.Rows[0]["WkpUserCode"].ToString();
            plan.WkpDeptId = table.Rows[0]["WkpDeptId"].ToString();
            plan.WkpReportUser = table.Rows[0]["WkpReportUser"].ToString();
            plan.WkpReportType = Convert.ToInt32(table.Rows[0]["WkpReportType"]);
            plan.WkpRecordDate = DateTime.Parse(table.Rows[0]["WkpRecordDate"].ToString());
            plan.WkpIstrue = Convert.ToInt32(table.Rows[0]["WkpIstrue"]);
            plan.WkpIsReport = Convert.ToInt32(table.Rows[0]["WkpIsReport"]);
            plan.WkpCheckerUser = table.Rows[0]["WkpCheckerUser"].ToString();
            return plan;
        }
        return null;
    }

    public DataTable GetTable(Guid WkpId)
    {
        return publicDbOpClass.DataTableQuary("select * from Pm_WorkPlan_WeekPlan where wkpid='" + WkpId + "'");
    }

    public DataTable GetTable(string WkpUserCode)
    {
        return publicDbOpClass.DataTableQuary("select * from Pm_WorkPlan_WeekPlan where WkpUserCode='" + WkpUserCode + "'");
    }

    public static string GetUpdateStr(string wkpid, MainPlan model)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("update pm_workplan_weekplan set WkpDeptId='" + model.WkpDeptId + "',WkpRegisterUser='" + model.WkpRegisterUser + "',WkpReportUser='" + model.WkpReportUser + "',WkpCheckerUser='" + model.WkpCheckerUser + "',", new object[0]);
        builder.AppendFormat(string.Concat(new object[] { "WkpReportType=", model.WkpReportType, ",WkpRecordDate='", model.WkpRecordDate, "',WkpIstrue=", model.WkpIstrue, ",WkpIsReport=", model.WkpIsReport, ",InputTime='", model.InputTime, "'" }), new object[0]);
        builder.AppendFormat(" where WkpId='" + wkpid + "'", new object[0]);
        return builder.ToString();
    }

    public bool InsertIntoBase(MainPlan model)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("insert into Pm_WorkPlan_WeekPlan (", new object[0]);
        builder.AppendFormat("WkpId,Wkpusercode,wkpdeptid,wkprecorddate,wkpreportuser,wkpcheckeruser,wkpisreport,wkpreporttype,wkpistrue,wkpregisteruser,InputTime) values ('", new object[0]);
        builder.AppendFormat(string.Concat(new object[] { model.WkpId, "','", model.WkpUserCode, "','", model.WkpDeptId, "','", model.WkpRecordDate, "','", model.WkpReportUser, "','" }), new object[0]);
        builder.AppendFormat(string.Concat(new object[] { model.WkpCheckerUser, "',", model.WkpIsReport, ",", model.WkpReportType, ",", model.WkpIstrue, ",'", model.WkpRegisterUser, "','", model.InputTime, "')" }), new object[0]);
        return (publicDbOpClass.ExecSqlString(builder.ToString()) > 0);
    }

    public bool UpdateResult(string wkpid, MainPlan model)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("update pm_workplan_weekplan set WkpDeptId='" + model.WkpDeptId + "',WkpRegisterUser='" + model.WkpRegisterUser + "',WkpReportUser='" + model.WkpReportUser + "',WkpCheckerUser='" + model.WkpCheckerUser + "',", new object[0]);
        builder.AppendFormat(string.Concat(new object[] { "WkpReportType=", model.WkpReportType, ",WkpRecordDate='", model.WkpRecordDate, "',WkpIstrue=", model.WkpIstrue, ",WkpIsReport=", model.WkpIsReport }), new object[0]);
        builder.AppendFormat(" where WkpId='" + wkpid + "'", new object[0]);
        return publicDbOpClass.NonQuerySqlString(builder.ToString());
    }
}

