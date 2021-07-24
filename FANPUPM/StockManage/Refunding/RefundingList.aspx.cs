using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Refunding_RefundingList : NBasePage, IRequiresSessionState
{
	private RefundingBll refundingBll = new RefundingBll();
	private BackStockBll backStockBll = new BackStockBll();
	private string prjId = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		this.gvRefunding.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["prjGuid"]))
		{
			this.prjId = base.Request["prjGuid"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			this.lblProject.Text = pTPrjInfoService.GetById(this.prjId).PrjName;
		}
	}
	public void BindGv()
	{
		string strWhere = string.Empty;
		if (!string.IsNullOrEmpty(this.prjId))
		{
			strWhere = " and p1.procode='" + this.prjId + "'";
		}
		else
		{
			strWhere = " and p1.procode is null ";
		}
		this.BindGv(this.refundingBll.GetListByTimeAndNum(this.txtStartTime.Text, this.txtEndTime.Text, this.txtPPcode.Text.Trim(), this.txtPeople.Value.Trim(), "", this.txtTrea.Text.Trim(), strWhere));
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvRefunding.DataSource = storageDataSource;
			this.gvRefunding.DataBind();
			this.gvRefunding.HeaderRow.Cells[0].Visible = false;
			this.gvRefunding.Rows[0].Visible = false;
			return;
		}
		this.gvRefunding.DataSource = storageDataSource;
		this.gvRefunding.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvRefunding.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						this.backStockBll.DeleteByWhere(sqlTransaction, " where rcode='" + checkBox.ToolTip + "'");
						this.refundingBll.Delete(sqlTransaction, checkBox.ToolTip);
					}
				}
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起删除失败！');");
			}
		}
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvRefunding.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvRefunding.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.gvRefunding.DataKeys[e.Row.RowIndex].Values[1].ToString();
				e.Row.Attributes["prjGuid"] = this.gvRefunding.DataKeys[e.Row.RowIndex].Values[2].ToString();
				e.Row.Attributes["auditContent"] = "退库管理 ：" + this.gvRefunding.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
}


