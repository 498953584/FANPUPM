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
	public partial class ConstructOrganizeAudit : NBasePage, IRequiresSessionState
	{
		private string _Id = "";
		private string _Type = "";
		private string _Style = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.IsPostBack)
			{
				this._Id = this.ViewState["ID"].ToString();
				this._Type = this.ViewState["TYPE"].ToString();
				if (this.ViewState["STYLE"].ToString() != "")
				{
					this._Style = this.ViewState["STYLE"].ToString();
				}
				return;
			}
			this.ViewState["ID"] = base.Request.QueryString["Id"].ToString();
			this.ViewState["TYPE"] = base.Request.QueryString["Type"].ToString();
			this._Id = this.ViewState["ID"].ToString();
			this._Type = this.ViewState["TYPE"].ToString();
			this.DDLAuditResult.Items.Add(new ListItem("通过", "0"));
			this.DDLAuditResult.Items.Add(new ListItem("不通过", "1"));
			this.TxtAuditPerson.Text = userManageDb.GetCurrentUserInfo().UserName;
			this.DateAuditTime.Text = DateTime.Now.ToShortDateString();
			if (base.Request["Style"] != null)
			{
				this.ViewState["STYLE"] = base.Request.QueryString["Style"].ToString();
				this._Style = this.ViewState["STYLE"].ToString();
				this.BtnSave.Visible = false;
				this.GetPPMView(this._Id);
				return;
			}
			this.ViewState["STYLE"] = "";
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void GetPPMView(string Id)
		{
			if (this._Style == "PPM")
			{
				DataTable singleConOrgInfo = ConstructOrganizeAction.GetSingleConOrgInfo(Id);
				this.TxtAuditPerson.Text = singleConOrgInfo.Rows[0]["PPMAuditPerson"].ToString();
				this.DateAuditTime.Text = singleConOrgInfo.Rows[0]["PPMAuditTime"].ToString();
				if (singleConOrgInfo.Rows[0]["PPMAuditResult"].ToString().Trim() != "")
				{
					this.DDLAuditResult.SelectedValue = singleConOrgInfo.Rows[0]["PPMAuditResult"].ToString().Trim();
					this.RadioButtonList1.SelectedValue = singleConOrgInfo.Rows[0]["PPMAuditResult"].ToString().Trim();
				}
				else
				{
					this.DDLAuditResult.SelectedItem.Text = "未审核";
				}
				this.TxtAuditIdea.Text = singleConOrgInfo.Rows[0]["PPMAuditIdea"].ToString();
				this.TxtRemark.Text = singleConOrgInfo.Rows[0]["PPMAuditRemark"].ToString();
			}
			else
			{
				if (this._Style == "Ent")
				{
					DataTable singleConOrgInfo2 = ConstructOrganizeAction.GetSingleConOrgInfo(Id);
					this.TxtAuditPerson.Text = singleConOrgInfo2.Rows[0]["EntAuditPerson"].ToString();
					this.DateAuditTime.Text = singleConOrgInfo2.Rows[0]["EntAuditTime"].ToString();
					if (singleConOrgInfo2.Rows[0]["EntAuditResult"].ToString().Trim() != "")
					{
						this.DDLAuditResult.SelectedValue = singleConOrgInfo2.Rows[0]["EntAuditResult"].ToString().Trim();
						this.RadioButtonList1.SelectedValue = singleConOrgInfo2.Rows[0]["EntAuditResult"].ToString().Trim();
					}
					else
					{
						this.DDLAuditResult.SelectedItem.Text = "未审核";
					}
					this.TxtAuditIdea.Text = singleConOrgInfo2.Rows[0]["EntAuditIdea"].ToString();
					this.TxtRemark.Text = singleConOrgInfo2.Rows[0]["EntAuditRemark"].ToString();
				}
			}
			this.TxtAuditPerson.Enabled = false;
			this.DateAuditTime.Enabled = false;
			this.DDLAuditResult.Enabled = false;
			this.TxtAuditIdea.Enabled = false;
			this.TxtRemark.Enabled = false;
		}
		private ConstructOrganizeInfo GetPPMContructInfo()
		{
			return new ConstructOrganizeInfo
			{
				PPMAuditPerson = this.TxtAuditPerson.Text,
				PPMAuditTime = Convert.ToDateTime(this.DateAuditTime.Text),
				PPMAuditResult = this.RadioButtonList1.SelectedValue.ToString(),
				PPMAuditIdea = this.TxtAuditIdea.Text,
				PPMAuditRemark = this.TxtRemark.Text,
				Id = int.Parse(this._Id)
			};
		}
		private ConstructOrganizeInfo GetEntContructInfo()
		{
			return new ConstructOrganizeInfo
			{
				EntAuditPerson = this.TxtAuditPerson.Text,
				EntAuditTime = Convert.ToDateTime(this.DateAuditTime.Text),
				EntAuditResult = this.RadioButtonList1.SelectedValue.ToString(),
				PPMAuditResult = this.RadioButtonList1.SelectedValue.ToString(),
				EntAuditIdea = this.TxtAuditIdea.Text,
				EntAuditRemark = this.TxtRemark.Text,
				Id = int.Parse(this._Id)
			};
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			int num;
			if (!(this._Type == "Auditing"))
			{
				if (this._Type == "List")
				{
					ConstructOrganizeInfo entContructInfo = this.GetEntContructInfo();
					num = ConstructOrganizeAction.SetEntAudit(entContructInfo);
					if (num == 1)
					{
						string text = "parent.desktop.flowclass.location='/EPC/17/Frame.aspx?url=../../epc/17/Ppm/ScienceInnovate/ConstructOrganizeQuery.aspx&Type=List&PrjState=0';";
						text += "alert('审核成功');";
						text += "top.frameWorkArea.window.desktop.getActive().close();";
						this.Js.Text = text;
						return;
					}
					this.Js.Text = "alert('审核失败！'); ";
				}
				return;
			}
			ConstructOrganizeInfo pPMContructInfo = this.GetPPMContructInfo();
			num = ConstructOrganizeAction.SetPPMAudit(pPMContructInfo);
			if (num == 1)
			{
				string text2 = "parent.desktop.flowclass.location='/EPC/17/Frame.aspx?url=../../epc/17/Ppm/ScienceInnovate/ConstructOrganizeQuery.aspx&Type=Auditing&PrjState=0';";
				text2 += "alert('审核成功');";
				text2 += "top.frameWorkArea.window.desktop.getActive().close();";
				this.Js.Text = text2;
				return;
			}
			this.Js.Text = "alert('审核失败！'); ";
		}
	}


