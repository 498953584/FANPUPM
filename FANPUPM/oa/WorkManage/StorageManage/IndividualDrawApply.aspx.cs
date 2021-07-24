using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_IndividualDrawApply : BasePage, IRequiresSessionState
{
	private string strWhere = "";
	public OAOfficeResPersonalApplicationAction amAction
	{
		get
		{
			return new OAOfficeResPersonalApplicationAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该条记录吗?')) return false;";
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.amAction.GetList(" UserCode = '" + base.UserCode + "'");
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["PARecordID"].ToString(),
				"','",
				dataRowView["IsSubmit"].ToString(),
				"','",
				dataRowView["UseMan"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Cells[2].Text = BooksCommonClass.GetUserName(dataRowView["ApplyMan"].ToString());
			e.Row.Cells[3].Text = BooksCommonClass.GetUserName(dataRowView["UseMan"].ToString());
			string a;
			if ((a = dataRowView["IsSubmit"].ToString()) != null)
			{
				if (a == "0")
				{
					e.Row.Cells[4].Text = "未提交";
					return;
				}
				if (!(a == "1"))
				{
					return;
				}
				e.Row.Cells[4].Text = "已提交";
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
		if (this.amAction.Delete(int.Parse(this.HdnInStorageID.Value)) > 0)
		{
			this.HdnInStorageID.Value = "0";
			this.Bind();
		}
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		if (this.amAction.UpdateSubmit(int.Parse(this.HdnInStorageID.Value)) > 0)
		{
			this.HdnInStorageID.Value = "0";
			this.Bind();
			this.Page.RegisterStartupScript("", "<script>alert('提交办理成功!');</script>");
		}
	}
}


