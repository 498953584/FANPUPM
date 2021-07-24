<%@ Application Language="C#" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="cn.justwin.Domain.Services"%>
<%@ Import Namespace="cn.justwin.stockBLL"%>
<%@ Import Namespace="cn.justwin.Warn"%>
<%@ Import Namespace="cn.justwin.Web"%>
<%@ Import Namespace="System"%>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Diagnostics"%>
<%@ Import Namespace="System.Runtime.CompilerServices"%>
<%@ Import Namespace="System.Text"%>
<%@ Import Namespace="System.Threading"%>
<%@ Import Namespace="System.Web"%>
<%@ Import Namespace="System.Web.Profile"%>
<%@ Import Namespace="WcfNHibernate"%>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        try
        {
            base.Application["num"] = 0;
            Log4netHelper.StartConfigure();
            NHibernateFactory.Initialize();
            Schedule schedule = new Schedule(3600000);
            schedule.ActionList.Add(new Action(WorkFlow.DealOutTimeAudit));
            schedule.ActionList.Add(new Action(Warning.MendingWarning));
            schedule.ActionList.Add(new Action(Common2.AddAlarm));
            schedule.ActionList.Add(new Action(new PTWarningService().DeleteInvalidData));
            schedule.ActionList.Add(new Action(new PrjMilestoneService().AddMilestone));
            Thread thread = new Thread(new ParameterizedThreadStart(schedule.Run));
            thread.Start();
            ThreadPool.QueueUserWorkItem(new WaitCallback(schedule.CreateChartImageHandlerPath));
            ThreadPool.QueueUserWorkItem(new WaitCallback(schedule.CreateSQLBackupPath));
            ThreadPool.QueueUserWorkItem(new WaitCallback(AppSettingHelper.WritToDB));
            ThreadPool.QueueUserWorkItem(new WaitCallback(ClearHelper.ClearBudTask));
            ThreadPool.QueueUserWorkItem(new WaitCallback(ClearHelper.ClearTotal2));
            ThreadPool.QueueUserWorkItem(new WaitCallback(ClearHelper.ClearPTPrjInfo));
        }
        catch (Exception ex)
        {
            Log4netHelper.Error(ex, "Application_Start", string.Empty);
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        try
        {
            if (base.Server.GetLastError() != null)
            {
                Exception baseException = base.Server.GetLastError().GetBaseException();
                StringBuilder stringBuilder = new StringBuilder();
                Log4netHelper.Error(baseException, "Global", base.User.ToString());
                try
                {
                    Log4netHelper.Error(baseException, "Global", string.Empty);
                    stringBuilder.Append("发生异常页: ").Append(base.Request.Url.ToString()).Append("</br>").Append("异常信息:").Append(baseException.Message).Append("</br>");
                    base.Server.ClearError();
                    base.Application["Error"] = stringBuilder.ToString();
                    base.Response.Redirect("~/StockManage/ErrorPage.aspx", true);
                }
                catch (Exception ex)
                {
                    Log4netHelper.Error(ex, "Application_Error", string.Empty);
                }
            }
        }
        catch
        {
            base.Response.Redirect("~/StockManage/ErrorPage.aspx", true);
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
        string b = base.Session["yhdm"].ToString();
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            if (dataTable.Rows[i]["userCode"].ToString() == b)
            {
                dataTable.Rows.Remove(dataTable.Rows[i]);
            }
        }
    }
       
</script>
