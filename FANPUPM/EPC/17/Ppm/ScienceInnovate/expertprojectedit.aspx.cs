using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ExpertProjectEdit : NBasePage, IRequiresSessionState
	{
		private string _Type = "";
		private string _Id = "";
		private string _PrjCode = "";
		private string _PrjName = "";
		private string _RecordId;
		private ConstructOrganizeBBl coBll = new ConstructOrganizeBBl();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
				this._Type = base.Request.QueryString["Type"].ToString();
				this._PrjCode = base.Request.QueryString["pc"].ToString();
				this._PrjName = base.Request.QueryString["pn"].ToString();
				this._Id = base.Request.QueryString["Id"].ToString();
				this.ViewState["TYPE"] = this._Type;
				this.ViewState["PRJCODE"] = this._PrjCode;
				this.ViewState["PRJNAME"] = this._PrjName;
				this.ViewState["ID"] = this._Id;
				this.FileLink1.MID = 1722;
				if (this._Type == "Upd")
				{
					this.FileLink1.Type = 1;
					this.GetExpertInfoOfUpd(this._Id);
					this.lb_change.Text = "修改";
					return;
				}
				if (this._Type == "Add")
				{
					this.TxtUnit.Text = userManageDb.GetCurrentUserInfo().UserDepartName;
					this.HdnUnit.Value = userManageDb.GetCurrentUserInfo().UserDepartCode;
					this.TxtPrjName.Text = this._PrjName;
					this.DateWeaverTime.Text = DateTime.Now.ToShortDateString();
					this.DateFillTime.Text = DateTime.Now.ToShortDateString();
					this.TxtWeave.Text = userManageDb.GetCurrentUserInfo().UserName;
					this.HdnWeaver.Value = userManageDb.GetCurrentUserInfo().UserCode;
					this.TxtFillName.Text = userManageDb.GetCurrentUserInfo().UserName;
					this.HdnFillName.Value = userManageDb.GetCurrentUserInfo().UserCode;
					this.ViewState["RecordId"] = ExperProjectAction.GetMaxId().ToString();
					this._RecordId = this.ViewState["RecordId"].ToString();
					this.hdnFlowGuid.Value = Guid.NewGuid().ToString();
					this.FileLink1.FID = this.hdnFlowGuid.Value.Trim();
					this.FileLink1.Type = 1;
					this.lb_change.Text = "新增";
					return;
				}
				if (this._Type == "View")
				{
					this.FileLink1.Type = 0;
					this.BtnSave.Visible = false;
					this.SetControl();
					this.GetExpertInfoOfUpd(this._Id);
					this.lb_change.Text = "查看";
					this.BunClose.Value = "关 闭";
					return;
				}
			}
			else
			{
				this._Type = this.ViewState["TYPE"].ToString();
				this._PrjCode = this.ViewState["PRJCODE"].ToString();
				if (this._Type == "Add")
				{
					this._PrjName = this.ViewState["PRJNAME"].ToString();
					this._RecordId = this.ViewState["RecordId"].ToString();
					return;
				}
				this._Id = this.ViewState["ID"].ToString();
			}
		}
		private void SetControl()
		{
			this.TxtUnit.Enabled = false;
			this.TxtPrjName.Enabled = false;
			this.TxtExperName.Enabled = false;
			this.TxtAuditCircs.Enabled = false;
			this.TxtWeave.Enabled = false;
			this.DateWeaverTime.Enabled = false;
			this.TxtFillName.Enabled = false;
			this.DateFillTime.Enabled = false;
			this.TxtScript.Enabled = false;
			this.TxtRemark.Enabled = false;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void GetExpertInfoOfUpd(string Id)
		{
			DataTable singleExpertInfo = ExperProjectAction.GetSingleExpertInfo(Id);
			this.TxtUnit.Text = singleExpertInfo.Rows[0]["V_BMMC"].ToString();
			this.HdnUnit.Value = singleExpertInfo.Rows[0]["PrjCode"].ToString();
			this.TxtPrjName.Text = singleExpertInfo.Rows[0]["PrjName"].ToString();
			this.TxtExperName.Text = singleExpertInfo.Rows[0]["SchemeName"].ToString();
			this.TxtAuditCircs.Text = singleExpertInfo.Rows[0]["ExamineApprove"].ToString();
			this.TxtWeave.Text = singleExpertInfo.Rows[0]["Weavemc"].ToString();
			this.HdnWeaver.Value = singleExpertInfo.Rows[0]["WeavePeople"].ToString();
			this.DateWeaverTime.Text = Convert.ToDateTime(singleExpertInfo.Rows[0]["WeaveDate"].ToString()).ToShortDateString();
			this.TxtFillName.Text = singleExpertInfo.Rows[0]["Fillmc"].ToString();
			this.HdnFillName.Value = singleExpertInfo.Rows[0]["FillPeople"].ToString();
			this.DateFillTime.Text = Convert.ToDateTime(singleExpertInfo.Rows[0]["FillDate"].ToString()).ToShortDateString();
			this.TxtScript.Text = singleExpertInfo.Rows[0]["SchemEbewrite"].ToString();
			this.TxtRemark.Text = singleExpertInfo.Rows[0]["Remark"].ToString();
			this.hdnFlowGuid.Value = singleExpertInfo.Rows[0]["FlowGuid"].ToString();
			this.FileLink1.FID = this.hdnFlowGuid.Value.Trim();
			this.DDTClass.SelectedValue = singleExpertInfo.Rows[0]["filesType"].ToString();
			if (singleExpertInfo.Rows[0]["mark"].ToString().Equals("2"))
			{
				this.cbkmark.Checked = false;
				this.TrGDType.Attributes.Add("style", "display:none;");
				return;
			}
			this.cbkmark.Checked = true;
			this.TrGDType.Attributes.Add("style", "display:block;");
		}
		private ExpertProjectInfo GetExpertInfo()
		{
			ExpertProjectInfo expertProjectInfo = new ExpertProjectInfo();
			if (this._Type == "Add")
			{
				expertProjectInfo.ManiId = int.Parse(this._RecordId);
			}
			else
			{
				expertProjectInfo.ManiId = int.Parse(this._Id);
			}
			expertProjectInfo.PrjCode = this.HdnUnit.Value;
			expertProjectInfo.PrejectName = this._PrjCode;
			expertProjectInfo.SchemeName = this.TxtExperName.Text;
			expertProjectInfo.WeavePeople = this.HdnWeaver.Value;
			expertProjectInfo.WeaveDate = Convert.ToDateTime(this.DateWeaverTime.Text);
			expertProjectInfo.FillPeople = this.HdnFillName.Value;
			expertProjectInfo.FillDate = Convert.ToDateTime(this.DateFillTime.Text);
			expertProjectInfo.ExamineApprove = this.TxtAuditCircs.Text;
			expertProjectInfo.SchemEbewrite = this.TxtScript.Text.Trim();
			expertProjectInfo.Remark = this.TxtRemark.Text.Trim();
			expertProjectInfo.FlowGuid = this.hdnFlowGuid.Value.Trim();
			return expertProjectInfo;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.TxtExperName.Text))
			{
				base.RegisterScript("top.ui.alert('方案名称不能为空');");
				return;
			}
			ExpertProjectInfo expertInfo = this.GetExpertInfo();
			int mark = 2;
			if (this.cbkmark.Checked)
			{
				mark = 3;
			}
			if (this._Type == "Add")
			{
				ExperProjectAction.ExperAdd(expertInfo);
				int num = this.coBll.UpdGuidang("Prj_ExpertSchemeManage", mark, Convert.ToInt32(this.DDTClass.SelectedValue.Trim()), " where MainID=" + expertInfo.ManiId);
				if (num == 1)
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_expertprojectquery' })");
					this.BtnSave.Enabled = false;
					return;
				}
				base.RegisterScript("top.ui.alert('保存失败');");
				return;
			}
			else
			{
				ExperProjectAction.ExperUpd(expertInfo, this._Id);
				int num = this.coBll.UpdGuidang("Prj_ExpertSchemeManage", mark, Convert.ToInt32(this.DDTClass.SelectedValue.Trim()), " where MainId=" + expertInfo.ManiId);
				if (num == 1)
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_expertprojectquery' })");
					return;
				}
				base.RegisterScript("top.ui.alert('保存失败');");
				return;
			}
		}
	}


