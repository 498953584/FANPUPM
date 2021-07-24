using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometBalance_ShowBalanceList : NBasePage, IRequiresSessionState
{
	private IncometContractBll incometContractBll = new IncometContractBll();
	private IncometBalanceBll incometBalanceBll = new IncometBalanceBll();
	private BudContractConsReportService budContractRptSev = new BudContractConsReportService();
	private ConBalanceItemService conBalItemSev = new ConBalanceItemService();

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
			this.BindGv(this.incometContractBll.GetInfoByParam(this.hdContractID.Value, "Con_Incomet_Balance", base.UserCode, ""));
			return;
		}
		this.BindGv(this.incometContractBll.GetInfoByParam(this.hdContractID.Value, "Con_Incomet_Balance", base.UserCode, " AND  FlowState=1 "));
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
						this.DeleteRalationBud(checkBox.ToolTip);
						this.conBalItemSev.DeleteByBalanceId(checkBox.ToolTip);
						int num = this.incometBalanceBll.Delete(sqlTransaction, checkBox.ToolTip);
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
	public void DeleteRalationBud(string balanceId)
	{
		StringBuilder stringBuilder = new StringBuilder();
		BudContractConsReport byBalanceIdAndContractIDAndType = this.budContractRptSev.getByBalanceIdAndContractIDAndType(balanceId, this.hdContractID.Value, "1");
		if (byBalanceIdAndContractIDAndType != null)
		{
			stringBuilder.Append("DELETE Bud_ContractConsTask WHERE rptId='" + byBalanceIdAndContractIDAndType.RptId + "' ");
			this.budContractRptSev.Delete(byBalanceIdAndContractIDAndType);
		}
		stringBuilder.Append(string.Concat(new string[]
		{
			" Update Bud_ContractConsReport SET BalanceId=NULL WHERE BalanceId='",
			balanceId,
			"' AND contractId='",
			this.hdContractID.Value,
			"' AND Type='0' "
		}));
		this.budContractRptSev.ExcuteSql(stringBuilder.ToString());
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


