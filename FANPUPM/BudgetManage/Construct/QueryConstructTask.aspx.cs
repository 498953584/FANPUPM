using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.PrjManager;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_ConstructMain : NBasePage, IRequiresSessionState
{
    private string conId = string.Empty;
    private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
    private StringBuilder strJS = new StringBuilder();
    protected override void OnInit(EventArgs e)
    {
        if (base.Request.QueryString["conId"] != null)
        {
            this.conId = base.Request.QueryString["conId"];
        }
        if (base.Request.QueryString["ic"] != null)
        {
            this.conId = base.Request.QueryString["ic"];
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
            this.BindGv();
            ConstructReport byId = ConstructReport.GetById(this.conId);
            this.txtDate.Text = byId.InputDate.ToString("yyyy-M-dd");
            this.txtInputUser.Text = byId.InputUser;
            this.txtWorkCard.Value = byId.WorkCard;
            this.hfldReportId.Value = this.conId;
            ProjectInfo byId2 = ProjectInfo.GetById(byId.ProjectId);
            this.lblPrjName.Text = StringUtility.GetStr(byId2.PrjName, 25);
            this.spPrjName.Attributes.Add("title", byId2.PrjName);
            this.FileUpload1.InnerHtml = this.FilesBind(this.conId);
        }
    }
    protected void BindGv()
    {
        List<string> taskIdsByReportId = ConstructTask.GetTaskIdsByReportId(this.conId);
        string text = "";
        foreach (string current in taskIdsByReportId)
        {
            if (current != "")
            {
                text += "'";
                text += current;
                text += "',";
            }
        }
        if (text != "")
        {
            text = text.Substring(0, text.Length - 1);
        }
        else
        {
            text = "''";
        }
        DataTable allByTaskIds = ConstructTask.GetAllByTaskIds(text, this.conId, true, false);
        this.ViewState["TasksTable"] = allByTaskIds;
        this.gvTask.DataSource = allByTaskIds;
        this.gvTask.DataBind();
    }
    protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string text = this.gvTask.DataKeys[e.Row.RowIndex].Values[1].ToString();
            e.Row.Attributes["id"] = text;
            if (this.hfldIsWBSRelevance.Value == "1")
            {
                string text2 = this.gvTask.DataKeys[e.Row.RowIndex].Values[0].ToString();
                e.Row.Attributes["onclick"] = string.Concat(new string[]
				{
					"showInfo('",
					text,
					"','",
					text2,
					"')"
				});
                e.Row.Style.Add("cursor", "pointer");
            }
        }
    }
    public string FilesBind(string consID)
    {
        string text = ConfigHelper.Get("ConstructReportAccessory");
        string result;
        try
        {
            string[] files = Directory.GetFiles(base.Server.MapPath(text) + consID);
            StringBuilder stringBuilder = new StringBuilder();
            string[] array = files;
            for (int i = 0; i < array.Length; i++)
            {
                string text2 = array[i];
                string text3 = string.Empty;
                text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
                string str = text + "/" + consID;
                string str2 = str + "/" + text3;
                text3 = string.Concat(new string[]
				{
					"<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
					HttpUtility.UrlEncode(str2),
					"\"  >",
					text3,
					"</a>"
				});
                stringBuilder.Append(text3);
                stringBuilder.Append(", ");
            }
            string text4 = string.Empty;
            if (stringBuilder.Length > 2)
            {
                text4 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
            }
            result = text4;
        }
        catch
        {
            result = "";
        }
        return result;
    }
}
