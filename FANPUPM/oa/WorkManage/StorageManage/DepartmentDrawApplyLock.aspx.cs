using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_DepartmentDrawApplyLock : BasePage, IRequiresSessionState
{
	public OAOfficeResDepartmentApplicationAction amAction
	{
		get
		{
			return new OAOfficeResDepartmentApplicationAction();
		}
	}
	public OAOfficeResDepartmentApplicationDetailAction dada
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

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack && base.Request["ic"].ToString() != "")
		{
			this.InStorageID = new Guid(base.Request["ic"].ToString());
			this.EditDisplay();
			this.Bind();
		}
	}
	private void EditDisplay()
	{
		DataTable list = this.amAction.GetList("DARecordID='" + this.InStorageID + "'");
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.LbApplyDate.Text = ((dataRow["ApplyDate"].ToString().Trim() == "") ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(dataRow["ApplyDate"].ToString().Trim()).ToShortDateString());
			this.LbApplyPerson.Text = BooksCommonClass.GetUserName(dataRow["ApplyMan"].ToString());
			this.LbApplyDept.Text = BooksCommonClass.GetDepartmentName(int.Parse(dataRow["ApplyDepartment"].ToString()));
			this.LbRemark.Text = dataRow["Remark"].ToString();
		}
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.dada.GetList("a.DARecordID='" + this.InStorageID + "'");
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


