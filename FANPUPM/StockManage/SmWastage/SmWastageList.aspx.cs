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
using Wuqi.Webdiyer;
public partial class StockManage_SmWastage_SmWastageList : NBasePage, IRequiresSessionState
{
	private readonly SmWastageBll smWastageBll = new SmWastageBll();
	private readonly SmWastageStockBll smWastageStockBll = new SmWastageStockBll();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindGv();
		}
	}
	protected void BindGv()
	{
		this.AspNetPager1.RecordCount = this.smWastageBll.GetListCountByParm(this.txtWastageCode.Text.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtPeople.Value.Trim(), this.txtTrea.Text.Trim(), "");
		DataTable listByParm = this.smWastageBll.GetListByParm(this.txtWastageCode.Text.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtPeople.Value.Trim(), this.txtTrea.Text.Trim(), "", this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvWastage.DataSource = listByParm;
		this.gvWastage.DataBind();
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void gvWastage_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvWastage.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.gvWastage.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvWastage.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						this.smWastageStockBll.DeleteByWhere(sqlTransaction, " where WastageCode='" + checkBox.ToolTip + "'");
						this.smWastageBll.Delete(sqlTransaction, checkBox.ToolTip);
					}
				}
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch (System.Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起删除时出现异常！');");
			}
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


