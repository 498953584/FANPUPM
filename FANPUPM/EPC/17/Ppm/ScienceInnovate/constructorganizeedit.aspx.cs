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
	public partial class ConstructOrganizeEdit : NBasePage, System.Web.SessionState.IRequiresSessionState
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
				this.hdnType.Value = this._Type;
				this.ViewState["PRJCODE"] = this._PrjCode;
				this.ViewState["PRJNAME"] = this._PrjName;
				this.HdnPc.Value = this._PrjCode;
				this.ViewState["ID"] = this._Id;
				this.FileLink1.MID = 1720;
				if (this._Type == "Upd")
				{
					this.FileLink1.Type = 1;
					this.GetConOrgInfoOfUpd(this._Id);
					this.HdnFileId.Value = this._Id;
				}
				else
				{
					if (this._Type == "Add")
					{
						this.TxtUnit.Text = userManageDb.GetCurrentUserInfo().UserDepartName;
						this.TxtPrjName.Text = this._PrjName;
						this.DateWeave.Text = DateTime.Now.ToShortDateString();
						this.TxtWeave.Value = userManageDb.GetCurrentUserInfo().UserName;
						this.ViewState["RecordId"] = ConstructOrganizeAction.GetMaxId().ToString();
						this.HdnFileId.Value = ConstructOrganizeAction.GetMaxId().ToString();
						this._RecordId = this.ViewState["RecordId"].ToString();
						this.hdnFlowGuid.Value = Guid.NewGuid().ToString();
						this.FileLink1.Type = 1;
					}
					else
					{
						if (this._Type == "View")
						{
							this.FileLink1.Type = 0;
							this.BtnSave.Visible = false;
							this.SetControl();
							this.GetConOrgInfoOfUpd(this._Id);
							this.HdnFileId.Value = this._Id;
							this.btnClose.Value = "关 闭";
						}
					}
				}
			}
			else
			{
				this._Type = this.ViewState["TYPE"].ToString();
				if (this._Type == "Add")
				{
					this._PrjCode = this.ViewState["PRJCODE"].ToString();
					this._PrjName = this.ViewState["PRJNAME"].ToString();
					this._RecordId = this.ViewState["RecordId"].ToString();
				}
				else
				{
					if (this._Type == "Upd")
					{
						this._Id = this.ViewState["ID"].ToString();
					}
					else
					{
						if (this._Type == "View")
						{
							this._Id = this.ViewState["ID"].ToString();
						}
					}
				}
			}
			this.FileLink1.FID = this.hdnFlowGuid.Value.Trim();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void SetControl()
		{
			this.TxtUnit.Enabled = false;
			this.TxtPrjName.Enabled = false;
			this.TxtWeave.Disabled = false;
			this.DateWeave.Enabled = false;
			this.TxtScript.Enabled = false;
			this.TxtRemark.Enabled = false;
		}
		private void GetConOrgInfoOfUpd(string Id)
		{
			DataTable singleConOrgInfo = ConstructOrganizeAction.GetSingleConOrgInfo(Id);
			this.TxtUnit.Text = singleConOrgInfo.Rows[0]["FillUnit"].ToString();
			this.TxtPrjName.Text = ((singleConOrgInfo.Rows[0]["TCO_Name"] != DBNull.Value) ? singleConOrgInfo.Rows[0]["TCO_Name"].ToString() : "");
			this.TxtWeave.Value = singleConOrgInfo.Rows[0]["WeavePerson"].ToString();
			this.DateWeave.Text = Convert.ToDateTime(singleConOrgInfo.Rows[0]["WeaveTime"].ToString()).ToShortDateString();
			this.TxtScript.Text = singleConOrgInfo.Rows[0]["Maindescripe"].ToString();
			this.TxtRemark.Text = singleConOrgInfo.Rows[0]["Remark"].ToString();
			this.DDTClass.SelectedValue = singleConOrgInfo.Rows[0]["filesType"].ToString();
			this.hdnFlowGuid.Value = singleConOrgInfo.Rows[0]["FlowGuid"].ToString();
			if (singleConOrgInfo.Rows[0]["mark"].ToString().Equals("2"))
			{
				this.cbkmark.Checked = false;
				this.TrGDType.Attributes.Add("style", "display:none;");
				return;
			}
			this.cbkmark.Checked = true;
			this.TrGDType.Attributes.Add("style", "display:block;");
		}
		private ConstructOrganizeInfo GetConOrgInfo()
		{
			ConstructOrganizeInfo constructOrganizeInfo = new ConstructOrganizeInfo();
			constructOrganizeInfo.TCO_Name = this.TxtPrjName.Text;
			if (this._Type == "Add")
			{
				constructOrganizeInfo.Id = int.Parse(this._RecordId);
			}
			else
			{
				constructOrganizeInfo.Id = int.Parse(this._Id);
			}
			constructOrganizeInfo.FillUnit = this.TxtUnit.Text;
			constructOrganizeInfo.PrjCode = this._PrjCode;
			constructOrganizeInfo.WeavePerson = this.TxtWeave.Value;
			constructOrganizeInfo.WeaveTime = Convert.ToDateTime(this.DateWeave.Text);
			constructOrganizeInfo.Manidescripe = this.TxtScript.Text.Trim();
			constructOrganizeInfo.Remark = this.TxtRemark.Text.Trim();
			constructOrganizeInfo.FlowGuid = this.hdnFlowGuid.Value.Trim();
			return constructOrganizeInfo;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			ConstructOrganizeInfo conOrgInfo = this.GetConOrgInfo();
			if (this._Type == "Add")
			{
				int mark = 2;
				if (this.cbkmark.Checked)
				{
					mark = 3;
				}
				ConstructOrganizeAction.Add(conOrgInfo);
				int num = this.coBll.UpdGuidang("Prj_TechnologyConstructOrganize", mark, Convert.ToInt32(this.DDTClass.SelectedValue.Trim()), " where id=" + conOrgInfo.Id);
				if (num == 1)
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_constructorganizequery' });");
					this.BtnSave.Enabled = false;
					return;
				}
				base.RegisterScript("top.ui.alert('保存成功！');");
				return;
			}
			else
			{
				conOrgInfo.Id = int.Parse(this._Id);
				int mark2 = 2;
				if (this.cbkmark.Checked)
				{
					mark2 = 3;
				}
				ConstructOrganizeAction.Update(conOrgInfo);
				int num = this.coBll.UpdGuidang("Prj_TechnologyConstructOrganize", mark2, Convert.ToInt32(this.DDTClass.SelectedValue.Trim()), " where id=" + conOrgInfo.Id);
				if (num == 1)
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_constructorganizequery' });");
					return;
				}
				base.RegisterScript("top.ui.alert('保存成功！');");
				return;
			}
		}
	}


