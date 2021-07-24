using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_DepartmentDrawApplyAssTab : BasePage, IRequiresSessionState
{
	public OAOfficeResDepartmentApplicationDetailAction amAction
	{
		get
		{
			return new OAOfficeResDepartmentApplicationDetailAction();
		}
	}
	public Guid InStorageID
	{
		get
		{
			object obj = this.ViewState["INSTORAGEID"];
			if (obj != null)
			{
				return (Guid)this.ViewState["INSTORAGEID"];
			}
			return Guid.NewGuid();
		}
		set
		{
			this.ViewState["INSTORAGEID"] = value;
		}
	}
	public int AuditState
	{
		get
		{
			object obj = this.ViewState["AUDITSTATE"];
			if (obj != null)
			{
				return (int)this.ViewState["AUDITSTATE"];
			}
			return -1;
		}
		set
		{
			this.ViewState["AUDITSTATE"] = value;
		}
	}
	public int DepartmentID
	{
		get
		{
			object obj = this.ViewState["DEPARTMENTID"];
			if (obj != null)
			{
				return (int)this.ViewState["DEPARTMENTID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["DEPARTMENTID"] = value;
		}
	}
	public string strWhere
	{
		get
		{
			object obj = this.ViewState["STRWHERE"];
			if (obj != null)
			{
				return (string)this.ViewState["STRWHERE"];
			}
			return "";
		}
		set
		{
			this.ViewState["STRWHERE"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["id"] == null || base.Request["ia"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;</script>");
			return;
		}
		if (base.Request["id"].ToString() != "")
		{
			this.InStorageID = new Guid(base.Request["id"].ToString());
			this.HdnInStorageID.Value = this.InStorageID.ToString();
		}
		if (base.Request["ia"].ToString() != "")
		{
			this.AuditState = int.Parse(base.Request["ia"].ToString());
		}
		if (base.Request["dm"].ToString() != "")
		{
			this.DepartmentID = int.Parse(base.Request["dm"].ToString());
			this.HdnDepartmentID.Value = this.DepartmentID.ToString();
		}
		if (!this.Page.IsPostBack)
		{
			if (this.AuditState == -1)
			{
				this.btnAdd.Enabled = true;
			}
			this.DisplayDepartmentRation();
			this.Bind();
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该项吗?')) return false;";
	}
	private void DisplayDepartmentRation()
	{
		decimal d = OAOfficeCommonClas.GetDepartmentRation(this.DepartmentID.ToString());
		this.LblMonth.Text = d.ToString("f2");
		d *= 12m;
		this.LblRation.Text = d.ToString("f2");
		this.LbAlready.Text = OAOfficeCommonClas.GetAlready(this.DepartmentID.ToString()).ToString("f2");
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.amAction.GetList("a.DARecordID='" + this.InStorageID + "'");
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["DADRecordID"].ToString(),
				"','",
				dataRowView["DARecordID"].ToString(),
				"','",
				this.AuditState,
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			string a;
			if ((a = dataRowView["UseType"].ToString()) != null)
			{
				if (a == "0")
				{
					e.Row.Cells[2].Text = "不回收";
					return;
				}
				if (!(a == "1"))
				{
					return;
				}
				e.Row.Cells[2].Text = "以旧换新";
			}
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.amAction.Delete(int.Parse(this.HdnRecordID.Value)) > 0)
		{
			this.HdnRecordID.Value = "-1";
			this.Bind();
		}
	}
}


