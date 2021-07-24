using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_QuaitySafety_QualityGoal_goalview : NBasePage, IRequiresSessionState
{
	private QualityGoalAction ActObj = new QualityGoalAction();
	private QualityGoalInfo InfoObj = new QualityGoalInfo();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && base.Request.QueryString["Code"] != null && base.Request.QueryString["Code"].ToString() != "")
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			this.InfoObj = QualityGoalAction.GetModel(base.Request.QueryString["Code"].ToString());
			if (this.InfoObj != null)
			{
				this.litName.Text = this.InfoObj.ScheduleName;
				this.litRemark.Text = this.InfoObj.Remark;
				this.litGoal.Text = this.InfoObj.QualityGoal;
				int arg_D9_0 = this.InfoObj.Mark;
				if (this.InfoObj.Mark.ToString() != "")
				{
					this.hdnmark.Value = this.InfoObj.Mark.ToString();
					if (this.InfoObj.Mark == 2)
					{
						this.litGD.Text = "否";
					}
					else
					{
						this.litGD.Text = "是";
					}
				}
				else
				{
					this.litGD.Text = "否";
				}
				int arg_164_0 = this.InfoObj.FilesType;
				if (this.InfoObj.FilesType.ToString() != "" && this.InfoObj.FilesType > 0)
				{
					this.DDTClass.SelectedValue = this.InfoObj.FilesType.ToString();
					if (this.DDTClass.SelectedItem.Text != null && this.DDTClass.SelectedItem.Text.ToString() != "")
					{
						this.litType.Text = this.DDTClass.SelectedItem.Text.ToString();
					}
				}
			}
		}
	}
}


