using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_BlocManage_SubCompanyDepartmentDraw : BasePage, IRequiresSessionState
{

	public OAOfficeResDepartmentApplicationDetailAction amAction
	{
		get
		{
			return new OAOfficeResDepartmentApplicationDetailAction();
		}
	}
	public Guid GRecordID
	{
		get
		{
			object obj = this.ViewState["GRecordID"];
			if (obj != null)
			{
				return (Guid)this.ViewState["GRecordID"];
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["GRecordID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["id"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;</script>");
			return;
		}
		if (base.Request["id"].ToString() != "")
		{
			this.GRecordID = new Guid(base.Request["id"].ToString());
		}
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.amAction.GetList("a.DARecordID='" + this.GRecordID + "'");
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
}


