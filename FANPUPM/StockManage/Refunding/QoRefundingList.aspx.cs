using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Refunding_QoRefundingList : NBasePage, IRequiresSessionState
{
	private RefundingBll refundingBll = new RefundingBll();
	private BackStockBll backStockBll = new BackStockBll();
	private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();
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
		}
	}
	public void BindGv()
	{
		string str = string.Empty;
		if (!string.IsNullOrEmpty(this.prjId))
		{
			str = " and procode='" + this.prjId + "'";
		}
		else
		{
			str = " and procode is null ";
		}
		this.BindGv(this.refundingBll.GetListByTimeAndNum(this.txtStartTime.Text, this.txtEndTime.Text, this.txtPPcode.Text.Trim(), this.txtPeople.Value.Trim(), "", this.hfldTrea.Value, " and flowstate=1 and isin='false' " + str));
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
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvRefunding.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void btnOk_Click(object sender, EventArgs e)
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
						List<BackStockModel> listArray = this.backStockBll.GetListArray(" where rcode='" + checkBox.ToolTip + "'");
						RefundingModel model = this.refundingBll.GetModel(checkBox.ToolTip);
						foreach (BackStockModel current in listArray)
						{
							TreasuryStockModel treasuryStockModel = new TreasuryStockModel();
							treasuryStockModel.corp = current.corp;
							treasuryStockModel.incode = current.rcode;
							treasuryStockModel.intime = model.intime;
							treasuryStockModel.intype = 0;
							treasuryStockModel.isfirst = false;
							treasuryStockModel.scode = current.scode;
							treasuryStockModel.snumber = current.number;
							treasuryStockModel.sprice = current.sprice;
							treasuryStockModel.tcode = model.tcode;
							treasuryStockModel.tsid = Guid.NewGuid().ToString();
							treasuryStockModel.Type = "B";
							this.treasuryStockBll.Add(sqlTransaction, treasuryStockModel);
						}
						RefundingModel model2 = this.refundingBll.GetModel(checkBox.ToolTip);
						model2.isin = true;
						model2.IsInTime = DateTime.Now;
						this.refundingBll.Update(sqlTransaction, model2);
					}
				}
				sqlTransaction.Commit();
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n退库出现异常,退库失败！');");
			}
		}
		base.RegisterScript("location='QoRefundingList.aspx?prjGuid=" + base.Request["prjGuid"].ToString() + "'");
	}
	protected void gvRefunding_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvRefunding.DataKeys[e.Row.RowIndex]["rcode"].ToString();
		}
	}
}


