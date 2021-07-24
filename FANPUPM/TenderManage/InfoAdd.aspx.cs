using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Project;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using cn.justwin.Tender;
using Newtonsoft.Json;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_TenderManage_InfoAdd : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string prjGuid = string.Empty;
	private string purl = string.Empty;
	private string SignUpWarnDay = ConfigurationManager.AppSettings["SignUpWarnDay"];
	private string isEditProjectCode = ConfigurationManager.AppSettings["IsEditProjectCode"];

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["PrjId"]))
		{
			this.prjGuid = base.Request["PrjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["purl"]))
		{
			this.purl = base.Request["purl"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindDrop();
			if (this.action == "Add")
			{
				this.divHeight.Value = this.action;
				this.hfldPrjGuid.Value = System.Guid.NewGuid().ToString();
				this.FileUpload1.Class = "ProjectFile";
				this.FileUpload1.RecordCode = this.hfldPrjGuid.Value + "_" + ProjectParameter.PreApproval;
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
				this.txtPrjCode.Value = projectCode.CreateTenderCode();
			}
			if (this.action == "Update")
			{
				this.hfldPrjGuid.Value = this.prjGuid;
				this.InitialTender(this.prjGuid);
				this.FileUpload1.Class = "ProjectFile";
				this.FileUpload1.RecordCode = this.prjGuid + "_" + ProjectParameter.PreApproval;
				this.FileStart.Class = "ProjectFile";
				this.FileStart.RecordCode = this.prjGuid + "_" + ProjectParameter.Initiate;
				this.Fileys.Class = "ProjectFile";
				this.Fileys.RecordCode = this.prjGuid + "_" + ProjectParameter.Prequalification;
				this.Fileyspass.Class = "ProjectFile";
				this.Fileyspass.RecordCode = this.prjGuid + "_" + ProjectParameter.QualificationPass;
				this.Fileysfail.Class = "ProjectFile";
				this.Fileysfail.RecordCode = this.prjGuid + "_" + ProjectParameter.QualificationFail;
				this.FileTender.Class = "ProjectFile";
				this.FileTender.RecordCode = this.prjGuid + "_" + ProjectParameter.Bid;
				this.FileSuccessBid.Class = "ProjectFile";
				this.FileSuccessBid.RecordCode = this.prjGuid + "_" + ProjectParameter.WinBid;
				this.FileOutBid.Class = "ProjectFile";
				this.FileOutBid.RecordCode = this.prjGuid + "_" + ProjectParameter.OutBid;
				return;
			}
		}
		else
		{
			if (!string.IsNullOrEmpty(this.dropprovince.SelectedValue))
			{
				this.BindCity();
			}
		}
	}
	protected void btnSaveData_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldstate.Value;
		string value2 = this.hfldPrjGuid.Value;
		TenderInfo byId = TenderInfo.GetById(value2);
		if (value == ProjectParameter.Initiate)
		{
			System.DateTime? projStartDate = null;
			if (!string.IsNullOrEmpty(this.txtProjStartDate.Text))
			{
				projStartDate = new System.DateTime?(System.Convert.ToDateTime(this.txtProjStartDate.Text));
			}
			byId.ProjStartDate = projStartDate;
			byId.ProjStartRemark = this.txtStartRemark.Text;
		}
		else
		{
			if (value == ProjectParameter.Prequalification)
			{
				System.DateTime? projApprovalDate = null;
				if (!string.IsNullOrEmpty(this.txtApprovalDate.Text))
				{
					projApprovalDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApprovalDate.Text));
				}
				byId.ProjApprovalDate = projApprovalDate;
				System.DateTime? projApplyDate = null;
				if (!string.IsNullOrEmpty(this.txtApplyDate.Text))
				{
					projApplyDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApplyDate.Text));
				}
				byId.ProjApplyDate = projApplyDate;
				byId.ProgAgent = this.hfldAgent.Value;
				System.DateTime? projTenderDate = null;
				if (!string.IsNullOrEmpty(this.txtTenderDate.Text))
				{
					projTenderDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderDate.Text));
				}
				byId.ProjTenderDate = projTenderDate;
				int? projRegistDeadline = null;
				if (!string.IsNullOrEmpty(this.txtRegistDeadline.Text))
				{
					projRegistDeadline = new int?(int.Parse(this.txtRegistDeadline.Text));
				}
				byId.ProjRegistDeadline = projRegistDeadline;
				byId.PrequalificationRequire = this.txtysdetail.Text;
			}
			else
			{
				if (value == ProjectParameter.QualificationPass)
				{
					System.DateTime? qualificationPassDate = null;
					if (!string.IsNullOrEmpty(this.txtPassDate.Text))
					{
						qualificationPassDate = new System.DateTime?(System.Convert.ToDateTime(this.txtPassDate.Text));
					}
					byId.QualificationPassDate = qualificationPassDate;
					byId.QualificationPassReason = this.txtPassReason.Text.Trim();
				}
				else
				{
					if (value == ProjectParameter.QualificationFail)
					{
						System.DateTime? qualificationFailData = null;
						if (!string.IsNullOrEmpty(this.txtFailDate.Text))
						{
							qualificationFailData = new System.DateTime?(System.Convert.ToDateTime(this.txtFailDate.Text));
						}
						byId.QualificationFailData = qualificationFailData;
						byId.QualificationFailReason = this.txtFailReason.Text.Trim();
					}
					else
					{
						if (value == ProjectParameter.Bid)
						{
							byId.ProjTenderBeginDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderBeginDate.Text));
							decimal? tenderAverage = null;
							if (!string.IsNullOrEmpty(this.txtTenderAverage.Text))
							{
								tenderAverage = new decimal?(System.Convert.ToDecimal(this.txtTenderAverage.Text));
							}
							byId.TenderAverage = tenderAverage;
							decimal? tenderCeilingPrice = null;
							if (!string.IsNullOrEmpty(this.txtTenderCeilingPrice.Text))
							{
								tenderCeilingPrice = new decimal?(System.Convert.ToDecimal(this.txtTenderCeilingPrice.Text));
							}
							byId.TenderCeilingPrice = tenderCeilingPrice;
							decimal? projTenderEarnestMoney = null;
							if (!string.IsNullOrEmpty(this.txtTenderEarnestMoney.Text))
							{
								projTenderEarnestMoney = new decimal?(System.Convert.ToDecimal(this.txtTenderEarnestMoney.Text));
							}
							byId.ProjTenderEarnestMoney = projTenderEarnestMoney;
							decimal? tenderQuote = null;
							if (!string.IsNullOrEmpty(this.txtTenderQuote.Text))
							{
								tenderQuote = new decimal?(System.Convert.ToDecimal(this.txtTenderQuote.Text));
							}
							byId.TenderQuote = tenderQuote;
							byId.TenderUnit = this.txtTenderUnit.Text;
							byId.TenderAppraiseMethod = this.dropTenderAppraiseMethod.SelectedValue;
							byId.ProjTenderCostContent = this.txtTenderCostContent.Text;
							byId.TenderReadOne = this.hfldTenderReadOne.Value;
							System.DateTime? projTenderAnswerDate = null;
							if (!string.IsNullOrEmpty(this.txtTenderAnswerDate.Text))
							{
								projTenderAnswerDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderAnswerDate.Text));
							}
							byId.ProjTenderAnswerDate = projTenderAnswerDate;
							byId.ProjTenderPayWay = this.dropTenderPayWay.SelectedValue;
							byId.ProjTenderRemark = this.txtTenderRemark.Text;
							byId.ProjTenderContent = this.txtTenderContent.Text;
							byId.PrjManager = this.txtPrjManager.Text;
						}
						else
						{
							if (value == ProjectParameter.WinBid)
							{
								System.DateTime? successBidDate = null;
								if (!string.IsNullOrEmpty(this.txtSuccessBidDate.Text))
								{
									successBidDate = new System.DateTime?(System.Convert.ToDateTime(this.txtSuccessBidDate.Text));
								}
								byId.SuccessBidDate = successBidDate;
								decimal? successBidPrice = null;
								if (!string.IsNullOrEmpty(this.txtSuccessBidPrice.Text))
								{
									successBidPrice = new decimal?(System.Convert.ToDecimal(this.txtSuccessBidPrice.Text));
								}
								byId.SuccessBidPrice = successBidPrice;
								byId.SuccessBidRemark = this.txtSuccessBidRemark.Text;
							}
							else
							{
								if (value == ProjectParameter.OutBid)
								{
									System.DateTime? outBidDate = null;
									if (!string.IsNullOrEmpty(this.txtOutBidDate.Text))
									{
										outBidDate = new System.DateTime?(System.Convert.ToDateTime(this.txtOutBidDate.Text));
									}
									byId.OutBidDate = outBidDate;
									bool? outBidIsReturn = null;
									if (this.RblOutBidIsReturn.SelectedValue == "0")
									{
										outBidIsReturn = new bool?(false);
									}
									else
									{
										if (this.RblOutBidIsReturn.SelectedValue == "1")
										{
											outBidIsReturn = new bool?(true);
										}
									}
									byId.OutBidIsReturn = outBidIsReturn;
									byId.OutBidRemark = this.txtOutBidRemark.Text;
								}
							}
						}
					}
				}
			}
		}
		byId.PrjState = int.Parse(value);
		byId.UpdatePart(byId, value);
		this.ReflushImg(value);
		this.hfldMaxState.Value = value;
		this.txtPrjManager.Text = byId.GetUserName(this.hfldPrjManager.Value);
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(this.txtPrjCode.Value) || string.IsNullOrEmpty(this.txtPrjName.Text.Trim()))
		{
			base.RegisterScript("top.ui.alert('项目名称和计划开始日期');");
			return;
		}
		string value = this.txtEndDate.Text.Trim();
		if (!string.IsNullOrEmpty(value))
		{
			string value2 = this.txtStartDate.Text.Trim();
			int num = System.Convert.ToDateTime(value).CompareTo(System.Convert.ToDateTime(value2));
			if (num < 0)
			{
				base.RegisterScript("top.ui.alert('计划结束日期不能小于计划开始日期，请重新选择日期！');");
				return;
			}
		}
		if (this.action == "Add")
		{
			try
			{
				bool flag = TenderInfo.IsSameCode(this.txtPrjCode.Value.Trim());
				bool flag2 = TenderInfo.IsSameName(this.txtPrjName.Text.Trim());
				if (flag2)
				{
					base.RegisterScript("top.ui.alert('该项目名称已经存在');");
					return;
				}
				string prjCode = this.txtPrjCode.Value.Trim();
				if (flag)
				{
					IApplicationContext context = ContextRegistry.GetContext();
					IProjectCode projectCode = context.GetObject("ProjectCode") as IProjectCode;
					prjCode = projectCode.CreateTenderCode();
				}
				TenderInfo tenderInfo = this.CreateTenderInfo(null);
				tenderInfo.PrjCode = prjCode;
				tenderInfo.Add(tenderInfo);
				this.MsgReadOne(tenderInfo.PrjGuid.ToString(), tenderInfo.PrjName, tenderInfo.PrjReadOne);
				base.RegisterScript("winclose('InfoAdd', 'InfoSetUp.aspx', true);");
				System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(this.AddToPrjInfo));
				return;
			}
			catch (System.Exception)
			{
				base.RegisterScript("top.ui.alert('添加失败');");
				return;
			}
		}
		string value3 = this.hfldMaxState.Value;
		if (value3 == ProjectParameter.Initiate)
		{
			if (string.IsNullOrEmpty(this.txtProjStartDate.Text.Trim()))
			{
				base.RegisterScript("top.ui.alert('启动日期不能为空');");
				return;
			}
		}
		else
		{
			if (value3 == ProjectParameter.Prequalification || value3 == ProjectParameter.QualificationPass || value3 == ProjectParameter.QualificationFail)
			{
				if (string.IsNullOrEmpty(this.txtProjStartDate.Text.Trim()))
				{
					base.RegisterScript("top.ui.alert('启动日期不能为空');");
					return;
				}
				if (string.IsNullOrEmpty(this.txtApprovalDate.Text.Trim()))
				{
					base.RegisterScript("top.ui.alert('预审日期不能为空');");
					return;
				}
			}
			else
			{
				if (value3 == ProjectParameter.Bid || value3 == ProjectParameter.WinBid || value3 == ProjectParameter.OutBid)
				{
					if (string.IsNullOrEmpty(this.txtProjApplyDate.Text.Trim()))
					{
						base.RegisterScript("top.ui.alert('报名日期不能为空');");
						return;
					}
					if (string.IsNullOrEmpty(this.txtApprovalDate.Text.Trim()))
					{
						base.RegisterScript("top.ui.alert('预审日期不能为空');");
						return;
					}
					if (string.IsNullOrEmpty(this.txtTenderBeginDate.Text.Trim()))
					{
						base.RegisterScript("top.ui.alert('开标日期不能为空');");
						return;
					}
					if (value3 == ProjectParameter.WinBid)
					{
						base.Request["PrjId"].ToString();
						PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
						PTPrjInfo byId = pTPrjInfoService.GetById(this.prjGuid);
						if (byId != null)
						{
							byId.PrjCode = this.txtPrjCode.Value;
							pTPrjInfoService.Update(byId);
						}
					}
				}
			}
		}
		bool flag3 = TenderInfo.UpdateIsSameCode(this.ViewState["oldCode"].ToString(), this.txtPrjCode.Value.Trim());
		bool flag4 = TenderInfo.UpdateIsSameName(this.ViewState["oldName"].ToString(), this.txtPrjName.Text.Trim());
		if (flag3)
		{
			base.RegisterScript("top.ui.alert('该项目编号已经存在');");
			return;
		}
		if (!flag4)
		{
			TenderInfo tenderInfo2 = TenderInfo.GetById(this.prjGuid);
			if (tenderInfo2 != null)
			{
				tenderInfo2 = this.CreateTenderInfo(tenderInfo2);
				try
				{
					tenderInfo2.UpdateMultiplePart(tenderInfo2, tenderInfo2.PrjState.ToString());
					this.MsgReadOne(tenderInfo2.PrjGuid.ToString(), tenderInfo2.PrjName, tenderInfo2.PrjReadOne);
					this.MsgReadOne(tenderInfo2.PrjGuid.ToString(), tenderInfo2.PrjName, this.hfldQualificationReadOne.Value);
					if (!string.IsNullOrEmpty(this.txtApplyDate.Text.Trim()))
					{
						PTDBSJTodayService pTDBSJTodayService = new PTDBSJTodayService();
						System.Collections.Generic.List<PTDBSJToday> list = (
							from dbsj in pTDBSJTodayService
							where dbsj.I_XGID == this.prjGuid
							select dbsj).ToList<PTDBSJToday>();
						foreach (PTDBSJToday current in list)
						{
							pTDBSJTodayService.Delete(current);
						}
						System.Collections.Generic.List<TenderUser> byId2 = TenderUser.GetById(tenderInfo2.PrjGuid.ToString());
						System.DateTime value4 = System.Convert.ToDateTime(this.txtApplyDate.Text.Trim()).AddDays((double)(-(double)System.Convert.ToInt32(this.SignUpWarnDay)));
						foreach (TenderUser current2 in byId2)
						{
							pTDBSJTodayService.Add(new PTDBSJToday
							{
								I_XGID = this.prjGuid,
								V_LXBM = "027",
								V_YHDM = current2.UserCode,
								DTM_DBSJ = new System.DateTime?(value4),
								V_TPLJ = "",
								V_DBLJ = "TenderManage/InfoView.aspx?ic=" + this.prjGuid,
								V_Content = "名称为：" + tenderInfo2.PrjName + "的项目已经开始报名。",
								C_OpenFlag = "1"
							});
						}
					}
					string str = "InfoSetUp.aspx";
					if (!string.IsNullOrEmpty(base.Request.QueryString["purl"]))
					{
						str = this.purl;
					}
					base.RegisterScript("winclose('InfoAdd', '" + str + "', true);");
					return;
				}
				catch (System.Exception)
				{
					base.RegisterScript("top.ui.alert('添加失败');");
					return;
				}
			}
			base.RegisterScript("top.ui.alert('该项目已经被删除，不能进行编辑');");
			return;
		}
		base.RegisterScript("top.ui.alert('该项目名称已经存在');");
		return;
	}
	private void BindDrop()
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
		TypeList.BindDrop(this.dropTenderAppraiseMethod, "TenderAppraiseMethod", "", null, true);
		TypeList.BindDrop(this.dropTenderPayWay, "PaymentMethods", "", null, true);
		this.hfldIsEditProjectCode.Value = this.isEditProjectCode;
		this.BindProvice();
	}
	private void BindProvice()
	{
		DataTable aLLProvince = new BasicProvinceService().GetALLProvince();
		this.dropprovince.DataSource = aLLProvince;
		this.dropprovince.DataValueField = "Id";
		this.dropprovince.DataTextField = "Name";
		this.dropprovince.DataBind();
		this.dropprovince.Items.Insert(0, new ListItem("请选择", ""));
		this.dropcity.Items.Add(new ListItem("请选择", ""));
	}
	private void BindCity()
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
	private void InitialTender(string PrjId)
	{
		TenderInfo byId = TenderInfo.GetById(PrjId);
		string text = byId.PrjState.ToString();
		this.divHeight.Value = this.action + text;
		this.hfldMaxState.Value = text;
		this.DisplayTenderImg(text);
		this.ViewState["oldCode"] = byId.PrjCode;
		this.ViewState["oldName"] = byId.PrjName;
		this.txtName.Text = byId.ProjPeopleUserName;
		this.hfldPrjPeople.Value = byId.ProjPeopleCode;
		this.txtCorp.Text = byId.ProjPeopleCorp;
		this.txtDuty.Text = byId.ProjPeopleDuty;
		this.txtPhone.Text = byId.ProjPeopleTel;
		if (byId.OwnerCode.HasValue)
		{
			this.txtOwner.Text = byId.OwnerName;
			this.hfldOwner.Value = byId.OwnerCode.Value.ToString();
		}
		this.txtOwnerLinkMan.Text = byId.OwnerLinkManName;
		this.txtOwnerLinkManPhone.Text = byId.OwnerLinkPhone;
		this.txtOwnerAddress.Text = byId.OwnerAddress;
		this.txtPrjCode.Value = byId.PrjCode;
		this.txtPrjName.Text = byId.PrjName;
		this.txtStartDate.Text = this.ConvertDateTime(byId.StartDate);
		this.txtEndDate.Text = this.ConvertDateTime(byId.EndDate);
		if (!string.IsNullOrEmpty(byId.Province))
		{
			this.dropprovince.Items.FindByText(byId.Province).Selected = true;
		}
		if (!string.IsNullOrEmpty(byId.City))
		{
			this.hfldCity.Value = byId.City;
			this.BindCity();
		}
		this.txtPrjPlace.Text = byId.PrjPlace;
		this.txtDuration.Text = byId.Duration;
		this.txtEngineeringEstimates.Text = byId.EngineeringEstimates;
		if (byId.PrjCost.ToString() != "0")
		{
			this.txtPrjCost.Text = byId.PrjCost.ToString();
		}
		this.txtForecastProfitRate.Text = byId.ForecastProfitRate.ToString();
		this.hfldPrjKindJson.Value = JsonHelper.JsonSerializer<ProjectKind[]>(byId.PrjKinds.ToArray<ProjectKind>());
		this.dropQualityClass.SelectedValue = byId.QualityClass;
		this.txtDesigner.Text = byId.Designer;
		this.dropProperty.SelectedValue = byId.PrjProperty;
		this.txtApprovalOf.Text = byId.PrjApprovalOf;
		this.txtCounsellor.Text = byId.Counsellor;
		this.txtInspector.Text = byId.Inspector;
		this.txtPrjManagerRequire.Text = byId.PrjManagerRequire;
		this.txtTechnicalLeaderRequire.Text = byId.TechnicalLeaderRequire;
		this.txtPrjInfo.Text = byId.PrjInfo;
		this.txtInfoOrigin.Text = byId.ProjInfoOrigin;
		this.txtElseRequest.Text = byId.ProjElseRequest;
		this.txtRemark1.Text = byId.Remark;
		if (!string.IsNullOrEmpty(byId.PrjDutyPerson))
		{
			this.txtDutyPerson.Text = byId.PrjDutyPersonName;
			this.hfldDutyPerson.Value = byId.PrjDutyPerson;
		}
		this.hfldRankJson.Value = JsonHelper.JsonSerializer<ProjectRank[]>(byId.PrjRanks.ToArray<ProjectRank>());
		this.dropContractWay.SelectedValue = byId.ContractWay;
		this.dropPayCondition.SelectedValue = byId.PayCondition;
		this.dropTenderWay.SelectedValue = byId.TenderWay;
		this.dropPayWay.SelectedValue = byId.PayWay;
		this.dropBudgetWay.SelectedValue = byId.BudgetWay;
		this.dropKeyPart.SelectedValue = byId.KeyPart;
		if (!string.IsNullOrEmpty(byId.PrjManager))
		{
			this.txtPrjManager.Text = byId.PrjManagerName;
			this.hfldPrjManager.Value = byId.PrjManager;
		}
		if (!string.IsNullOrWhiteSpace(byId.PrjReadOne))
		{
			this.hfldPrjReadOne.Value = byId.PrjReadOne;
			PtYhmc modelById = new PtYhmcBll().GetModelById(byId.PrjReadOne);
			if (modelById != null)
			{
				this.txtPrjReadOne.Text = modelById.v_xm;
			}
		}
		if (!string.IsNullOrEmpty(byId.WorkUnit))
		{
			this.txtWorkUnit.Text = byId.WorkUnitName;
			this.hfldWorkUnit.Value = byId.WorkUnit;
		}
		this.txtTelphone.Text = byId.Telephone;
		this.hfldEngTypeJson.Value = JsonConvert.SerializeObject(byId.EngineeringTypes);
		this.txtTotalHouseNum.Text = byId.TotalHouseNum;
		if (byId.BuildingTypeNo != 0)
		{
			this.txtBuildingTypeNo.Text = byId.BuildingTypeNo.ToString();
		}
		this.txtPrjFundInfo.Text = byId.PrjFundInfo;
		this.txtFundWorkable.Text = byId.PrjFundWorkable;
		this.txtBuildingArea.Text = byId.BuildingArea;
		this.txtUsegrounArea.Text = byId.UsegrounArea;
		this.txtUndergroundArea.Text = byId.UndergroundArea;
		this.txtOtherStatement.Text = byId.OtherStatement;
		this.txtAfforestArea.Text = byId.AfforestArea;
		this.txtParkArea.Text = byId.ParkArea;
		this.txtManagementMargin.Text = byId.ManagementMargin.ToString();
		this.txtMigrantQualityMarginRate.Text = byId.MigrantQualityMarginRate.ToString();
		this.txtWithholdingTaxRate.Text = byId.WithholdingTaxRate.ToString();
		this.txtPerformanceBond.Text = byId.PerformanceBond.ToString();
		this.txtElseMargin.Text = byId.ElseMargin.ToString();
		this.txtProjStartDate.Text = this.ConvertDateTime(byId.ProjStartDate);
		this.txtProjApplyDate.Text = this.ConvertDateTime(byId.ProjApplyDate);
		this.txtStartRemark.Text = byId.ProjStartRemark;
		this.txtProjStartDate1.Text = this.ConvertDateTime(byId.ProjStartDate);
		this.txtProjApplyDate1.Text = this.ConvertDateTime(byId.ProjApplyDate);
		this.txtStartRemark1.Text = byId.ProjStartRemark;
		if (byId.ProjApprovalDate.HasValue)
		{
			this.txtApprovalDate.Text = this.ConvertDateTime(byId.ProjApprovalDate);
		}
		this.txtQualificationMargin.Text = byId.QualificationMargin.ToString();
		this.txtTenderDate.Text = this.ConvertDateTime(byId.ProjTenderDate);
		this.txtRegistDeadline.Text = byId.ProjRegistDeadline.ToString();
		if (!string.IsNullOrEmpty(byId.ProgAgent))
		{
			this.txtAgent.Text = byId.ProgAgentName;
			this.hfldAgent.Value = byId.ProgAgent;
		}
		this.txtApplyDate.Text = this.ConvertDateTime(byId.ProjApplyDate);
		if (!string.IsNullOrEmpty(byId.QualificationReadOne))
		{
			this.hfldQualificationReadOne.Value = byId.QualificationReadOne;
			this.txtQualificationReadOne.Text = new PtYhmcBll().GetModelById(byId.QualificationReadOne).v_xm;
		}
		this.txtysdetail.Text = byId.PrequalificationRequire;
		this.txtPassDate.Text = this.ConvertDateTime(byId.QualificationPassDate);
		this.txtPassReason.Text = byId.QualificationPassReason;
		this.txtFailDate.Text = this.ConvertDateTime(byId.QualificationFailData);
		this.txtFailReason.Text = byId.QualificationFailReason;
		this.txtTenderBeginDate.Text = this.ConvertDateTime(byId.ProjTenderBeginDate);
		this.txtTenderCeilingPrice.Text = byId.TenderCeilingPrice.ToString();
		this.txtTenderUnit.Text = byId.TenderUnit;
		this.txtTenderQuote.Text = byId.TenderQuote.ToString();
		this.dropTenderAppraiseMethod.SelectedValue = byId.TenderAppraiseMethod;
		this.txtTenderAverage.Text = byId.TenderAverage.ToString();
		this.txtTenderAnswerDate.Text = this.ConvertDateTime(byId.ProjTenderAnswerDate);
		this.txtTenderEarnestMoney.Text = byId.ProjTenderEarnestMoney.ToString();
		this.dropTenderPayWay.SelectedValue = byId.ProjTenderPayWay;
		this.txtTenderProspect.Text = this.ConvertDateTime(byId.TenderProspect);
		this.txtTenderCostContent.Text = byId.ProjTenderCostContent;
		if (!string.IsNullOrEmpty(byId.TenderReadOne))
		{
			this.txtTenderReadOne.Text = WebUtil.GetUserNames(byId.TenderReadOne);
		}
		this.hfldTenderReadOne.Value = byId.TenderReadOne;
		this.txtTenderContent.Text = byId.ProjTenderContent;
		this.txtTenderRemark.Text = byId.ProjTenderRemark;
		this.txtSuccessBidDate.Text = this.ConvertDateTime(byId.SuccessBidDate);
		this.txtSuccessBidPrice.Text = byId.SuccessBidPrice.ToString();
		this.txtSuccessBidRemark.Text = byId.SuccessBidRemark;
		this.txtOutBidDate.Text = this.ConvertDateTime(byId.OutBidDate);
		if (byId.OutBidIsReturn.HasValue)
		{
			this.RblOutBidIsReturn.SelectedValue = (byId.OutBidIsReturn.Value ? "0" : "1");
		}
		this.txtOutBidRemark.Text = byId.OutBidRemark;
		this.ShowProjectModule(text);
	}
	private TenderInfo CreateTenderInfo(TenderInfo tender)
	{
		if (tender == null)
		{
			tender = new TenderInfo();
			tender.PrjState = 1;
		}
		tender.ComputeClass = "1";
		tender.ContractSum = "";
		tender.FileName = "";
		tender.FileURL = "";
		tender.MarketInfoGuid = new System.Guid?(System.Guid.Empty);
		tender.ProjPeopleCode = this.hfldPrjPeople.Value;
		tender.ProjPeopleUserName = this.txtName.Text.ToString().Trim();
		tender.ProjPeopleCorp = this.txtCorp.Text.ToString().Trim();
		tender.ProjPeopleDuty = this.txtDuty.Text.ToString().Trim();
		tender.ProjPeopleTel = this.txtPhone.Text.ToString().Trim();
		if (!string.IsNullOrEmpty(this.hfldOwner.Value))
		{
			tender.OwnerCode = new int?(int.Parse(this.hfldOwner.Value));
		}
		tender.OwnerLinkManName = this.txtOwnerLinkMan.Text;
		tender.OwnerLinkPhone = this.txtOwnerLinkManPhone.Text;
		tender.OwnerAddress = this.txtOwnerAddress.Text.ToString().Trim();
		tender.PrjGuid = new System.Guid(this.hfldPrjGuid.Value);
		tender.PrjCode = this.txtPrjCode.Value;
		tender.PrjName = this.txtPrjName.Text;
		if (!string.IsNullOrEmpty(this.txtStartDate.Text))
		{
			tender.StartDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartDate.Text));
		}
		else
		{
			tender.StartDate = null;
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text))
		{
			tender.EndDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndDate.Text));
		}
		else
		{
			tender.EndDate = null;
		}
		if (!string.IsNullOrEmpty(this.dropprovince.SelectedValue))
		{
			tender.Province = this.dropprovince.SelectedItem.Text;
			if (!string.IsNullOrEmpty(this.hfldCity.Value))
			{
				if (this.hfldCity.Value == "请选择")
				{
					tender.City = string.Empty;
				}
				else
				{
					tender.City = this.hfldCity.Value;
				}
			}
		}
		tender.PrjPlace = this.txtPrjPlace.Text;
		tender.Duration = this.txtDuration.Text;
		tender.EngineeringEstimates = this.txtEngineeringEstimates.Text;
		if (!string.IsNullOrEmpty(this.txtPrjCost.Text))
		{
			tender.PrjCost = new double?(System.Convert.ToDouble(this.txtPrjCost.Text));
		}
		if (!string.IsNullOrEmpty(this.txtForecastProfitRate.Text))
		{
			tender.ForecastProfitRate = new decimal?(System.Convert.ToDecimal(this.txtForecastProfitRate.Text));
		}
		tender.PrjKinds = JsonHelper.JsonDeserialize<ProjectKind[]>(this.hfldPrjKindJson.Value);
		tender.QualityClass = this.dropQualityClass.SelectedValue;
		tender.Designer = this.txtDesigner.Text;
		tender.PrjProperty = this.dropProperty.SelectedValue;
		tender.PrjApprovalOf = this.txtApprovalOf.Text;
		tender.Counsellor = this.txtCounsellor.Text;
		tender.Inspector = this.txtInspector.Text;
		tender.PrjManagerRequire = this.txtPrjManagerRequire.Text.Trim();
		tender.TechnicalLeaderRequire = this.txtTechnicalLeaderRequire.Text.Trim();
		tender.PrjInfo = this.txtPrjInfo.Text;
		tender.ProjInfoOrigin = this.txtInfoOrigin.Text;
		tender.ProjElseRequest = this.txtElseRequest.Text;
		tender.Remark = this.txtRemark1.Text;
		tender.PrjDutyPerson = this.hfldDutyPerson.Value;
		tender.PrjRanks = JsonHelper.JsonDeserialize<ProjectRank[]>(this.hfldRankJson.Value);
		tender.ContractWay = this.dropContractWay.SelectedValue;
		tender.PayCondition = this.dropPayCondition.SelectedValue;
		tender.TenderWay = this.dropTenderWay.SelectedValue;
		tender.PayWay = this.dropPayWay.SelectedValue;
		tender.BudgetWay = this.dropBudgetWay.SelectedValue;
		tender.KeyPart = this.dropKeyPart.SelectedValue;
		tender.PrjManager = this.txtPrjManager.Text;
		tender.PrjReadOne = this.hfldPrjReadOne.Value;
		tender.WorkUnit = this.txtWorkUnit.Text;
		tender.Telephone = this.txtTelphone.Text;
		tender.EngineeringTypes = JsonHelper.JsonDeserialize<EngineeringType[]>(this.hfldEngTypeJson.Value);
		tender.TotalHouseNum = this.txtTotalHouseNum.Text;
		tender.BuildingTypeNo = (string.IsNullOrEmpty(this.txtBuildingTypeNo.Text) ? 0 : int.Parse(this.txtBuildingTypeNo.Text));
		tender.PrjFundInfo = this.txtPrjFundInfo.Text;
		tender.PrjFundWorkable = this.txtFundWorkable.Text;
		tender.BuildingArea = this.txtBuildingArea.Text;
		tender.UsegrounArea = this.txtUsegrounArea.Text;
		tender.UndergroundArea = this.txtUndergroundArea.Text;
		tender.OtherStatement = this.txtOtherStatement.Text;
		tender.AfforestArea = this.txtAfforestArea.Text;
		tender.ParkArea = this.txtParkArea.Text;
		if (!string.IsNullOrEmpty(this.txtManagementMargin.Text))
		{
			tender.ManagementMargin = new decimal?(System.Convert.ToDecimal(this.txtManagementMargin.Text));
		}
		if (!string.IsNullOrEmpty(this.txtMigrantQualityMarginRate.Text))
		{
			tender.MigrantQualityMarginRate = new decimal?(System.Convert.ToDecimal(this.txtMigrantQualityMarginRate.Text));
		}
		if (!string.IsNullOrEmpty(this.txtWithholdingTaxRate.Text))
		{
			tender.WithholdingTaxRate = new decimal?(System.Convert.ToDecimal(this.txtWithholdingTaxRate.Text));
		}
		if (!string.IsNullOrEmpty(this.txtPerformanceBond.Text))
		{
			tender.PerformanceBond = new decimal?(System.Convert.ToDecimal(this.txtPerformanceBond.Text));
		}
		if (!string.IsNullOrEmpty(this.txtElseMargin.Text))
		{
			tender.ElseMargin = new decimal?(System.Convert.ToDecimal(this.txtElseMargin.Text));
		}
		if (!string.IsNullOrEmpty(this.txtProjStartDate.Text))
		{
			tender.ProjStartDate = new System.DateTime?(System.Convert.ToDateTime(this.txtProjStartDate.Text));
		}
		tender.ProjStartRemark = this.txtStartRemark.Text;
		tender.PrjManager = this.txtPrjManager.Text;
		if (!string.IsNullOrEmpty(this.txtApprovalDate.Text))
		{
			tender.ProjApprovalDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApprovalDate.Text));
		}
		if (!string.IsNullOrWhiteSpace(this.txtQualificationMargin.Text))
		{
			tender.QualificationMargin = System.Convert.ToDecimal(this.txtQualificationMargin.Text);
		}
		if (!string.IsNullOrEmpty(this.txtTenderDate.Text))
		{
			tender.ProjTenderDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderDate.Text));
		}
		if (!string.IsNullOrEmpty(this.txtRegistDeadline.Text))
		{
			tender.ProjRegistDeadline = new int?(int.Parse(this.txtRegistDeadline.Text));
		}
		tender.ProgAgent = this.hfldAgent.Value;
		if (!string.IsNullOrEmpty(this.txtApplyDate.Text))
		{
			tender.ProjApplyDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApplyDate.Text));
		}
		tender.QualificationReadOne = this.hfldQualificationReadOne.Value;
		tender.PrequalificationRequire = this.txtysdetail.Text;
		if (!string.IsNullOrEmpty(this.txtPassDate.Text))
		{
			tender.QualificationPassDate = new System.DateTime?(System.Convert.ToDateTime(this.txtPassDate.Text));
		}
		tender.QualificationPassReason = this.txtPassReason.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtFailDate.Text))
		{
			tender.QualificationFailData = new System.DateTime?(System.Convert.ToDateTime(this.txtFailDate.Text));
		}
		tender.QualificationFailReason = this.txtFailReason.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtTenderBeginDate.Text))
		{
			tender.ProjTenderBeginDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderBeginDate.Text));
		}
		if (!string.IsNullOrEmpty(this.txtTenderCeilingPrice.Text))
		{
			tender.TenderCeilingPrice = new decimal?(System.Convert.ToDecimal(this.txtTenderCeilingPrice.Text));
		}
		tender.TenderUnit = this.txtTenderUnit.Text;
		if (!string.IsNullOrEmpty(this.txtTenderQuote.Text))
		{
			tender.TenderQuote = new decimal?(System.Convert.ToDecimal(this.txtTenderQuote.Text));
		}
		tender.TenderAppraiseMethod = this.dropTenderAppraiseMethod.SelectedValue;
		if (!string.IsNullOrEmpty(this.txtTenderAverage.Text))
		{
			tender.TenderAverage = new decimal?(System.Convert.ToDecimal(this.txtTenderAverage.Text));
		}
		if (!string.IsNullOrEmpty(this.txtTenderAnswerDate.Text))
		{
			tender.ProjTenderAnswerDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderAnswerDate.Text));
		}
		if (!string.IsNullOrEmpty(this.txtTenderEarnestMoney.Text))
		{
			tender.ProjTenderEarnestMoney = new decimal?(System.Convert.ToDecimal(this.txtTenderEarnestMoney.Text));
		}
		tender.ProjTenderPayWay = this.dropTenderPayWay.SelectedValue;
		if (!string.IsNullOrEmpty(this.txtTenderProspect.Text))
		{
			tender.TenderProspect = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderProspect.Text));
		}
		tender.ProjTenderCostContent = this.txtTenderCostContent.Text;
		tender.TenderReadOne = this.hfldTenderReadOne.Value;
		tender.ProjTenderContent = this.txtTenderContent.Text;
		tender.ProjTenderRemark = this.txtTenderRemark.Text;
		if (!string.IsNullOrEmpty(this.txtSuccessBidDate.Text))
		{
			tender.SuccessBidDate = new System.DateTime?(System.Convert.ToDateTime(this.txtSuccessBidDate.Text));
		}
		if (!string.IsNullOrEmpty(this.txtSuccessBidPrice.Text))
		{
			tender.SuccessBidPrice = new decimal?(System.Convert.ToDecimal(this.txtSuccessBidPrice.Text));
		}
		tender.SuccessBidRemark = this.txtSuccessBidRemark.Text;
		if (!string.IsNullOrEmpty(this.txtOutBidDate.Text))
		{
			tender.OutBidDate = new System.DateTime?(System.Convert.ToDateTime(this.txtOutBidDate.Text));
		}
		tender.OutBidIsReturn = new bool?(this.RblOutBidIsReturn.SelectedValue == "0");
		tender.OutBidRemark = this.txtOutBidRemark.Text;
		tender.InputUser = base.UserCode;
		tender.InputDate = new System.DateTime?(System.DateTime.Now);
		return tender;
	}
	private void ReflushImg(string state)
	{
		if (state == ProjectParameter.Initiate)
		{
			this.qd.Src = "PrjImg/qd.gif";
			this.qd1.Src = "PrjImg/jt.gif";
			this.qd.Attributes["Style"] = "";
			this.lx.Src = "PrjImg/lx-1.gif";
			this.lx1.Src = "PrjImg/jt-1.gif";
			this.qd.Attributes["onClick"] = "";
			this.tr_qd.Attributes["Style"] = "";
			this.div3.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.Prequalification)
		{
			this.ys.Src = "PrjImg/ys.gif";
			this.ys1.Src = "PrjImg/jt1.gif";
			this.ys2.Src = "PrjImg/jt2.gif";
			this.ys.Attributes["Style"] = "";
			this.ys.Attributes["onClick"] = "";
			this.qd.Src = "PrjImg/qd-1.gif";
			this.qd1.Src = "PrjImg/jt-1.gif";
			this.tr_ys.Attributes["Style"] = "";
			this.div14.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.QualificationPass)
		{
			this.yspass.Src = "PrjImg/yspass.gif";
			this.yspass1.Src = "PrjImg/jt2.gif";
			this.yspass.Attributes["onClick"] = "";
			this.yspass.Attributes["Style"] = "";
			this.ysfail.Attributes["onClick"] = "";
			this.ysfail.Attributes["Style"] = "";
			this.ys.Src = "PrjImg/ys.gif";
			this.ys1.Src = "PrjImg/jt1-1.gif";
			this.ys2.Src = "PrjImg/jt2-1.gif";
			this.tr_yspass.Attributes["Style"] = "";
			this.div15.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.QualificationFail)
		{
			this.ysfail.Src = "PrjImg/ysfail.gif";
			this.ysfail.Attributes["onClick"] = "";
			this.ysfail.Attributes["Style"] = "";
			this.yspass.Attributes["onClick"] = "";
			this.yspass.Attributes["Style"] = "";
			this.ys.Src = "PrjImg/ys.gif";
			this.ys1.Src = "PrjImg/jt1-1.gif";
			this.ys2.Src = "PrjImg/jt2-1.gif";
			this.tr_ysfail.Attributes["Style"] = "";
			this.div16.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.Bid)
		{
			this.tb.Src = "PrjImg/tb.gif";
			this.tb1.Src = "PrjImg/jt1.gif";
			this.tb2.Src = "PrjImg/jt2.gif";
			this.tb.Attributes["Style"] = "";
			this.tb.Attributes["onClick"] = "";
			this.tr_tb.Attributes["Style"] = "";
			this.div4.Attributes["Style"] = "";
			this.yspass.Src = "PrjImg/yspass-1.gif";
			this.yspass1.Src = "PrjImg/jt2-1.gif";
		}
		if (state == ProjectParameter.WinBid)
		{
			this.tb.Src = "PrjImg/tb-1.gif";
			this.tb1.Src = "PrjImg/jt1-1.gif";
			this.tb2.Src = "PrjImg/jt2-1.gif";
			this.zb.Src = "PrjImg/zb.gif";
			this.tr_zb.Attributes["Style"] = "";
			this.div5.Attributes["Style"] = "";
			this.zb.Style.Add("cursor", " ");
			this.zb.Attributes["onClick"] = "";
			this.lb.Attributes["onClick"] = "";
			this.lb.Style.Add("cursor", " ");
		}
		if (state == ProjectParameter.OutBid)
		{
			this.tb.Src = "PrjImg/tb-1.gif";
			this.tb1.Src = "PrjImg/jt1-1.gif";
			this.tb2.Src = "PrjImg/jt2-1.gif";
			this.lb.Src = "PrjImg/lb.gif";
			this.tr_lb.Attributes["Style"] = "";
			this.div6.Attributes["Style"] = "";
			this.zb.Style.Add("cursor", " ");
			this.zb.Attributes["onClick"] = "";
			this.lb.Attributes["onClick"] = "";
			this.lb.Style.Add("cursor", " ");
		}
	}
	private string ConvertDateTime(System.DateTime? date)
	{
		if (date.HasValue)
		{
			return System.Convert.ToDateTime(date).ToString("yyyy-M-d");
		}
		return "";
	}
	private void DisplayTenderImg(string State)
	{
		if (State == ProjectParameter.PreApproval)
		{
			this.prjState.Visible = false;
			return;
		}
		this.prjState.Visible = true;
		if (State == ProjectParameter.Approval)
		{
			if (this.purl == "InfoSetUp.aspx")
			{
				this.prjState.Visible = false;
			}
			else
			{
				this.lx.Src = "PrjImg/lx.gif";
				this.lx1.Src = "PrjImg/jt.gif";
				this.qd.Attributes["onClick"] = "imageClick(" + ProjectParameter.Initiate + ")";
				this.qd.Attributes["Style"] = " cursor:pointer";
			}
		}
		if (State == ProjectParameter.Initiate)
		{
			this.qd.Src = "PrjImg/qd.gif";
			this.qd1.Src = "PrjImg/jt.gif";
			if (this.purl == "InitiateManage.aspx")
			{
				this.ys.Attributes["onClick"] = "";
			}
			else
			{
				this.ys.Attributes["onClick"] = "imageClick(" + ProjectParameter.Prequalification + ")";
				this.ys.Attributes["Style"] = " cursor:pointer";
			}
		}
		if (State == ProjectParameter.Prequalification)
		{
			this.ys.Src = "PrjImg/ys.gif";
			this.ys1.Src = "PrjImg/jt1.gif";
			this.ys2.Src = "PrjImg/jt2.gif";
			if (this.purl == "PrequalificationManage.aspx")
			{
				this.yspass.Attributes["onClick"] = "";
				this.ysfail.Attributes["onClick"] = "";
			}
			else
			{
				this.yspass.Attributes["onClick"] = "imageClick(" + ProjectParameter.QualificationPass + ")";
				this.yspass.Attributes["Style"] = " cursor:pointer";
				this.ysfail.Attributes["onClick"] = "imageClick(" + ProjectParameter.QualificationFail + ")";
				this.ysfail.Attributes["Style"] = " cursor:pointer";
			}
		}
		if (State == ProjectParameter.QualificationPass)
		{
			this.yspass.Src = "PrjImg/yspass.gif";
			this.yspass1.Src = "PrjImg/jt2.gif";
			if (this.purl == "Prequalification.aspx")
			{
				this.tb.Attributes["onClick"] = "";
			}
			else
			{
				this.tb.Attributes["onClick"] = "imageClick(" + ProjectParameter.Bid + ")";
				this.tb.Attributes["Style"] = " cursor:pointer";
			}
		}
		if (State == ProjectParameter.QualificationFail)
		{
			this.ysfail.Src = "PrjImg/ysfail.gif";
		}
		if (State == ProjectParameter.Bid)
		{
			this.tb.Src = "PrjImg/tb.gif";
			this.tb1.Src = "PrjImg/jt1.gif";
			this.tb2.Src = "PrjImg/jt2.gif";
			if (this.purl == "BidManage.aspx")
			{
				this.zb.Attributes["onClick"] = "";
				this.lb.Attributes["onClick"] = "";
			}
			else
			{
				this.zb.Attributes["onClick"] = "imageClick(" + ProjectParameter.WinBid + ")";
				this.zb.Style.Add("cursor", "pointer");
				this.lb.Attributes["onClick"] = "imageClick(" + ProjectParameter.OutBid + ")";
				this.lb.Style.Add("cursor", "pointer");
			}
		}
		if (State == ProjectParameter.WinBid)
		{
			this.zb.Src = "PrjImg/zb.gif";
		}
		if (State == ProjectParameter.OutBid)
		{
			this.lb.Src = "PrjImg/lb.gif";
		}
	}
	private void ShowProjectModule(string state)
	{
		if (state == ProjectParameter.Initiate)
		{
			this.tr_qd.Attributes["Style"] = "";
			this.div3.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.InitiateFail)
		{
			this.tr_qdn.Attributes["Style"] = "";
			this.div2.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.Prequalification)
		{
			this.tr_qd.Attributes["Style"] = "";
			this.div3.Attributes["Style"] = "";
			this.tr_ys.Attributes["Style"] = "";
			this.div14.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.QualificationPass)
		{
			this.tr_qd.Attributes["Style"] = "";
			this.div3.Attributes["Style"] = "";
			this.tr_ys.Attributes["Style"] = "";
			this.div14.Attributes["Style"] = "";
			this.tr_yspass.Attributes["Style"] = "";
			this.div15.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.QualificationFail)
		{
			this.tr_qd.Attributes["Style"] = "";
			this.div3.Attributes["Style"] = "";
			this.tr_ys.Attributes["Style"] = "";
			this.div14.Attributes["Style"] = "";
			this.tr_ysfail.Attributes["Style"] = "";
			this.div16.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.Bid)
		{
			this.tr_qd.Attributes["Style"] = "";
			this.div3.Attributes["Style"] = "";
			this.tr_ys.Attributes["Style"] = "";
			this.div14.Attributes["Style"] = "";
			this.tr_yspass.Attributes["Style"] = "";
			this.div15.Attributes["Style"] = "";
			this.tr_tb.Attributes["Style"] = "";
			this.div4.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.WinBid)
		{
			this.tr_qd.Attributes["Style"] = "";
			this.div3.Attributes["Style"] = "";
			this.tr_ys.Attributes["Style"] = "";
			this.div14.Attributes["Style"] = "";
			this.tr_yspass.Attributes["Style"] = "";
			this.div15.Attributes["Style"] = "";
			this.tr_zb.Attributes["Style"] = "";
			this.div5.Attributes["Style"] = "";
			this.tr_tb.Attributes["Style"] = "";
			this.div4.Attributes["Style"] = "";
		}
		if (state == ProjectParameter.OutBid)
		{
			this.tr_qd.Attributes["Style"] = "";
			this.div3.Attributes["Style"] = "";
			this.tr_ys.Attributes["Style"] = "";
			this.div14.Attributes["Style"] = "";
			this.tr_yspass.Attributes["Style"] = "";
			this.div15.Attributes["Style"] = "";
			this.tr_lb.Attributes["Style"] = "";
			this.div6.Attributes["Style"] = "";
			this.tr_tb.Attributes["Style"] = "";
			this.div4.Attributes["Style"] = "";
		}
	}
	private void MsgReadOne(string prjId, string prjName, string userCode)
	{
		if (!string.IsNullOrEmpty(userCode))
		{
			PTDBSJService pTDBSJService = new PTDBSJService();
			pTDBSJService.Add(new PTDBSJ
			{
				I_XGID = prjId,
				V_LXBM = "024",
				V_YHDM = userCode,
				DTM_DBSJ = new System.DateTime?(System.DateTime.Now),
				C_OpenFlag = "0",
				V_Content = "项目：" + prjName + "已经编制成功",
				V_DBLJ = "TenderManage/InfoView.aspx?ic=" + prjId,
				V_TPLJ = "new_Mail.gif"
			});
		}
	}
	public void AddToPrjInfo(object obj)
	{
		PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfoZTB byId = pTPrjInfoZTBService.GetById(this.hfldPrjGuid.Value);
		pTPrjInfoService.ChangePrjInfo(byId, -1, 1);
	}
}


