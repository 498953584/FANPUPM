using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_SubCompanyManage_PersonDrawLock : BasePage, IRequiresSessionState
{

	public OAOfficeResApplicationCollectAction amAction
	{
		get
		{
			return new OAOfficeResApplicationCollectAction();
		}
	}
	public OAOfficeResApplicationCollectDetailAction acda
	{
		get
		{
			return new OAOfficeResApplicationCollectDetailAction();
		}
	}
	public Guid RecordID
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (Guid)this.ViewState["RECORDID"];
			}
			return Guid.NewGuid();
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack && base.Request["ic"].ToString() != "")
		{
			this.RecordID = new Guid(base.Request["ic"].ToString());
			this.EditDisplay();
			this.Bind();
		}
	}
	private void EditDisplay()
	{
		DataTable list = this.amAction.GetList("ACRecordID='" + this.RecordID + "'");
		if (list.Rows.Count > 0)
		{
			userManageDb userManageDb = new userManageDb();
			DataRow dataRow = list.Rows[0];
			this.lbUserName.Text = userManageDb.GetUserName(dataRow["UserCode"].ToString());
			this.lbApplyType.Text = ((dataRow["ApplyType"].ToString() == "P") ? "个人" : "部门");
			this.lbYear.Text = dataRow["iYear"].ToString();
			this.lbMonth.Text = dataRow["iMonth"].ToString();
			this.lbRemark.Text = dataRow["Remark"].ToString();
		}
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.acda.GetList("a.ACRecordID='" + this.RecordID + "'");
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
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
}


