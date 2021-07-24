using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.Tender;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometContract_AddIncometContract : NBasePage, IRequiresSessionState
{
	private ContractType contractType = new ContractType();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private PTPrjInfoBll prjInfoBll = new PTPrjInfoBll();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
			this.InitConBedget();
		}
	}
	public void InitPage()
	{
		this.BindDdl();
		string arg_1B_0 = base.Request.QueryString["view"];
		if (base.Request.QueryString["id"] != null)
		{
			this.lblTitle.Text = "编辑收入合同结算";
			IncometContractModel model = this.incometContractBll.GetModel(base.Request.QueryString["id"]);
			if (model != null)
			{
				this.txtCllectionCondition.Text = model.CllectionCondition;
				this.txtContractCode.Text = model.ContractCode;
				this.hdContractCode.Value = model.ContractCode;
				this.txtContractName.Text = model.ContractName;
				this.txtContractPrice.Text = model.ContractPrice.ToString();
				this.txtEndDate.Text = Common2.GetTime(model.EndDate.ToString());
				this.txtMainProvision.Text = model.MainProvision;
				if (model.Party != null)
				{
					this.txtParty.Value = model.Party.CorpName.ToString();
					this.hdParty.Value = model.Party.CorpID.ToString();
				}
				if (model.Project != null)
				{
					this.txtProject.Value = model.Project.PrjName;
					this.hdnProjectCode.Value = model.Project.PrjGuid.ToString();
				}
				this.txtRegisterTime.Text = Common2.GetTime(model.RegisterTime.ToString());
				this.txtRemark.Text = model.Remark;
				this.txtSecond.Text = model.Second;
				this.txtSignedAddress.Text = model.SignedAddress;
				this.txtSignedTime.Text = Common2.GetTime(model.SignedTime.ToString());
				this.txtStartDate.Text = Common2.GetTime(model.StartDate.ToString());
				this.txtReFundDate.Text = Common2.GetTime(model.ReFundDate.ToString());
				this.txtSubscriber.Text = model.Subscriber;
				this.txtTypeName.Text = model.TypeID.TypeName;
				this.hfldTypeID.Value = model.TypeID.TypeID;
				this.hdGuid.Value = model.ContractID;
				this.hdFileTime.Value = Common2.GetTime(model.FileTime.ToString());
				this.hdFCode.Value = model.FCode;
				this.hdIsArchived.Value = model.IsArchived.ToString();
				this.hdisFContract.Value = model.isFContract.ToString();
				this.hdUserCodes.Value = model.UserCodes;
				this.ddlBalanceMode.SelectedValue = model.BalanceMode;
				this.ddlPayMode.SelectedValue = model.PayMode;
				this.hfldSignPeople.Value = model.SignPepole;
				this.txtSignPeople.Value = WebUtil.GetUserNames(model.SignPepole);
				this.txtQualityPeriod.Value = model.QualityPeriod;
				this.txtCParty.Text = model.CParty;
				if (model.Sign.ToString() != "")
				{
					if (model.Sign == 0)
					{
						this.rdbNoSign.Checked = true;
					}
					else
					{
						if (model.Sign == 1)
						{
							this.rdbisSign.Checked = true;
						}
					}
				}
				if (model.Project != null)
				{
					System.Collections.Generic.List<string> prjInfoIncoment = this.prjInfoBll.GetPrjInfoIncoment(model.Project.PrjGuid.ToString());
					if (prjInfoIncoment.Count > 0)
					{
						this.txtPrjFundInfo.Value = prjInfoIncoment[3].ToString();
						this.txtPrjFundWorkable.Value = prjInfoIncoment[0].ToString();
						this.txtQualityClass.Value = prjInfoIncoment[2].ToString();
						this.txtForecastProfitRate.Value = prjInfoIncoment[1].ToString();
					}
					this.txtPrjType.Value = this.GetPrjTypeName(model.Project.PrjGuid);
				}
				else
				{
					System.Collections.Generic.List<string> prjInfoZTBIncoment = this.prjInfoBll.GetPrjInfoZTBIncoment(base.Request.QueryString["id"]);
					if (prjInfoZTBIncoment.Count > 0)
					{
						this.txtPrjFundInfo.Value = prjInfoZTBIncoment[3].ToString();
						this.txtPrjFundWorkable.Value = prjInfoZTBIncoment[0].ToString();
						this.txtQualityClass.Value = prjInfoZTBIncoment[2].ToString();
						this.txtForecastProfitRate.Value = prjInfoZTBIncoment[1].ToString();
						this.txtProject.Value = prjInfoZTBIncoment[4].ToString();
						this.hdnProjectCode.Value = prjInfoZTBIncoment[5].ToString();
					}
				}
				if (this.hdisFContract.Value == "True")
				{
					this.lblTitle.Text = "编辑收入合同";
				}
				else
				{
					this.lblTitle.Text = "编辑收入合同补充协议";
				}
			}
		}
		else
		{
			this.txtRegisterTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.txtSubscriber.Text = PageHelper.QueryUser(this, base.UserCode);
			DataTable table = Common2.GetTable("XPM_Basic_ContactCorp", " where corpTypeID=1");
			if (table.Rows.Count > 0)
			{
				this.txtSecond.Text = table.Rows[0]["corpName"].ToString();
			}
			this.hdGuid.Value = System.Guid.NewGuid().ToString();
			this.hdFileTime.Value = Common2.GetTime(System.DateTime.Now.ToString());
			if (base.Request.QueryString["b"] != null)
			{
				this.hdisFContract.Value = "false";
				IncometContractModel model2 = this.incometContractBll.GetModel(base.Request.QueryString["b"]);
				if (model2.FCode != "0")
				{
					this.hdFCode.Value = model2.FCode;
				}
				else
				{
					this.hdFCode.Value = model2.ContractID;
				}
				this.txtProject.Value = model2.Project.PrjName;
				this.hdnProjectCode.Value = model2.Project.PrjGuid.ToString();
			}
			else
			{
				this.lblTitle.Text = "收入合同登记";
				this.hdFCode.Value = "0";
				this.hdisFContract.Value = "true";
			}
			this.hdIsArchived.Value = "false";
			if (base.UserCode != "00000000")
			{
				this.hdUserCodes.Value = JsonHelper.Serialize(new string[]
				{
					base.UserCode,
					"00000000"
				});
			}
			else
			{
				this.hdUserCodes.Value = JsonHelper.Serialize(new string[]
				{
					"00000000"
				});
			}
		}
		this.FileLink1.MID = 1905;
		this.FileLink1.FID = this.hdGuid.Value;
		this.FileLink1.Type = 1;
	}
	public void InitConBedget()
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			string value = this.hdnProjectCode.Value;
			string value2 = this.hdGuid.Value;
			try
			{
				DataTable conByParent = cn.justwin.Domain.BudContractTask.GetConByParent(value, value2, "", "", "", "", 1);
				this.gvBudget.DataSource = conByParent;
				this.gvBudget.DataBind();
			}
			catch
			{
			}
		}
	}
	public void BindDdl()
	{
		this.ddlBalanceMode.DataSource = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=27 and ParentCodeID=0 AND XPM_Basic_CodeList.IsValid = 1");
		this.ddlBalanceMode.DataBind();
		this.ddlPayMode.DataSource = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=25 and ParentCodeID=0 AND XPM_Basic_CodeList.IsValid = 1");
		this.ddlPayMode.DataBind();
		WebUtil.AddItem(this.ddlBalanceMode, "结算类型");
		WebUtil.AddItem(this.ddlPayMode, "付款方式");
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		int num;
		if (base.Request.QueryString["id"] != null)
		{
			if (this.hdContractCode.Value != this.txtContractCode.Text && this.incometContractBll.GetListArray(" where ContractCode='" + this.txtContractCode.Text + "'").Count > 0)
			{
				base.RegisterScript("top.ui.alert('合同编号已存在！')");
				return;
			}
			num = this.UpdateModel();
		}
		else
		{
			if (this.incometContractBll.GetListArray(" where ContractCode='" + this.txtContractCode.Text + "'").Count > 0)
			{
				base.RegisterScript("top.ui.alert('合同编号已存在！')");
				return;
			}
			num = this.AddModel();
			if (!string.IsNullOrEmpty(base.Request["b"]))
			{
				this.AddContractConfig(base.Request["b"].ToString());
			}
			else
			{
				this.AddContractConfig("");
			}
		}
		this.UpdateContractBudget();
		if (num != 0)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("top.ui.show('" + this.SetMsg() + "成功！');").Append(System.Environment.NewLine);
			stringBuilder.Append("winclose('AddIncometContract','IncometContractList.aspx',true)");
			base.RegisterScript(stringBuilder.ToString());
			return;
		}
		base.RegisterScript("top.ui.alert('" + this.SetMsg() + "失败！');");
	}
	public void UpdateContractBudget()
	{
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		string text = string.Empty;
		string value = this.hdnProjectCode.Value;
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			text = base.Request["id"];
		}
		string value2 = this.isDeteleConBudget.Value;
		if (value2 == "0")
		{
			if (!string.IsNullOrEmpty(text))
			{
				budContractTaskService.DelRalationBudgetAndContract(value, text);
			}
			budContractTaskService.AddRalationBudgetAndContract(value, this.hdGuid.Value, this.hdOrderNumber.Value);
			return;
		}
		if (value2 == "1" && !string.IsNullOrEmpty(text))
		{
			budContractTaskService.DelRalationBudgetAndContract(value, text);
		}
	}
	public int AddModel()
	{
		IncometContractModel model = this.GetModel();
		model.FlowState = -1;
		return this.incometContractBll.Add(model);
	}
	public int UpdateModel()
	{
		return this.incometContractBll.Update(this.GetModel());
	}
	public IncometContractModel GetModel()
	{
		IncometContractModel incometContractModel = new IncometContractModel();
		incometContractModel.Annex = "";
		incometContractModel.BalanceMode = this.ddlBalanceMode.SelectedValue;
		incometContractModel.CllectionCondition = this.txtCllectionCondition.Text;
		incometContractModel.ContractCode = this.txtContractCode.Text;
		incometContractModel.ContractID = this.hdGuid.Value;
		incometContractModel.ContractName = this.txtContractName.Text;
		incometContractModel.ContractPrice = new decimal?(System.Convert.ToDecimal(this.txtContractPrice.Text));
		incometContractModel.EndDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndDate.Text));
		incometContractModel.FCode = this.hdFCode.Value;
		incometContractModel.FileTime = new System.DateTime?(System.Convert.ToDateTime(this.hdFileTime.Value));
		incometContractModel.IsArchived = System.Convert.ToBoolean(this.hdIsArchived.Value);
		incometContractModel.isFContract = System.Convert.ToBoolean(this.hdisFContract.Value);
		incometContractModel.MainProvision = this.txtMainProvision.Text;
		incometContractModel.Party.CorpName = this.txtParty.Value;
		incometContractModel.Party.CorpID = System.Convert.ToInt32(this.hdParty.Value);
		incometContractModel.PayMode = this.ddlPayMode.SelectedValue;
		incometContractModel.Project.PrjGuid = new System.Guid(this.hdnProjectCode.Value);
		incometContractModel.RegisterTime = new System.DateTime?(System.Convert.ToDateTime(this.txtRegisterTime.Text));
		incometContractModel.Remark = this.txtRemark.Text;
		incometContractModel.Second = this.txtSecond.Text;
		incometContractModel.SignedAddress = this.txtSignedAddress.Text;
		incometContractModel.SignedTime = new System.DateTime?(System.Convert.ToDateTime(this.txtSignedTime.Text));
		incometContractModel.StartDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartDate.Text));
		incometContractModel.Subscriber = this.txtSubscriber.Text;
		incometContractModel.TypeID.TypeID = this.hfldTypeID.Value;
		incometContractModel.TypeID.TypeName = this.txtTypeName.Text;
		if (string.IsNullOrEmpty(base.Request.QueryString["id"]))
		{
			string a = ConfigurationManager.AppSettings["ContractPower"].ToString();
			if (a == "0")
			{
				this.hdUserCodes.Value = this.GetContractLimits(incometContractModel.TypeID.TypeID);
			}
			else
			{
				this.hdUserCodes.Value = this.GetContractLimitsByContractTypeAndPrj(incometContractModel.TypeID.TypeID, this.hdnProjectCode.Value);
			}
		}
		incometContractModel.UserCodes = this.hdUserCodes.Value;
		incometContractModel.QualityPeriod = this.txtQualityPeriod.Value;
		incometContractModel.CParty = this.txtCParty.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtReFundDate.Text.Trim()))
		{
			incometContractModel.ReFundDate = new System.DateTime?(System.Convert.ToDateTime(this.txtReFundDate.Text.Trim()));
		}
		else
		{
			incometContractModel.ReFundDate = null;
		}
		if (this.hfldSignPeople.Value.Trim() == "")
		{
			incometContractModel.SignPepole = null;
		}
		else
		{
			incometContractModel.SignPepole = this.hfldSignPeople.Value.Trim();
		}
		if (this.rdbisSign.Checked)
		{
			incometContractModel.Sign = 1;
		}
		else
		{
			if (this.rdbNoSign.Checked)
			{
				incometContractModel.Sign = 0;
			}
		}
		return incometContractModel;
	}
	public string SetMsg()
	{
		if (base.Request.QueryString["id"] != null)
		{
			return "更新";
		}
		return "添加";
	}
	private void AddContractConfig(string contractID)
	{
		ConConfigContract conConfigContract = new ConConfigContract();
		ConConfigContractService conConfigContractService = new ConConfigContractService();
		string contractId = string.Empty;
		if (!string.IsNullOrEmpty(this.txtContractCode.Text.Trim()))
		{
			ConIncometContract byContractCode = new ConIncometContractService().GetByContractCode(this.txtContractCode.Text.Trim());
			contractId = byContractCode.ContractID;
		}
		if (string.IsNullOrEmpty(contractID))
		{
			conConfigContract.Id = System.Guid.NewGuid().ToString();
			conConfigContract.ContractId = contractId;
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
			conConfigContract.ContractId = contractId;
		}
		conConfigContractService.Add(conConfigContract);
	}
	public void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			e.Row.Attributes["orderNumber"] = text;
			int num = text.Length / 3;
			e.Row.Attributes["layer"] = num.ToString();
		}
	}
	public void ShowTaskInfo_click(object sender, System.EventArgs e)
	{
		string id = string.Empty;
		string arg_0B_0 = string.Empty;
		cn.justwin.Domain.Entities.BudContractTask budContractTask = null;
		DataTable dataSource = null;
		string contractID = null;
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			contractID = base.Request["id"];
		}
		if (!string.IsNullOrEmpty(this.hdTaskID.Value))
		{
			id = this.hdTaskID.Value;
			BudContractTaskService budContractTaskService = new BudContractTaskService();
			budContractTask = budContractTaskService.GetTaskById(id);
			this.hdOrderNumber.Value = budContractTask.OrderNumber;
		}
		if (budContractTask != null)
		{
			dataSource = cn.justwin.Domain.BudContractTask.GetConByParent(this.hdnProjectCode.Value, contractID, budContractTask.OrderNumber, "", "", "", 0);
		}
		this.gvBudget.DataSource = dataSource;
		this.gvBudget.DataBind();
	}
	public void BtnDel_click(object sender, System.EventArgs e)
	{
		this.isDeteleConBudget.Value = "1";
		this.gvBudget.DataSource = null;
		this.gvBudget.DataBind();
	}
	private string GetContractLimits(string ContractTypeId)
	{
		System.Collections.Generic.List<string> userCodes = new System.Collections.Generic.List<string>();
		userCodes.Add(base.UserCode);
		if (base.UserCode != "00000000")
		{
			userCodes.Add("00000000");
		}
		ContractType contractType = new ContractType();
		ContractTypeModel model = contractType.GetModel(ContractTypeId);
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
		return JsonHelper.Serialize(userCodes.ToArray());
	}
	private string GetContractLimitsByContractTypeAndPrj(string contractTypeID, string prjId)
	{
		System.Collections.Generic.List<string> UserCodes = new System.Collections.Generic.List<string>();
		System.Collections.Generic.List<string> ztbUsers = (System.Collections.Generic.List<string>)new PTPrjInfoZTBUserService().GetUser(prjId);
		System.Collections.Generic.List<string> userCodesByIDThroughPrjProperty = new PTPrjInfoZTBDetailService().getUserCodesByIDThroughPrjProperty(prjId);
		System.Collections.Generic.List<string> userCode2 = new PrivBusiDataRoleService().GetUserCode(prjId);
		ConContractType byID = new ConContractTypeService().GetByID(contractTypeID);
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
		return JsonHelper.Serialize(UserCodes.ToArray());
	}
	protected void BtnShowProjectInfo_Click(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(this.hdnProjectCode.Value.Trim()))
		{
			return;
		}
		System.Collections.Generic.List<string> prjInfoIncoment = this.prjInfoBll.GetPrjInfoIncoment(this.hdnProjectCode.Value.Trim());
		if (prjInfoIncoment.Count > 0)
		{
			this.txtPrjFundInfo.Value = prjInfoIncoment[3].ToString();
			this.txtPrjFundWorkable.Value = prjInfoIncoment[0].ToString();
			this.txtQualityClass.Value = prjInfoIncoment[2].ToString();
			this.txtForecastProfitRate.Value = prjInfoIncoment[1].ToString();
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


