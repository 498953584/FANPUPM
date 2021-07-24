using ASP;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using cn.justwin.Web;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_AuditViewPrint : BasePage, IRequiresSessionState
{
	public System.Guid InstanceCode
	{
		get
		{
			object obj = this.ViewState["InstanceCode"];
			if (obj != null)
			{
				return (System.Guid)obj;
			}
			return System.Guid.Empty;
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

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		this.InstanceCode = new System.Guid(base.Request["ic"]);
		this.BusinessCode = base.Request["bc"].ToString();
		this.BusinessClass = base.Request["bcl"].ToString();
		this.AddNewTr(FlowAuditAction.QueryAuditStatus(this.InstanceCode, this.BusinessCode, this.BusinessClass));
		this.btnPrint.Attributes["onclick"] = "PrintPage();return false;";
		this.divTemplateName.InnerText = FlowAuditAction.TemplateName(this.InstanceCode, this.BusinessCode, this.BusinessClass);
		this.LbUserName.Text = FlowAuditAction.OrganigerName(this.InstanceCode, this.BusinessCode, this.BusinessClass);
		if (this.divTemplateName.InnerText.Length > 10)
		{
			this.divTemplateName.Style.Add("word-wrap", "break-word");
		}
		this.LbStartTime.Text = FlowAuditAction.StartTime(this.InstanceCode, this.BusinessCode, this.BusinessClass);
		DataTable dataTable = publicDbOpClass.DataTableQuary(string.Concat(new string[]
		{
			" select * from WF_Business_Class  where BusinessCode= '",
			this.BusinessCode,
			"' and BusinessClass='",
			this.BusinessClass,
			"' "
		}));
		this.LbConter.Text = dataTable.Rows[0]["BusinessClassName"].ToString() + ":";
		DataTable dataTable2 = publicDbOpClass.DataTableQuary(" select * from WF_BusinessCode where BusinessCode='" + this.BusinessCode + "' ");
		try
		{
			DataTable dataTable3 = publicDbOpClass.DataTableQuary(string.Concat(new object[]
			{
				" select  ",
				dataTable2.Rows[0]["NameField"].ToString(),
				" from  ",
				dataTable2.Rows[0]["LinkTable"].ToString(),
				"  where ",
				dataTable2.Rows[0]["PrimaryField"].ToString(),
				" ='",
				this.InstanceCode,
				"' "
			}));
			if (this.BusinessCode == "089")
			{
				this.LinkButton1.Text = TenderInfo.GetProjectName(dataTable3.Rows[0][0].ToString());
			}
			else
			{
				if (this.BusinessCode == "100" || this.BusinessCode == "106")
				{
					this.LinkButton1.Text = ProjectInfo.GetProjectName(dataTable3.Rows[0][0].ToString());
				}
				else
				{
					this.LinkButton1.Text = dataTable3.Rows[0][0].ToString();
				}
			}
		}
		catch
		{
			this.LinkButton1.Text = "查看";
		}
		this.LinkButton1.Attributes["onclick"] = string.Concat(new object[]
		{
			" viewopen ('",
			FlowAuditAction.DoWithUrl(this.BusinessCode),
			"ic=",
			this.InstanceCode,
			"')"
		});
		this.LbAuditCode.Text = FlowAuditAction.GetTemplateCode(this.InstanceCode, this.BusinessCode, this.BusinessClass);
	}
	protected void AddNewTr(DataTable dt)
	{
		if (dt.Rows.Count > 0)
		{
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			htmlTableRow.Attributes["style"] = "text-align:center;";
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.InnerHtml = "<b>序号</b>";
			htmlTableCell.Width = "120px";
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
			HtmlTableCell htmlTableCell6 = new HtmlTableCell();
			htmlTableCell6.InnerHtml = "<b>流程状态</b>";
			htmlTableRow.Cells.Add(htmlTableCell6);
			this.printTable.Rows.Add(htmlTableRow);
		}
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			HtmlTableRow htmlTableRow2 = new HtmlTableRow();
			htmlTableRow2.Attributes["style"] = "text-align:center;";
			HtmlTableCell htmlTableCell7 = new HtmlTableCell();
			htmlTableCell7.RowSpan = 3;
			htmlTableCell7.InnerText = (i + 1).ToString();
			htmlTableRow2.Cells.Add(htmlTableCell7);
			HtmlTableCell htmlTableCell8 = new HtmlTableCell();
			htmlTableCell8.InnerText = dt.Rows[i]["NodeName"].ToString();
			htmlTableRow2.Cells.Add(htmlTableCell8);
			HtmlTableCell htmlTableCell9 = new HtmlTableCell();
			htmlTableCell9.InnerText = dt.Rows[i]["auditor"].ToString();
			htmlTableRow2.Cells.Add(htmlTableCell9);
			HtmlTableCell htmlTableCell10 = new HtmlTableCell();
			htmlTableCell10.InnerText = dt.Rows[i]["AuditDate"].ToString();
			htmlTableRow2.Cells.Add(htmlTableCell10);
			HtmlTableCell htmlTableCell11 = new HtmlTableCell();
			htmlTableCell11.InnerText = dt.Rows[i]["Result"].ToString();
			htmlTableRow2.Cells.Add(htmlTableCell11);
			HtmlTableCell htmlTableCell12 = new HtmlTableCell();
			htmlTableCell12.InnerText = dt.Rows[i]["Status"].ToString();
			htmlTableRow2.Cells.Add(htmlTableCell12);
			this.printTable.Rows.Add(htmlTableRow2);
			HtmlTableRow htmlTableRow3 = new HtmlTableRow();
			HtmlTableCell htmlTableCell13 = new HtmlTableCell();
			htmlTableCell13.InnerHtml = "<b>审核，审核意见</b>";
			htmlTableCell13.Attributes["style"] = "padding-right:10px";
			htmlTableCell13.Height = "auto";
			htmlTableCell13.Align = "right";
			htmlTableRow3.Cells.Add(htmlTableCell13);
			HtmlTableCell htmlTableCell14 = new HtmlTableCell();
			htmlTableCell14.InnerHtml = "<div style=\"width: 95%; word-break: break-all;\">";
			HtmlTableCell expr_2EA = htmlTableCell14;
			expr_2EA.InnerHtml += dt.Rows[i]["AuditInfo"].ToString();
			HtmlTableCell expr_318 = htmlTableCell14;
			expr_318.InnerHtml += "</div>";
			htmlTableCell14.ColSpan = 3;
			htmlTableRow3.Cells.Add(htmlTableCell14);
			HtmlTableCell htmlTableCell15 = new HtmlTableCell();
			htmlTableCell15.RowSpan = 2;
			DataTable dataTable = FlowAuditAction.ImageListPath(dt.Rows[i]["Operator"].ToString());
			string text = "";
			if (dataTable.Rows.Count > 0)
			{
				text = dataTable.Rows[0]["AuditNameImagePath"].ToString();
			}
			if (text == "")
			{
				htmlTableCell15.InnerHtml = " <img src=\"/images/defaultaudit.gif\" height=\"80px\" width=\"80px\"/>";
			}
			else
			{
				htmlTableCell15.InnerHtml = " <img src=\"" + text + "\" height=\"80px\" width=\"80px\"/>";
			}
			htmlTableRow3.Cells.Add(htmlTableCell15);
			this.printTable.Rows.Add(htmlTableRow3);
			HtmlTableRow htmlTableRow4 = new HtmlTableRow();
			HtmlTableCell htmlTableCell16 = new HtmlTableCell();
			htmlTableCell16.InnerHtml = "<b >相关附件</b>";
			htmlTableCell16.Attributes["style"] = "padding-right:10px";
			htmlTableCell16.Height = "30px";
			htmlTableCell16.Align = "right";
			htmlTableRow4.Cells.Add(htmlTableCell16);
			HtmlTableCell htmlTableCell17 = new HtmlTableCell();
			htmlTableCell17.InnerHtml = this.FilesBind(this.InstanceCode.ToString(), dt.Rows[i]["NoteID"].ToString());
			htmlTableCell17.ColSpan = 3;
			htmlTableRow4.Cells.Add(htmlTableCell17);
			this.printTable.Rows.Add(htmlTableRow4);
		}
		HtmlTableRow htmlTableRow5 = new HtmlTableRow();
		HtmlTableCell htmlTableCell18 = new HtmlTableCell();
		htmlTableCell18.InnerHtml = "<b >流程有效性审核人</b>";
		htmlTableCell18.Attributes["style"] = "white-space:nowrap;padding-right:10px";
		htmlTableRow5.Cells.Add(htmlTableCell18);
		HtmlTableCell htmlTableCell19 = new HtmlTableCell();
		htmlTableCell19.InnerText = "";
		htmlTableCell19.ColSpan = 5;
		htmlTableCell19.Attributes["style"] = "height:50px;";
		htmlTableRow5.Cells.Add(htmlTableCell19);
		this.printTable.Rows.Add(htmlTableRow5);
	}
	public string FilesBind(string recordCode, string noteid)
	{
		string text = ConfigHelper.Get("Audit");
		string result;
		try
		{
			string[] files = System.IO.Directory.GetFiles(string.Concat(new string[]
			{
				base.Server.MapPath(text),
				"\\",
				recordCode,
				"\\",
				noteid
			}));
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
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
	protected void DLPrint_ItemDataBound(object sender, DataListItemEventArgs e)
	{
		if (e.Item.ItemIndex != -1)
		{
			DataRowView dataRowView = (DataRowView)e.Item.DataItem;
			Image image = (Image)e.Item.FindControl("ImgName");
			if (dataRowView["Result"].ToString() == "已通过")
			{
				DataTable dataTable = FlowAuditAction.ImageListPath(dataRowView["Operator"].ToString());
				image.ImageUrl = dataTable.Rows[0]["AuditNameImagePath"].ToString();
				return;
			}
			image.ImageUrl = "/images/blank.gif";
		}
	}
}


