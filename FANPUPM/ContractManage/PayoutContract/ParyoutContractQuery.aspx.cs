using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using cn.justwin.Tender;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutContract_ParyoutContractQuery : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private PTPrjInfoBll prjInfoBll = new PTPrjInfoBll();
	private static string contractID = string.Empty;
	private string ic = string.Empty;
	private string wantplanCode = string.Empty;
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private MaterialWantPlan materialWant = new MaterialWantPlan();
	private MeterialPlanStock materialStock = new MeterialPlanStock();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private PayoutContract payoutContract = new PayoutContract();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			ContractManage_PayoutContract_ParyoutContractQuery.contractID = base.Request["ic"];
		}
		if (!string.IsNullOrEmpty(base.Request["ContractId"]))
		{
			if (base.Request["ContractId"].Contains("["))
			{
				ContractManage_PayoutContract_ParyoutContractQuery.contractID = JsonHelper.GetListFromJson(base.Request["ContractId"])[0];
			}
			else
			{
				ContractManage_PayoutContract_ParyoutContractQuery.contractID = base.Request["ContractId"];
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.InitUpdateAndQuery();
			DataTable purchaseStockByContractId = this.purchaseStock.GetPurchaseStockByContractId(ContractManage_PayoutContract_ParyoutContractQuery.contractID, true);
			this.ViewState["resource"] = purchaseStockByContractId;
			this.DataBindPurchaseplanStock();
			this.DataBindDiff();
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
		this.materialWant.GetModel(this.wantplanCode);
		this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
		PayoutContractModel model = this.payoutContract.GetModel(ContractManage_PayoutContract_ParyoutContractQuery.contractID);
		if (model != null)
		{
			this.bindTarget(ContractManage_PayoutContract_ParyoutContractQuery.contractID);
			this.txtProject.Text = model.PrjName;
			this.txtPrjType.Text = this.GetPrjTypeName(new System.Guid(model.PrjGuid));
			this.txtContractCode.Text = model.ContractCode;
			this.txtContractName.Text = model.ContractName;
			this.txtPreMoney.Text = ((!model.PrepayMoney.HasValue) ? string.Empty : model.PrepayMoney.ToString());
			this.txtContractMoney.Text = ((!model.ContractMoney.HasValue) ? string.Empty : model.ContractMoney.ToString());
			this.txtModifiedMoney.Text = ((!model.ModifiedMoney.HasValue) ? string.Empty : model.ModifiedMoney.ToString());
			this.contractType.Text = this.getContractName(model.TypeID);
			IncometContractBll incometContractBll = new IncometContractBll();
			IncometContractModel model2 = incometContractBll.GetModel(model.InContractID);
			if (model2 != null)
			{
				PrjInfoModel arg_1CE_0 = model2.Project;
				this.txtIncomeContract.Text = ((model2.ContractName == null) ? "" : model2.ContractName);
			}
			else
			{
				this.txtIncomeContract.Text = "";
			}
			this.txtAName.Text = model.AName;
			this.txtBName.Text = model.CorpName;
			this.txtAddress.Text = model.Address;
			this.txtSignDate.Text = ((!model.SignDate.HasValue) ? string.Empty : System.Convert.ToDateTime(model.SignDate).ToShortDateString());
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
			this.txtPaymentCondition.Text = model.PaymentCondition;
			this.txtMainItem.Text = model.MainItem;
			this.txtNotes.Text = model.Notes;
			this.txtCapitalNumber.Text = ((model.CapitalNumber == null) ? "" : model.CapitalNumber.ToString());
			this.txtfinanceNumber.Text = ((model.FinanceNumber == null) ? "" : model.FinanceNumber.ToString());
			this.txtfinanceProject.Text = ((model.FinanceProject == null) ? "" : model.FinanceProject.ToString());
			this.Literal1.Text = this.FilesBind(1901, model.ContractID);
			if (model.ContractMoney.ToString() == "")
			{
				this.txtCapitalNumber.Text = ConverRMB.Convert(0m);
			}
			else
			{
				decimal number = System.Convert.ToDecimal(model.ContractMoney);
				this.txtCapitalNumber.Text = ConverRMB.Convert(number);
			}
			if (model.ModifiedMoney.ToString() == "")
			{
				this.txtCapitalizationModifiedMoney.Text = ConverRMB.Convert(0m);
			}
			else
			{
				decimal number2 = System.Convert.ToDecimal(model.ModifiedMoney);
				this.txtCapitalizationModifiedMoney.Text = ConverRMB.Convert(number2);
			}
			string key;
			switch (key = model.ConState.ToString())
			{
			case "0":
				this.txtTypeName.Text = "执 行";
				return;
			case "1":
				this.txtTypeName.Text = "中 止";
				return;
			case "2":
				this.txtTypeName.Text = "保 内";
				return;
			case "3":
				this.txtTypeName.Text = "保 外";
				return;
			case "4":
				this.txtTypeName.Text = "解 除";
				return;
			case "5":
				this.txtTypeName.Text = "终 止";
				return;
			}
			this.txtTypeName.Text = "----";
		}
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
	private string getContractName(string guid)
	{
		ContractType contractType = new ContractType();
		ContractTypeModel model = contractType.GetModel(guid);
		string empty = string.Empty;
		return model.TypeName;
	}
	protected void bindTarget(string contractId)
	{
		DataTable taskByContract = BudTask.GetTaskByContract(contractId);
		if (taskByContract != null && taskByContract.Rows.Count > 0)
		{
			this.lblTitlTarget.Text = "控制指标";
			this.Repeater1.DataSource = taskByContract;
			this.Repeater1.DataBind();
		}
	}
	private void DataBindPurchaseplanStock()
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			if (dataTable.Rows.Count > 0)
			{
				this.lblTitalPurchase.Text = "采购单";
				GridViewUtility.DataBind(this.gvwPurchaseplanStock, dataTable);
				string total = System.Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
				GridViewUtility.AddTotalRow(this.gvwPurchaseplanStock, total, 8);
				return;
			}
			this.lblTitalPurchase.Text = "";
		}
	}
	public string FilesBind(int moduleID, string recordCode)
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		string sqlString = string.Concat(new string[]
		{
			"select * from XPM_Basic_AnnexList where (RecordCode = '",
			recordCode,
			"' and ModuleID = ",
			moduleID.ToString(),
			"  and state<>-1)"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		int arg_53_0 = dataTable.Rows.Count;
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string text = string.Empty;
			text = dataRow["OriginalName"].ToString();
			text = string.Concat(new object[]
			{
				"<span class=\"link\" onclick=\"javascript:return openannexpage('",
				dataRow["RecordCode"],
				"',",
				dataRow["ModuleID"],
				")\" title=\"",
				text,
				"\">",
				text,
				"</span>"
			});
			stringBuilder.Append(text);
			stringBuilder.Append(", ");
		}
		string result = string.Empty;
		if (stringBuilder.Length > 2)
		{
			result = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
		}
		return result;
	}
	private string GetCorpName(string corpId)
	{
		DataTable table = Common2.GetTable("XPM_Basic_ContactCorp", "where CorpId='" + corpId + "'");
		if (table.Rows.Count > 0)
		{
			return table.Rows[0]["CorpName"].ToString();
		}
		return string.Empty;
	}
	protected void gvwDiff_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		int arg_0D_0 = e.Row.RowIndex;
	}
	private void DataBindDiff()
	{
		DataTable dataTable = this.ViewState["resource"] as DataTable;
		if (dataTable.Rows.Count > 0)
		{
			DataTable diff = this.purchaseStock.GetDiff(ContractManage_PayoutContract_ParyoutContractQuery.contractID, dataTable.Rows[0]["pscode"].ToString(), this.hfldIsWBSRelevance.Value);
			this.gvwDiff.DataSource = diff;
			this.gvwDiff.DataBind();
			if (diff.Rows.Count == 0)
			{
				this.diffTitle.Visible = false;
				return;
			}
		}
		else
		{
			this.diffTitle.Visible = false;
		}
	}
}


