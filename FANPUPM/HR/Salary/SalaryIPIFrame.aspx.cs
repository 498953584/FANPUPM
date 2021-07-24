using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryIPIFrame : BasePage, IRequiresSessionState
{

	protected int Tempid
	{
		get
		{
			return Convert.ToInt32(this.ViewState["TEMPID"]);
		}
		set
		{
			this.ViewState["TEMPID"] = value;
		}
	}
	protected int Year
	{
		get
		{
			return Convert.ToInt32(this.ViewState["YEAR"]);
		}
		set
		{
			this.ViewState["YEAR"] = value;
		}
	}
	protected string bmdm
	{
		get
		{
			return this.ViewState["BMDM"].ToString();
		}
		set
		{
			this.ViewState["BMDM"] = value;
		}
	}
	protected SalaryIPIAction sipi
	{
		get
		{
			return new SalaryIPIAction();
		}
	}
	protected string CorpCode
	{
		get
		{
			return this.ViewState["CORPCODE"].ToString();
		}
		set
		{
			this.ViewState["CORPCODE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && base.Request["vb"] != null)
		{
			this.bmdm = base.Request["vb"].ToString();
			this.DDLDataBind();
			this.DDLTempID.DataBind();
			if (this.DDLTempID.Items.Count <= 0)
			{
				base.Response.Write("请创建员工工资模板！");
				base.Response.End();
			}
			this.hdnTempID.Value = this.DDLTempID.SelectedValue;
			this.HdnYear.Value = this.DDLYear.SelectedValue;
			this.CorpCode = this.sipi.strCorpCode(this.bmdm);
			this.GVIssuePayInfoDataBind();
			this.btnIssuePayInfo.Attributes["onclick"] = "OpenIssuePayInfo('" + this.bmdm + "');";
			this.btnAdjust.Attributes["onclick"] = "OpenSalaryIPIEdit('" + this.bmdm + "');";
			this.btnViewWF.Attributes["onclick"] = "OpenViewWF('023', '001');";
			this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF('023', '001');";
			this.btnIssuePay.Attributes["onclick"] = " return confirm('你确定发放当前月的工资吗？发放将不能修改！');";
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.hdnTempID.Value = this.DDLTempID.SelectedValue;
		this.HdnYear.Value = this.DDLYear.SelectedValue;
		this.GVIssuePayInfoDataBind();
	}
	protected void DDLDataBind()
	{
		int year = DateTime.Now.Year;
		for (int i = 2006; i < year + 5; i++)
		{
			ListItem item = new ListItem(i.ToString(), i.ToString());
			this.DDLYear.Items.Add(item);
		}
		this.DDLYear.SelectedValue = year.ToString();
	}
	protected void GVIssuePayInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"','",
				this.bmdm,
				"','",
				dataRowView["IssueMonth"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			switch (int.Parse(dataRowView["AuditState"].ToString()))
			{
			case -2:
				e.Row.Cells[3].Text = "打回到发起人";
				break;
			case -1:
				e.Row.Cells[3].Text = "未启动流程";
				break;
			case 0:
				e.Row.Cells[3].Text = "流程流转中";
				break;
			case 1:
				e.Row.Cells[3].Text = "审核通过";
				break;
			}
			string a;
			if ((a = dataRowView["IsIssuePay"].ToString()) != null)
			{
				if (a == "1")
				{
					e.Row.Cells[4].Text = "已发放";
					return;
				}
				if (!(a == "0"))
				{
					return;
				}
				e.Row.Cells[4].Text = "未发放";
			}
		}
	}
	protected void btnIssuePayInfo_Click(object sender, EventArgs e)
	{
		this.GVIssuePayInfoDataBind();
	}
	protected void GVIssuePayInfoDataBind()
	{
		this.Tempid = Convert.ToInt32(this.hdnTempID.Value);
		this.Year = Convert.ToInt32(this.HdnYear.Value);
		SqlDataSource expr_32 = this.SqlIssuePayInfo;
		object selectCommand = expr_32.SelectCommand;
		expr_32.SelectCommand = string.Concat(new object[]
		{
			selectCommand,
			" where IssueYear =",
			this.Year,
			" and TempletID=",
			this.Tempid,
			" and CorpCode = '",
			this.CorpCode,
			"' order by IssueMonth asc"
		});
		this.GVIssuePayInfo.DataBind();
	}
	protected void btnAdjust_Click(object sender, EventArgs e)
	{
		this.frmPage.Attributes["src"] = string.Concat(new string[]
		{
			"SalaryIPIViewList.aspx?temid=",
			this.hdnTempID.Value,
			"&year=",
			this.HdnYear.Value,
			"&bm=",
			this.bmdm,
			"&m=",
			this.hdnMonth.Value
		});
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		this.Page.SmartNavigation = false;
		string text = FlowAuditAction.BeginFlow("023", "001", new Guid(this.HdnRecoreId.Value), "", base.UserCode);
		if (text == "工作流程已成功启动")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			this.GVIssuePayInfo.DataBind();
			return;
		}
		if (text == "请先设置当前模块的审核流程")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
		this.GVIssuePayInfo.DataBind();
	}
	protected void btnIssuePay_Click(object sender, EventArgs e)
	{
		SalaryIPIModel salaryIPIModel = new SalaryIPIModel();
		salaryIPIModel.CorpCode = this.CorpCode;
		salaryIPIModel.IsIssuePay = "1";
		salaryIPIModel.IssueMonth = Convert.ToInt32(this.hdnMonth.Value);
		salaryIPIModel.IssueYear = Convert.ToInt32(this.HdnYear.Value);
		if (this.sipi.UpdateIsIssuePay(salaryIPIModel) == 1)
		{
			this.JS.Text = "alert('工资发放成功！');";
			this.GVIssuePayInfoDataBind();
			this.frmPage.Attributes["src"] = string.Concat(new string[]
			{
				"SalaryIPIViewList.aspx?temid=",
				this.hdnTempID.Value,
				"&year=",
				this.HdnYear.Value,
				"&bm=",
				this.bmdm,
				"&m=",
				this.hdnMonth.Value
			});
			return;
		}
		this.JS.Text = "alert('工资发放失败！');";
	}
	protected void DDLTempID_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.hdnTempID.Value = this.DDLTempID.SelectedValue;
	}
	protected void DDLYear_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.HdnYear.Value = this.DDLYear.SelectedValue.ToString();
	}
}


