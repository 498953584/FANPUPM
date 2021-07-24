using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_IndirectView : NBasePage, IRequiresSessionState
{
	private string ic = string.Empty;
	private string businessCode = string.Empty;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Initalize();
		}
	}
	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.ic = base.Request["ic"];
		}
		if (!string.IsNullOrEmpty(base.Request["businessCode"]))
		{
			this.businessCode = base.Request["businessCode"];
			this.ShowAudit1.BusiCode = this.businessCode;
		}
		base.OnInit(e);
	}
	protected void Initalize()
	{
		string text = base.Request["ic"];
		string relatedId = IndirectBudget.GetRelatedId(text);
		this.lblBllProducer.Text = PageHelper.QueryUser(this, base.UserCode);
		this.lblPrintDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
		this.bindPrjOrgName(text);
		this.Bind(relatedId);
	}
	protected void bindPrjOrgName(string relatedId)
	{
		DataTable wFInfo = IndirectBudget.GetWFInfo(relatedId);
		if (wFInfo != null && wFInfo.Rows.Count > 0)
		{
			DataRow dataRow = wFInfo.Rows[0];
			string text = dataRow["userName"].ToString();
			string text2 = dataRow["Name"].ToString();
			string text3 = System.Convert.ToDateTime(dataRow["InputDate"].ToString()).ToString("yyyy-MM-dd");
			this.lblRelatedName.Text = text2;
			this.lblInputDate.Text = text3;
			this.lblInputUser.Text = text;
		}
	}
	protected void Bind(string id)
	{
		try
		{
			new System.Guid(id);
			this.gvBudget.DataSource = IndirectBudget.GetAll(id);
		}
		catch
		{
			this.gvBudget.DataSource = OrganizationBudget.GetAll(id);
			this.lblPrjOrgName.Text = "组织机构名称";
		}
		this.gvBudget.DataBind();
	}
}


