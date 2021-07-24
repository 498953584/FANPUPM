using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProceedOneEdit : BasePage, IRequiresSessionState
	{
		protected bool isNew;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["prjCode"] == null)
				{
					this.btnSave.Enabled = false;
					return;
				}
				this.hidPrjCode.Value = base.Request.Params["prjCode"].ToString();
				if (base.Request.Params["mainId"] != null)
				{
					this.hidMainId.Value = base.Request.Params["mainId"].ToString();
					this.hidIsNew.Value = "false";
				}
				else
				{
					this.hidIsNew.Value = "true";
				}
				this.SetControlMothd();
				this.BindData();
			}
			if (this.hidIsNew.Value == "true" && this.hidMainId.Value == "0")
			{
				this.isNew = true;
				return;
			}
			this.isNew = false;
		}
		private void SetControlMothd()
		{
			this.txtPPMAdvancementIncomeCount.Attributes["onblur"] = "checkNum(this);";
			this.txtEtcaeterasPeopleAmount.Attributes["onblur"] = "checkNum(this);";
			this.txtEndDate.ReadOnly = true;
			this.txtStartDate.ReadOnly = true;
			if (base.Request.Params["Type"].ToString() == "View")
			{
				this.btnSave.Visible = false;
				this.txtAdministerFruitUnit.ReadOnly = true;
				this.txtEtcaeterasPeopleAmount.ReadOnly = true;
				this.txtAppPrejectName.ReadOnly = true;
				this.txtPPMAdvancementIncomeCount.ReadOnly = true;
				this.txtAccount.ReadOnly = true;
				this.txtFruitContent.ReadOnly = true;
				this.txtEngineer.ReadOnly = true;
				this.txtPrejectMinister.ReadOnly = true;
				this.txtDealinMinister.ReadOnly = true;
				this.txtFruitName.ReadOnly = true;
				this.txtEndDate.Enabled = false;
				this.txtStartDate.Enabled = false;
				this.Button1.Value = "关 闭";
			}
		}
		private void BindData()
		{
			ProgressProceedAction progressProceedAction = new ProgressProceedAction();
			int mainID = (this.hidMainId.Value == "") ? 0 : int.Parse(this.hidMainId.Value);
			ProgressProceedInfo proceInfo = progressProceedAction.GetProceInfo(mainID);
			this.txtAccount.Text = proceInfo.Account;
			this.txtAdministerFruitUnit.Text = proceInfo.AdministerFruitUnit;
			this.txtAppPrejectName.Text = proceInfo.AppPrejectName;
			this.txtDealinMinister.Text = proceInfo.DealinMinister;
			this.txtEndDate.Text = proceInfo.EndDate.ToShortDateString();
			this.txtEngineer.Text = proceInfo.Engineer;
			this.txtEtcaeterasPeopleAmount.Text = proceInfo.EtcaeterasPeopleAmount.ToString();
			this.txtFruitContent.Text = proceInfo.FruitContent;
			this.txtFruitName.Text = proceInfo.FruitName;
			this.txtPPMAdvancementIncomeCount.Text = proceInfo.PPMAdvancementIncomeCount.ToString();
			this.txtPrejectMinister.Text = proceInfo.PrejectMinister;
			this.txtStartDate.Text = proceInfo.StartDate.ToShortDateString();
			this.hidMainId.Value = proceInfo.MainID.ToString();
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
			ProgressProceedInfo progressProceedInfo = new ProgressProceedInfo();
			progressProceedInfo.Account = this.txtAccount.Text;
			progressProceedInfo.AdministerFruitUnit = this.txtAdministerFruitUnit.Text;
			progressProceedInfo.AppPrejectName = this.txtAppPrejectName.Text;
			progressProceedInfo.DealinMinister = this.txtDealinMinister.Text;
			progressProceedInfo.EndDate = DateTime.Parse(this.txtEndDate.Text);
			progressProceedInfo.Engineer = this.txtEngineer.Text;
			progressProceedInfo.EtcaeterasPeopleAmount = int.Parse(this.txtEtcaeterasPeopleAmount.Text);
			progressProceedInfo.FruitContent = this.txtFruitContent.Text;
			progressProceedInfo.FruitName = this.txtFruitName.Text;
			progressProceedInfo.PPMAdvancementIncomeCount = decimal.Parse(this.txtPPMAdvancementIncomeCount.Text);
			progressProceedInfo.PrejectMinister = this.txtPrejectMinister.Text;
			progressProceedInfo.StartDate = DateTime.Parse(this.txtStartDate.Text);
			progressProceedInfo.PrjCode = this.hidPrjCode.Value;
			int mainID;
			if (!this.isNew)
			{
				progressProceedInfo.MainID = int.Parse(this.hidMainId.Value);
				mainID = progressProceedInfo.MainID;
				if (ProgressProceedAction.EditProce(progressProceedInfo))
				{
					this.js.Text = "alert(\"操作成功！\");window.returnValue=true;window.close();";
				}
				else
				{
					this.js.Text = "alert(\"操作失败！\");";
				}
			}
			else
			{
				if (ProgressProceedAction.AddNewProce(progressProceedInfo, out mainID))
				{
					this.js.Text = "alert(\"操作成功！\");window.returnValue=true;window.close();";
				}
				else
				{
					this.js.Text = "alert(\"操作失败！\");";
				}
			}
			this.hidMainId.Value = mainID.ToString();
			this.hidIsNew.Value = "false";
			this.BindData();
		}
	}


