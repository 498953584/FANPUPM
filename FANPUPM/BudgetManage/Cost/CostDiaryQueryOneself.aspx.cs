using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Cost_CostDiaryQueryOneself : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();
	private PCPettyCashService pcSer = new PCPettyCashService();
	private BudIndirectDiaryDetails indirDetail = new BudIndirectDiaryDetails();
	private BudIndirectDiaryCostService budInCostSer = new BudIndirectDiaryCostService();
	private BudIndirectDiaryDetailsService indirDetailSer = new BudIndirectDiaryDetailsService();
	private string prjId = string.Empty;
	private string year = string.Empty;
	private string costType = "P";

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		if (this.year == "zzjg")
		{
			this.costType = "O";
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindPrjName();
		this.BindGv();
	}
	public void BindGv()
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		string person = this.txtPerson.Text.Trim();
		string userName = string.Empty;
		if (base.UserCode != "00000000")
		{
			userName = WebUtil.GetUserNames(base.UserCode);
		}
		string text = this.txtStartDate.Text;
		string text2 = this.txtEndDate.Text;
		string deparment = this.txtDeparment.Text.Trim();
		string name = this.txtName.Text.Trim();
		decimal? totalAmount = null;
		decimal? endCash = null;
		if (!string.IsNullOrEmpty(this.txtTotalAmount.Text))
		{
			totalAmount = new decimal?(System.Convert.ToDecimal(this.txtTotalAmount.Text));
		}
		if (!string.IsNullOrEmpty(this.txtEndCash.Text))
		{
			endCash = new decimal?(System.Convert.ToDecimal(this.txtEndCash.Text));
		}
		int count = CostDiary.GetCount(this.prjId, person, text, text2, userName, deparment, this.txtName.Text, this.ddlFlowState.SelectedValue, totalAmount, endCash, this.costType);
		DataTable diaries = CostDiary.GetDiaries(this.prjId, person, text, text2, userName, deparment, this.txtName.Text, this.ddlFlowState.SelectedValue, totalAmount, endCash, this.costType, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.AspNetPager1.RecordCount = count;
		this.gvBudget.DataSource = diaries;
		this.gvBudget.DataBind();
		this.hfldPurchaseChecked.Value = string.Empty;
		if (this.gvBudget.Rows.Count > 0)
		{
			base.RegisterScript("fillTotalAmount('" + CostDiary.GetDiariesTotal(this.prjId, person, userName, name, this.ddlFlowState.SelectedValue, text, text2, deparment, this.costType, totalAmount, endCash).ToString("#,##0.000") + "');showDetails();");
			return;
		}
		base.RegisterScript("showDetails();");
	}
	protected void ClearSearchCondition()
	{
		this.txtPerson.Text = string.Empty;
		this.txtStartDate.Text = string.Empty;
		this.txtEndDate.Text = string.Empty;
		this.txtDeparment.Text = string.Empty;
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex]["InDiaryId"].ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex]["InDiaryId"].ToString();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	private void BindPrjName()
	{
		if (this.year != "zzjg")
		{
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
			if (byId != null)
			{
				this.hfldPrjName.Value = byId.PrjName;
				return;
			}
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId2 = pTPrjInfoZTBService.GetById(this.prjId);
			if (byId2 != null)
			{
				this.hfldPrjName.Value = byId2.PrjName;
			}
		}
	}
	public decimal GetAmountSum(string Id)
	{
		return (
			from p in this.indirDetailSer
			where p.InDiaryId == Id
			select p.Amount).ToList<decimal>().Sum();
	}
	public decimal GetAuditAmountSum(string Id)
	{
		return (
			from p in this.indirDetailSer
			where p.InDiaryId == Id
			select p.AuditAmount).ToList<decimal>().Sum();
	}
	public string IsAudit(string Id)
	{
		BudIndirectDiaryCost byId = this.budInCostSer.GetById(Id);
		if (byId == null)
		{
			return string.Empty;
		}
		if (!byId.IsAudit)
		{
			return "未核销";
		}
		return "已核销";
	}
}


