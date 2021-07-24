using ASP;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public partial class StockManage_ViewAuditFrame : Page, IRequiresSessionState
{

	public Guid InstanceCode
	{
		get
		{
			object obj = this.ViewState["InstanceCode"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["InstanceCode"] = value;
		}
	}
	protected string BusinessCode
	{
		get
		{
			return this.ViewState["BUSINESSCODE"].ToString();
		}
		set
		{
			this.ViewState["BUSINESSCODE"] = value;
		}
	}
	protected string BusinessClass
	{
		get
		{
			return this.ViewState["BUSINESSCLASS"].ToString();
		}
		set
		{
			this.ViewState["BUSINESSCLASS"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.InstanceCode = new Guid(base.Request["ic"]);
			this.BusinessCode = base.Request["bc"].ToString();
			this.BusinessClass = base.Request["bcl"].ToString();
			this.AddNewTr(FlowAuditAction.QueryAuditStatus(this.InstanceCode, this.BusinessCode, this.BusinessClass));
		}
	}
	protected void AddNewTr(DataTable dt)
	{
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			htmlTableRow.BgColor = "#cccccc";
			htmlTableRow.Attributes["style"] = "text-align:center;";
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.InnerHtml = "<b>序号</b>";
			htmlTableRow.Cells.Add(htmlTableCell);
			HtmlTableCell htmlTableCell2 = new HtmlTableCell();
			htmlTableCell2.InnerHtml = "<b>节点名称</b>";
			htmlTableRow.Cells.Add(htmlTableCell2);
			HtmlTableCell htmlTableCell3 = new HtmlTableCell();
			htmlTableCell3.InnerHtml = "<b>审核人</b>";
			htmlTableRow.Cells.Add(htmlTableCell3);
			HtmlTableCell htmlTableCell4 = new HtmlTableCell();
			htmlTableCell4.InnerHtml = "<b>审核时间</b>";
			htmlTableRow.Cells.Add(htmlTableCell4);
			HtmlTableCell htmlTableCell5 = new HtmlTableCell();
			htmlTableCell5.InnerHtml = "<b>审核结果</b>";
			htmlTableRow.Cells.Add(htmlTableCell5);
			this.printTable.Rows.Add(htmlTableRow);
			HtmlTableRow htmlTableRow2 = new HtmlTableRow();
			htmlTableRow2.Attributes["style"] = "text-align:center;";
			HtmlTableCell htmlTableCell6 = new HtmlTableCell();
			htmlTableCell6.InnerText = (i + 1).ToString();
			htmlTableCell6.RowSpan = 2;
			htmlTableRow2.Cells.Add(htmlTableCell6);
			HtmlTableCell htmlTableCell7 = new HtmlTableCell();
			htmlTableCell7.InnerText = dt.Rows[i]["NodeName"].ToString();
			htmlTableRow2.Cells.Add(htmlTableCell7);
			HtmlTableCell htmlTableCell8 = new HtmlTableCell();
			htmlTableCell8.InnerText = dt.Rows[i]["auditor"].ToString();
			htmlTableRow2.Cells.Add(htmlTableCell8);
			HtmlTableCell htmlTableCell9 = new HtmlTableCell();
			htmlTableCell9.InnerText = dt.Rows[i]["AuditDate"].ToString();
			htmlTableRow2.Cells.Add(htmlTableCell9);
			HtmlTableCell htmlTableCell10 = new HtmlTableCell();
			htmlTableCell10.InnerText = dt.Rows[i]["Result"].ToString();
			htmlTableRow2.Cells.Add(htmlTableCell10);
			this.printTable.Rows.Add(htmlTableRow2);
			HtmlTableRow htmlTableRow3 = new HtmlTableRow();
			HtmlTableCell htmlTableCell11 = new HtmlTableCell();
			htmlTableCell11.Attributes["style"] = "font-family: 华文行楷";
			htmlTableCell11.InnerHtml = "<font size=\"4pt\">" + dt.Rows[i]["AuditInfo"].ToString() + "</font>";
			HtmlTableCell expr_26A = htmlTableCell11;
			expr_26A.InnerHtml += this.FilesBind(30, dt.Rows[i]["NoteID"].ToString());
			htmlTableCell11.ColSpan = 4;
			htmlTableRow3.Cells.Add(htmlTableCell11);
			this.printTable.Rows.Add(htmlTableRow3);
		}
	}
	protected string FilesBind(int moduleID, string recordCode)
	{
		string text = "";
		int annexType = 0;
		AnnexAction annexAction = new AnnexAction();
		DataTable fileList = annexAction.GetFileList(recordCode, annexType, moduleID);
		foreach (DataRow dataRow in fileList.Rows)
		{
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"<a href='#' onclick=\"javascript:download('",
				dataRow["FilePath"].ToString(),
				dataRow["AnnexName"].ToString(),
				"','",
				dataRow["OriginalName"].ToString(),
				"');\" >",
				dataRow["OriginalName"].ToString(),
				"</a> <br>"
			});
		}
		return text;
	}
}


