using ASP;
using cn.justwin.BLL;
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
	public partial class EntStandardEdit : NBasePage, System.Web.SessionState.IRequiresSessionState
	{
		private string _Id = "";
		private string _ClassId = "";
		private string _Type = "";
		protected EditDropDownList ddlPlanSort = new EditDropDownList();
		private string _MaxId;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.BindDropDownList();
			if (!base.IsPostBack)
			{
				this.ViewState["Type"] = base.Request.QueryString["Type"].ToString();
				this._Type = this.ViewState["Type"].ToString();
				this.HdnType.Value = this._Type;
				this.FileLink1.MID = 1721;
				this.FileLink1.Type = 1;
				if (this._Type == "Upd")
				{
					this.ViewState["Id"] = base.Request.QueryString["Id"].ToString();
					this._Id = this.ViewState["Id"].ToString();
					this.GetEntStandardInfoOfUpd(this._Id);
					this.lb_change.Text = "编辑";
					return;
				}
				if (this._Type == "Add")
				{
					this.ViewState["ClassId"] = base.Request.QueryString["ClassId"].ToString();
					this.ViewState["ID"] = EntStandardAction.GetMaxId();
					this.ViewState["ID"].ToString();
					this._ClassId = this.ViewState["ClassId"].ToString();
					this._MaxId = this.ViewState["ID"].ToString();
					this.lb_change.Text = "新增";
					this.hdnEnterGuid.Value = Guid.NewGuid().ToString();
					this.FileLink1.FID = this.hdnEnterGuid.Value.Trim();
					return;
				}
				if (this._Type == "View")
				{
					this.FileLink1.Type = 0;
					this.FileLink1.Visible = false;
					this.ControlSet();
					this.BtnSave.Enabled = false;
					this.ViewState["Id"] = base.Request.QueryString["Id"].ToString();
					this._Id = this.ViewState["Id"].ToString();
					this.GetEntStandardInfoOfUpd(this._Id);
					this.lb_change.Text = "查看";
					this.BunClose.Value = "关 闭";
					this.Literal1.Text = FileView.FilesBind(1721, this.hdnEnterGuid.Value.Trim());
					return;
				}
			}
			else
			{
				this._Type = this.ViewState["Type"].ToString();
				if (this._Type == "Upd")
				{
					this._Id = this.ViewState["Id"].ToString();
					return;
				}
				if (this._Type == "Add")
				{
					this._ClassId = this.ViewState["ClassId"].ToString();
					this._MaxId = this.ViewState["ID"].ToString();
					return;
				}
				if (this._Type == "View")
				{
					this._Id = this.ViewState["Id"].ToString();
				}
			}
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
		private void ControlSet()
		{
			this.TxtStandCode.Enabled = false;
			this.TxtStandName.Enabled = false;
			((EditDropDownList)this.lbPlanSort.FindControl("ddlPlanSort")).Enabled = false;
			this.DatePublicTime.Enabled = false;
			this.TxtRemark.Enabled = false;
		}
		private void GetEntStandardInfoOfUpd(string Id)
		{
			DataTable standardSingle = EntStandardAction.GetStandardSingle(Id);
			this.TxtStandCode.Text = standardSingle.Rows[0]["TechnologyCriterionID"].ToString();
			this.TxtStandName.Text = standardSingle.Rows[0]["TechnologyName"].ToString();
			((EditDropDownList)this.lbPlanSort.FindControl("ddlPlanSort")).Text = standardSingle.Rows[0]["TechnologyClassify"].ToString().Trim();
			this.DatePublicTime.Text = standardSingle.Rows[0]["TechnologyPromulgateTime"].ToString();
			this.TxtRemark.Text = standardSingle.Rows[0]["Remark"].ToString();
			this.hdnEnterGuid.Value = standardSingle.Rows[0]["EnterGuid"].ToString();
			this.FileLink1.FID = this.hdnEnterGuid.Value.Trim();
		}
		private EntStandardInfo GetEntStandInfo()
		{
			EntStandardInfo entStandardInfo = new EntStandardInfo();
			if (this._Type == "Upd")
			{
				this._Id = this.ViewState["Id"].ToString();
			}
			else
			{
				entStandardInfo.MainKey = int.Parse(this._MaxId);
				entStandardInfo.ClassID = new Guid(this._ClassId);
			}
			entStandardInfo.TechnologyCriterionID = this.TxtStandCode.Text.Trim();
			entStandardInfo.TechnologyName = this.TxtStandName.Text.Trim();
			entStandardInfo.TechnologyClassify = ((EditDropDownList)this.lbPlanSort.FindControl("ddlPlanSort")).Text;
			entStandardInfo.TechnologyPromulgateTime = this.DatePublicTime.Text;
			entStandardInfo.Remark = this.TxtRemark.Text.Trim();
			entStandardInfo.EnterGuid = this.hdnEnterGuid.Value.Trim();
			return entStandardInfo;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			EntStandardInfo entStandInfo = this.GetEntStandInfo();
			if (this._Type == "Add")
			{
				int num = EntStandardAction.StandardAdd(entStandInfo);
				if (num == 1)
				{
					string text = "parent.desktop.flowclass.location=parent.desktop.flowclass.location;";
					text += "alert('保存成功');";
					text += "top.frameWorkArea.window.desktop.getActive().close();";
					this.Js.Text = text;
					this.BtnSave.Enabled = false;
					return;
				}
				this.Js.Text = "alert('保存失败！'); ";
				return;
			}
			else
			{
				int num = EntStandardAction.StandUpd(entStandInfo, this._Id);
				if (num >= 1)
				{
					string text2 = "parent.desktop.flowclass.location=parent.desktop.flowclass.location;";
					text2 += "alert('保存成功');";
					text2 += "top.frameWorkArea.window.desktop.getActive().close();";
					this.Js.Text = text2;
					this.BtnSave.Enabled = false;
					return;
				}
				this.Js.Text = "alert('保存失败！'); ";
				return;
			}
		}
	}


