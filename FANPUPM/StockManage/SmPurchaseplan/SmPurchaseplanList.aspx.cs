using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
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
public partial class StockManage_SmPurchaseplan_SmPurchaseplanList : NBasePage, IRequiresSessionState
{
	private SmPurchaseplanBll smPurchaseplanBll = new SmPurchaseplanBll();
	private SmPurchaseplanStock smPurchaseplanStock = new SmPurchaseplanStock();
	private PTPrjInfoService prjSer = new PTPrjInfoService();
	private string prjId = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["PrjGuid"]))
		{
			this.prjId = base.Request["PrjGuid"].ToString();
		}
		this.gvPurchaseplan.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldPrjId.Value = this.prjId;
			PTPrjInfo byId = this.prjSer.GetById(this.prjId);
			this.lblProject.Text = byId.PrjName;
			this.BindGv();
		}
	}
	public void BindGv()
	{
		string strWhere = "WHERE Project='" + this.prjId + "'";
		this.BindGv(this.smPurchaseplanBll.GetListArray(strWhere));
	}
	public void BindGv(List<SmPurchaseplanModel> storageDataSource)
	{
		if (storageDataSource.Count == 0)
		{
			storageDataSource.Add(new SmPurchaseplanModel());
			this.gvPurchaseplan.DataSource = storageDataSource;
			this.gvPurchaseplan.DataBind();
			this.gvPurchaseplan.HeaderRow.Cells[0].Visible = false;
			this.gvPurchaseplan.Rows[0].Visible = false;
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
						this.smPurchaseplanStock.DeleteByWhere(sqlTransaction, " where ppcode='" + checkBox.ToolTip + "'");
						this.smPurchaseplanBll.Delete(sqlTransaction, checkBox.ToolTip);
					}
				}
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起添加失败！');");
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
		this.BindGv(this.smPurchaseplanBll.GetListByTimeAndNum(this.txtStartTime.Text, this.txtEndTime.Text, this.txtPPcode.Text.Trim(), this.txtPeople.Value, this.prjId));
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Values[1].ToString();
				e.Row.Attributes["prjGuid"] = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Values[2].ToString();
				e.Row.Attributes["auditContent"] = "物资采购计划：" + this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
}


