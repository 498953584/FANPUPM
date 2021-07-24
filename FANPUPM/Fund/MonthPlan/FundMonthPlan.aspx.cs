using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Fund.FundConfig;
using cn.justwin.Fund.MonthPlan;
using cn.justwin.stockBLL.Fund.FundConfig;
using cn.justwin.Web;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_FundMonthPlan : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected Guid PrjGuid
	{
		get
		{
			object obj = this.ViewState["PRJGUID"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["PRJGUID"] = value;
		}
	}
	public string plantype
	{
		get
		{
			object obj = this.ViewState["plantype"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["plantype"] = value;
		}
	}
	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["plantype"] != null)
			{
				this.plantype = base.Request["plantype"].ToString();
				this.hfplantype.Value = this.plantype;
				this.setTitle();
			}
			this.InitPage();
			this.gvBudget.SelectedIndex = 0;
			this.checkWeave();
		}
	}
	private void checkWeave()
	{
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
		if (list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].ParaName == "BeginDate")
				{
					if (list[i].ParaValue.ToString() != "")
					{
						this.hdnBeginDate.Value = list[i].ParaValue.ToString();
					}
				}
				else
				{
					if (list[i].ParaName == "isBeginDate" && list[i].ParaValue.ToString() != "")
					{
						this.hdnIsBeginDate.Value = list[i].ParaValue.ToString();
					}
				}
			}
		}
	}
	public void setTitle()
	{
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
		this.hfPrjName.Value = byId.PrjName;
		if (this.plantype == "payout")
		{
			this.hfldAdjunctPath.Value = ConfigHelper.Get("MonthPlanPayOut");
			this.lblTitle.Text = this.hfPrjName.Value + "  支出计划";
			this.WF1.BusiCode = "091";
			return;
		}
		if (this.plantype == "income")
		{
			this.hfldAdjunctPath.Value = ConfigHelper.Get("MonthPlanIncome");
			this.lblTitle.Text = this.hfPrjName.Value + "  收入计划";
			this.WF1.BusiCode = "090";
		}
	}
	public void InitPage()
	{
		this.BindGv();
		this.SetFlow();
	}
	public void BindGv()
	{
		this.hfldPrjId.Value = this.prjId;
		this.hfldYear.Value = this.year;
		this.hfldPurchaseChecked.Value = string.Empty;
		Fund_Plan_MonthMainAction fund_Plan_MonthMainAction = new Fund_Plan_MonthMainAction();
		if (this.hfldPrjId.Value.ToString() != "" && this.hfldYear.Value.ToString() != "")
		{
			this.setTitle();
			Fund_Plan_MonthMainInfo fund_Plan_MonthMainInfo = new Fund_Plan_MonthMainInfo();
			fund_Plan_MonthMainInfo.PrjGuid = new Guid(this.hfldPrjId.Value.ToString());
			fund_Plan_MonthMainInfo.PlanType = this.plantype;
			fund_Plan_MonthMainInfo.PlanYear = Convert.ToInt32(this.hfldYear.Value.ToString());
			Random random = new Random();
			this.framShowDetail.Attributes["src"] = "showDetailMonthPlan.aspx?e=" + random.Next(1, 999) + "&mpid=";
			this.gvBudget.DataSource = fund_Plan_MonthMainAction.GetMainList(fund_Plan_MonthMainInfo);
		}
		this.gvBudget.DataBind();
	}
	private void DisabledBtnAdd(int dataCount)
	{
		if (dataCount >= 1)
		{
			this.btnAdd.Disabled = false;
			return;
		}
		this.btnAdd.Disabled = true;
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType != DataControlRowType.DataRow)
		{
			if (e.Row.RowType == DataControlRowType.Header)
			{
				if (base.Request["plantype"] == "payout")
				{
					e.Row.Cells[5].Text = "本期实际发生金额<img title=\"＝本期支出计划已通过资金支付申请审核金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
					e.Row.Cells[6].Text = "本期计划执行情况 <img title=\"＝本期实际发生金额/本期计划金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
					return;
				}
				e.Row.Cells[5].Text = "本期实际发生金额<img title=\"＝本期收入计划已回款金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
				e.Row.Cells[6].Text = "本期计划执行情况<img title=\"＝本期实际发生金额/本期计划金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
			}
			return;
		}
		e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex]["MonthPlanID"].ToString();
		e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex]["MonthPlanID"].ToString();
		e.Row.Attributes["prjguid"] = this.PrjGuid.ToString();
		DataRowView dataRowView = (DataRowView)e.Row.DataItem;
		if (e.Row.RowIndex == 0)
		{
			Random random = new Random();
			this.framShowDetail.Attributes["src"] = string.Concat(new object[]
			{
				"showDetailMonthPlan.aspx?e=",
				random.Next(1, 999),
				"&mpid=",
				this.gvBudget.DataKeys[e.Row.RowIndex]["MonthPlanID"].ToString()
			});
		}
		e.Row.Attributes["PlanMonth"] = dataRowView["PlanMonth"].ToString();
		e.Row.Attributes["PlanYear"] = dataRowView["PlanYear"].ToString();
		e.Row.Attributes["PrjGuid"] = dataRowView["PrjGuid"].ToString();
		e.Row.Attributes["flowState"] = dataRowView["FlowState"].ToString();
		DateTime dateTime = DateTime.Parse(dataRowView["PlanDate"].ToString());
		string text = dateTime.ToString("yyyy-MM-dd");
		e.Row.Attributes["onclick"] = string.Concat(new string[]
		{
			"OnRecord(this);clickRows('",
			this.gvBudget.DataKeys[e.Row.RowIndex]["MonthPlanID"].ToString(),
			"',this,'",
			text,
			"')"
		});
		string text2 = string.Concat(new string[]
		{
			"<a href='javascript:void(0);' class='link' onclick=alertMonthPlanView('",
			dataRowView["MonthPlanID"].ToString(),
			"','",
			this.plantype,
			"')>",
			string.Format("{0:yyyy年MM月}", dateTime),
			"</a>"
		});
		e.Row.Cells[2].Text = text2;
		string text3 = dataRowView["Remark"].ToString();
		if (text3 != "" && text3.Length > 8)
		{
			e.Row.Cells[12].ToolTip = text3.ToString();
			e.Row.Cells[12].Text = text3.Substring(0, 6) + "...";
		}
		double num = 0.0;
		double num2 = 0.0;
		if (dataRowView["PlanMoney"].ToString() != "")
		{
			num2 = double.Parse(dataRowView["PlanMoney"].ToString());
			e.Row.Cells[4].Text = string.Format("{0:F3}", num2).Replace(",", "");
		}
		else
		{
			e.Row.Cells[4].Text = "0.00";
			e.Row.Cells[6].Text = "0.00%";
		}
		if (dataRowView["PaymentMoney"].ToString() != "")
		{
			num = double.Parse(dataRowView["PaymentMoney"].ToString());
			e.Row.Cells[5].Text = Math.Round(num, 3).ToString();
		}
		else
		{
			e.Row.Cells[5].Text = "0.000";
			e.Row.Cells[6].Text = "0.00%";
		}
		if (num2 > 0.0 && num > 0.0)
		{
			e.Row.Cells[6].Text = Math.Round(num / num2, 2).ToString();
			return;
		}
		e.Row.Cells[6].Text = "0.00%";
	}
	protected void SetFlow()
	{
		if (this.plantype == "income")
		{
			this.WF1.BusiCode = "090";
		}
		else
		{
			this.WF1.BusiCode = "091";
		}
		this.WF1.URLParameter = string.Concat(new string[]
		{
			"plantype=",
			this.hfplantype.Value,
			"&year=",
			this.year,
			"&prjId=",
			this.prjId
		});
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		MonthDetailService monthDetailService = new MonthDetailService();
		Fund_Plan_MonthMainAction fund_Plan_MonthMainAction = new Fund_Plan_MonthMainAction();
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		string arrayToInStr = StringUtility.GetArrayToInStr(listFromJson.ToArray());
		List<string> monthDetailId = fund_Plan_MonthMainAction.getMonthDetailId(listFromJson);
		string text = string.Empty;
		if (monthDetailId.Count > 0)
		{
			text = StringUtility.GetArrayToInStr(monthDetailId.ToArray());
		}
		try
		{
			using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
			{
				sqlConnection.Open();
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				if (!string.IsNullOrEmpty(text))
				{
					monthDetailService.DeleteList(text, sqlTransaction);
				}
				fund_Plan_MonthMainAction.DeleteMainInfoList(arrayToInStr);
				sqlTransaction.Commit();
			}
			base.RegisterScript("alert('系统提示：\\n\\n删除成功！');");
			this.BindGv();
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
		}
	}
}


