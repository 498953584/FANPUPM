using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.Fund.FundConfig;
using cn.justwin.Fund.MonthPlan;
using cn.justwin.stockBLL.Fund.Account;
using cn.justwin.stockBLL.Fund.FundConfig;
using cn.justwin.stockBLL.Fund.MonthPlan;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_MonthPlanSet : NBasePage, IRequiresSessionState
{
	private Fund_Plan_MonthMainAction FA = new Fund_Plan_MonthMainAction();
	private MonthDetailLogic bll = new MonthDetailLogic();
	public static string pathLine = string.Empty;
	private string plantype = string.Empty;
	private string prjName = string.Empty;
	private Guid PrjGuid = Guid.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.PrjGuid = new Guid(base.Request["prjId"]);
		}
		if (!string.IsNullOrEmpty(base.Request["plantype"]))
		{
			this.plantype = base.Request["plantype"];
		}
		if (!string.IsNullOrEmpty(base.Request["prjname"]))
		{
			this.prjName = base.Request["prjname"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		this.hdfcontrcn.Value = this.PrjGuid.ToString();
		if (this.hdfMonthPalnID.Value == "")
		{
			this.hdfMonthPalnID.Value = (string.IsNullOrEmpty(base.Request["planMainId"]) ? Guid.NewGuid().ToString() : new Guid(base.Request["planMainId"]).ToString());
		}
		if (!base.IsPostBack)
		{
			this.btnEdit.Enabled = false;
			this.QueryDateCtrl1.DateType = "M";
			this.hdfModel.Value = "0";
			DateTime dateTime = Convert.ToDateTime(publicDbOpClass.DataTableQuary(" select getdate() ").Rows[0][0]);
			if (this.checkWeave())
			{
				this.QueryDateCtrl1.Month = dateTime.AddMonths(1).ToString("yyyy-MM");
			}
			else
			{
				this.QueryDateCtrl1.Month = dateTime.ToString("yyyy-MM");
			}
			this.getMonthPlanInfo();
		}
	}
	public void getMonthPlanInfo()
	{
		DateTime dateTime = Convert.ToDateTime(this.QueryDateCtrl1.Month + "-01");
		this.hdnYear.Value = dateTime.Year.ToString();
		this.hdnMonth.Value = dateTime.Month.ToString();
		string text = string.Empty;
		this.hfplantype.Value = this.plantype;
		if (this.plantype == "payout")
		{
			this.FileUpload1.Class = "MonthPlanPayOut";
			text = "支出";
		}
		else
		{
			if (this.plantype == "income")
			{
				this.FileUpload1.Class = "MonthPlanIncome";
				text = "收入";
			}
		}
		this.FileUpload1.RecordCode = this.hdfMonthPalnID.Value;
		this.lblTitle.Text = string.Concat(new string[]
		{
			this.prjName,
			"  ",
			dateTime.ToString("yyyy年MM月"),
			"资金",
			text,
			"计划"
		});
		bool flag = false;
		if (!string.IsNullOrEmpty(this.hdfMonthPalnID.Value))
		{
			flag = this.FA.IsExitFundPlanMainInfo(this.plantype, this.hdfMonthPalnID.Value, this.PrjGuid.ToString());
		}
		if (flag)
		{
			Fund_Plan_MonthMainInfo mainInfoByMonthPlanID = this.FA.GetMainInfoByMonthPlanID(this.hdfMonthPalnID.Value);
			this.txtPlanMonth.Text = string.Concat(new object[]
			{
				mainInfoByMonthPlanID.PlanYear,
				"年",
				mainInfoByMonthPlanID.PlanMonth,
				"月"
			});
			this.txtProName.Value = mainInfoByMonthPlanID.PrjName;
			this.hdfcontrcn.Value = mainInfoByMonthPlanID.PrjGuid.ToString();
			this.txtadduser.Text = mainInfoByMonthPlanID.OperatorName;
			this.hdfMonthPalnID.Value = mainInfoByMonthPlanID.MonthPlanID.ToString();
			this.txtadddate.Text = Common2.GetTime(mainInfoByMonthPlanID.OperateTime);
			this.txtRemark.Text = mainInfoByMonthPlanID.Remark;
		}
		else
		{
			this.txtPlanMonth.Text = dateTime.ToString("yyyy年MM月");
			this.txtProName.Value = this.prjName;
			AccountLogic accountLogic = new AccountLogic();
			this.txtadduser.Text = accountLogic.GetUserNameByUserCode(this.Session["yhdm"].ToString());
			this.txtadddate.Text = Common2.GetTime(DateTime.Now);
		}
		this.BindPlanDetail();
	}
	public void getMonthPlanMainInfo(DateTime dtime)
	{
		Fund_Plan_MonthMainInfo fund_Plan_MonthMainInfo = new Fund_Plan_MonthMainInfo();
		fund_Plan_MonthMainInfo.PrjGuid = this.PrjGuid;
		fund_Plan_MonthMainInfo.PlanType = this.plantype;
		fund_Plan_MonthMainInfo.PlanYear = dtime.Year;
		fund_Plan_MonthMainInfo.PlanMonth = dtime.Month;
		fund_Plan_MonthMainInfo.OperateTime = DateTime.Now;
		fund_Plan_MonthMainInfo.OperatorCode = base.UserCode;
		fund_Plan_MonthMainInfo = this.FA.GetMainInfo(fund_Plan_MonthMainInfo);
		this.hdfMonthPalnID.Value = fund_Plan_MonthMainInfo.MonthPlanID.ToString();
		this.FileUpload1.RecordCode = fund_Plan_MonthMainInfo.MonthPlanID.ToString();
		this.txtRemark.Text = fund_Plan_MonthMainInfo.Remark.ToString();
		this.txtadddate.Text = fund_Plan_MonthMainInfo.OperateTime.ToString("yyyy-MM-dd");
		if (fund_Plan_MonthMainInfo.OperatorName != null && fund_Plan_MonthMainInfo.OperatorName != "")
		{
			this.txtadduser.Text = fund_Plan_MonthMainInfo.OperatorName.ToString();
		}
		else
		{
			AccountLogic accountLogic = new AccountLogic();
			string userNameByUserCode = accountLogic.GetUserNameByUserCode(this.Session["yhdm"].ToString());
			this.txtadduser.Text = userNameByUserCode;
		}
		DateTime t = Convert.ToDateTime(string.Concat(new object[]
		{
			DateTime.Today.Year.ToString(),
			'-',
			DateTime.Today.Month.ToString(),
			"-01"
		}));
		if (dtime < t)
		{
			this.hdfFlowState.Value = "10";
			this.trtoolbar.Style.Add("display", "none");
			this.trtoolbar2.Style.Add("display", "");
			this.lbEidtTS.Text = "    不能编制过去的计划,现在是查看状态！";
		}
		else
		{
			this.hdfFlowState.Value = fund_Plan_MonthMainInfo.FlowState.ToString();
			if (fund_Plan_MonthMainInfo.FlowState > -1)
			{
				this.trtoolbar.Style.Add("display", "none");
				this.trtoolbar2.Style.Add("display", "");
				this.lbEidtTS.Text = "    此计划" + Common2.GetState(fund_Plan_MonthMainInfo.FlowState.ToString()) + "不能编制,现在是查看状态！";
			}
			else
			{
				this.trtoolbar.Style.Add("display", "");
				this.trtoolbar2.Style.Add("display", "none");
				this.lbEidtTS.Text = "";
			}
		}
		this.BindPlanDetail();
	}
	public void BindPlanDetail()
	{
		this.mpid.Value = this.hdfMonthPalnID.Value;
		Fund_Plan_MonthMainInfo mainInfoByMonthPlanID = this.FA.GetMainInfoByMonthPlanID(this.hdfMonthPalnID.Value);
		int num = mainInfoByMonthPlanID.PlanMonth;
		int num2 = mainInfoByMonthPlanID.PlanYear;
		DataTable dataTable = new DataTable();
		if (this.plantype == "payout")
		{
			Fund_MonthPlanSet.pathLine = "MonthPlanPayOut";
			dataTable = this.FA.GetPlanDetailAboutPayout(this.hdfMonthPalnID.Value, string.Empty);
			num--;
			num2 = mainInfoByMonthPlanID.PlanYear;
			if (num < 1)
			{
				num2--;
				num = 12;
			}
		}
		else
		{
			if (this.plantype == "income")
			{
				Fund_MonthPlanSet.pathLine = "MonthPlanIncome";
				dataTable = this.FA.GetPlanDetailAboutIncomet(this.hdfMonthPalnID.Value, string.Empty);
				num--;
				if (num < 1)
				{
					num2--;
					num = 12;
				}
			}
		}
		DataTable lastPlan = this.getLastPlan(this.plantype, num, num2);
		dataTable.Columns.Add("pathLine", Type.GetType("System.String"));
		if (dataTable.Rows.Count > 0)
		{
			dataTable = this.UniteDataTable(dataTable, lastPlan, "newtable");
		}
		else
		{
			this.HiddenField1.Value = "1";
		}
		this.ViewState["temDT"] = dataTable;
		object arg_153_0 = this.ViewState["temDT"];
		dataTable = (this.ViewState["temDT"] as DataTable);
		if (dataTable.Rows.Count < 1)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["UID"] = Guid.NewGuid();
			dataRow["MonthPlanID"] = this.hdfMonthPalnID.Value;
			dataRow["ContractID"] = "";
			dataRow["Plansubject"] = "";
			dataRow["PlanMoney"] = "0.00";
			dataRow["OldBalance"] = "0.00";
			dataRow["OrderID"] = "-1";
			dataRow["ReMark"] = "";
			dataRow["ThisBalance"] = "0.00";
			dataTable.Rows.Add(dataRow);
		}
		this.gvwWebLineList.DataSource = dataTable;
		this.gvwWebLineList.DataBind();
		if (this.gvwWebLineList.Rows.Count > 0 && this.lbEidtTS.Text.Trim() == "")
		{
			this.btnSave.Enabled = true;
			return;
		}
		this.btnSave.Enabled = false;
	}
	public DataTable getLastPlan1(string plantype, int _PlanMonth, int _PlanYear)
	{
		DataTable result = new DataTable();
		if (plantype.ToString() == "payout")
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("  SELECT fpmd.PlanMoney AS EarlierMoney,  ");
			stringBuilder.Append(" sum(cpp.PaymentMoney) AS ActuallyMoney,cpc.ContractName AS sqcname,cpc.ContractID AS NEWConID");
			stringBuilder.Append(" FROM   Fund_Plan_MonthDetail fpmd ");
			stringBuilder.Append(" LEFT JOIN Fund_Plan_MonthMain fpmm ");
			stringBuilder.Append(" ON  fpmm.PlanMonth =").Append(_PlanMonth).Append(" ");
			stringBuilder.Append("  AND fpmm.PlanYear =").Append(_PlanYear).Append(" ");
			stringBuilder.Append(" AND PlanType = '").Append(base.Request.QueryString["plantype"].ToString()).Append("' ");
			stringBuilder.Append(" AND fpmm.PrjGuid = '").Append(this.PrjGuid).Append("' ");
			stringBuilder.Append(" LEFT JOIN Con_Payout_Contract cpc ON cpc.ContractID = fpmd.ContractID ");
			stringBuilder.Append(" LEFT JOIN Con_Payout_Payment cpp ON  cpp.MonthPlanUID=fpmd.UID ");
			stringBuilder.Append(" AND cpp.FlowState=1");
			stringBuilder.Append(" WHERE  fpmd.MonthPlanID = fpmm.MonthPlanID");
			stringBuilder.Append(" GROUP BY cpc.ContractID,cpc.ContractName,fpmd.PlanMoney");
			result = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		}
		else
		{
			if (plantype.ToString() == "income")
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("SELECT fpmd.PlanMoney AS EarlierMoney,cic.ContractName AS sqcname  ").Append(" ");
				stringBuilder2.Append(", sum (cip.CllectionPrice) AS ActuallyMoney ,cic.ContractID AS NEWConID ").Append(" ");
				stringBuilder2.Append("FROM   Fund_Plan_MonthDetail fpmd ").Append(" ");
				stringBuilder2.Append("LEFT JOIN Fund_Plan_MonthMain fpmm ").Append(" ");
				stringBuilder2.Append("ON  fpmm.PlanMonth = ").Append(_PlanMonth);
				stringBuilder2.Append(" AND fpmm.PlanYear =").Append(_PlanYear);
				stringBuilder2.Append(" AND fpmm.PrjGuid = '").Append(this.PrjGuid).Append("' ");
				stringBuilder2.Append(" LEFT JOIN Con_Incomet_Contract cic ON cic.ContractID = fpmd.ContractID ").Append(" ");
				stringBuilder2.Append(" LEFT JOIN Con_Incomet_Payment cip ON cip.MonthPlanUID = fpmd.UID  AND cip.[state] = 1 ").Append(" ");
				stringBuilder2.Append(" WHERE  fpmd.MonthPlanID = fpmm.MonthPlanID  ").Append(" ");
				stringBuilder2.Append(" GROUP BY cic.ContractID,cic.ContractName,fpmd.PlanMoney");
				result = publicDbOpClass.DataTableQuary(stringBuilder2.ToString());
			}
		}
		return result;
	}
	private DataTable getLastPlan(string plantype, int _PlanMonth, int _PlanYear)
	{
		DataTable result = new DataTable();
		if (plantype == "payout")
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" WITH CTE as(");
			stringBuilder.Append(" SELECT  fpmd.PlanMoney AS EarlierMoney,fpmd.MonthPlanID,fpmm.PlanMonth,fpmm.PlanYear,fpmm.PrjGuid,fpmd.ContractID,fpmd.UID FROM   Fund_Plan_MonthDetail fpmd");
			stringBuilder.Append(" LEFT JOIN Fund_Plan_MonthMain fpmm ");
			stringBuilder.Append(" ON  fpmd.MonthPlanID=fpmm.MonthPlanID ");
			stringBuilder.Append(" WHERE fpmm.PrjGuid ='").Append(this.PrjGuid);
			stringBuilder.Append("' AND fpmm.PlanYear =").Append(_PlanYear);
			stringBuilder.Append(" AND PlanType = 'payout')");
			stringBuilder.Append(" SELECT  cic.ContractID AS NEWConID,CTE1.EarlierMoney, cic.ContractName AS sqcname ,");
			stringBuilder.Append(" SUM (cip.PaymentMoney) AS ActuallyMoney");
			stringBuilder.Append(" FROM CTE");
			stringBuilder.Append(" LEFT JOIN (Select CTE.ContractID,CTE.EarlierMoney,CTE.UID FROM CTE where CTE.PlanMonth=").Append(_PlanMonth);
			stringBuilder.Append(" )AS CTE1 ON CTE.ContractID=CTE1.ContractID");
			stringBuilder.Append(" LEFT JOIN Con_Payout_Contract cic ON cic.ContractID = CTE1.ContractID");
			stringBuilder.Append(" LEFT JOIN Con_Payout_Payment cip ON cip.MonthPlanUID = CTE1.UID AND cip.FlowState=1");
			stringBuilder.Append(" WHERE CTE.ContractID=CTE1.ContractID AND CTE.PlanMonth=").Append(_PlanMonth + 1);
			stringBuilder.Append(" GROUP BY cic.ContractName,CTE1.EarlierMoney,CTE.UID, cic.ContractID");
			result = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		}
		else
		{
			if (plantype == "income")
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("WITH CTE as(");
				stringBuilder2.Append(" SELECT  fpmd.PlanMoney AS EarlierMoney,fpmd.MonthPlanID,fpmm.PlanMonth,fpmm.PlanYear,fpmm.PrjGuid,fpmd.ContractID,fpmd.UID FROM   Fund_Plan_MonthDetail fpmd");
				stringBuilder2.Append("  LEFT JOIN Fund_Plan_MonthMain fpmm");
				stringBuilder2.Append(" ON  fpmd.MonthPlanID=fpmm.MonthPlanID ");
				stringBuilder2.Append(" WHERE fpmm.PrjGuid = '").Append(this.PrjGuid).Append("' ");
				stringBuilder2.Append(" AND fpmm.PlanYear =").Append(_PlanYear);
				stringBuilder2.Append(" AND fpmm.PlanType='income')SELECT  cic.ContractID AS NEWConID,CTE1.EarlierMoney, cic.ContractName AS sqcname ,");
				stringBuilder2.Append(" SUM (cip.CllectionPrice) AS ActuallyMoney ");
				stringBuilder2.Append(" FROM CTE");
				stringBuilder2.Append(" LEFT JOIN (Select CTE.ContractID,CTE.EarlierMoney,CTE.UID FROM CTE where CTE.PlanMonth=").Append(_PlanMonth);
				stringBuilder2.Append(" )AS CTE1 ON CTE.ContractID=CTE1.ContractID");
				stringBuilder2.Append(" LEFT JOIN Con_Incomet_Contract cic ON cic.ContractID = CTE1.ContractID");
				stringBuilder2.Append(" LEFT JOIN Con_Incomet_Payment cip ON cip.MonthPlanUID = CTE1.UID");
				stringBuilder2.Append(" WHERE CTE.ContractID=CTE1.ContractID AND CTE.PlanMonth=").Append(_PlanMonth + 1);
				stringBuilder2.Append(" GROUP BY cic.ContractName,CTE1.EarlierMoney,CTE.UID, cic.ContractID");
				result = publicDbOpClass.DataTableQuary(stringBuilder2.ToString());
			}
		}
		return result;
	}
	protected void gvwModelList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwWebLineList.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwWebLineList.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + this.gvwWebLineList.DataKeys[e.Row.RowIndex].Value.ToString() + "');";
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			HtmlInputText htmlInputText = e.Row.FindControl("txtOrderID") as HtmlInputText;
			htmlInputText.Value = dataRowView["OrderID"].ToString();
			HtmlInputText htmlInputText2 = e.Row.FindControl("txtConRemark") as HtmlInputText;
			htmlInputText2.Value = dataRowView["Remark"].ToString();
			Label label = e.Row.FindControl("txtBName") as Label;
			label.Text = dataRowView["corpName"].ToString();
			HiddenField hiddenField = e.Row.FindControl("hidenContractID") as HiddenField;
			hiddenField.Value = dataRowView["ContractID"].ToString();
			HtmlInputText htmlInputText3 = e.Row.FindControl("txtPlanMoney") as HtmlInputText;
			htmlInputText3.Value = dataRowView["PlanMoney"].ToString();
			double num = 0.0;
			double num2 = 0.0;
			e.Row.Cells[5].Text = string.Format("{0:N}", num);
			e.Row.Cells[6].Text = string.Format("{0:N}", num2);
			usercontrol_fileupload_fileupload_ascx usercontrol_fileupload_fileupload_ascx = e.Row.FindControl("FileUpload2") as usercontrol_fileupload_fileupload_ascx;
			usercontrol_fileupload_fileupload_ascx.Class = ((base.Request["plantype"] != null) ? ((base.Request["plantype"].ToString() == "income") ? "MonthPlanIncome" : "MonthPlanPayOut") : dataRowView["pathLine"].ToString());
			DataTable infoByNEWConID = this.getInfoByNEWConID(dataRowView["ContractID"].ToString());
			if (infoByNEWConID.Rows.Count > 0)
			{
				if (infoByNEWConID.Rows[0]["EarlierMoney"] != null && infoByNEWConID.Rows[0]["EarlierMoney"].ToString() != "")
				{
					num = double.Parse(infoByNEWConID.Rows[0]["EarlierMoney"].ToString());
					e.Row.Cells[5].Text = string.Format("{0:N}", num);
				}
				if (infoByNEWConID.Rows[0]["ActuallyMoney"] != null && infoByNEWConID.Rows[0]["ActuallyMoney"].ToString() != "")
				{
					num2 = double.Parse(infoByNEWConID.Rows[0]["ActuallyMoney"].ToString());
					e.Row.Cells[6].Text = string.Format("{0:N}", num2);
				}
			}
			if (num > 0.0 && num2 > 0.0)
			{
				e.Row.Cells[7].Text = string.Format("{0:P}", num2 / num);
				return;
			}
			e.Row.Cells[7].Text = "0.00%";
		}
	}
	protected Fund_Plan_MonthMainInfo GetMonthMainInfo()
	{
		Fund_Plan_MonthMainInfo fund_Plan_MonthMainInfo = new Fund_Plan_MonthMainInfo();
		bool flag = this.FA.IsExitFundPlanMainInfo(this.plantype, this.hdfMonthPalnID.Value, this.PrjGuid.ToString());
		if (flag)
		{
			fund_Plan_MonthMainInfo = this.FA.GetMainInfoByMonthPlanID(this.hdfMonthPalnID.Value);
		}
		Fund_Plan_MonthMainInfo fund_Plan_MonthMainInfo2 = new Fund_Plan_MonthMainInfo();
		fund_Plan_MonthMainInfo2.MonthPlanID = new Guid(this.hdfMonthPalnID.Value);
		fund_Plan_MonthMainInfo2.PrjGuid = this.PrjGuid;
		if (!string.IsNullOrEmpty(this.txtPlanMonth.Text.Trim()))
		{
			DateTime dateTime = Convert.ToDateTime(this.txtPlanMonth.Text);
			fund_Plan_MonthMainInfo2.PlanYear = dateTime.Year;
			fund_Plan_MonthMainInfo2.PlanMonth = dateTime.Month;
		}
		fund_Plan_MonthMainInfo2.OperatorCode = base.UserCode;
		fund_Plan_MonthMainInfo2.OperateTime = Convert.ToDateTime(this.txtadddate.Text.Trim());
		fund_Plan_MonthMainInfo2.PlanType = this.plantype;
		fund_Plan_MonthMainInfo2.FlowState = (flag ? fund_Plan_MonthMainInfo.FlowState : -1);
		fund_Plan_MonthMainInfo2.Remark = this.txtRemark.Text.Trim();
		return fund_Plan_MonthMainInfo2;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		string text = string.Empty;
		int num = 0;
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			for (int i = 0; i < this.gvwWebLineList.Rows.Count; i++)
			{
				MonthDetailModel monthDetailModel = new MonthDetailModel();
				monthDetailModel.ThisBalance = new decimal?(0m);
				monthDetailModel.UID = new Guid(this.gvwWebLineList.DataKeys[i].Value.ToString());
				monthDetailModel.MonthPlanID = new Guid(this.hdfMonthPalnID.Value);
				monthDetailModel.OrderID = new int?(0);
				HiddenField hiddenField = this.gvwWebLineList.Rows[i].FindControl("hidenContractID") as HiddenField;
				if (!string.IsNullOrEmpty(hiddenField.Value.Trim()))
				{
					if (this.findInfoByContractID(hiddenField.Value.ToString(), monthDetailModel.UID.ToString()))
					{
						text += "依据合同已存在!";
						num = 0;
						break;
					}
					monthDetailModel.ContractID = hiddenField.Value.ToString();
				}
				monthDetailModel.Plansubject = string.Empty;
				HtmlInputText htmlInputText = this.gvwWebLineList.Rows[i].FindControl("txtOldBalance") as HtmlInputText;
				monthDetailModel.OldBalance = new decimal?(string.IsNullOrEmpty(htmlInputText.Value.Trim()) ? 0m : decimal.Parse(htmlInputText.Value));
				HtmlInputText htmlInputText2 = this.gvwWebLineList.Rows[i].FindControl("txtPlanMoney") as HtmlInputText;
				monthDetailModel.PlanMoney = new decimal?(string.IsNullOrEmpty(htmlInputText2.Value.Trim()) ? 0m : decimal.Parse(htmlInputText2.Value.Trim()));
				HtmlInputText htmlInputText3 = this.gvwWebLineList.Rows[i].FindControl("txtConRemark") as HtmlInputText;
				monthDetailModel.ReMark = (string.IsNullOrEmpty(htmlInputText3.Value.Trim()) ? string.Empty : htmlInputText3.Value.Trim());
				if (!string.IsNullOrEmpty(monthDetailModel.ContractID))
				{
					if (monthDetailModel.PlanMoney < 1m)
					{
						num = 2;
						text = "计划金额应大于0";
						break;
					}
					if (this.findInfoByUID(monthDetailModel.UID.ToString()))
					{
						if (this.bll.Add(monthDetailModel))
						{
							num = 1;
						}
					}
					else
					{
						if (this.bll.Update(monthDetailModel, sqlTransaction))
						{
							num = 1;
						}
					}
				}
			}
			if (num == 1)
			{
				sqlTransaction.Commit();
				Fund_Plan_MonthMainInfo monthMainInfo = this.GetMonthMainInfo();
				monthMainInfo.MonthPlanID = new Guid(this.hdfMonthPalnID.Value);
				monthMainInfo.OperatorCode = base.UserCode;
				monthMainInfo.Remark = this.txtRemark.Text.ToString().Trim();
				if (this.FA.IsExitFundPlanMainInfo(monthMainInfo.PlanType, monthMainInfo.MonthPlanID.ToString(), monthMainInfo.PrjGuid.ToString()))
				{
					this.FA.updateMainInfo(monthMainInfo);
				}
				else
				{
					this.FA.AddPlanMainInfo(monthMainInfo);
				}
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_Flowclass' });");
				this.BindPlanDetail();
			}
			else
			{
				sqlTransaction.Rollback();
				base.RegisterScript("top.ui.alert('保存失败！ " + text + "')");
			}
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		MonthDetailLogic monthDetailLogic = new MonthDetailLogic();
		try
		{
			using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
			{
				sqlConnection.Open();
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				for (int i = 0; i < this.gvwWebLineList.Rows.Count; i++)
				{
					CheckBox checkBox = (CheckBox)this.gvwWebLineList.Rows[i].FindControl("chk");
					if (checkBox.Checked)
					{
						monthDetailLogic.Delete(new Guid(this.gvwWebLineList.DataKeys[i].Value.ToString()), sqlTransaction);
					}
				}
				sqlTransaction.Commit();
			}
			base.RegisterScript("top.ui.show('系统提示：\\n\\n删除成功！');");
			this.BindPlanDetail();
		}
		catch
		{
			base.RegisterScript("top.ui.show('系统提示：\\n\\n删除失败！');");
		}
	}
	protected void btndatabind_Click(object sender, EventArgs e)
	{
		this.BindPlanDetail();
	}
	protected void btnClose_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(string.Concat(new string[]
		{
			" window.parent.desktop.flowclass.location='../../fund/monthplan/FundMonthPlan.aspx?plantype=",
			this.plantype,
			"&year=",
			base.Request["year"],
			"&prjId=",
			base.Request["prjId"],
			"';"
		}));
		stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
		base.RegisterScript(stringBuilder.ToString());
	}
	private string GetContractNameByContractID(string _ContractID)
	{
		string result = string.Empty;
		if (_ContractID.Length == 36)
		{
			if (this.plantype != "" && this.plantype == "payout")
			{
				PayoutContract payoutContract = new PayoutContract();
				PayoutContractModel payoutContractModel = new PayoutContractModel();
				payoutContractModel = payoutContract.GetModel(_ContractID);
				if (payoutContractModel != null)
				{
					result = payoutContractModel.ContractName;
				}
			}
			else
			{
				if (this.plantype != "" && this.plantype == "income")
				{
					DataTable table = Common2.GetTable("Con_Incomet_Contract", " where ContractID='" + _ContractID + "'");
					if (table.Rows.Count > 0 && table.Rows[0]["ContractName"] != null)
					{
						result = table.Rows[0]["ContractName"].ToString();
					}
				}
			}
		}
		return result;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.gvwWebLineList.Style.Add("min-width", "1024px");
		string text = string.Empty;
		MonthDetailLogic monthDetailLogic = new MonthDetailLogic();
		int num = 0;
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			for (int i = 0; i < this.gvwWebLineList.Rows.Count; i++)
			{
				MonthDetailModel monthDetailModel = new MonthDetailModel();
				ClientScriptManager arg_5B_0 = this.Page.ClientScript;
				monthDetailModel.UID = new Guid(this.gvwWebLineList.DataKeys[i].Value.ToString());
				monthDetailModel.MonthPlanID = new Guid(this.hdfMonthPalnID.Value);
				monthDetailModel.ThisBalance = new decimal?(0m);
				HtmlInputText htmlInputText = this.gvwWebLineList.Rows[i].FindControl("txtOrderID") as HtmlInputText;
				monthDetailModel.OrderID = new int?(string.IsNullOrEmpty(htmlInputText.Value.Trim()) ? 0 : Convert.ToInt32(htmlInputText.Value.ToString()));
				HiddenField hiddenField = this.gvwWebLineList.Rows[i].FindControl("hidenContractID") as HiddenField;
				if (hiddenField.Value.ToString() != "")
				{
					if (this.findInfoByContractID(hiddenField.Value.ToString(), monthDetailModel.UID.ToString()))
					{
						text += "依据合同已存在!";
						num = 2;
						break;
					}
					monthDetailModel.ContractID = hiddenField.Value.ToString();
				}
				monthDetailModel.Plansubject = "";
				HtmlInputText htmlInputText2 = this.gvwWebLineList.Rows[i].FindControl("txtOldBalance") as HtmlInputText;
				monthDetailModel.OldBalance = new decimal?(string.IsNullOrEmpty(htmlInputText2.Value.Trim()) ? 0m : decimal.Parse(htmlInputText2.Value.ToString()));
				HtmlInputText htmlInputText3 = this.gvwWebLineList.Rows[i].FindControl("txtPlanMoney") as HtmlInputText;
				monthDetailModel.PlanMoney = new decimal?(string.IsNullOrEmpty(htmlInputText3.Value.Trim()) ? 0m : decimal.Parse(htmlInputText3.Value.ToString()));
				HtmlInputText htmlInputText4 = this.gvwWebLineList.Rows[i].FindControl("txtConRemark") as HtmlInputText;
				monthDetailModel.ReMark = (string.IsNullOrEmpty(htmlInputText4.Value.Trim()) ? string.Empty : htmlInputText4.Value.ToString());
				if (!string.IsNullOrEmpty(monthDetailModel.ContractID))
				{
					if (monthDetailModel.PlanMoney < 1m)
					{
						num = 2;
						text = "计划金额应大于0";
						break;
					}
					if (this.findInfoByUID(monthDetailModel.UID.ToString()))
					{
						if (monthDetailLogic.Add(monthDetailModel))
						{
							num = 1;
						}
					}
					else
					{
						if (monthDetailLogic.Update(monthDetailModel, sqlTransaction))
						{
							num = 1;
						}
					}
				}
			}
			if (num == 1)
			{
				sqlTransaction.Commit();
				this.BindPlanDetail();
				DataTable dataTable = this.ViewState["temDT"] as DataTable;
				DataRow dataRow = dataTable.NewRow();
				dataRow["UID"] = Guid.NewGuid();
				dataRow["MonthPlanID"] = this.hdfMonthPalnID.Value;
				dataRow["ContractID"] = "";
				dataRow["Plansubject"] = "";
				dataRow["PlanMoney"] = "0.00";
				dataRow["OldBalance"] = "0.00";
				dataRow["OrderID"] = "-1";
				dataRow["ReMark"] = "";
				dataRow["ThisBalance"] = "0.00";
				dataTable.Rows.Add(dataRow);
				this.gvwWebLineList.DataSource = dataTable;
				this.gvwWebLineList.DataBind();
			}
			else
			{
				if (num == 2)
				{
					sqlTransaction.Rollback();
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(" top.ui.alert('" + text + "');");
					base.RegisterScript(stringBuilder.ToString());
				}
			}
		}
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		string arg_05_0 = string.Empty;
		MonthDetailLogic monthDetailLogic = new MonthDetailLogic();
		int num = 0;
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			for (int i = 0; i < this.gvwWebLineList.Rows.Count; i++)
			{
				MonthDetailModel monthDetailModel = new MonthDetailModel();
				ClientScriptManager arg_40_0 = this.Page.ClientScript;
				monthDetailModel.UID = new Guid(this.gvwWebLineList.DataKeys[i].Value.ToString());
				monthDetailModel.MonthPlanID = new Guid(this.hdfMonthPalnID.Value);
				HtmlInputText htmlInputText = this.gvwWebLineList.Rows[i].FindControl("txtOrderID") as HtmlInputText;
				if (htmlInputText.Value != "" && htmlInputText.Value != "0")
				{
					monthDetailModel.OrderID = new int?(Convert.ToInt32(htmlInputText.Value.ToString()));
				}
				else
				{
					monthDetailModel.OrderID = new int?(i + 1);
				}
				HiddenField hiddenField = this.gvwWebLineList.Rows[i].FindControl("hidenContractID") as HiddenField;
				if (!(hiddenField.Value.ToString() != ""))
				{
					num = 0;
					break;
				}
				monthDetailModel.ContractID = hiddenField.Value.ToString();
				monthDetailModel.Plansubject = "";
				HtmlInputText htmlInputText2 = this.gvwWebLineList.Rows[i].FindControl("txtOldBalance") as HtmlInputText;
				if (htmlInputText2.Value.ToString() != "")
				{
					monthDetailModel.OldBalance = new decimal?(decimal.Parse(htmlInputText2.Value.ToString()));
				}
				else
				{
					monthDetailModel.OldBalance = new decimal?(0m);
				}
				HtmlInputText htmlInputText3 = this.gvwWebLineList.Rows[i].FindControl("txtPlanMoney") as HtmlInputText;
				if (htmlInputText3.Value.ToString() != "")
				{
					monthDetailModel.PlanMoney = new decimal?(decimal.Parse(htmlInputText3.Value.ToString()));
				}
				else
				{
					monthDetailModel.PlanMoney = new decimal?(0m);
				}
				HtmlInputText htmlInputText4 = this.gvwWebLineList.Rows[i].FindControl("txtRemark") as HtmlInputText;
				if (htmlInputText4.Value.ToString() != "")
				{
					monthDetailModel.ReMark = htmlInputText4.Value.ToString();
				}
				else
				{
					monthDetailModel.ReMark = "";
				}
				if (monthDetailLogic.Update(monthDetailModel, sqlTransaction))
				{
					num = 1;
				}
			}
			if (num == 1)
			{
				sqlTransaction.Commit();
				Fund_Plan_MonthMainInfo fund_Plan_MonthMainInfo = new Fund_Plan_MonthMainInfo();
				fund_Plan_MonthMainInfo.MonthPlanID = new Guid(this.hdfMonthPalnID.Value);
				fund_Plan_MonthMainInfo.OperatorCode = base.UserCode;
				fund_Plan_MonthMainInfo.Remark = this.txtRemark.Text.ToString().Trim();
				this.FA.updateMainInfo(fund_Plan_MonthMainInfo);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("btnEdit_onclick();");
				base.RegisterScript(stringBuilder.ToString());
			}
			else
			{
				sqlTransaction.Rollback();
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("btnEdit_onclick();");
				base.RegisterScript(stringBuilder2.ToString());
			}
		}
	}
	private DataTable UniteDataTable(DataTable dt1, DataTable dt2, string DTName)
	{
		DataTable dataTable = dt1.Clone();
		for (int i = 0; i < dt2.Columns.Count; i++)
		{
			dataTable.Columns.Add(dt2.Columns[i].ColumnName);
		}
		object[] array = new object[dataTable.Columns.Count];
		for (int j = 0; j < dt1.Rows.Count; j++)
		{
			dt1.Rows[j].ItemArray.CopyTo(array, 0);
			dataTable.Rows.Add(array);
		}
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			if (dataTable.Rows[k]["ContractID"].ToString() != "")
			{
				DataRow[] array2 = dt2.Select("NEWConID='" + dataTable.Rows[k]["ContractID"].ToString() + "'");
				if (array2.Length > 0)
				{
					dataTable.Rows[k]["sqcname"] = array2[0]["sqcname"].ToString();
					dataTable.Rows[k]["NEWConID"] = array2[0]["NEWConID"].ToString();
					dataTable.Rows[k]["ActuallyMoney"] = array2[0]["ActuallyMoney"].ToString();
					dataTable.Rows[k]["EarlierMoney"] = array2[0]["EarlierMoney"].ToString();
				}
			}
			dataTable.Rows[k]["pathLine"] = Fund_MonthPlanSet.pathLine;
		}
		dataTable.TableName = DTName;
		for (int l = dataTable.Rows.Count - 1; l > 0; l--)
		{
			string value = dataTable.Rows[l]["UID"].ToString();
			if (string.IsNullOrEmpty(value))
			{
				dataTable.Rows.RemoveAt(l);
			}
		}
		return dataTable;
	}
	private DataTable getInfoByNEWConID(string conid)
	{
		DataTable dataTable = this.ViewState["temDT"] as DataTable;
		if (dataTable.Rows.Count > 0)
		{
			DataTable dataTable2 = new DataTable();
			if (dataTable.Columns["NEWConID"] != null)
			{
				DataRow[] array = dataTable.Select("NEWConID='" + conid + "'");
				dataTable2 = dataTable.Clone();
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow row = array2[i];
					dataTable2.ImportRow(row);
				}
			}
			return dataTable2;
		}
		return dataTable;
	}
	private bool findInfoByContractID(string conid, string _uid)
	{
		bool result = false;
		DataTable dataTable = this.ViewState["temDT"] as DataTable;
		DataRow[] array = dataTable.Select("UID='" + _uid + "'");
		DataTable dataTable2 = new DataTable();
		dataTable2 = dataTable.Clone();
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow row = array2[i];
			dataTable2.ImportRow(row);
		}
		if (dataTable2.Rows[0]["ContractID"].ToString() == "")
		{
			DataRow[] array3 = dataTable.Select("ContractID='" + conid + "'");
			DataTable dataTable3 = new DataTable();
			dataTable3 = dataTable.Clone();
			DataRow[] array4 = array3;
			for (int j = 0; j < array4.Length; j++)
			{
				DataRow row2 = array4[j];
				dataTable3.ImportRow(row2);
			}
			result = (dataTable3.Rows.Count > 0);
		}
		return result;
	}
	private bool findInfoByUID(string _uid)
	{
		bool result = false;
		DataTable dataTable = this.ViewState["temDT"] as DataTable;
		DataRow[] array = dataTable.Select("UID='" + _uid + "'");
		DataTable dataTable2 = new DataTable();
		dataTable2 = dataTable.Clone();
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow row = array2[i];
			dataTable2.ImportRow(row);
		}
		if (dataTable2.Rows[0]["ContractID"].ToString() == "")
		{
			result = true;
		}
		return result;
	}
	private bool checkWeave()
	{
		bool result = false;
		configBll configBll = new configBll();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(" ParaName='BeginDate' UNION ALL ");
		stringBuilder.Append(" SELECT * FROM Fund_Config WHERE ParaName='EndDate' UNION ALL");
		stringBuilder.Append(" SELECT * FROM Fund_Config WHERE ParaName='isBeginDate'  ");
		IList<configModel> list = new List<configModel>();
		if (this.ViewState["list"] == null)
		{
			list = configBll.GetList(stringBuilder.ToString());
			this.ViewState["list"] = list;
		}
		else
		{
			list = (this.ViewState["list"] as IList<configModel>);
		}
		DateTime dateTime = Convert.ToDateTime(publicDbOpClass.DataTableQuary(" select getdate() ").Rows[0][0]);
		int num = 6;
		int num2 = 0;
		if (list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].ParaName == "BeginDate")
				{
					if (list[i].ParaValue.ToString() != "")
					{
						num = int.Parse(list[i].ParaValue.ToString());
					}
				}
				else
				{
					if (list[i].ParaName == "isBeginDate" && list[i].ParaValue.ToString() != "")
					{
						num2 = int.Parse(list[i].ParaValue.ToString());
					}
				}
			}
		}
		if (num2 == 1)
		{
			result = (dateTime.Day >= num);
		}
		else
		{
			if (num2 == 0)
			{
				result = false;
			}
		}
		return result;
	}
}


