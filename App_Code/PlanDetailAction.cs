using com.jwsoft.common.data;
using com.jwsoft.pm.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class PlanDetailAction
{
    public bool AddModelIntoBaseData(PlanDetail plan)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("insert into Pm_WorkPlan_WeekPlanDetails (WkpDetailsId,WkpId,WkpContents,WkpStartTime,WkpEndTime,WkpPersons,WkpChief,WkpStandard) values('", new object[0]);
        builder.AppendFormat(string.Concat(new object[] { plan.WkpDetailsId, "','", plan.WkpId, "','", plan.WkpContents, "','", plan.WkpStartTime, "','", plan.WkpEndTime, "','" }), new object[0]);
        builder.AppendFormat(plan.WkpPersons + "','" + plan.WkpChief + "','" + plan.WkpStandard + "')", new object[0]);
        return publicDbOpClass.NonQuerySqlString(builder.ToString());
    }

    public static string DeCodeStr(string note)
    {
        return note.Replace("&nbsp;", " ").Replace("<br/>", "\n");
    }

    public static string EncodeStr(string note)
    {
        return note.Replace("?", ".").Replace("|", ",").Replace(" ", "&nbsp;").Replace("\n", "<br/>");
    }

    public static bool ExecuteResult(string sqlstr)
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

    public static string GetInsertStr(PlanDetail plan)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("insert into Pm_WorkPlan_WeekPlanDetails (WkpDetailsId,WkpId,WkpContents,WkpStartTime,WkpEndTime,WkpPersons,WkpChief,WkpStandard) values('", new object[0]);
        builder.AppendFormat(string.Concat(new object[] { plan.WkpDetailsId, "','", plan.WkpId, "','", plan.WkpContents, "','", plan.WkpStartTime, "','", plan.WkpEndTime, "','" }), new object[0]);
        builder.AppendFormat(plan.WkpPersons + "','" + plan.WkpChief + "','" + plan.WkpStandard + "')", new object[0]);
        return builder.ToString();
    }

    public List<PlanDetail> GetPlanDemos(Guid wkpId)
    {
        DataTable table = publicDbOpClass.DataTableQuary("select * from Pm_WorkPlan_WeekPlanDetails where wkpid='" + wkpId + "'");
        List<PlanDetail> list = new List<PlanDetail>();
        if (table.Rows.Count <= 0)
        {
            return null;
        }
        for (int i = 0; i < table.Rows.Count; i++)
        {
            PlanDetail item = new PlanDetail {
                WkpChief = table.Rows[i]["WkpChief"].ToString(),
                WkpContents = table.Rows[i]["WkpContents"].ToString(),
                WkpDetailsId = new Guid(table.Rows[i]["WkpDetailsId"].ToString()),
                WkpEndTime = DateTime.Parse(table.Rows[i]["WkpEndTime"].ToString()),
                WkpId = new Guid(table.Rows[i]["WkpId"].ToString()),
                WkpPersons = table.Rows[i]["WkpPersons"].ToString(),
                WkpStandard = table.Rows[i]["WkpStandard"].ToString(),
                WkpStartTime = DateTime.Parse(table.Rows[i]["WkpStartTime"].ToString())
            };
            list.Add(item);
        }
        return list;
    }

    public static string GetUpdateStr(PlanDetail plModel, Guid WkpDetailsId)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("update pm_workplan_weekplandetails set WkpChief='" + plModel.WkpChief + "', WkpContents='" + plModel.WkpContents + "', WkpEndTime='", new object[0]);
        builder.AppendFormat(string.Concat(new object[] { plModel.WkpEndTime, "', WkpPersons='", plModel.WkpPersons, "', WkpStandard='", plModel.WkpStandard, "' ,WkpStartTime='", plModel.WkpStartTime, "'" }), new object[0]);
        builder.AppendFormat(" where WkpdetailsId='" + WkpDetailsId + "'", new object[0]);
        return builder.ToString();
    }

    public bool UpdateModel(PlanDetail plModel, Guid WkpId)
    {
        return publicDbOpClass.NonQuerySqlString(GetUpdateStr(plModel, WkpId));
    }
}

