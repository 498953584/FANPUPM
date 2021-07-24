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
public partial class oa_WorkManage_StorageManage_StorageInitializtion : BasePage, IRequiresSessionState
{

	public ptOfficeResStockAction ofAction
	{
		get
		{
			return new ptOfficeResStockAction();
		}
	}
	public int DepotID
	{
		get
		{
			object obj = this.ViewState["DepotID"];
			if (obj != null)
			{
				return (int)this.ViewState["DepotID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["DepotID"] = value;
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
		base.Response.Cache.SetNoStore();
		if (base.Request["dd"] != null && base.Request["dd"].ToString() != "")
		{
			this.btnAdd.Enabled = true;
			this.DepotID = int.Parse(base.Request["dd"].ToString());
		}
		if (!this.Page.IsPostBack)
		{
			this.strWhere = "a.DepotID='" + this.DepotID.ToString() + "'";
			this.Bind(this.strWhere);
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add','" + this.DepotID.ToString() + "')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd','" + this.DepotID.ToString() + "')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该项吗?')) return false;";
	}
	private void Bind(string strWhere)
	{
		this.GVBook.DataSource = this.ofAction.GetList(strWhere);
		this.GVBook.DataBind();
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
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind(this.strWhere);
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.Bind(this.strWhere);
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.Bind(this.strWhere);
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.ofAction.Delete(int.Parse(this.HdnRecordID.Value)) > 0)
		{
			this.Bind(this.strWhere);
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.strWhere = " b.TypeCode='" + this.DDLMatterType.SelectedValue + "' ";
		this.strWhere = this.strWhere + " and b.ResName='" + this.txtSearch.Text.Trim() + "' ";
		this.Bind(this.strWhere);
	}
}


