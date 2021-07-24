using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using cn.justwin.Tender;
using cn.justwin.Web;
using Newtonsoft.Json;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_PrjInfoAdd : NBasePage, IRequiresSessionState
{
	private string isEditProjectCode = ConfigurationManager.AppSettings["IsEditProjectCode"];
	private string prjId = string.Empty;
	private string action = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["PrjId"]))
		{
			this.prjId = base.Request["PrjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.txtAgent.Attributes["ReadOnly"] = "true";
			this.txtName.Attributes["ReadOnly"] = "true";
			this.txtBusinessman.Attributes["ReadOnly"] = "true";
			this.txtOwner.Attributes["ReadOnly"] = "true";
			this.txtDutyPerson.Attributes["ReadOnly"] = "true";
			this.hfldIsEditProjectCode.Value = this.isEditProjectCode;
			this.dropBind();
			if (base.Request["Action"].ToString() == "Add")
			{
				this.hfldPrjGuid.Value = System.Guid.NewGuid().ToString();
				this.FileUpload1.Class = "ProjectFile";
				this.FileUpload1.RecordCode = this.hfldPrjGuid.Value;
				this.hfldPrjState.Value = "17";
				this.hfldPrjPeople.Value = base.UserCode;
				DataTable userInfo = TenderInfo.GetUserInfo(base.UserCode);
				if (userInfo.Rows.Count > 0)
				{
					this.txtName.Text = userInfo.Rows[0]["UserName"].ToString();
					this.txtCorp.Text = userInfo.Rows[0]["CorpName"].ToString();
					this.txtDuty.Text = userInfo.Rows[0]["Duty"].ToString();
					this.txtPhone.Text = userInfo.Rows[0]["Phone"].ToString();
				}
				IApplicationContext context = ContextRegistry.GetContext();
				IProjectCode projectCode = context.GetObject("ProjectCode") as IProjectCode;
				this.txtPrjCode.Value = projectCode.CreateProjectCode();
			}
			if (base.Request["Action"].ToString() == "Update")
			{
				this.hfldPrjGuid.Value = base.Request["PrjId"];
				this.BindTxt(base.Request["PrjId"].ToString());
				this.FileUpload1.Class = "ProjectFile";
				this.FileUpload1.RecordCode = base.Request["PrjId"].ToString();
				return;
			}
		}
		else
		{
			if (!string.IsNullOrEmpty(this.dropprovince.SelectedValue))
			{
				this.bindCity();
			}
		}
	}
	public void dropBind()
	{
		TypeList.BindDrop(this.dropBudgetWay, "ysType", "", null, true);
		TypeList.BindDrop(this.dropContractWay, "cbType", "", null, true);
		TypeList.BindDrop(this.dropKeyPart, "primaryGrade", "", null, true);
		TypeList.BindDrop(this.dropPayCondition, "payment", "", null, true);
		TypeList.BindDrop(this.dropPayWay, "jsType", "", null, true);
		TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
		TypeList.BindDrop(this.dropQualityClass, "ProjectQuality", "", null, true);
		TypeList.BindDrop(this.dropRank, "zzGrade", "", null, true);
		TypeList.BindDrop(this.dropTenderWay, "zbType", "", null, true);
		TypeList.BindDrop(this.dropProperty, "ProjectProperty", "", null, true);
		TypeList.BindBuildingTypeDrop(this.dropBuildingType_0);
		TypeList.BindXmgroupDrop(this.dropXmgroup);
		this.BindProvice();
	}
	public void BindProvice()
	{
		DataTable aLLProvince = new BasicProvinceService().GetALLProvince();
		this.dropprovince.DataSource = aLLProvince;
		this.dropprovince.DataValueField = "Id";
		this.dropprovince.DataTextField = "Name";
		this.dropprovince.DataBind();
		this.dropprovince.Items.Insert(0, new ListItem("请选择", ""));
		this.dropcity.Items.Add(new ListItem("请选择", ""));
	}
	public void bindCity()
	{
		this.dropcity.Items.Clear();
		DataTable dataTable = new DataTable();
		string text = "请选择|";
		if (!string.IsNullOrEmpty(this.dropprovince.SelectedItem.Text))
		{
			dataTable = new BasicCityService().GetDtbByProviceId(this.dropprovince.SelectedItem.Value);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				text = text + dataRow["Name"].ToString() + "|";
			}
		}
		text = text.Substring(0, text.Length - 1);
		string[] array = text.Split(new char[]
		{
			'|'
		});
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j] == "请选择")
			{
				this.dropcity.Items.Add(new ListItem("请选择", ""));
			}
			else
			{
				this.dropcity.Items.Add(new ListItem(array[j], j.ToString()));
				if (array[j] == this.hfldCity.Value)
				{
					this.dropcity.Items[j].Selected = true;
				}
			}
		}
	}
	public void BindTxt(string PrjId)
	{
		ProjectInfo byId = ProjectInfo.GetById(PrjId);
		this.ViewState["oldCode"] = byId.PrjCode;
		this.ViewState["oldName"] = byId.PrjName;
		this.dropXmgroup.SelectedValue = byId.Xmgroup;
		this.txtTelphone.Text = byId.Telephone;
		if (byId.ProgAgent != "")
		{
			this.txtAgent.Text = byId.ProgAgentName;
			this.hfldAgent.Value = byId.ProgAgent;
		}
		this.hfldAgent.Value = byId.ProgAgent;
		this.dropBudgetWay.SelectedValue = byId.BudgetWay;
		this.txtBuildingArea.Text = byId.BuildingArea;
		this.dropContractWay.SelectedValue = byId.ContractWay;
		this.txtDesigner.Text = byId.Designer;
		this.txtDuration.Text = byId.Duration;
		this.txtEndDate.Text = Common2.GetTime(byId.EndDate);
		this.txtBuildingTypeNo.Text = byId.PrjGrade;
		this.txtInspector.Text = byId.Inspector;
		this.dropKeyPart.SelectedValue = byId.KeyPart;
		this.txtOtherStatement.Text = byId.OtherStatement;
		this.hfldOwner.Value = byId.OwnerCode.ToString();
		this.dropPayCondition.SelectedValue = byId.PayCondition;
		this.dropPayWay.SelectedValue = byId.PayWay;
		this.txtPrjCode.Value = byId.PrjCode;
		if (byId.PrjCost.ToString() != "0")
		{
			this.txtPrjCost.Text = byId.PrjCost.ToString();
		}
		this.txtPrjFundInfo.Text = byId.PrjFundInfo;
		this.txtPrjInfo.Text = byId.PrjInfo;
		if (byId.PrjManager != "")
		{
			this.txtPrjManager.Text = byId.PrjManagerName;
			this.hfldPrjManager.Value = byId.PrjManager;
		}
		if (byId.Businessman != "")
		{
			this.txtBusinessman.Text = byId.BusinessmanName;
			this.hfldBusinessman.Value = byId.Businessman;
		}
		this.txtPrjName.Text = byId.PrjName;
		this.txtPrjPlace.Text = byId.PrjPlace;
		this.hfldPrjState.Value = byId.PrjState.ToString();
		this.dropQualityClass.SelectedValue = byId.QualityClass;
		this.dropRank.SelectedValue = byId.Rank;
		this.txtRemark1.Text = byId.Remark;
		this.txtStartDate.Text = Common2.GetTime(byId.StartDate);
		this.dropTenderWay.SelectedValue = byId.TenderWay;
		this.txtTotalHouseNum.Text = byId.TotalHouseNum;
		this.txtUndergroundArea.Text = byId.UndergroundArea;
		this.txtUsegrounArea.Text = byId.UsegrounArea;
		this.txtAfforestArea.Text = byId.AfforestArea;
		this.txtParkArea.Text = byId.ParkArea;
		if (byId.WorkUnit != "")
		{
			this.txtWorkUnit.Text = byId.WorkUnitName;
			this.hfldWorkUnit.Value = byId.WorkUnit;
		}
		this.txtForecastProfitRate.Text = byId.ForecastProfitRate.ToString();
		this.txtEngineeringEstimates.Text = byId.EngineeringEstimates;
		if (byId.PrjDutyPerson != "")
		{
			this.hfldDutyPerson.Value = byId.PrjDutyPerson;
			this.txtDutyPerson.Text = byId.PrjDutyPersonName;
		}
		this.txtApprovalOf.Text = byId.PrjApprovalOf;
		this.txtFundWorkable.Text = byId.PrjFundWorkable;
		this.txtManagementMargin.Text = byId.ManagementMargin.ToString();
		this.txtMigrantQualityMarginRate.Text = byId.MigrantQualityMarginRate.ToString();
		this.txtWithholdingTaxRate.Text = byId.WithholdingTaxRate.ToString();
		this.txtPerformanceBond.Text = byId.PerformanceBond.ToString();
		this.txtElseMargin.Text = byId.ElseMargin.ToString();
		this.txtName.Text = byId.ProjPeopleUserName;
		this.hfldPrjPeople.Value = byId.ProjPeopleCode;
		this.txtCorp.Text = byId.ProjPeopleCorp;
		this.txtDuty.Text = byId.ProjPeopleDuty;
		this.txtPhone.Text = byId.ProjPeopleTel;
		this.txtOwner.Text = byId.OwnerName;
		this.txtOwnerLinkMan.Text = byId.OwnerLinkMan;
		this.txtOwnerLinkManPhone.Text = byId.OwnerLinkPhone;
		this.txtOwnerAddress.Text = byId.OwnerAddress;
		this.txtPrjManagerRequire.Text = byId.PrjManagerRequire;
		this.txtTechnicalLeaderRequire.Text = byId.TechnicalLeaderRequire;
		if (!string.IsNullOrEmpty(byId.Province))
		{
			this.dropprovince.Items.FindByText(byId.Province).Selected = true;
		}
		if (!string.IsNullOrEmpty(byId.City))
		{
			this.hfldCity.Value = byId.City;
			this.bindCity();
		}
		this.hfldEngTypeJson.Value = JsonConvert.SerializeObject(byId.EngineeringTypes);
		this.hfldPrjKindJson.Value = JsonHelper.JsonSerializer<ProjectKind[]>(byId.PrjTypes.ToArray<ProjectKind>());
		this.hfldRankJson.Value = JsonHelper.JsonSerializer<ProjectRank[]>(byId.PrjRanks.ToArray<ProjectRank>());
		if (!string.IsNullOrWhiteSpace(byId.PrjReadOne))
		{
			this.hfldPrjReadOne.Value = byId.PrjReadOne;
			PtYhmc modelById = new PtYhmcBll().GetModelById(byId.PrjReadOne);
			if (modelById != null)
			{
				this.txtPrjReadOne.Text = modelById.v_xm;
			}
		}
		this.txtInfoOrigin.Text = byId.ProjInfoOrigin;
		this.txtElseRequest.Text = byId.ProjElseRequest;
		this.dropProperty.SelectedValue = byId.PrjProperty;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.txtPrjCode.Value) && !string.IsNullOrEmpty(this.txtPrjName.Text.Trim()) && !string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			string value = this.txtEndDate.Text.Trim();
			if (!string.IsNullOrEmpty(value))
			{
				string value2 = this.txtStartDate.Text.Trim();
				int num = System.Convert.ToDateTime(value).CompareTo(System.Convert.ToDateTime(value2));
				if (num < 0)
				{
					base.RegisterShow("系统提示", "计划结束日期不能小于计划开始日期，请重新选择日期！");
					return;
				}
			}
			if (this.action == "Add")
			{
				try
				{
					bool flag = ProjectInfo.IsSameCode(this.txtPrjCode.Value.Trim());
					bool flag2 = TenderInfo.IsSameName(this.txtPrjName.Text.Trim());
					if (!flag)
					{
						if (!flag2)
						{
							ProjectInfo model = this.getModel(null);
							model.InputDate = new System.DateTime?(System.DateTime.Now);
							model.InputUser = base.UserCode;
							model.PrjGuid = new System.Guid(this.hfldPrjGuid.Value);
							model.Add(model);
							if (!string.IsNullOrEmpty(model.PrjReadOne))
							{
								PTDBSJService pTDBSJService = new PTDBSJService();
								pTDBSJService.Add(new PTDBSJ
								{
									I_XGID = model.PrjGuid.ToString(),
									V_LXBM = "024",
									V_YHDM = model.PrjReadOne,
									DTM_DBSJ = new System.DateTime?(System.DateTime.Now),
									C_OpenFlag = "0",
									V_Content = "项目：" + model.PrjName + "已经编制成功",
									V_DBLJ = "PrjManager/PrjInfoView.aspx?ic=" + model.PrjGuid.ToString(),
									V_TPLJ = "new_Mail.gif"
								});
							}
							string str = "PrjInfoList.aspx";
							base.RegisterScript("winclose('PrjInfoAdd', '" + str + "', true);");
						}
						else
						{
							base.RegisterShow("系统提示", "该项目名称已经存在");
						}
					}
					else
					{
						if (!flag2)
						{
							IApplicationContext context = ContextRegistry.GetContext();
							IProjectCode projectCode = context.GetObject("ProjectCode") as IProjectCode;
							string text = projectCode.CreateProjectCode();
							ProjectInfo model2 = this.getModel(null);
							model2.InputDate = new System.DateTime?(System.DateTime.Now);
							model2.InputUser = base.UserCode;
							string prjGuid = "";
							model2.TypeCode = ProjectInfo.GetTypeCode(prjGuid);
							model2.PrjCode = text;
							model2.Add(model2);
							if (!string.IsNullOrEmpty(model2.PrjReadOne))
							{
								PTDBSJService pTDBSJService2 = new PTDBSJService();
								pTDBSJService2.Add(new PTDBSJ
								{
									I_XGID = model2.PrjGuid.ToString(),
									V_LXBM = "024",
									V_YHDM = model2.PrjReadOne,
									DTM_DBSJ = new System.DateTime?(System.DateTime.Now),
									C_OpenFlag = "0",
									V_Content = "项目：" + model2.PrjName + "已经编制成功",
									V_DBLJ = "PrjManager/PrjInfoView.aspx?ic=" + model2.PrjGuid.ToString(),
									V_TPLJ = "new_Mail.gif"
								});
							}
							string str2 = "添加成功，但编号为" + this.txtPrjCode.Value.Trim() + "的项目已经存在，当前项目的项目编号已经保存为" + text;
							string text2 = "PrjInfoList.aspx";
							string str3 = "?returnMsg=" + str2;
							text2 += str3;
							base.RegisterScript("winclose('PrjInfoAdd', '" + text2 + "', true);");
						}
						else
						{
							base.RegisterShow("系统提示", "该项目名称已经存在");
						}
					}
					return;
				}
				catch (System.Exception ex)
				{
					Log4netHelper.Error(ex, "项目立项添加失败", base.UserCode);
					base.RegisterShow("系统提示", "添加失败");
					return;
				}
			}
			try
			{
				bool flag3 = ProjectInfo.UpdateIsSameCode(this.ViewState["oldCode"].ToString(), this.txtPrjCode.Value.Trim());
				bool flag4 = TenderInfo.UpdateIsSameName(this.ViewState["oldName"].ToString(), this.txtPrjName.Text.Trim());
				if (!flag3)
				{
					if (!flag4)
					{
						ProjectInfo projectInfo = ProjectInfo.GetById(base.Request["PrjId"].ToString());
						if (projectInfo != null)
						{
							projectInfo = this.getModel(projectInfo);
							projectInfo.Update(projectInfo, base.Request["PrjId"].ToString());
							if (!string.IsNullOrEmpty(projectInfo.PrjReadOne))
							{
								PTDBSJService pTDBSJService3 = new PTDBSJService();
								pTDBSJService3.Add(new PTDBSJ
								{
									I_XGID = projectInfo.PrjGuid.ToString(),
									V_LXBM = "024",
									V_YHDM = projectInfo.PrjReadOne,
									DTM_DBSJ = new System.DateTime?(System.DateTime.Now),
									C_OpenFlag = "0",
									V_Content = "项目：" + projectInfo.PrjName + "已经编制成功",
									V_DBLJ = "PrjManager/PrjInfoView.aspx?ic=" + projectInfo.PrjGuid.ToString(),
									V_TPLJ = "new_Mail.gif"
								});
							}
							new System.Text.StringBuilder();
							string text3 = "PrjInfoList.aspx";
							if (!string.IsNullOrEmpty(base.Request.QueryString["purl"]))
							{
								text3 = base.Request["purl"].ToString();
							}
							string str4 = "?returnMsg=更新成功";
							text3 += str4;
							base.RegisterScript("winclose('PrjInfoAdd', '" + text3 + "', true);");
						}
						else
						{
							base.RegisterShow("系统提示", "该项目已经被删除，不能进行编辑");
						}
					}
					else
					{
						base.RegisterShow("系统提示", "该项目名称已经存在");
					}
				}
				else
				{
					base.RegisterShow("系统提示", "该项目编号已经存在");
				}
				return;
			}
			catch (System.Exception ex2)
			{
				Log4netHelper.Error(ex2, "项目立项编辑失败", base.UserCode);
				base.RegisterShow("系统提示", "更新失败");
				return;
			}
		}
		base.RegisterShow("系统提示", "项目名称和计划开始日期都不能为空!");
	}
	public ProjectInfo getModel(ProjectInfo project)
	{
		if (project == null)
		{
			project = new ProjectInfo();
			project.PrjState = new int?(1);
			project.TypeCode = ProjectInfo.GetTypeCode(this.prjId);
		}
		project.PrjGuid = new System.Guid(this.hfldPrjGuid.Value);
		project.BudgetWay = this.dropBudgetWay.SelectedValue;
		project.BuildingArea = this.txtBuildingArea.Text.Trim();
		project.PrjGrade = this.txtBuildingTypeNo.Text.Trim();
		project.Businessman = this.hfldBusinessman.Value;
		project.ContractWay = this.dropContractWay.SelectedValue;
		project.Designer = this.txtDesigner.Text.Trim();
		project.Duration = this.txtDuration.Text.Trim();
		System.DateTime? endDate = null;
		if (this.txtEndDate.Text != "")
		{
			endDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndDate.Text.Trim()));
		}
		project.EndDate = endDate;
		project.EngineeringSubType = this.dropvalue.Value;
		project.EngineeringType = this.dropBuildingType_0.SelectedValue;
		project.Grade = this.txtDetailGrade_0.Text.Trim();
		project.Inspector = this.txtInspector.Text.Trim();
		project.IsTender = false;
		project.KeyPart = this.dropKeyPart.SelectedValue;
		project.OtherStatement = this.txtOtherStatement.Text.Trim();
		int? ownerCode = null;
		if (this.hfldOwner.Value != "")
		{
			ownerCode = new int?(int.Parse(this.hfldOwner.Value));
		}
		project.OwnerCode = ownerCode;
		project.PayCondition = this.dropPayCondition.SelectedValue;
		project.PayWay = this.dropPayWay.SelectedValue;
		project.PrjCode = this.txtPrjCode.Value.Trim();
		double? prjCost = null;
		if (this.txtPrjCost.Text.Trim() != "")
		{
			prjCost = new double?(System.Convert.ToDouble(this.txtPrjCost.Text));
		}
		project.PrjCost = prjCost;
		project.PrjFundInfo = this.txtPrjFundInfo.Text.Trim();
		project.PrjGrade = this.txtBuildingTypeNo.Text.Trim();
		project.PrjInfo = this.txtPrjInfo.Text.Trim();
		project.PrjManager = this.txtPrjManager.Text.Trim();
		project.PrjName = this.txtPrjName.Text.Trim();
		project.PrjPlace = this.txtPrjPlace.Text.Trim();
		project.PrjState = new int?(int.Parse(this.hfldPrjState.Value));
		project.ProgAgent = this.hfldAgent.Value;
		project.QualityClass = this.dropQualityClass.SelectedValue;
		project.Remark = this.txtRemark1.Text.Trim();
		System.DateTime? startDate = null;
		if (this.txtStartDate.Text != "")
		{
			startDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartDate.Text));
		}
		project.StartDate = startDate;
		project.Telephone = this.txtTelphone.Text.Trim();
		project.TenderWay = this.dropTenderWay.SelectedValue;
		project.TotalHouseNum = this.txtTotalHouseNum.Text.Trim();
		project.UndergroundArea = this.txtUndergroundArea.Text.Trim();
		project.UsegrounArea = this.txtUsegrounArea.Text.Trim();
		project.AfforestArea = this.txtAfforestArea.Text.Trim();
		project.ParkArea = this.txtParkArea.Text.Trim();
		project.UserCode = base.UserCode;
		project.WorkUnit = this.txtWorkUnit.Text.Trim();
		project.Xmgroup = this.dropXmgroup.SelectedValue;
		project.OwnerLinkMan = this.txtOwnerLinkMan.Text.Trim();
		project.OwnerLinkPhone = this.txtOwnerLinkManPhone.Text.Trim();
		decimal? forecastProfitRate = null;
		if (this.txtForecastProfitRate.Text != "")
		{
			forecastProfitRate = new decimal?(System.Convert.ToDecimal(this.txtForecastProfitRate.Text.Trim()));
		}
		project.ForecastProfitRate = forecastProfitRate;
		project.EngineeringEstimates = this.txtEngineeringEstimates.Text.Trim();
		project.PrjDutyPerson = this.hfldDutyPerson.Value;
		project.PrjApprovalOf = this.txtApprovalOf.Text.Trim();
		project.PrjFundWorkable = this.txtFundWorkable.Text.Trim();
		decimal? managementMargin = null;
		if (this.txtManagementMargin.Text.Trim() != "")
		{
			managementMargin = new decimal?(System.Convert.ToDecimal(this.txtManagementMargin.Text.Trim()));
		}
		project.ManagementMargin = managementMargin;
		decimal? migrantQualityMarginRate = null;
		if (this.txtMigrantQualityMarginRate.Text.Trim() != "")
		{
			migrantQualityMarginRate = new decimal?(System.Convert.ToDecimal(this.txtMigrantQualityMarginRate.Text.Trim()));
		}
		project.MigrantQualityMarginRate = migrantQualityMarginRate;
		decimal? withholdingTaxRate = null;
		if (this.txtWithholdingTaxRate.Text.Trim() != "")
		{
			withholdingTaxRate = new decimal?(System.Convert.ToDecimal(this.txtWithholdingTaxRate.Text.Trim()));
		}
		project.WithholdingTaxRate = withholdingTaxRate;
		decimal? performanceBond = null;
		if (this.txtPerformanceBond.Text.Trim() != "")
		{
			performanceBond = new decimal?(System.Convert.ToDecimal(this.txtPerformanceBond.Text.Trim()));
		}
		project.PerformanceBond = performanceBond;
		decimal? elseMargin = null;
		if (this.txtElseMargin.Text.Trim() != "")
		{
			elseMargin = new decimal?(System.Convert.ToDecimal(this.txtElseMargin.Text.Trim()));
		}
		project.ElseMargin = elseMargin;
		project.ActualRunDate = null;
		project.ProjPeopleCode = this.hfldPrjPeople.Value;
		project.ProjPeopleUserName = this.txtName.Text.ToString().Trim();
		project.ProjPeopleCorp = this.txtCorp.Text.ToString().Trim();
		project.ProjPeopleDuty = this.txtDuty.Text.ToString().Trim();
		project.ProjPeopleTel = this.txtPhone.Text.ToString().Trim();
		project.OwnerAddress = this.txtOwnerAddress.Text.ToString().Trim();
		project.PrjManagerRequire = this.txtPrjManagerRequire.Text.Trim();
		project.TechnicalLeaderRequire = this.txtTechnicalLeaderRequire.Text.Trim();
		if (!string.IsNullOrEmpty(this.dropprovince.SelectedValue))
		{
			project.Province = this.dropprovince.SelectedItem.Text;
			if (!string.IsNullOrEmpty(this.hfldCity.Value))
			{
				if (this.hfldCity.Value == "请选择")
				{
					project.City = string.Empty;
				}
				else
				{
					project.City = this.hfldCity.Value;
				}
			}
		}
		project.EngineeringTypes = JsonHelper.JsonDeserialize<EngineeringType[]>(this.hfldEngTypeJson.Value);
		project.PrjTypes = JsonHelper.JsonDeserialize<ProjectKind[]>(this.hfldPrjKindJson.Value);
		project.PrjRanks = JsonHelper.JsonDeserialize<ProjectRank[]>(this.hfldRankJson.Value);
		project.PrjReadOne = this.hfldPrjReadOne.Value;
		project.ProjInfoOrigin = this.txtInfoOrigin.Text;
		project.ProjElseRequest = this.txtElseRequest.Text;
		project.PrjProperty = this.dropProperty.SelectedValue;
		return project;
	}
}


