using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Allocation_AllocationManager : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		base.Response.Clear();
		if (!base.IsPostBack)
		{
			this.GVAllocationList.PageSize = NBasePage.pagesize;
			this.Bind_GVAllocationList();
		}
	}
	protected void Bind_GVAllocationList()
	{
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		DataTable allocationList = allocationBllAction.GetAllocationList(" 1 = 1 order by intime desc");
		if (allocationList != null && allocationList.Rows.Count > NBasePage.pagesize)
		{
			this.HdnIsPage.Value = "1";
		}
		Common2.BindGvTable(allocationList, this.GVAllocationList, false);
	}
	protected void GVAllocationList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				AllocationBllAction allocationBllAction = new AllocationBllAction();
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				e.Row.Cells[1].Text = (this.GVAllocationList.PageSize * this.GVAllocationList.PageIndex + e.Row.RowIndex + 1).ToString();
				HyperLink hyperLink = (HyperLink)e.Row.Cells[2].FindControl("HyperLink1");
				hyperLink.Text = string.Concat(new string[]
				{
					"<a class='al' href=javascript:viewopen('AuditPage.aspx?ic=",
					dataRowView["aid"].ToString(),
					"&BusiClass=001&BusiCode=075',820,500) >",
					dataRowView["acode"].ToString(),
					"</a>"
				});
				e.Row.Cells[3].Text = allocationBllAction.GetTreasuryNameByCode(dataRowView["tcodea"].ToString()).Rows[0][0].ToString();
				e.Row.Cells[4].Text = allocationBllAction.GetTreasuryNameByCode(dataRowView["tcodeb"].ToString()).Rows[0][0].ToString();
				e.Row.Attributes["id"] = dataRowView["acode"].ToString();
				e.Row.Attributes["guid"] = dataRowView["aid"].ToString();
				e.Row.Attributes["auditContent"] = "调拨单管理：" + this.GVAllocationList.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.GVAllocationList.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						allocationBllAction.Delete(sqlTransaction, checkBox.ToolTip);
					}
				}
				sqlTransaction.Commit();
				this.Bind_GVAllocationList();
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起删除失败！');");
			}
		}
	}
	protected void btnSertch_Click(object sender, EventArgs e)
	{
		string text = " acode like '%" + this.txtAllocationBill.Text.Trim() + "%'";
		if (!string.IsNullOrEmpty(this.txtAllocationDateStarts.Text.Trim()))
		{
			text = text + " and intime >='" + this.txtAllocationDateStarts.Text.Trim() + "'";
		}
		if (!string.IsNullOrEmpty(this.txtAllocationDateEnd.Text.Trim()))
		{
			text = text + " and intime < '" + Convert.ToDateTime(this.txtAllocationDateEnd.Text.Trim()).AddDays(1.0).ToString() + "'";
		}
		this.GVAllocationList.AllowPaging = true;
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		this.GVAllocationList.DataSource = allocationBllAction.GetAllocationList(text);
		this.GVAllocationList.DataBind();
	}
	protected void GVAllocationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVAllocationList.PageIndex = e.NewPageIndex;
		this.Bind_GVAllocationList();
	}
}


