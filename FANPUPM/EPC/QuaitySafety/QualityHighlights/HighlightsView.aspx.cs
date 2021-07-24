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
public partial class EPC_QuaitySafety_HighlightsView : NBasePage, IRequiresSessionState
{
	public static string _showAffairTitle = string.Empty;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			if (base.Request.QueryString["DatumClass"] == "8")
			{
				EPC_QuaitySafety_HighlightsView._showAffairTitle = "质量亮点名称";
			}
			else
			{
				if (base.Request.QueryString["DatumClass"] == "9")
				{
					EPC_QuaitySafety_HighlightsView._showAffairTitle = "质量活动名称";
				}
			}
			AffairAction affairAction = new AffairAction();
			AffairModel singleAffair = affairAction.GetSingleAffair(base.Request.QueryString["i_id"].ToString());
			this.litContent.Text = singleAffair.Context;
			this.litRemark.Text = singleAffair.Remark;
			DateTime arg_C0_0 = singleAffair.Date;
			if (singleAffair.Date.ToString() != "")
			{
				this.litDate.Text = singleAffair.Date.ToShortDateString().Replace("/", "-");
			}
			this.litName.Text = singleAffair.AffairTitle;
			int arg_120_0 = singleAffair.Mark;
			if (singleAffair.Mark.ToString() != "")
			{
				this.hdnmark.Value = singleAffair.Mark.ToString();
				if (singleAffair.Mark == 2)
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
			this.Literal1.Text = FileView.FilesBind(1755, singleAffair.i_id.ToString());
			int arg_1C3_0 = singleAffair.FilesType;
			if (singleAffair.FilesType.ToString() != "" && singleAffair.FilesType > 0)
			{
				this.DDTClass.SelectedValue = singleAffair.FilesType.ToString();
				if (this.DDTClass.SelectedItem.Text != null && this.DDTClass.SelectedItem.Text.ToString() != "")
				{
					this.litType.Text = this.DDTClass.SelectedItem.Text.ToString();
				}
			}
		}
	}
}


