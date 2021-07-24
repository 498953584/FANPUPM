using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_System_SystemInfoList : BasePage, IRequiresSessionState
{

	protected SystemInfoAction sia
	{
		get
		{
			return new SystemInfoAction();
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
	protected int ClassID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["CLASSID"]);
		}
		set
		{
			this.ViewState["CLASSID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.LBTitle.Text = "制度管理";
			if (base.Request["audit"] != null)
			{
				this.btnAdd.Width = (this.btnEdit.Width = (this.btnDel.Width = 0));
				this.LBTitle.Text = "制度管理";
				this.BusinessCode = "005";
			}
			else
			{
				this.btnAudit.Width = 0;
			}
			if (base.Request["cid"] != null)
			{
				this.LBTitle.Text = "制度管理";
				this.BusinessCode = "005";
				this.ClassID = Convert.ToInt32(base.Request["cid"]);
				this.btnAdd.Attributes["onclick"] = string.Concat(new object[]
				{
					"return openEdit('Add','",
					this.ClassID,
					"','",
					base.Request["ctc"].ToString(),
					"');"
				});
			}
			this.btnEdit.Attributes["onclick"] = "return openEdit('Edit','0','" + base.Request["ctc"].ToString() + "');";
			this.BtnView.Attributes["onclick"] = "return openEdit('View','0','" + base.Request["ctc"].ToString() + "');";
			this.btnDel.Attributes["onclick"] = " return confirm('确定删除吗？');";
			this.btnAudit.Attributes["onclick"] = "OpenAudit()";
			this.databind();
		}
	}
	public void databind()
	{
		string sqlString;
		if (base.Request["cid"] != null)
		{
			if (base.Request["audit"] != null)
			{
				sqlString = string.Concat(new string[]
				{
					"SELECT [RecordID], [ClassID], [AuditState], [SystemType], [CorpCode], [SignDate], [SignMan], [SystemCode], [SystemName], [RecordDate], [UserCode],[IsCurrent], [Remark] FROM [OA_System_Info] WHERE ([AuditState]<> 1 and [ClassID] = ",
					base.Request["cid"].ToString(),
					" and [SystemName] LIKE '%",
					this.txtName.Text.Trim(),
					"%') order by SignDate desc"
				});
			}
			else
			{
				sqlString = string.Concat(new string[]
				{
					"SELECT [RecordID], [ClassID], [AuditState], [SystemType], [CorpCode], [SignDate], [SignMan], [SystemCode], [SystemName], [RecordDate], [UserCode],[IsCurrent], [Remark] FROM [OA_System_Info] WHERE ( [ClassID] = ",
					base.Request["cid"].ToString(),
					" and [SystemName] LIKE '%",
					this.txtName.Text.Trim(),
					"%') order by SignDate desc"
				});
				this.btnAudit.Width = 0;
			}
		}
		else
		{
			sqlString = "SELECT [RecordID], [ClassID], [AuditState], [SystemType], [CorpCode], [SignDate], [SignMan], [SystemCode], [SystemName], [RecordDate], [UserCode],[IsCurrent], [Remark] FROM [OA_System_Info] WHERE ([AuditState]<> 1 and [SystemName] LIKE '%" + this.txtName.Text.Trim() + "%' ) order by SignDate desc";
			if (base.Request["audit"] == null)
			{
				this.btnAudit.Width = 0;
				this.btnAdd.Width = 0;
			}
		}
		this.GVInOutMain.DataSource = publicDbOpClass.DataTableQuary(sqlString);
		this.GVInOutMain.DataBind();
	}
	protected void GVInOutMain_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"','",
				dataRowView["SystemName"],
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			string value = AduitAction.SetOkState(dataRowView["AuditState"].ToString()).Substring(3);
			e.Row.Cells[4].Style.Add("color", value);
			e.Row.Cells[4].Text = AduitAction.SetOkState(dataRowView["AuditState"].ToString()).Substring(0, 3);
			e.Row.Cells[5].Text = ((dataRowView["IsCurrent"].ToString() == "1") ? "是" : "否");
			HyperLink hyperLink = (HyperLink)e.Row.Cells[1].Controls[0];
			hyperLink.NavigateUrl = "#";
			hyperLink.NavigateUrl = string.Concat(new object[]
			{
				"javascript:void(window.open('SystemInfoAdd.aspx?t=View&rid=",
				dataRowView["RecordID"].ToString(),
				"&cd=",
				this.ClassID,
				"&ctc=",
				base.Request["ctc"].ToString(),
				"','','left=150,top=150,width=500,height=250,toolbar=no,status=yes,scrollbars=yes,resizable=no'));"
			});
			e.Row.Attributes["onDblClick"] = string.Concat(new object[]
			{
				"return openEdit('View','",
				this.ClassID,
				"','",
				base.Request["ctc"].ToString(),
				"');"
			});
			this.GVInOutMain.ToolTip = "请双击查看详细信息";
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.databind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.databind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.sia.Delete(new Guid(this.HdnRecordID.Value)) > 0)
		{
			this.JS.Text = "alert('删除成功!');";
			this.databind();
			return;
		}
		this.JS.Text = "alert('该制度已被使用，目前不能删除!');";
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.databind();
	}
	protected void btnAudit_Click(object sender, EventArgs e)
	{
		this.databind();
	}
	protected void BtnView_Click(object sender, EventArgs e)
	{
	}
	protected void GVInOutMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVInOutMain.PageIndex = e.NewPageIndex;
		this.databind();
	}
}


