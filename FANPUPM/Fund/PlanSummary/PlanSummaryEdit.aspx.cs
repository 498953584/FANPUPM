using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.PlanSummary.BLL;
using cn.justwin.Fund.PlanSummary.Model;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccounIncome_AccounIncomeEdit : NBasePage, IRequiresSessionState
{
	private PlanSummaryMain MainModel;
	private PlanSummaryDetailBLL DetaillBLL = new PlanSummaryDetailBLL();
	private PlanSummaryMainBLL MainBLL = new PlanSummaryMainBLL();
	private PlanSummaryDetail detailModel;
	private decimal LastPlanMoney = 0.00m;
	private decimal LastActualMoney = 0.00m;
	private decimal CurrPlanMoney = 0.00m;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.HdnPlanType.Value = base.Request["PlanType"].ToString();
			if (base.Request.QueryString["Action"] == "Add")
			{
				this.txtInPeople.Text = PageHelper.QueryUser(this, base.UserCode);
				this.hdnPeopleCode.Value = base.UserCode;
				this.txtInDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.hdnAccountID.Value = Guid.NewGuid().ToString();
			}
			else
			{
				this.txtDate.ReadOnly = true;
				this.hdnAccountID.Value = base.Request.QueryString["AccountID"].ToString();
				this.MainModel = this.MainBLL.GetModel(new Guid(this.hdnAccountID.Value));
				this.txtDate.Text = this.MainModel.PlanName.ToString();
				this.txtre.Text = this.MainModel.remark;
				this.hdnPeopleCode.Value = this.MainModel.Reporter.ToString();
				this.txtInPeople.Text = PageHelper.QueryUser(this, this.MainModel.Reporter.ToString());
				this.txtInDate.Text = Convert.ToDateTime(this.MainModel.ReportTime).ToString("yyyy-MM-dd");
				this.BtnPlanSummary.Text = "按提交的计划重新生成";
				this.BindDetail();
			}
			this.FileUpload1.Class = "PlanSummary";
			this.FileUpload1.RecordCode = this.hdnAccountID.Value;
		}
	}
	private void BindDetail()
	{
		DataTable list = this.DetaillBLL.GetList(" MID ='" + this.hdnAccountID.Value + "' ");
		this.SetSource(list);
	}
	private PlanSummaryMain getModel()
	{
		this.MainModel = new PlanSummaryMain();
		this.MainModel.PlanName = this.txtDate.Text;
		this.MainModel.PlanType = base.Request["PlanType"].ToString();
		this.MainModel.remark = this.txtre.Text;
		this.MainModel.Reporter = this.hdnPeopleCode.Value;
		this.MainModel.ReportTime = new DateTime?(Convert.ToDateTime(this.txtInDate.Text));
		this.MainModel.MID = new Guid(this.hdnAccountID.Value);
		return this.MainModel;
	}
	private void SaveDetail(string MID)
	{
		this.DetaillBLL.DeleteByMain(MID);
		this.SaveView();
		DataTable dataTable = new DataTable();
		if (this.ViewState["DetailTab"] != null)
		{
			dataTable = (this.ViewState["DetailTab"] as DataTable);
		}
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			this.detailModel = new PlanSummaryDetail();
			this.detailModel.DID = new Guid(dataTable.Rows[i]["DID"].ToString());
			this.detailModel.MID = MID;
			this.detailModel.PlanID = dataTable.Rows[i]["MonthPlanID"].ToString();
			this.detailModel.PrjID = dataTable.Rows[i]["PrjGuid"].ToString();
			this.detailModel.Remark = dataTable.Rows[i]["Remark"].ToString();
			this.detailModel.InputPeople = dataTable.Rows[i]["OperatorCode"].ToString();
			this.detailModel.InputTime = new DateTime?(Convert.ToDateTime(dataTable.Rows[i]["OperateTime"].ToString()));
			this.detailModel.LastActualMoney = new decimal?(Convert.ToDecimal(dataTable.Rows[i]["LastActualMoney"].ToString()));
			this.detailModel.LastPlanMoney = new decimal?(Convert.ToDecimal(dataTable.Rows[i]["LastPlanMoney"].ToString()));
			this.detailModel.CurrPlanMoney = new decimal?(Convert.ToDecimal(dataTable.Rows[i]["CurrPlanMoney"].ToString()));
			this.DetaillBLL.Add(this.detailModel);
		}
	}
	private void SaveView()
	{
		for (int i = 0; i < this.gvwDetail.Rows.Count; i++)
		{
			GridViewRow gridViewRow = this.gvwDetail.Rows[i];
			DataTable dataTable = this.ViewState["DetailTab"] as DataTable;
			DataRow dataRow = dataTable.Rows[gridViewRow.RowIndex];
			TextBox textBox = gridViewRow.Cells[9].FindControl("txtRemark") as TextBox;
			dataRow["Remark"] = textBox.Text.ToString();
			this.ViewState["DetailTab"] = dataTable;
		}
	}
	private void GVBIND()
	{
		string text = " (FlowState=1) and PlanType='" + this.HdnPlanType.Value + "' ";
		DataTable source = new DataTable();
		text = text + " and CONVERT(VARCHAR(12) , planDate, 23 ) like '%" + this.txtDate.Text + "%'";
		source = this.DetaillBLL.getPlanSummary(text, this.HdnPlanType.Value);
		this.SetSource(source);
	}
	private void SetSource(DataTable dt)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add(new DataColumn("DID"));
		dataTable.Columns.Add(new DataColumn("MonthPlanID"));
		dataTable.Columns.Add(new DataColumn("PrjGuid"));
		dataTable.Columns.Add(new DataColumn("PrjName"));
		dataTable.Columns.Add(new DataColumn("LastPlanMoney"));
		dataTable.Columns.Add(new DataColumn("LastActualMoney"));
		dataTable.Columns.Add(new DataColumn("CurrPlanMoney"));
		dataTable.Columns.Add(new DataColumn("OperatorCode"));
		dataTable.Columns.Add(new DataColumn("OperateTime"));
		dataTable.Columns.Add(new DataColumn("Remark"));
		dataTable.Columns.Add(new DataColumn("FlowState"));
		dataTable.Columns.Add(new DataColumn("BL"));
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["DID"] = new Guid(dt.Rows[i]["DID"].ToString());
			dataRow["MonthPlanID"] = dt.Rows[i]["MonthPlanID"].ToString();
			dataRow["PrjGuid"] = dt.Rows[i]["PrjGuid"].ToString();
			dataRow["PrjName"] = dt.Rows[i]["PrjName"].ToString();
			dataRow["LastPlanMoney"] = Convert.ToDecimal(dt.Rows[i]["LastPlanMoney"].ToString());
			if (string.IsNullOrEmpty(dt.Rows[i]["LastActualMoney"].ToString()))
			{
				dataRow["LastActualMoney"] = "0.00";
			}
			else
			{
				dataRow["LastActualMoney"] = Convert.ToDecimal(dt.Rows[i]["LastActualMoney"].ToString());
			}
			dataRow["CurrPlanMoney"] = Convert.ToDecimal(dt.Rows[i]["CurrPlanMoney"].ToString());
			dataRow["OperatorCode"] = dt.Rows[i]["OperatorCode"].ToString();
			dataRow["OperateTime"] = Convert.ToDateTime(dt.Rows[i]["OperateTime"].ToString());
			dataRow["Remark"] = dt.Rows[i]["Remark"].ToString();
			dataRow["FlowState"] = dt.Rows[i]["FlowState"].ToString();
			dataRow["BL"] = Convert.ToDecimal(dt.Rows[i]["BL"].ToString());
			dataTable.Rows.Add(dataRow);
			this.LastPlanMoney += Convert.ToDecimal(dataRow["LastPlanMoney"].ToString());
			this.LastActualMoney += Convert.ToDecimal(dataRow["LastActualMoney"].ToString());
			this.CurrPlanMoney += Convert.ToDecimal(dataRow["CurrPlanMoney"].ToString());
		}
		this.ViewState["DetailTab"] = dataTable;
		this.gvwDetail.DataSource = dataTable;
		this.gvwDetail.DataBind();
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		this.MainModel = this.getModel();
		if (base.Request.QueryString["Action"] == "Add")
		{
			this.MainModel.FlowState = new int?(-1);
			if (!this.MainBLL.Exists(this.MainModel.PlanName, base.Request["PlanType"].ToString()))
			{
				this.SaveDetail(this.MainModel.MID.ToString());
				this.MainBLL.Add(this.MainModel);
				stringBuilder.Append("winclose('ModifyEdit', 'PlanSummary.aspx?PlanType=" + base.Request["PlanType"].ToString() + "&State=Manage', true)");
			}
			else
			{
				this.SetSource(new DataTable());
				stringBuilder.Append("alert('系统提示：\\n\\n该月份的计划汇总已存在！');");
			}
		}
		else
		{
			this.SaveDetail(this.MainModel.MID.ToString());
			if (this.MainBLL.Update(this.MainModel))
			{
				stringBuilder.Append("winclose('ModifyEdit', 'PlanSummary.aspx?PlanType=" + base.Request["PlanType"].ToString() + "&State=Manage', true)");
			}
		}
		base.RegisterScript(stringBuilder.ToString());
	}
	protected void BtnPlanSummary_Click(object sender, EventArgs e)
	{
		this.BtnPlanSummary.Text = "按提交的计划重新生成";
		if (base.Request.QueryString["Action"] == "Add" && this.MainBLL.Exists(this.txtDate.Text, base.Request["PlanType"].ToString()))
		{
			base.RegisterScript("alert('系统提示：\\n\\n该月份的计划汇总已存在！');");
			return;
		}
		this.GVBIND();
	}
	protected void BtnChangeState_Click(object sender, EventArgs e)
	{
		string value = this.HdnPlanGuid.Value;
		this.SaveView();
		DataTable dataTable = this.ViewState["DetailTab"] as DataTable;
		foreach (GridViewRow arg_46_0 in this.gvwDetail.Rows)
		{
			foreach (DataRow dataRow in dataTable.Rows)
			{
				if (dataRow["MonthPlanID"].ToString() == value.ToString())
				{
					this.MainBLL.UpdateFlow(value);
					dataTable.Rows.Remove(dataRow);
					break;
				}
			}
		}
		this.SetSource(dataTable);
	}
	protected void gvwDetail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwDetail.DataKeys[e.Row.RowIndex].Value.ToString();
			Label label = (Label)e.Row.Cells[10].FindControl("lblstate");
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"ClickRow('",
				this.gvwDetail.DataKeys[e.Row.RowIndex].Value.ToString(),
				"','",
				label.Text.ToString(),
				"');"
			});
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Attributes.Add("Style", "text-align:right;");
			e.Row.Cells[1].Attributes.Add("Style", "font-weight:bold;");
			e.Row.Cells[2].Attributes.Add("Style", "font-weight:bold;");
			e.Row.Cells[3].Attributes.Add("Style", "font-weight:bold;");
			e.Row.Cells[4].Attributes.Add("Style", "font-weight:bold;");
			e.Row.Cells[5].Attributes.Add("Style", "font-weight:bold;");
			e.Row.Cells[1].Text = "总计";
			e.Row.Cells[2].Text = this.LastPlanMoney.ToString();
			e.Row.Cells[3].Text = this.LastActualMoney.ToString();
			if (this.LastPlanMoney.ToString() == "0.000")
			{
				e.Row.Cells[4].Text = "0.00%";
			}
			else
			{
				e.Row.Cells[4].Text = Math.Round(Convert.ToDecimal(this.LastActualMoney / this.LastPlanMoney * 100m), 2).ToString() + "%";
			}
			e.Row.Cells[5].Text = this.CurrPlanMoney.ToString();
		}
	}
}


