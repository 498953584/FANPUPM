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
using UserControl;
	public partial class TechnologyStandardEdit : NBasePage, System.Web.SessionState.IRequiresSessionState
	{
		private string _PrjCode = "";
		private string _Type = "";
		private string _MaxId = "";
		protected EditDropDownList ddlPlanSort = new EditDropDownList();
		private string _Id;
		private ConstructOrganizeBBl coBll = new ConstructOrganizeBBl();

		protected void Page_Load(object sender, EventArgs e)
		{
			this.BindDropDownList();
			if (!base.IsPostBack)
			{
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
				this.ViewState["PRJCODE"] = base.Request.QueryString["pc"].ToString();
				this.ViewState["TYPE"] = base.Request.QueryString["Type"].ToString();
				this._PrjCode = this.ViewState["PRJCODE"].ToString();
				this._Type = this.ViewState["TYPE"].ToString();
				this.HdnTypeOld.Value = this._Type;
				this.FileLink1.MID = 1725;
				if (this._Type == "Upd")
				{
					this.FileLink1.Type = 1;
					this.TxtStandCode.Enabled = false;
					this.ViewState["ID"] = base.Request.QueryString["Id"].ToString();
					this._Id = this.ViewState["ID"].ToString();
					this.GetStandardInfoOfUpd(this._Id);
					this.lb_change.Text = "修改";
					return;
				}
				if (this._Type == "Add")
				{
					this.ViewState["MAXID"] = TechnologyStandardAction.GetMaxId();
					this._MaxId = this.ViewState["MAXID"].ToString();
					this.FileLink1.Type = 1;
					this.hdnTechGuid.Value = Guid.NewGuid().ToString();
					this.FileLink1.FID = this.hdnTechGuid.Value.Trim();
					this.lb_change.Text = "新增";
					return;
				}
				if (this._Type == "View")
				{
					this.BtnSave.Attributes.Add("style", "display:none;");
					this.SetControl();
					this.TxtStandCode.Enabled = false;
					this.ViewState["ID"] = base.Request.QueryString["Id"].ToString();
					this._Id = this.ViewState["ID"].ToString();
					this.GetStandardInfoOfUpd(this._Id);
					this.lb_change.Text = "查看";
					this.BunClose.Value = "关 闭";
					this.hdnType.Value = "view";
					this.cbkmark.Disabled = true;
					this.DDTClass.Enabled = false;
					this.FileLink1.Type = 1;
					this.FileLink1.Visible = false;
					this.Literal1.Text = FileView.FilesBind(1725, this.hdnTechGuid.Value);
					return;
				}
			}
			else
			{
				this._Type = this.ViewState["TYPE"].ToString();
				if (this._Type == "Add")
				{
					this._PrjCode = this.ViewState["PRJCODE"].ToString();
					this._MaxId = this.ViewState["MAXID"].ToString();
					return;
				}
				this._Id = this.ViewState["ID"].ToString();
			}
		}
		private void SetControl()
		{
			this.TxtStandCode.Enabled = false;
			((EditDropDownList)this.lbPlanSort.FindControl("ddlPlanSort")).Enabled = false;
			this.TxtStandName.Enabled = false;
			this.DatePublic.Enabled = false;
			this.TxtRemark.Enabled = false;
		}
		private void BindDropDownList()
		{
			this.ddlPlanSort.ID = "ddlPlanSort";
			this.ddlPlanSort.Items.Add(new System.Web.UI.WebControls.ListItem("十七冶标准", "十七冶标准"));
			this.ddlPlanSort.Items.Add(new System.Web.UI.WebControls.ListItem("行业标准", "行业标准"));
			this.ddlPlanSort.Items.Add(new System.Web.UI.WebControls.ListItem("地方标准", "地方标准"));
			this.ddlPlanSort.Items.Add(new System.Web.UI.WebControls.ListItem("国家标准", "国家标准"));
			this.lbPlanSort.Controls.Add(this.ddlPlanSort);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private TechnologyStandardInfo GetStandardInfo()
		{
			TechnologyStandardInfo technologyStandardInfo = new TechnologyStandardInfo();
			if (this._Type == "Add")
			{
				technologyStandardInfo.MainId = int.Parse(this._MaxId);
			}
			else
			{
				technologyStandardInfo.MainId = int.Parse(this._Id);
			}
			technologyStandardInfo.PrjCode = this._PrjCode;
			technologyStandardInfo.TechnologyCriterionID = this.TxtStandCode.Text.Trim();
			technologyStandardInfo.TechnologyName = this.TxtStandName.Text.Trim();
			technologyStandardInfo.TechnologyClassify = ((EditDropDownList)this.lbPlanSort.FindControl("ddlPlanSort")).Text;
			technologyStandardInfo.TechnologyPromulgateTime = this.DatePublic.Text;
			technologyStandardInfo.Remark = this.TxtRemark.Text.Trim();
			technologyStandardInfo.TechGuid = this.hdnTechGuid.Value.Trim();
			return technologyStandardInfo;
		}
		private void GetStandardInfoOfUpd(string Id)
		{
			DataTable standardOfSingle = TechnologyStandardAction.GetStandardOfSingle(Id);
			this.TxtStandCode.Text = standardOfSingle.Rows[0]["TechnologyCriterionID"].ToString();
			((EditDropDownList)this.lbPlanSort.FindControl("ddlPlanSort")).Text = standardOfSingle.Rows[0]["TechnologyClassify"].ToString().Trim();
			this.TxtStandName.Text = standardOfSingle.Rows[0]["TechnologyName"].ToString();
			this.DatePublic.Text = standardOfSingle.Rows[0]["TechnologyPromulgateTime"].ToString();
			this.TxtRemark.Text = standardOfSingle.Rows[0]["Remark"].ToString();
			this.hdnTechGuid.Value = standardOfSingle.Rows[0]["TechGuid"].ToString();
			this.FileLink1.FID = this.hdnTechGuid.Value.Trim();
			this.hdnmark.Value = standardOfSingle.Rows[0]["mark"].ToString();
			this.DDTClass.SelectedValue = standardOfSingle.Rows[0]["filesType"].ToString();
			if (standardOfSingle.Rows[0]["mark"].ToString().Equals("2"))
			{
				this.cbkmark.Checked = false;
				this.TrGDType.Attributes.Add("style", "display:none;");
				return;
			}
			this.cbkmark.Checked = true;
			this.TrGDType.Attributes.Add("style", "display:block;");
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			TechnologyStandardInfo standardInfo = this.GetStandardInfo();
			int mark = 2;
			if (this.cbkmark.Checked)
			{
				mark = 3;
			}
			if (this._Type == "Add")
			{
				TechnologyStandardAction.TechnologyAdd(standardInfo);
				int num = this.coBll.UpdGuidang("Prj_TechnologyCriterion", mark, Convert.ToInt32(this.DDTClass.SelectedValue.Trim()), " where MainId=" + standardInfo.MainId);
				if (num == 1)
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_technologystandardquery' });");
					this.BtnSave.Enabled = false;
					return;
				}
				base.RegisterScript("top.ui.alert('保存失败！'); ");
				return;
			}
			else
			{
				TechnologyStandardAction.TechnologyUpd(standardInfo);
				int num = this.coBll.UpdGuidang("Prj_TechnologyCriterion", mark, Convert.ToInt32(this.DDTClass.SelectedValue.Trim()), " where MainId=" + standardInfo.MainId);
				if (num == 1)
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_technologystandardquery' });");
					return;
				}
				base.RegisterScript("top.ui.alert('保存失败！'); ");
				return;
			}
		}
	}


