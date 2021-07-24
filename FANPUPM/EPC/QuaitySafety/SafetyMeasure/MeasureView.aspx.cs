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
public partial class EPC_QuaitySafety_SafetyMeasure_MeasureView : NBasePage, IRequiresSessionState
{
	private SafetyMeasureInfo InfoObj = new SafetyMeasureInfo();
	private SafetyMeasureAction ActObj = new SafetyMeasureAction();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && base.Request.QueryString["Code"] != null && base.Request.QueryString["Code"].ToString() != "")
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			this.InfoObj = this.ActObj.GetSafetyMeasureModel(base.Request.QueryString["Code"].ToString());
			if (this.InfoObj != null)
			{
				this.litName.Text = this.InfoObj.TaskName;
				this.litSaftyMeasure.Text = this.InfoObj.SaftyMeasure;
				this.litRemark.Text = this.InfoObj.Remark;
				int arg_DF_0 = this.InfoObj.Mark;
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
				int arg_16A_0 = this.InfoObj.FilesType;
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


