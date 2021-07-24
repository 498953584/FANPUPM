using ASP;
using cn.justwin.Web;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Common_ShowAudit : System.Web.UI.UserControl
{
	private string businessClass;
	private string businessCode;

	public string BusiClass
	{
		set
		{
			this.businessClass = value;
		}
	}
	public string BusiCode
	{
		set
		{
			this.businessCode = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (base.Request.QueryString["bc"] != null)
			{
				this.businessCode = base.Request.QueryString["bc"].ToString();
			}
			if (base.Request.QueryString["ic"] != null)
			{
				this.AddNewTr(base.Request.QueryString["ic"], this.businessCode, this.businessClass);
			}
			this.LbUserName.Text = FlowAuditAction.OrganigerName(new Guid(base.Request["ic"]), this.businessCode, this.businessClass);
			this.LbStartTime.Text = FlowAuditAction.StartTime(new Guid(base.Request["ic"]), this.businessCode, this.businessClass);
		}
		catch
		{
			throw new Exception("请确保Get请求中包含'ic',并且配置有BusiClass和BusiCode");
		}
	}
	protected void AddNewTr(string ic, string businessCode, string businessClass)
	{
		Guid instanceCode = new Guid(ic);
		DataTable dataTable = FlowAuditAction.QueryAuditStatus(instanceCode, businessCode, businessClass);
		if (dataTable.Rows.Count == 0)
		{
			this.table.Visible = false;
			this.tableHeader.Visible = false;
			return;
		}
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			htmlTableRow.Attributes["class"] = "rowa";
			htmlTableRow.Attributes["style"] = "text-align:center;";
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.InnerText = (i + 1).ToString();
			htmlTableCell.RowSpan = 3;
			htmlTableRow.Cells.Add(htmlTableCell);
			HtmlTableCell htmlTableCell2 = new HtmlTableCell();
			htmlTableCell2.InnerText = dataTable.Rows[i]["NodeName"].ToString();
			htmlTableRow.Cells.Add(htmlTableCell2);
			HtmlTableCell htmlTableCell3 = new HtmlTableCell();
			htmlTableCell3.InnerText = dataTable.Rows[i]["auditor"].ToString();
			htmlTableRow.Cells.Add(htmlTableCell3);
			HtmlTableCell htmlTableCell4 = new HtmlTableCell();
			htmlTableCell4.InnerText = dataTable.Rows[i]["AuditDate"].ToString();
			htmlTableRow.Cells.Add(htmlTableCell4);
			HtmlTableCell htmlTableCell5 = new HtmlTableCell();
			htmlTableCell5.InnerText = dataTable.Rows[i]["Result"].ToString();
			htmlTableRow.Cells.Add(htmlTableCell5);
			this.table.Rows.Add(htmlTableRow);
			HtmlTableRow htmlTableRow2 = new HtmlTableRow();
			HtmlTableCell htmlTableCell6 = new HtmlTableCell();
			htmlTableCell6.Style.Add("white-space", "nowrap");
			htmlTableCell6.InnerHtml = "<b>审核意见:</b>";
			htmlTableCell6.InnerText = "审核意见：";
			htmlTableCell6.Height = "auto";
			htmlTableCell6.Align = "right";
			htmlTableRow2.Cells.Add(htmlTableCell6);
			HtmlTableCell htmlTableCell7 = new HtmlTableCell();
			htmlTableCell7.InnerHtml = "<div style=\"width: 95%; word-break: break-all;\">";
			HtmlTableCell expr_1FD = htmlTableCell7;
			expr_1FD.InnerHtml += dataTable.Rows[i]["AuditInfo"].ToString();
			HtmlTableCell expr_22A = htmlTableCell7;
			expr_22A.InnerHtml += "</div>";
			htmlTableCell7.ColSpan = 4;
			htmlTableRow2.Cells.Add(htmlTableCell7);
			this.table.Rows.Add(htmlTableRow2);
			HtmlTableRow htmlTableRow3 = new HtmlTableRow();
			HtmlTableCell htmlTableCell8 = new HtmlTableCell();
			htmlTableCell8.Style.Add("white-space", "nowrap");
			htmlTableCell8.InnerHtml = "<b>相关附件:</b>";
			htmlTableCell8.InnerText = "相关附件：";
			htmlTableCell8.Align = "right";
			htmlTableRow3.Cells.Add(htmlTableCell8);
			HtmlTableCell htmlTableCell9 = new HtmlTableCell();
			htmlTableCell9.InnerHtml = this.FilesBind(ic, dataTable.Rows[i]["NoteID"].ToString());
			htmlTableCell9.ColSpan = 4;
			htmlTableRow3.Cells.Add(htmlTableCell9);
			this.table.Rows.Add(htmlTableRow3);
		}
	}
	public string FilesBind(string recordCode, string noteid)
	{
		string text = ConfigHelper.Get("Audit");
		string result;
		try
		{
			string[] files = Directory.GetFiles(string.Concat(new string[]
			{
				base.Server.MapPath(text),
				"\\",
				recordCode,
				"\\",
				noteid
			}));
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = string.Concat(new string[]
				{
					text,
					"/",
					recordCode,
					"/",
					noteid
				});
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


