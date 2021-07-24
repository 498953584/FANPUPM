using ASP;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.common.baseclass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_CostVerifyRecord2 : NBasePage, IRequiresSessionState
{
	private PTdbmService bmSer = new PTdbmService();
	private BudIndirectDiaryCostService IndiCostSer = new BudIndirectDiaryCostService();
	private BudIndirectDiaryCost indirDiaryCost = new BudIndirectDiaryCost();
	private BudIndirectDiaryDetailsService IndiDetailSer = new BudIndirectDiaryDetailsService();
	private BudIndirectDiaryDetails indirDetail = new BudIndirectDiaryDetails();
	private BasicSerialNumberService ser = new BasicSerialNumberService();
	private string ic = string.Empty;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitDepartment();
			this.GetDiaryDetail(0, "");
			this.InitRptAudit();
			this.GetNamePath(base.UserCode);
		}
	}
	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.ic = base.Request["ic"];
		}
		base.OnInit(e);
	}
	protected string GetDiaryDetail(int i, string t)
	{
		BudIndirectDiaryCost byId = this.IndiCostSer.GetById(this.ic);
		if (byId.CostType == "P")
		{
			this.lblProjectCode.Text = this.GetPrjCode(byId.ProjectId);
		}
		else
		{
			this.lblProjectCode.Text = this.GetDepFullName(byId.ProjectId);
		}
		this.lblPayMentDate.Text = System.DateTime.Now.ToShortDateString();
		this.lblDepartMent.Text = byId.Department;
		this.lblOperator.Text = byId.InputUser;
		this.lblTotalMoney.Text = System.Convert.ToString(this.GetAmountSum());
		this.lblFileNumber.Text = byId.IndireCode;
		System.Collections.Generic.IList<BudIndirectDiaryDetails> byDearyId = this.IndiDetailSer.GetByDearyId(this.ic);
		try
		{
			if (t == "name")
			{
				string result = byDearyId[i].Name;
				return result;
			}
			if (t == "amount")
			{
				string result = byDearyId[i].Amount.ToString();
				return result;
			}
		}
		catch
		{
		}
		return string.Empty;
	}
	public decimal GetAmountSum()
	{
		System.Collections.Generic.IList<BudIndirectDiaryDetails> byDearyId = this.IndiDetailSer.GetByDearyId(this.ic);
		decimal num = 0m;
		int num2 = 0;
		while (num2 < byDearyId.Count && num2 <= 4)
		{
			num += byDearyId[num2].Amount;
			num2++;
		}
		return num;
	}
	public void InitDepartment()
	{
		PTdbm byId = this.bmSer.GetById(1);
		this.lblTitle.Text = byId.V_BMMC + "预报销明细";
	}
	public string GetPrjCode(string prjId)
	{
		string result = string.Empty;
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(prjId);
		if (byId != null)
		{
			result = byId.PrjCode;
		}
		else
		{
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId2 = pTPrjInfoZTBService.GetById(prjId);
			if (byId2 != null)
			{
				result = byId2.PrjCode;
			}
		}
		return result;
	}
	private string GetDepFullName(string depid)
	{
		PTdbmService pTdbmService = new PTdbmService();
		PTdbm byId = pTdbmService.GetById(depid);
		if (byId != null)
		{
			return byId.V_bmqc;
		}
		return string.Empty;
	}
	public void InitRptAudit()
	{
		WFInstanceMainService wFInstanceMainService = new WFInstanceMainService();
		WFInstanceService source = new WFInstanceService();
		System.Collections.Generic.IList<WFInstanceMain> byInstanceCode = wFInstanceMainService.GetByInstanceCode(this.ic);
		System.Collections.Generic.List<WFInstance> list = new System.Collections.Generic.List<WFInstance>();
		foreach (WFInstanceMain im in byInstanceCode)
		{
			System.Collections.Generic.List<WFInstance> list2 = (
				from i in source
				where i.ID == (int?)im.ID && i.AuditResult == (int?)1
				orderby i.AuditDate descending
				select i).ToList<WFInstance>();
			foreach (WFInstance current in list2)
			{
				if ((im.ID == byInstanceCode.Last<WFInstanceMain>().ID || current.Sing.Value != -1) && !string.IsNullOrEmpty(this.GetNamePath(current.Operator)))
				{
					list.Add(current);
				}
			}
		}
		if (list.Count > 0)
		{
			this.rptAudit.DataSource = list;
			this.rptAudit.DataBind();
		}
	}
	public string GetNamePath(object userCode)
	{
		string result = string.Empty;
		PTLOGINService pTLOGINService = new PTLOGINService();
		PTLOGIN byUserCode = pTLOGINService.GetByUserCode(userCode.ToString());
		if (byUserCode != null && !string.IsNullOrEmpty(byUserCode.AuditNameImagePath))
		{
			result = "../.." + byUserCode.AuditNameImagePath;
		}
		return result;
	}
}


