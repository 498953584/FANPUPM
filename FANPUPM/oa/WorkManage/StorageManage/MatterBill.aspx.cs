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
public partial class oa_WorkManage_StorageManage_MatterBill : BasePage, IRequiresSessionState
{

	public ptOfficeResResourcesAction ofAction
	{
		get
		{
			return new ptOfficeResResourcesAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (base.Request["cc"] != null && base.Request["cc"].ToString() != "")
		{
			this.btnAdd.Enabled = true;
			this.HdnTypeCode.Value = base.Request["cc"].ToString();
		}
		bool arg_7A_0 = this.Page.IsPostBack;
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该项吗?')) return false;";
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["RecordID"].ToString() + "');";
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			string a;
			if ((a = dataRowView["UseType"].ToString()) != null)
			{
				if (!(a == "0"))
				{
					if (a == "1")
					{
						e.Row.Cells[2].Text = "以旧换新";
					}
				}
				else
				{
					e.Row.Cells[2].Text = "不回收";
				}
			}
			string a2;
			if ((a2 = dataRowView["GetType"].ToString()) != null)
			{
				if (a2 == "0")
				{
					e.Row.Cells[3].Text = "个人领用";
					return;
				}
				if (!(a2 == "1"))
				{
					return;
				}
				e.Row.Cells[3].Text = "部门公共";
			}
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVBook.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVBook.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.ofAction.Delete(int.Parse(this.HdnRecordID.Value)) > 0)
		{
			this.GVBook.DataBind();
		}
	}
}


