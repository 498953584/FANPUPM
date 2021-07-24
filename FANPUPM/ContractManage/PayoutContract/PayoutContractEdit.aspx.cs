using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using cn.justwin.Tender;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutContract_PayoutContractEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractID = string.Empty;
	private PTPrjInfoBll prjInfoBll = new PTPrjInfoBll();
	private readonly Purchase purchase = new Purchase();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private readonly ModifyStockBll modifyStock = new ModifyStockBll();
	private readonly BalanceStockBll balanceStock = new BalanceStockBll();
	private readonly PaymentTarget paymentTarget = new PaymentTarget();
	private PayoutContract payoutContract = new PayoutContract();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private int flowState = -1;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.contractID = base.Request["ic"];
		}
		if (!string.IsNullOrEmpty(base.Request["ContractId"]))
		{
			if (base.Request["ContractId"].Contains("["))
			{
				this.contractID = JsonHelper.GetListFromJson(base.Request["ContractId"])[0];
			}
			else
			{
				this.contractID = base.Request["ContractId"];
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.DataBindDrop();
			if (string.Compare(this.action, "Add", true) == 0)
			{
				this.InitAdd();
				this.gvwPurchaseStock.DataSource = null;
				this.gvwPurchaseStock.DataBind();
			}
			else
			{
				if (string.Compare(this.action, "AddProtocol", true) == 0)
				{
					this.InitAddProtocol();
				}
				else
				{
					this.InitUpdateAndQuery();
					this.InitBudTask();
					DataTable purchaseStockByContractId = this.purchaseStock.GetPurchaseStockByContractId(this.hfldContractId.Value, true);
					this.ViewState["resource"] = purchaseStockByContractId;
					this.DataBindPurchaseplanStock();
				}
			}
			this.flAnnx.MID = 1901;
			this.flAnnx.FID = this.hfldContractId.Value;
			this.flAnnx.Type = 1;
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (this.txtContractCode.Text.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('合同编号必须输入')");
			return;
		}
		if (this.action == "Add" || this.action == "AddProtocol")
		{
			if (this.payoutContract.Exists(this.txtContractCode.Text.Trim()))
			{
				base.RegisterScript("top.ui.alert('此合同编号已经存在')");
				return;
			}
		}
		else
		{
			if (this.txtContractCode.Text.Trim() != this.ViewState["strconcode"].ToString() && this.payoutContract.Exists(this.txtContractCode.Text.Trim()))
			{
				base.RegisterScript("top.ui.alert('此合同编号已经存在')");
				return;
			}
		}
		this.UpdatePurchaseplanStockDataSource();
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = (DataTable)this.ViewState["resource"];
			if (dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					string value = dataTable.Rows[i]["CorpName"].ToString();
					if (string.IsNullOrEmpty(value))
					{
						return;
					}
				}
			}
		}
		if (this.txtContractName.Text.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('合同名称必须输入')");
			return;
		}
		if (this.txtProject.Value.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('项目必须输入')");
			return;
		}
		if (this.txtTypeName.Value.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('合同类型必须输入')");
			return;
		}
		if (this.txtContractMoney.Text.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('合同金额必须输入')");
			return;
		}
		if (this.txtAName.Text.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('甲方必须输入')");
			return;
		}
		if (this.txtBName.Value.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('乙方必须输入')");
			return;
		}
		if (this.txtSignDate.Text.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('签约时间必须输入')");
			return;
		}
		if (this.txtAddress.Text.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('签约地点必须输入')");
			return;
		}
		if (this.dropBalanceMode.SelectedValue.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('结算方式必须输入')");
			return;
		}
		if (this.dropPayMode.SelectedValue.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('付款方式必须输入')");
			return;
		}
		if (this.txtStartDate.Text.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('开始时间必须输入')");
			return;
		}
		if (string.Compare(this.action, "Add", true) == 0)
		{
			this.AddContract();
			this.AddContractConfig("");
		}
		else
		{
			if (string.Compare(this.action, "AddProtocol", true) == 0)
			{
				this.AddProtocol();
				this.AddContractConfig(base.Request["ContractId"].ToString());
			}
			else
			{
				this.UpdateContract();
			}
		}
		this.SaveTarget();
		this.SavePurchase();
	}
	protected void SaveTarget()
	{
		DataTable taskInfoByParent = cn.justwin.Domain.BudTask.GetTaskInfoByParent(this.hfldTaskIds.Value);
		if (taskInfoByParent == null)
		{
			return;
		}
		string text = (string.Compare(this.action, "Update", true) == -1) ? this.hfldContractId.Value : this.contractID;
		BudTaskService budTaskService = new BudTaskService();
		BudModifyTaskService budModifyTaskService = new BudModifyTaskService();
		string cmdText = string.Format("UPDATE Bud_Task SET ContractId = '' WHERE ContractId = '{0}'", text);
		string cmdText2 = string.Format("UPDATE Bud_ModifyTask SET ContractId = '' WHERE ContractId = '{0}'", text);
		SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[0]);
		SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText2, new SqlParameter[0]);
		for (int i = 0; i < taskInfoByParent.Rows.Count; i++)
		{
			string taskId = taskInfoByParent.Rows[i]["TaskId"].ToString();
			string a = taskInfoByParent.Rows[i]["btype"].ToString();
			if (a == "T")
			{
				cn.justwin.Domain.Entities.BudTask byId = budTaskService.GetById(taskId);
				byId.ContractId = text;
				budTaskService.Update(byId);
			}
			else
			{
				BudModifyTask byTaskId = budModifyTaskService.GetByTaskId(taskId);
				byTaskId.ContractId = text;
				budModifyTaskService.Update(byTaskId);
			}
		}
	}
	protected void SavePurchase()
	{
		this.UpdatePurchaseplanStockDataSource();
		DataTable dataTable = this.ViewState["resource"] as DataTable;
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			if (dataTable != null)
			{
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				PurchaseModel purchaseModel = new PurchaseModel();
				if (dataTable.Rows.Count > 0)
				{
					if (this.action == "Add")
					{
						purchaseModel = this.GetNewPurchase();
						this.purchase.AddInContract(sqlTransaction, purchaseModel);
					}
					else
					{
						purchaseModel = this.purchase.GetModelByContractId(this.hfldContractId.Value.Trim());
						if (purchaseModel == null)
						{
							purchaseModel = this.GetNewPurchase();
							purchaseModel.flowstate = this.flowState;
							this.purchase.AddInContract(sqlTransaction, purchaseModel);
						}
						else
						{
							purchaseModel.Project = this.hdnProjectCode.Value;
							purchaseModel.flowstate = this.flowState;
							this.purchase.Update(sqlTransaction, purchaseModel);
						}
					}
					this.SavePurchaseStock(sqlTransaction, purchaseModel);
				}
				else
				{
					string condition = string.Format(" contract = '{0}' and IsConPurchase='true'", this.hfldContractId.Value.Trim());
					System.Collections.Generic.List<PurchaseModel> list = this.purchase.GetList(condition);
					foreach (PurchaseModel current in list)
					{
						this.purchase.Delete(sqlTransaction, current.pcode);
					}
				}
				sqlTransaction.Commit();
			}
		}
	}
	private void AddContractConfig(string contractID)
	{
		ConConfigContract conConfigContract = new ConConfigContract();
		ConConfigContractService conConfigContractService = new ConConfigContractService();
		string empty = string.Empty;
		if (!string.IsNullOrEmpty(this.txtContractCode.Text.Trim()))
		{
			ConPayoutContract byContractCode = new ConPayoutContractService().GetByContractCode(this.txtContractCode.Text.Trim());
			empty = byContractCode.ContractID;
		}
		if (string.IsNullOrEmpty(contractID))
		{
			conConfigContract.Id = System.Guid.NewGuid().ToString();
			conConfigContract.ContractId = empty;
			conConfigContract.PayoutAlarmDays = new int?(ContractParameter.PayoutAlarmDays);
			conConfigContract.IncomeAlarmDays = new int?(ContractParameter.IncomeAlarmDays);
			conConfigContract.HighPayAlarmLimit = new decimal?(ContractParameter.HighPayAlarmLimit);
			conConfigContract.HighBalanceAlarmLimit = new decimal?(ContractParameter.HighBalanceAlarmLimit);
			conConfigContract.MidPayAlarmUpperLimit = new decimal?(ContractParameter.MidPayAlarmUpperLimit);
			conConfigContract.MidPayAlarmLowerLimit = new decimal?(ContractParameter.MidPayAlarmLowerLimit);
			conConfigContract.MidBalanceAlarmUpperLimit = new decimal?(ContractParameter.LowBalanceAlarmUpperLimit);
			conConfigContract.MidBalanceAlarmLowerLimit = new decimal?(ContractParameter.LowBalanceAlarmLowerLimit);
			conConfigContract.LowPayAlarmUpperLimit = new decimal?(ContractParameter.LowPayAlarmUpperLimit);
			conConfigContract.LowPayAlarmLowerLimit = new decimal?(ContractParameter.LowBalanceAlarmLowerLimit);
			conConfigContract.LowBalanceAlarmUpperLimit = new decimal?(ContractParameter.LowBalanceAlarmUpperLimit);
			conConfigContract.LowBalanceAlarmLowerLimit = new decimal?(ContractParameter.LowBalanceAlarmLowerLimit);
			conConfigContract.IsPayoutAlarm = ContractParameter.IsPayoutAlarm;
			conConfigContract.IsPaymentAlarm = ContractParameter.IsPaymentAlarm;
			conConfigContract.IsIncomeAlarm = ContractParameter.IsIncomeAlarm;
			conConfigContract.IsBalanceAlarm = ContractParameter.IsBalanceAlarm;
		}
		else
		{
			conConfigContract = conConfigContractService.GetByContractID(contractID);
			conConfigContract.Id = System.Guid.NewGuid().ToString();
			conConfigContract.ContractId = empty;
		}
		conConfigContractService.Add(conConfigContract);
	}
	private PurchaseModel GetNewPurchase()
	{
		return new PurchaseModel
		{
			pid = System.Guid.NewGuid().ToString(),
			pcode = System.DateTime.Now.ToString("yyyyMMddHHmmss"),
			contract = this.hfldContractId.Value,
			flowstate = -1,
			person = PageHelper.QueryUser(this, base.UserCode),
			intime = System.DateTime.Now,
			acceptstate = 0,
			Project = this.hdnProjectCode.Value,
			annx = string.Empty,
			explain = string.Empty,
			IsConPurchase = true
		};
	}
	private void SavePurchaseStock(SqlTransaction trans, PurchaseModel model)
	{
		this.UpdatePurchaseplanStockDataSource();
		DataTable dataTable = this.ViewState["resource"] as DataTable;
		if (string.Compare(this.action, "Update", true) == -1)
		{
			System.Collections.Generic.List<PurchaseStockModel> list = new System.Collections.Generic.List<PurchaseStockModel>();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				list.Add(new PurchaseStockModel
				{
					psid = System.Guid.NewGuid().ToString(),
					scode = dataRow["ResourceCode"].ToString(),
					pscode = model.pcode,
					number = System.Convert.ToDecimal(dataRow["number"]),
					sprice = System.Convert.ToDecimal(dataRow["sprice"]),
					corp = dataRow["corp"].ToString()
				});
			}
			this.purchaseStock.Add(trans, list);
			return;
		}
		if (this.ViewState["PurchaseIdsDel"] != null)
		{
			System.Collections.Generic.List<string> psids = (System.Collections.Generic.List<string>)this.ViewState["PurchaseIdsDel"];
			this.purchaseStock.Delete(trans, psids);
		}
		DataRow[] array = dataTable.Select("psid<>''");
		for (int j = 0; j < array.Length; j++)
		{
			DataRow dataRow2 = array[j];
			PurchaseStockModel purchaseStockModel = new PurchaseStockModel();
			purchaseStockModel.psid = dataRow2["psid"].ToString();
			purchaseStockModel.scode = dataRow2["ResourceCode"].ToString();
			purchaseStockModel.pscode = model.pcode;
			purchaseStockModel.number = System.Convert.ToDecimal(dataRow2["number"]);
			purchaseStockModel.sprice = System.Convert.ToDecimal(dataRow2["sprice"]);
			purchaseStockModel.corp = dataRow2["corp"].ToString();
			this.purchaseStock.Update(trans, purchaseStockModel);
		}
		DataRow[] array2 = dataTable.Select("psid is null");
		if (array2 != null)
		{
			System.Collections.Generic.List<PurchaseStockModel> list2 = new System.Collections.Generic.List<PurchaseStockModel>();
			for (int k = 0; k < array2.Length; k++)
			{
				DataRow dataRow3 = array2[k];
				list2.Add(new PurchaseStockModel
				{
					psid = System.Guid.NewGuid().ToString(),
					scode = dataRow3["ResourceCode"].ToString(),
					pscode = model.pcode,
					number = System.Convert.ToDecimal(dataRow3["number"]),
					sprice = System.Convert.ToDecimal(dataRow3["sprice"]),
					corp = dataRow3["corp"].ToString()
				});
			}
			this.purchaseStock.Add(trans, list2);
		}
	}
	private void InitAdd()
	{
		this.hfldContractId.Value = System.Guid.NewGuid().ToString();
		this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
		this.txtInputDate.Text = System.DateTime.Now.ToShortDateString();
	}
	private void InitAddProtocol()
	{
		this.lblTitle.Text = "新增支出合同补充协议";
		this.InitAdd();
		PayoutContractModel model = this.payoutContract.GetModel(this.contractID);
		this.txtProject.Value = model.PrjName;
		this.hdnProjectCode.Value = model.PrjGuid;
		this.txtPrjType.Value = this.GetPrjTypeName(new System.Guid(model.PrjGuid));
	}
	private void InitUpdateAndQuery()
	{
		PayoutContractModel model = this.payoutContract.GetModel(this.contractID);
		if (model != null)
		{
			this.txtProject.Value = model.PrjName;
			this.hdnProjectCode.Value = model.PrjGuid;
			this.txtPrjType.Value = this.GetPrjTypeName(new System.Guid(model.PrjGuid));
			this.hfldContractId.Value = model.ContractID;
			this.txtContractCode.Text = model.ContractCode;
			this.ViewState["strconcode"] = model.ContractCode;
			this.txtContractName.Text = model.ContractName;
			this.txtContractMoney.Text = ((!model.ContractMoney.HasValue) ? string.Empty : model.ContractMoney.ToString());
			this.hfldTypeID.Value = model.TypeID;
			this.txtTypeName.Value = model.TypeName;
			this.txtPrepayMoney.Text = ((!model.PrepayMoney.HasValue) ? string.Empty : model.PrepayMoney.ToString());
			this.hfldIncomeContractId.Value = model.InContractID;
			IncometContractBll incometContractBll = new IncometContractBll();
			IncometContractModel model2 = incometContractBll.GetModel(model.InContractID);
			this.txtIncomeContract.Value = ((model2 == null) ? string.Empty : model2.ContractName);
			this.txtAName.Text = model.AName;
			if (!string.IsNullOrEmpty(model.CorpName))
			{
				this.txtBName.Value = model.CorpName;
				this.hfldBName.Value = model.BName;
			}
			this.txtAddress.Text = model.Address;
			this.txtSignDate.Text = ((!model.SignDate.HasValue) ? string.Empty : System.Convert.ToDateTime(model.SignDate).ToShortDateString());
			this.dropBalanceMode.SelectedValue = model.BalanceMode;
			this.dropPayMode.SelectedValue = model.PayMode;
			this.txtStartDate.Text = ((!model.StartDate.HasValue) ? string.Empty : System.Convert.ToDateTime(model.StartDate).ToShortDateString());
			this.txtEndDate.Text = ((!model.EndDate.HasValue) ? string.Empty : System.Convert.ToDateTime(model.EndDate).ToShortDateString());
			this.txtInputPerson.Text = model.InputPerson;
			this.txtInputDate.Text = ((!model.InputDate.HasValue) ? string.Empty : System.Convert.ToDateTime(model.InputDate).ToShortDateString());
			this.txtPaymentCondition.Text = model.PaymentCondition;
			this.txtMainItem.Text = model.MainItem;
			this.txtNotes.Text = model.Notes;
			this.chkFictitious.Checked = false;
			if (model.Fictitious == 0)
			{
				this.chkFictitious.Checked = true;
			}
			this.txtCapitalNumber.Text = model.CapitalNumber;
			this.txtfinanceProject.Text = model.FinanceProject.ToString();
			this.txtfinanceNumber.Text = model.FinanceNumber.ToString();
			if (!string.IsNullOrEmpty(model.SignPerson))
			{
				this.hfldSignPerson.Value = model.SignPerson;
				this.txtSignPerson.Value = PageHelper.QueryUser(this, model.SignPerson);
			}
		}
	}
	private void DataBindDrop()
	{
		WebUtil.DataBindBalanceMode(this.dropBalanceMode);
		WebUtil.DataBindPayMode(this.dropPayMode);
	}
	private void AddContract()
	{
		PayoutContractModel payoutContractModel = new PayoutContractModel();
		payoutContractModel.ContractID = this.hfldContractId.Value;
		this.InitContract(payoutContractModel);
		payoutContractModel.IsMainContract = true;
		string a = ConfigurationManager.AppSettings["ContractPower"].ToString();
		if (a == "0")
		{
			this.AddUserCodes(payoutContractModel);
		}
		else
		{
			this.AddUserCodes(payoutContractModel, this.hdnProjectCode.Value);
		}
		this.Add(payoutContractModel);
	}
	private void AddProtocol()
	{
		PayoutContractModel payoutContractModel = new PayoutContractModel();
		payoutContractModel.ContractID = this.hfldContractId.Value;
		this.InitContract(payoutContractModel);
		payoutContractModel.IsMainContract = false;
		payoutContractModel.MainContractID = this.contractID;
		string a = ConfigurationManager.AppSettings["ContractPower"].ToString();
		if (a == "0")
		{
			this.AddUserCodes(payoutContractModel);
		}
		else
		{
			this.AddUserCodes(payoutContractModel, this.hdnProjectCode.Value);
		}
		this.Add(payoutContractModel);
	}
	private void Add(PayoutContractModel contract)
	{
		try
		{
			if (this.payoutContract.Exists(contract.ContractCode))
			{
				base.RegisterScript("top.ui.alert('此合同编号已经存在')");
				this.DataBindPurchaseplanStock();
			}
			else
			{
				this.payoutContract.Add(contract);
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append("top.ui.show('添加成功')").Append(System.Environment.NewLine);
				stringBuilder.Append("winclose('PayoutContractEdit', 'PayoutContract.aspx', true)");
				base.RegisterScript(stringBuilder.ToString());
			}
		}
		catch (System.Exception)
		{
			base.RegisterScript("top.ui.alert('添加失败')");
		}
	}
	private void UpdateContract()
	{
		PayoutContractModel model = this.payoutContract.GetModel(this.contractID);
		this.flowState = model.FlowState.Value;
		this.InitContract(model);
		try
		{
			this.payoutContract.Update(model, null);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("top.ui.show('修改成功')").Append(System.Environment.NewLine);
			stringBuilder.Append("winclose('PayoutContractEdit', 'PayoutContract.aspx', true)");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch (System.Exception)
		{
			base.RegisterScript("top.ui.alert('修改失败')");
		}
	}
	private void InitContract(PayoutContractModel contract)
	{
		contract.ContractCode = this.txtContractCode.Text.Trim();
		contract.ContractName = this.txtContractName.Text.Trim();
		contract.TypeID = this.hfldTypeID.Value;
		contract.AName = this.txtAName.Text.Trim();
		if (this.GetCorpID(this.txtBName.Value.Trim()) != "")
		{
			contract.BName = this.GetCorpID(this.txtBName.Value.Trim());
		}
		else
		{
			contract.BName = null;
		}
		if (!string.IsNullOrEmpty(this.txtContractMoney.Text.Trim()))
		{
			contract.ModifiedMoney = (contract.ContractMoney = new decimal?(System.Convert.ToDecimal(this.txtContractMoney.Text.Trim())));
		}
		if (!string.IsNullOrEmpty(this.txtPrepayMoney.Text.Trim()))
		{
			contract.PrepayMoney = new decimal?(System.Convert.ToDecimal(this.txtPrepayMoney.Text.Trim()));
		}
		contract.MainItem = this.txtMainItem.Text.Trim();
		contract.PaymentCondition = this.txtPaymentCondition.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtSignDate.Text.Trim()))
		{
			contract.SignDate = new System.DateTime?(System.Convert.ToDateTime(this.txtSignDate.Text.Trim()));
		}
		if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			contract.StartDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartDate.Text.Trim()));
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			contract.EndDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndDate.Text.Trim()));
		}
		if (this.dropBalanceMode.SelectedIndex != 0)
		{
			contract.BalanceMode = this.dropBalanceMode.SelectedValue;
		}
		if (this.dropPayMode.SelectedIndex != 0)
		{
			contract.PayMode = this.dropPayMode.SelectedValue;
		}
		contract.Address = this.txtAddress.Text.Trim();
		if (string.Compare(this.action, "Update", true) == -1)
		{
			contract.FlowState = new int?(-1);
		}
		contract.IsArchived = false;
		contract.ArchiveDate = null;
		contract.PrjGuid = this.hdnProjectCode.Value;
		contract.InContractID = this.hfldIncomeContractId.Value;
		contract.Notes = this.txtNotes.Text.Trim();
		contract.InputPerson = this.txtInputPerson.Text;
		contract.InputDate = new System.DateTime?(System.DateTime.Now);
		contract.Fictitious = 1;
		if (this.chkFictitious.Checked)
		{
			contract.Fictitious = 0;
		}
		contract.CapitalNumber = this.txtCapitalNumber.Text.ToString().Trim();
		contract.FinanceNumber = this.txtfinanceNumber.Text.Trim().ToString();
		contract.FinanceProject = this.txtfinanceProject.Text.Trim().ToString();
		contract.SignPerson = this.hfldSignPerson.Value;
	}
	private void AddUserCodes(PayoutContractModel contract)
	{
		System.Collections.Generic.List<string> userCodes = new System.Collections.Generic.List<string>();
		userCodes.Add(base.UserCode);
		if (base.UserCode != "00000000")
		{
			userCodes.Add("00000000");
		}
		ContractType contractType = new ContractType();
		ContractTypeModel model = contractType.GetModel(contract.TypeID);
		if (model != null)
		{
			System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(model.UserCodes);
			listFromJson.ForEach(delegate(string userCode)
			{
				if (!userCodes.Contains(userCode))
				{
					userCodes.Add(userCode);
				}
			});
		}
		string text = this.hdnProjectCode.Value.Trim();
		System.Collections.Generic.List<string> list = (System.Collections.Generic.List<string>)new PTPrjInfoZTBUserService().GetUser(text);
		list.ForEach(delegate(string userCode)
		{
			if (!userCodes.Contains(userCode))
			{
				userCodes.Add(userCode);
			}
		});
		System.Collections.Generic.List<string> userCodesByIDThroughPrjProperty = new PTPrjInfoZTBDetailService().getUserCodesByIDThroughPrjProperty(text);
		System.Collections.Generic.List<string> userCode2 = new PrivBusiDataRoleService().GetUserCode(text);
		userCodesByIDThroughPrjProperty.ForEach(delegate(string userCode)
		{
			if (!userCodes.Contains(userCode))
			{
				userCodes.Add(userCode);
			}
		});
		userCode2.ForEach(delegate(string userCode)
		{
			if (!userCodes.Contains(userCode))
			{
				userCodes.Add(userCode);
			}
		});
		contract.UserCodes = JsonHelper.Serialize(userCodes.ToArray());
	}
	private void AddUserCodes(PayoutContractModel contract, string prjId)
	{
		System.Collections.Generic.List<string> UserCodes = new System.Collections.Generic.List<string>();
		System.Collections.Generic.List<string> ztbUsers = (System.Collections.Generic.List<string>)new PTPrjInfoZTBUserService().GetUser(prjId);
		System.Collections.Generic.List<string> userCodesByIDThroughPrjProperty = new PTPrjInfoZTBDetailService().getUserCodesByIDThroughPrjProperty(prjId);
		System.Collections.Generic.List<string> userCode2 = new PrivBusiDataRoleService().GetUserCode(prjId);
		ConContractType byID = new ConContractTypeService().GetByID(contract.TypeID);
		System.Collections.Generic.List<string> ContractTypeUsers = JsonHelper.GetListFromJson(byID.UserCodes);
		userCodesByIDThroughPrjProperty.ForEach(delegate(string userCode)
		{
			if (!ztbUsers.Contains(userCode))
			{
				ztbUsers.Add(userCode);
			}
		});
		userCode2.ForEach(delegate(string userCode)
		{
			if (!ztbUsers.Contains(userCode))
			{
				ztbUsers.Add(userCode);
			}
		});
		if (ztbUsers.Count >= ContractTypeUsers.Count)
		{
			ContractTypeUsers.ForEach(delegate(string userCode)
			{
				if (ztbUsers.Contains(userCode))
				{
					UserCodes.Add(userCode);
				}
			});
		}
		else
		{
			ztbUsers.ForEach(delegate(string userCode)
			{
				if (ContractTypeUsers.Contains(userCode))
				{
					UserCodes.Add(userCode);
				}
			});
		}
		if (!UserCodes.Contains("00000000"))
		{
			UserCodes.Add("00000000");
		}
		if (!UserCodes.Contains(base.UserCode))
		{
			UserCodes.Add(base.UserCode);
		}
		contract.UserCodes = JsonHelper.Serialize(UserCodes.ToArray());
	}
	protected void btnBindTarget_Click(object sender, System.EventArgs e)
	{
		this.UpdatePurchaseplanStockDataSource();
		this.ConvertToTable();
		this.bindTarget();
		this.DataBindPurchaseplanStock();
	}
	protected void btnPrjChange_Click(object sender, System.EventArgs e)
	{
		this.ConvertToTable();
		this.bindTarget();
		DataTable dataTable = new DataTable();
		dataTable = (DataTable)this.ViewState["dtTarget"];
		if (dataTable != null)
		{
			dataTable.Rows.Clear();
		}
		this.ViewState["dtTarget"] = dataTable;
		this.bindTarget();
	}
	protected void btnBindResource_Click(object sender, System.EventArgs e)
	{
		this.UpdatePurchaseplanStockDataSource();
		this.InitResource(this.hfldResourceId.Value);
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		if (string.Compare(this.action, "Update", true) == 0)
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			if (this.ViewState["PurchaseIdsDel"] != null)
			{
				list = (System.Collections.Generic.List<string>)this.ViewState["PurchaseIdsDel"];
			}
			System.Collections.Generic.List<string> listFromJson2 = JsonHelper.GetListFromJson(this.hfldPurchaseIds.Value);
			foreach (string current in listFromJson2)
			{
				if (!string.IsNullOrEmpty(current))
				{
					list.Add(current);
				}
			}
			this.ViewState["PurchaseIdsDel"] = list;
		}
		System.Collections.Generic.List<string> list2 = new System.Collections.Generic.List<string>();
		if (this.ViewState["PurchaseIdsDel"] != null)
		{
			System.Collections.Generic.List<string> list3 = (System.Collections.Generic.List<string>)this.ViewState["PurchaseIdsDel"];
			System.Collections.Generic.List<string> list4 = new System.Collections.Generic.List<string>();
			foreach (string current2 in list3)
			{
				DataTable infoByPurchaseId = this.balanceStock.GetInfoByPurchaseId(current2);
				if (infoByPurchaseId.Rows.Count > 0)
				{
					PurchaseStockModel model = this.purchaseStock.GetModel(current2);
					if (model != null)
					{
						list2.Add(model.scode);
						listFromJson.Remove(model.scode);
						list4.Add(current2);
					}
				}
			}
			foreach (string current3 in list4)
			{
				list3.Remove(current3);
			}
			this.ViewState["PurchaseIdsDel"] = list3;
		}
		if (list2.Count > 0)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("alert('编号为");
			foreach (string current4 in list2)
			{
				stringBuilder.Append(current4.Trim() + ",");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append("的物资已经被结算，不能进行删除操作!');");
			base.RegisterScript(stringBuilder.ToString());
		}
		this.UpdatePurchaseplanStockDataSource();
		this.DeleteResource(listFromJson);
		this.DataBindPurchaseplanStock();
		this.hfldIsTarPur.Value = "Purchase";
	}
	private void DeleteResource(System.Collections.Generic.List<string> lstResourceID)
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (lstResourceID.Contains(dataTable.Rows[i]["ResourceCode"].ToString()))
				{
					dataTable.Rows.RemoveAt(i);
					i--;
				}
			}
			dataTable.AcceptChanges();
			this.ViewState["resource"] = dataTable;
		}
	}
	private void UpdatePurchaseplanStockDataSource()
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				GridViewRow gridViewRow = this.gvwPurchaseStock.Rows[i];
				DataRow dataRow = dataTable.Rows[i];
				if (gridViewRow.FindControl("txtNumber") is TextBox)
				{
					TextBox textBox = gridViewRow.FindControl("txtNumber") as TextBox;
					dataRow["number"] = textBox.Text.Trim();
				}
				if (gridViewRow.FindControl("txtSprice") is TextBox)
				{
					TextBox textBox2 = gridViewRow.FindControl("txtSprice") as TextBox;
					dataRow["sprice"] = textBox2.Text.Trim();
				}
				dataRow["Total"] = System.Convert.ToDecimal(dataRow["number"]) * System.Convert.ToDecimal(dataRow["sprice"]);
				if (gridViewRow.FindControl("hfldCorp") is HiddenField)
				{
					HiddenField hiddenField = gridViewRow.FindControl("hfldCorp") as HiddenField;
					dataRow["corp"] = hiddenField.Value;
				}
				if (gridViewRow.FindControl("txtCorp") is TextBox)
				{
					TextBox textBox3 = gridViewRow.FindControl("txtCorp") as TextBox;
					dataRow["CorpName"] = textBox3.Text;
				}
			}
			this.ViewState["resource"] = dataTable;
		}
	}
	private void InitResource(string resources)
	{
		if (!string.IsNullOrEmpty(resources))
		{
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(resources);
			if (array != null)
			{
				cn.justwin.BLL.Resource resource = new cn.justwin.BLL.Resource();
				DataTable resource2 = resource.GetResource(array);
				DataColumn dataColumn = new DataColumn("sprice", typeof(decimal));
				dataColumn.DefaultValue = 0m;
				DataColumn dataColumn2 = new DataColumn("number", typeof(decimal));
				dataColumn2.DefaultValue = 0m;
				DataColumn dataColumn3 = new DataColumn("Total", typeof(decimal));
				dataColumn3.DefaultValue = 0m;
				resource2.Columns.Add(dataColumn2);
				System.Collections.Generic.List<string> resourceNumber = this.GetResourceNumber();
				for (int i = 0; i < resource2.Rows.Count; i++)
				{
					DataRow dataRow = resource2.Rows[i];
					try
					{
						dataRow["number"] = System.Convert.ToDecimal(resourceNumber[i]);
					}
					catch
					{
					}
				}
				resource2.Columns.Add(dataColumn);
				resource2.Columns.Add(dataColumn3);
				resource2.Columns.Remove("corpId");
				resource2.Columns.Remove("corpName");
				DataColumn dataColumn4 = new DataColumn("corp", typeof(string));
				dataColumn4.DefaultValue = this.hfldBName.Value.Trim();
				resource2.Columns.Add(dataColumn4);
				DataColumn dataColumn5 = new DataColumn("CorpName", typeof(string));
				dataColumn5.DefaultValue = this.txtBName.Value.Trim();
				resource2.Columns.Add(dataColumn5);
				DataColumn dataColumn6 = new DataColumn("PsId", typeof(string));
				dataColumn6.DefaultValue = null;
				resource2.Columns.Add(dataColumn6);
				if (this.ViewState["resource"] == null)
				{
					this.ViewState["resource"] = resource2;
				}
				else
				{
					DataTable dataTable = this.ViewState["resource"] as DataTable;
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						DataRow dataRow2 = dataTable.Rows[j];
						for (int k = 0; k < array.Length; k++)
						{
							if (dataRow2["ResourceId"].ToString() == array[k])
							{
								try
								{
									dataRow2["number"] = resourceNumber[k];
								}
								catch
								{
								}
							}
						}
					}
					dataTable.PrimaryKey = new DataColumn[]
					{
						dataTable.Columns["ResourceId"]
					};
					resource2.PrimaryKey = new DataColumn[]
					{
						resource2.Columns["ResourceId"]
					};
					dataTable.Merge(resource2, true);
				}
				this.DataBindPurchaseplanStock();
				this.hfldIsTarPur.Value = "Purchase";
			}
		}
	}
	private void DataBindPurchaseplanStock()
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			if (dataTable.Rows.Count > 0)
			{
				this.gvwPurchaseStock.DataSource = dataTable;
				this.gvwPurchaseStock.DataBind();
				string total = System.Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
				GridViewUtility.AddTotalRow(this.gvwPurchaseStock, total, 9);
				return;
			}
			this.gvwPurchaseStock.DataSource = dataTable;
			this.gvwPurchaseStock.DataBind();
		}
	}
	protected void btnPartBSelect_click(object sender, System.EventArgs e)
	{
		this.UpdatePurchaseplanStockDataSource();
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = (DataTable)this.ViewState["resource"];
			if (dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					string value = dataTable.Rows[i]["CorpName"].ToString();
					if (string.IsNullOrEmpty(value))
					{
						dataTable.Rows[i]["corp"] = this.hfldBName.Value;
						dataTable.Rows[i]["CorpName"] = this.txtBName.Value;
					}
				}
				this.gvwPurchaseStock.DataSource = dataTable;
				this.gvwPurchaseStock.DataBind();
				this.ViewState["resource"] = dataTable;
			}
		}
	}
	private System.Collections.Generic.List<string> GetResourceNumber()
	{
		System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();
		string value = this.hfldppcode.Value;
		if (!string.IsNullOrEmpty(value) && value.Length > 2)
		{
			result = JsonHelper.GetListFromJson(value);
		}
		this.hfldppcode.Value = "";
		return result;
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
			e.Row.Attributes["TargetId"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			int num = text.Length / 3;
			e.Row.Attributes["layer"] = num.ToString();
		}
	}
	protected void gvwPurchaseStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwPurchaseStock.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["purchaseId"] = this.gvwPurchaseStock.DataKeys[e.Row.RowIndex].Values["PsId"].ToString();
		}
	}
	protected void bindTarget()
	{
		this.gvBudget.DataSource = cn.justwin.Domain.BudTask.GetTaskInfoByParent(this.hfldTaskIds.Value);
		this.gvBudget.DataBind();
	}
	protected void chkContaint_CheckedChanged(object sender, System.EventArgs e)
	{
		string text = string.Empty;
		DataTable dataTable = this.ViewState["dtTarget"] as DataTable;
		if (dataTable == null)
		{
			return;
		}
		foreach (DataRow dataRow in dataTable.Rows)
		{
			text = text + "'" + dataRow["TaskId"].ToString() + "',";
		}
		if (text.Length > 0)
		{
			text = text.Substring(0, text.Length - 1);
			dataTable.Rows.Clear();
			this.ViewState["dtTarget"] = dataTable;
			this.FillTable(this.hfldContractId.Value, text, this.chkContaint.Checked);
			this.bindTarget();
		}
		this.DataBindPurchaseplanStock();
	}
	protected void ConvertToTable()
	{
		string value = this.hfldTaskIds.Value;
		string text = string.Empty;
		bool @checked = this.chkContaint.Checked;
		DataTable dataTable = new DataTable();
		dataTable = (DataTable)this.ViewState["dtTarget"];
		if (!string.IsNullOrEmpty(value))
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			if (!string.IsNullOrEmpty(value))
			{
				if (value.Contains("["))
				{
					list = JsonHelper.GetListFromJson(value);
				}
				else
				{
					list.Add(value);
				}
			}
			System.Collections.Generic.List<string> list2 = new System.Collections.Generic.List<string>();
			foreach (string current in list)
			{
				if (!cn.justwin.Domain.BudTask.CheckChilds(current))
				{
					list2.Add(current);
				}
			}
			if (dataTable != null && dataTable.Rows.Count > 0 && list2.Count > 0)
			{
				System.Collections.Generic.List<string> list3 = new System.Collections.Generic.List<string>();
				foreach (string current2 in list2)
				{
					foreach (DataRow dataRow in dataTable.Rows)
					{
						string b = dataRow["TaskId"].ToString();
						if (current2 == b)
						{
							list3.Add(current2);
							break;
						}
					}
				}
				foreach (string current3 in list3)
				{
					list2.Remove(current3);
				}
			}
			using (System.Collections.Generic.List<string>.Enumerator enumerator5 = list2.GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					string current4 = enumerator5.Current;
					text = text + "'" + current4 + "',";
				}
				goto IL_252;
			}
		}
		PayoutTarget payoutTarget = new PayoutTarget();
		System.Collections.Generic.List<string> taskIdsByContractId = payoutTarget.GetTaskIdsByContractId(this.hfldContractId.Value);
		foreach (string current5 in taskIdsByContractId)
		{
			text = text + "'" + current5 + "',";
		}
		if (dataTable != null)
		{
			dataTable.Clear();
		}
		IL_252:
		this.ViewState["dtTarget"] = dataTable;
		if (text.Length > 0)
		{
			text = text.Substring(0, text.Length - 1);
			this.FillTable(this.hfldContractId.Value, text, @checked);
		}
	}
	protected void FillTable(string contractId, string targetIds, bool isContaint)
	{
		DataTable dataTable = new DataTable();
		if ((DataTable)this.ViewState["dtTarget"] != null)
		{
			dataTable = (DataTable)this.ViewState["dtTarget"];
		}
		if (dataTable != null && dataTable.Columns.Count == 0)
		{
			dataTable = new DataTable();
			dataTable.Columns.Add("TargetId");
			dataTable.Columns.Add("TaskId");
			dataTable.Columns.Add("TaskName");
			dataTable.Columns.Add("TotalAmount");
			dataTable.Columns.Add("SignedAmount");
			dataTable.Columns.Add("NotSignAmount");
			dataTable.Columns.Add("CurSignAmount");
			dataTable.Columns.Add("BalSignAmount");
		}
		PayoutTarget payoutTarget = new PayoutTarget();
		DataTable target = payoutTarget.GetTarget(contractId, targetIds, isContaint, this.hfldIsWBSRelevance.Value);
		for (int i = 0; i < target.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["TargetId"] = target.Rows[i]["TargetId"];
			dataRow["TaskId"] = target.Rows[i]["TaskId"];
			dataRow["TaskName"] = target.Rows[i]["TaskName"];
			dataRow["TotalAmount"] = target.Rows[i]["TotalAmount"];
			dataRow["SignedAmount"] = target.Rows[i]["SignedAmount"];
			dataRow["NotSignAmount"] = target.Rows[i]["NotSignAmount"];
			dataRow["CurSignAmount"] = target.Rows[i]["CurSignAmount"];
			dataRow["BalSignAmount"] = target.Rows[i]["BalSignAmount"];
			dataTable.Rows.Add(dataRow);
		}
		this.ViewState["dtTarget"] = dataTable;
	}
	private string GetCorpID(string corpName)
	{
		DataTable table = Common2.GetTable("XPM_Basic_ContactCorp", "where CorpName='" + corpName + "'");
		if (table.Rows.Count > 0)
		{
			return table.Rows[0]["CorpID"].ToString();
		}
		return string.Empty;
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
	private void InitBudTask()
	{
		this.gvBudget.DataSource = cn.justwin.Domain.BudTask.GetTaskByContract(this.contractID);
		this.gvBudget.DataBind();
	}
	protected void btnDelTarget_Click1(object sender, System.EventArgs e)
	{
		this.hfldTaskIds.Value = "";
		this.bindTarget();
	}
	protected void BtnShowPrjTypeName_Click(object sender, System.EventArgs e)
	{
		this.txtPrjType.Value = string.Empty;
		if (string.IsNullOrEmpty(this.hdnProjectCode.Value.Trim()))
		{
			return;
		}
		this.txtPrjType.Value = this.GetPrjTypeName(new System.Guid(this.hdnProjectCode.Value.Trim()));
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


