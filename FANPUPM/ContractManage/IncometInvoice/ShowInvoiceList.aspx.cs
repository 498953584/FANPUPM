using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.DAL;
using cn.justwin.Web;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometInvoice_ShowInvoiceList : NBasePage, IRequiresSessionState
{
	private IncometContractBll incometContractBll = new IncometContractBll();
	private IncometInvoiceBll incometInvoiceBll = new IncometInvoiceBll();

	protected override void OnInit(EventArgs e)
	{
		this.gvConract.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdContractID.Value = base.Request.QueryString["ContractID"];
			this.hldfIsExamineApprove.Value = ConfigHelper.Get("IsIncomeContractApprove");
			this.BindGv();
		}
	}
	public void BindGv()
	{
		if (this.hldfIsExamineApprove.Value == "0")
		{
			this.BindGv(this.incometContractBll.GetInfoByParam(this.hdContractID.Value, "Con_Incomet_Invoice", base.UserCode, ""));
			return;
		}
		this.BindGv(this.incometContractBll.GetInfoByParam(this.hdContractID.Value, "Con_Incomet_Invoice", base.UserCode, " AND p1.FlowState=1 "));
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvConract.DataSource = storageDataSource;
			this.gvConract.DataBind();
			this.gvConract.HeaderRow.Cells[0].Visible = false;
			this.gvConract.Rows[0].Visible = false;
			return;
		}
		this.gvConract.DataSource = storageDataSource;
		this.gvConract.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvConract.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						int num = this.incometInvoiceBll.Delete(sqlTransaction, checkBox.ToolTip);
						if (num > 0)
						{
							base.RegisterScript("alert('系统提示：\\n\\n数据删除成功！');");
							base.RegisterScript("window.location = window.location");
						}
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
		this.gvConract.PageIndex = e.NewPageIndex;
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
				e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
}


