using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_CostVerifyEdit : NBasePage, IRequiresSessionState
{
	private string diaryId = string.Empty;
	private BudIndirectDiaryCostService diarySer = new BudIndirectDiaryCostService();
	private BudIndirectDiaryDetailsService detailSer = new BudIndirectDiaryDetailsService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["diaryId"]))
		{
			this.diaryId = base.Request["diaryId"];
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
	protected void gvwDiaryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwDiaryDetails.DataKeys[e.Row.RowIndex]["InDiaryDetailsId"].ToString();
		}
	}
	protected void btnOk_Click(object sender, System.EventArgs e)
	{
		this.SaveDiary();
		this.SaveDiaryDetails();
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_costverifylist' });");
	}
	private void InitPage()
	{
		this.InitDiary();
		this.BindDiaryDetails();
	}
	private void InitDiary()
	{
		BudIndirectDiaryCost byId = this.diarySer.GetById(this.diaryId);
		this.lblName.Text = byId.Name;
		this.lblInputDate.Text = byId.InputDate.ToString("yyyy-MM-dd");
		this.lblDepartment.Text = byId.Department;
		this.lblInputUser.Text = byId.InputUser;
		this.lblIssueBy.Text = byId.IssuedBy;
		this.lblCode.Text = byId.IndireCode;
		if (byId.CostType == "P")
		{
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId2 = pTPrjInfoService.GetById(byId.ProjectId);
			if (byId2 != null)
			{
				this.lblPrjName.Text = byId2.PrjName;
			}
		}
		else
		{
			this.ltlCostTypeName.Text = "组织机构";
			PTdbmService pTdbmService = new PTdbmService();
			PTdbm byId3 = pTdbmService.GetById(byId.ProjectId);
			if (byId3 != null)
			{
				this.lblPrjName.Text = byId3.V_bmqc;
			}
		}
		PCPettyCashService pCPettyCashService = new PCPettyCashService();
		PCPettyCash byId4 = pCPettyCashService.GetById(byId.PettyCashId);
		if (byId4 != null)
		{
			this.lblPettyCash.Text = byId4.Matter;
		}
	}
	private void BindDiaryDetails()
	{
		this.gvwDiaryDetails.DataSource = this.detailSer.GetByDearyId(this.diaryId);
		this.gvwDiaryDetails.DataBind();
	}
	private void SaveDiary()
	{
		BudIndirectDiaryCost byId = this.diarySer.GetById(this.diaryId);
		byId.IsAudit = true;
		byId.AuditDate = System.DateTime.Now;
		this.diarySer.Update(byId);
	}
	private void SaveDiaryDetails()
	{
		try
		{
			for (int i = 0; i < this.gvwDiaryDetails.Rows.Count; i++)
			{
				GridViewRow gridViewRow = this.gvwDiaryDetails.Rows[i];
				string inDiaryDetailsId = gridViewRow.Attributes["id"];
				BudIndirectDiaryDetails byId = this.detailSer.GetById(inDiaryDetailsId);
				TextBox textBox = gridViewRow.FindControl("txtAuditAmount") as TextBox;
				if (textBox != null && byId != null)
				{
					decimal auditAmount = System.Convert.ToDecimal(textBox.Text);
					byId.AuditAmount = auditAmount;
					this.detailSer.Update(byId);
				}
			}
		}
		catch
		{
		}
	}
}


