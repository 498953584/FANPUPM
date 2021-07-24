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
public partial class StockManage_sendGoods_SendNoteList : NBasePage, IRequiresSessionState
{
	private SmPurchaseplanBll smPurchaseplanBll = new SmPurchaseplanBll();
	private SmPurchaseplanStock smPurchaseplanStock = new SmPurchaseplanStock();
	private sendNoteBll sendNote = new sendNoteBll();
	private sendGoodsBLL sendGoods = new sendGoodsBLL();
	private PtYhmcBll yhmc = new PtYhmcBll();
	private string prjId = string.Empty;

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
			this.hdfState.Value = "已验收";
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
			strWhere = " where prjCode='" + this.prjId + "'";
		}
		else
		{
			strWhere = " where prjCode is null ";
		}
		this.BindGv(this.sendNote.GetListArray(strWhere));
	}
	public void BindGv(List<SendNodteModel> storageDataSource)
	{
		if (storageDataSource.Count == 0)
		{
			storageDataSource.Add(new SendNodteModel());
			this.gvPurchaseplan.DataSource = null;
			this.gvPurchaseplan.DataBind();
			return;
		}
		this.gvPurchaseplan.DataSource = storageDataSource;
		this.gvPurchaseplan.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvPurchaseplan.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						this.sendNote.DeleteBysnId(sqlTransaction, checkBox.ToolTip);
						this.sendGoods.DeleteBysnId(sqlTransaction, checkBox.ToolTip);
					}
				}
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
			}
		}
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvPurchaseplan.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv(this.sendNote.GetListByTimeAndNum1(this.txtStartTime.Text, this.txtEndTime.Text, this.txtsnCode.Text.Trim(), this.txtPeople.Value, this.prjId));
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Values[1].ToString();
				e.Row.Attributes["prjGuid"] = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Values[2].ToString();
				Label label = e.Row.FindControl("labUser") as Label;
				label.Text = this.yhmc.GetModelById(label.Text.ToString()).v_xm;
				((Label)e.Row.Cells[4].FindControl("labState")).Text.ToString();
			}
			catch
			{
			}
		}
	}
}


