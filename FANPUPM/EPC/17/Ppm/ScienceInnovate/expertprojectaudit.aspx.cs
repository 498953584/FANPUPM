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
	public partial class ExpertProjectAudit : NBasePage, IRequiresSessionState
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
			this.DDLAuditResult.Items.Add(new ListItem("审核通过", "0"));
			this.DDLAuditResult.Items.Add(new ListItem("未通过审核", "1"));
			this.DDLAuditResult.Items.Add(new ListItem("待审", "2"));
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
				DataTable singleExpertInfo = ExperProjectAction.GetSingleExpertInfo(Id);
				this.TxtAuditPerson.Text = singleExpertInfo.Rows[0]["PPMExamineApprovePeople"].ToString();
				this.DateAuditTime.Text = singleExpertInfo.Rows[0]["PPMExamineApproveDate"].ToString();
				if (singleExpertInfo.Rows[0]["PPMExamineApproveResult"].ToString().Trim() != "")
				{
					this.DDLAuditResult.SelectedValue = singleExpertInfo.Rows[0]["PPMExamineApproveResult"].ToString().Trim();
				}
				else
				{
					this.DDLAuditResult.SelectedItem.Text = "未通过审核";
				}
				this.TxtAuditIdea.Text = singleExpertInfo.Rows[0]["PPMExamineApproveIdea"].ToString();
				this.TxtRemark.Text = singleExpertInfo.Rows[0]["PPMRemark"].ToString();
				return;
			}
			if (this._Style == "Ent")
			{
				DataTable singleExpertInfo2 = ExperProjectAction.GetSingleExpertInfo(Id);
				this.TxtAuditPerson.Text = singleExpertInfo2.Rows[0]["EntExamineApprovePeople"].ToString();
				this.DateAuditTime.Text = singleExpertInfo2.Rows[0]["EntExamineApproveDate"].ToString();
				if (singleExpertInfo2.Rows[0]["EntExamineApproveResult"].ToString().Trim() != "")
				{
					this.DDLAuditResult.SelectedValue = singleExpertInfo2.Rows[0]["EntExamineApproveResult"].ToString().Trim();
				}
				else
				{
					this.DDLAuditResult.SelectedItem.Text = "未通过审核";
				}
				this.TxtAuditIdea.Text = singleExpertInfo2.Rows[0]["EntExamineApproveIdea"].ToString();
				this.TxtRemark.Text = singleExpertInfo2.Rows[0]["EntRemark"].ToString();
			}
		}
		private ExpertProjectInfo GetPPMContructInfo()
		{
			return new ExpertProjectInfo
			{
				PPMExamineApprovePeople = this.TxtAuditPerson.Text,
				PPMExamineApproveDate = Convert.ToDateTime(this.DateAuditTime.Text),
				PPMExamineApproveResult = int.Parse(this.DDLAuditResult.SelectedValue.ToString()),
				PPMExamineApproveIdea = this.TxtAuditIdea.Text,
				PPMRemark = this.TxtRemark.Text,
				ManiId = int.Parse(this._Id)
			};
		}
		private ExpertProjectInfo GetEntContructInfo()
		{
			return new ExpertProjectInfo
			{
				EntExamineApprovePeople = this.TxtAuditPerson.Text,
				EntExamineApproveDate = Convert.ToDateTime(this.DateAuditTime.Text),
				EntExamineApproveResult = int.Parse(this.DDLAuditResult.SelectedValue.ToString()),
				EntExamineApproveIdea = this.TxtAuditIdea.Text,
				EntRemark = this.TxtRemark.Text,
				ManiId = int.Parse(this._Id)
			};
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (this._Type == "Auditing")
			{
				ExpertProjectInfo pPMContructInfo = this.GetPPMContructInfo();
				int num = ExperProjectAction.SetPPMAudit(pPMContructInfo);
				if (num == 1)
				{
					string text = " parent.desktop.flowclass.location='/EPC/17/Frame.aspx?url=../../epc/17/Ppm/ScienceInnovate/ExpertProjectQuery.aspx&Type=Auditing&PrjState=0';";
					text += "alert('审核成功');";
					text += "top.frameWorkArea.window.desktop.getActive().close();";
					this.Js.Text = text;
					return;
				}
				this.Js.Text = "alert('审核失败！'); ";
				return;
			}
			else
			{
				ExpertProjectInfo entContructInfo = this.GetEntContructInfo();
				int num = ExperProjectAction.SetEntAudit(entContructInfo);
				if (num == 1)
				{
					string text2 = " parent.desktop.flowclass.location='/EPC/17/Frame.aspx?url=../../epc/17/Ppm/ScienceInnovate/ExpertProjectQuery.aspx&Type=List&PrjState=0';";
					text2 += "alert('审核成功');";
					text2 += "top.frameWorkArea.window.desktop.getActive().close();";
					this.Js.Text = text2;
					return;
				}
				this.Js.Text = "alert('审核失败！'); ";
				return;
			}
		}
	}


