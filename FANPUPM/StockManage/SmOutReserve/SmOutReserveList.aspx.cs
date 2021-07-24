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
public partial class StockManage_SmOutReserve_SmOutReserveList : NBasePage, IRequiresSessionState
{
	private OutReserveBll outReserveBll = new OutReserveBll();
	private OutStockBll outStockBll = new OutStockBll();
	private string prjId = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		this.gvOutReserve.PageSize = NBasePage.pagesize;
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
			strWhere = " and procode='" + this.prjId + "'";
		}
		else
		{
			strWhere = " and procode is null ";
		}
		this.BindGv(this.outReserveBll.GetListByParm(this.txtPPcode.Text.Trim(), this.txtStartTime.Text, this.txtEndTime.Text, this.txtPeople.Value.Trim(), "", this.txtTrea.Text.Trim(), strWhere));
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvOutReserve.DataSource = storageDataSource;
			this.gvOutReserve.DataBind();
			this.gvOutReserve.HeaderRow.Cells[0].Visible = false;
			this.gvOutReserve.Rows[0].Visible = false;
			return;
		}
		this.gvOutReserve.DataSource = storageDataSource;
		this.gvOutReserve.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvOutReserve.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						this.outStockBll.DeleteByWhere(sqlTransaction, " where orcode='" + checkBox.ToolTip + "'");
						this.outReserveBll.Delete(sqlTransaction, checkBox.ToolTip);
					}
				}
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起删除时出现异常！');");
			}
		}
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvOutReserve.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv(this.outReserveBll.GetListByParm(this.txtPPcode.Text.Trim(), this.txtStartTime.Text, this.txtEndTime.Text, this.txtPeople.Value.Trim(), this.prjId, this.txtTrea.Text.Trim(), ""));
	}
	protected void gvOutReserve_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvOutReserve.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.gvOutReserve.DataKeys[e.Row.RowIndex].Values[1].ToString();
				e.Row.Attributes["prjGuid"] = this.gvOutReserve.DataKeys[e.Row.RowIndex].Values[2].ToString();
				e.Row.Attributes["auditContent"] = "出库管理 ：" + this.gvOutReserve.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
	protected void btndd_Click(object sender, EventArgs e)
	{
	}
}


