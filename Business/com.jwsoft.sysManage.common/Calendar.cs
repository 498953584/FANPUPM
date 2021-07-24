namespace com.jwsoft.sysManage.common
{
    using System;
    using System.Web.UI;

    public class Calendar
    {
        public Calendar(Page myPage)
        {
            string script = "";
            script = ((((((script + "<SCRIPT language=\"JavaScript\" type=\"text/JavaScript\">" + "function opencalendar(obj)") + "{" + "\turl='/oa/common/calendar.aspx';") + "\twindow.showModalDialog(url,window,'dialogHeight: 230px; dialogWidth: 350px; center: Yes; help: No; resizable: Yes; status: No;');" + "try{") + "\tif(returnYear!=\"\" && returnMonth!=\"\" && returnDay!=\"\")" + "\t{") + "\t\tobj.value = returnYear + \"-\" + returnMonth + \"-\" + returnDay" + "\t}") + "}" + "catch(e){}") + "}" + "</SCRIPT>";
            myPage.RegisterStartupScript("calendar", script);
        }
    }
}

