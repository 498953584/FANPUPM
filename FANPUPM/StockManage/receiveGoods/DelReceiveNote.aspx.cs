using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_receiveGoods_DelReceiveNote : NBasePage, IRequiresSessionState
{
	private PtYhmcBll yhmc = new PtYhmcBll();
	private receiveNoteBLL receiveNote = new receiveNoteBLL();
	private OutStockBll outStockBll = new OutStockBll();
	private OutReserveBll outReserveBll = new OutReserveBll();
	private receiveGoodsBLL receiveGoods = new receiveGoodsBLL();
	private sendNoteBll sendNote = new sendNoteBll();
	private string prjId = string.Empty;
	private readonly cn.justwin.stockBLL.Storage storage = new cn.justwin.stockBLL.Storage();

	protected override void OnInit(EventArgs e)
	{
		this.gvPurchaseplan.PageSize = NBasePage.pagesize;
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
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			this.lblProject.Text = pTPrjInfoService.GetById(this.prjId).PrjName;
		}
	}
	public void BindGv()
	{
		this.BindGv(this.receiveNote.GetListByTimeAndNum(this.txtStartTime.Text, this.txtEndTime.Text, this.txtrnCode.Text.Trim(), this.txtPeople.Value, this.prjId));
	}
	public void BindGv(List<sm_receiveNote> storageDataSource)
	{
		if (storageDataSource.Count == 0)
		{
			storageDataSource.Add(new sm_receiveNote());
			this.gvPurchaseplan.DataSource = null;
			this.gvPurchaseplan.DataBind();
			return;
		}
		this.gvPurchaseplan.DataSource = storageDataSource;
		this.gvPurchaseplan.DataBind();
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvPurchaseplan.PageIndex = e.NewPageIndex;
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
				e.Row.Attributes["id"] = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Values[1].ToString();
				Label label = e.Row.FindControl("labUser") as Label;
				label.Text = this.yhmc.GetModelById(label.Text.ToString()).v_xm;
			}
			catch
			{
			}
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			List<string> list = new List<string>();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvPurchaseplan.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						sm_receiveNote modelByrnId = this.receiveNote.GetModelByrnId(checkBox.ToolTip);
						OutReserveModel modelByIc = this.outReserveBll.GetModelByIc(modelByrnId.soId);
						if (modelByIc != null)
						{
							this.outStockBll.DeleteByWhere(sqlTransaction, " where orcode ='" + modelByIc.orcode + "'");
							this.outReserveBll.Delete(sqlTransaction, modelByIc.orcode);
						}
						StorageModel modelBySid = this.storage.GetModelBySid(modelByrnId.stId);
						if (modelBySid != null)
						{
							list.Add(modelBySid.scode);
						}
						if (!string.IsNullOrEmpty(modelByrnId.SAllocationId))
						{
							AllocationBllAction allocationBllAction = new AllocationBllAction();
							AllocationModel allocationModel = new AllocationModel();
							allocationModel = allocationBllAction.ReturnAllocatonModel(" aid='" + modelByrnId.SAllocationId + "'");
							if (allocationModel != null)
							{
								allocationBllAction.DelAllocationStockByAcode(sqlTransaction, allocationModel.Acode);
								allocationBllAction.Delete(sqlTransaction, allocationModel.Acode);
							}
						}
						this.receiveGoods.Delete(sqlTransaction, modelByrnId.rnId);
						this.receiveNote.Delete(sqlTransaction, modelByrnId.rnId);
						this.sendNote.UpdateStateNo(sqlTransaction, modelByrnId.snId);
					}
				}
				if (list.Count != 0)
				{
					this.storage.DelByTrans(sqlTransaction, list);
				}
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起撤销失败！');");
			}
		}
	}
}


