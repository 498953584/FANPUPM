using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class InputReceiptsEditOne : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		protected string prjCode;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["MainId"] != null)
				{
					this.ViewState["MAINID"] = base.Request.Params["MainId"].ToString();
					this.BindDropDownList(this.ViewState["MAINID"].ToString());
				}
				else
				{
					this.BindDropDownList("");
				}
				this.BindData();
			}
		}
		private void BindDropDownList(string MainID)
		{
			inputReceiptAction inputReceiptAction = new inputReceiptAction();
			DataTable existPlanYear = inputReceiptAction.GetExistPlanYear("");
			string text = "";
			for (int i = DateTime.Today.Year - 10; i < DateTime.Today.Year + 10; i++)
			{
				bool flag = false;
				for (int j = 0; j < existPlanYear.Rows.Count; j++)
				{
					if (MainID == "")
					{
						if (i.ToString() == existPlanYear.Rows[j][0].ToString())
						{
							flag = true;
						}
					}
					else
					{
						inputReceiptYear yearPlanInfo = inputReceiptAction.GetYearPlanInfo(MainID);
						text = yearPlanInfo.PlanYear;
						if (i.ToString() == existPlanYear.Rows[j][0].ToString() && i.ToString() != yearPlanInfo.PlanYear)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					this.ddlPlanYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString() + "年度", i.ToString()));
				}
			}
			if (text != "")
			{
				this.ddlPlanYear.SelectedValue = text;
			}
		}
		private void BindData()
		{
			inputReceiptAction inputReceiptAction = new inputReceiptAction();
			inputReceiptYear inputReceiptYear = new inputReceiptYear();
			if (this.ViewState["MAINID"] != null)
			{
				inputReceiptYear = inputReceiptAction.GetYearPlanInfo(this.ViewState["MAINID"].ToString());
			}
			else
			{
				inputReceiptYear = inputReceiptAction.GetYearPlanInfo("");
			}
			this.txtExaminePeople.Text = inputReceiptYear.ExaminePeople;
			this.txtExamineTime.Text = inputReceiptYear.ExamineTime.ToShortDateString();
			this.txtExamineTime.ReadOnly = true;
			this.txtRemark.Text = inputReceiptYear.Remark;
			this.txtSerialNumber.Text = inputReceiptYear.SerialNumber;
			this.txtTabPeople.Text = inputReceiptYear.TabPeople;
			this.txtTabTime.Text = inputReceiptYear.TabTime.ToShortDateString();
		}
		private void BindData(inputReceiptYear objInfo)
		{
			this.txtExaminePeople.Text = objInfo.ExaminePeople;
			this.txtExamineTime.Text = objInfo.ExamineTime.ToShortDateString();
			this.txtExamineTime.ReadOnly = true;
			this.txtRemark.Text = objInfo.Remark;
			this.txtSerialNumber.Text = objInfo.SerialNumber;
			this.txtTabPeople.Text = objInfo.TabPeople;
			this.txtTabTime.Text = objInfo.TabTime.ToShortDateString();
			this.ddlPlanYear.SelectedValue = objInfo.PlanYear;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			inputReceiptYear inputReceiptYear = new inputReceiptYear();
			inputReceiptAction inputReceiptAction = new inputReceiptAction();
			inputReceiptYear.ExaminePeople = this.txtExaminePeople.Text;
			inputReceiptYear.ExamineTime = DateTime.Parse(this.txtExamineTime.Text);
			inputReceiptYear.Remark = this.txtRemark.Text;
			inputReceiptYear.SerialNumber = this.txtSerialNumber.Text;
			inputReceiptYear.TabPeople = this.txtTabPeople.Text;
			inputReceiptYear.TabTime = DateTime.Parse(this.txtTabTime.Text);
			inputReceiptYear.PrjCode = this.prjCode;
			inputReceiptYear.PlanYear = this.ddlPlanYear.SelectedValue;
			if (this.ViewState["MAINID"] != null)
			{
				inputReceiptYear.MainId = int.Parse(this.ViewState["MAINID"].ToString());
			}
			if (inputReceiptAction.SaveYearPlan(inputReceiptYear))
			{
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.BindData(inputReceiptYear);
		}
	}


