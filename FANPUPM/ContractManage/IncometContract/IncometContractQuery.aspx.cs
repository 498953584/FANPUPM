using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain;
using cn.justwin.stockBLL;
using cn.justwin.Tender;
using cn.justwin.Web;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometContract_IncometContractQuery : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private static string contractID = string.Empty;
	private string ic = string.Empty;
	private string wantplanCode = string.Empty;
	private MaterialWantPlan materialWant = new MaterialWantPlan();
	private MeterialPlanStock materialStock = new MeterialPlanStock();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private PTPrjInfoBll prjInfoBll = new PTPrjInfoBll();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			ContractManage_IncometContract_IncometContractQuery.contractID = base.Request["ic"];
		}
		if (!string.IsNullOrEmpty(base.Request["ContractId"]))
		{
			if (base.Request["ContractId"].Contains("["))
			{
				ContractManage_IncometContract_IncometContractQuery.contractID = JsonHelper.GetListFromJson(base.Request["ContractId"])[0];
			}
			else
			{
				ContractManage_IncometContract_IncometContractQuery.contractID = base.Request["ContractId"];
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hldfIsExamineApprove.Value = ConfigHelper.Get("IsIncomeContractApprove");
			this.InitUpdateAndQuery();
			this.BindgvBudget();
		}
	}
	protected void BindgvBudget()
	{
		if (!string.IsNullOrEmpty(ContractManage_IncometContract_IncometContractQuery.contractID))
		{
			IncometContractModel model = this.incometContractBll.GetModel(ContractManage_IncometContract_IncometContractQuery.contractID);
			this.lblTitalbudContract.Text = "合同预算";
			DataTable taskInfoByPrjIdAndConId = BudContractTask.GetTaskInfoByPrjIdAndConId(model.Project.PrjGuid.ToString(), ContractManage_IncometContract_IncometContractQuery.contractID, "", "", "");
			this.gvBudget.DataSource = taskInfoByPrjIdAndConId;
			this.gvBudget.DataBind();
		}
	}
	private void InitUpdateAndQuery()
	{
		string userCode = this.Session["yhdm"].ToString();
		DataTable dataTable = PersonnelAction.QueryPersonnelById(userCode);
		if (dataTable != null && dataTable.Rows.Count == 1)
		{
			this.lblBllProducer.Text = dataTable.Rows[0]["v_xm"].ToString();
		}
		this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
		IncometContractModel model = this.incometContractBll.GetModel(ContractManage_IncometContract_IncometContractQuery.contractID);
		if (model != null)
		{
			if (model.Project != null)
			{
				this.txtProject.Text = model.Project.PrjName;
			}
			this.txtContractCode.Text = model.ContractCode;
			this.txtContractName.Text = model.ContractName;
			this.txtContractMoney.Text = ((!model.ContractPrice.HasValue) ? string.Empty : WebUtil.GetEnPrice(model.ContractPrice.ToString(), model.ContractID));
			this.contractType.Text = model.TypeID.TypeName;
			this.txtSignPeople.Text = WebUtil.GetUserNames(model.SignPepole);
			this.txtQualityPeriod.Text = model.QualityPeriod;
			if (model.Party != null)
			{
				this.txtAName.Text = model.Party.CorpName;
			}
			this.txtBName.Text = model.Second;
			this.txtCParty.Text = model.CParty;
			this.txtAddress.Text = model.SignedAddress;
			this.txtSignDate.Text = ((!model.SignedTime.HasValue) ? string.Empty : System.Convert.ToDateTime(model.SignedTime).ToShortDateString());
			this.txtConState.Text = WebUtil.GetConState(model.ConState.ToString());
			DataTable table = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=27 and ParentCodeID=0");
			DataTable table2 = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=25 and ParentCodeID=0");
			if (table.Rows.Count > 0)
			{
				foreach (DataRow dataRow in table.Rows)
				{
					if (dataRow["NoteID"].ToString() == model.BalanceMode)
					{
						this.txtBalanceMode.Text = dataRow["CodeName"].ToString();
					}
				}
			}
			if (table2.Rows.Count > 0)
			{
				foreach (DataRow dataRow2 in table2.Rows)
				{
					if (dataRow2["NoteID"].ToString() == model.PayMode)
					{
						this.txtdropPayMode.Text = dataRow2["CodeName"].ToString();
					}
				}
			}
			this.txtStartDate.Text = ((!model.StartDate.HasValue) ? string.Empty : System.Convert.ToDateTime(model.StartDate).ToShortDateString());
			this.txtEndDate.Text = ((!model.EndDate.HasValue) ? string.Empty : System.Convert.ToDateTime(model.EndDate).ToShortDateString());
			this.txtCllectionCondition.Text = model.CllectionCondition;
			this.txtMainItem.Text = model.MainProvision;
			this.txtNotes.Text = model.Remark;
			this.txtRefundDate.Text = Common2.GetTime(model.ReFundDate.ToString());
			if (model.Project != null)
			{
				System.Collections.Generic.List<string> prjInfoIncoment = this.prjInfoBll.GetPrjInfoIncoment(model.Project.PrjGuid.ToString());
				if (prjInfoIncoment.Count > 0)
				{
					this.lblPrjFundInfo.Text = prjInfoIncoment[3].ToString();
					this.lblPrjFundWorkable.Text = prjInfoIncoment[0].ToString();
					this.lblQualityClass.Text = prjInfoIncoment[2].ToString();
					this.lblForecastProfitRate.Text = prjInfoIncoment[1].ToString();
				}
				this.lblPrjType.Text = this.GetPrjTypeName(model.Project.PrjGuid);
			}
			else
			{
				System.Collections.Generic.List<string> prjInfoZTBIncoment = this.prjInfoBll.GetPrjInfoZTBIncoment(ContractManage_IncometContract_IncometContractQuery.contractID);
				if (prjInfoZTBIncoment.Count > 0)
				{
					this.lblPrjFundInfo.Text = prjInfoZTBIncoment[3].ToString();
					this.lblPrjFundWorkable.Text = prjInfoZTBIncoment[0].ToString();
					this.lblQualityClass.Text = prjInfoZTBIncoment[2].ToString();
					this.lblForecastProfitRate.Text = prjInfoZTBIncoment[1].ToString();
					this.txtProject.Text = prjInfoZTBIncoment[4].ToString();
				}
			}
			this.ltlAnnex.Text = FileView.FilesBind(1905, model.ContractID);
		}
	}
	private string getContractName(string guid)
	{
		ContractType contractType = new ContractType();
		ContractTypeModel model = contractType.GetModel(guid);
		string empty = string.Empty;
		return model.TypeName;
	}
	private string GetPrjTypeName(System.Guid priGuid)
	{
		System.Collections.Generic.IList<ProjectKind> projectKinds = ProjectKind.GetProjectKinds(priGuid);
		if (projectKinds != null && projectKinds.Count > 0)
		{
			return TypeList.GetTypeName(projectKinds[0].PrjKind, "1", "ProjectType");
		}
		return string.Empty;
	}
}


