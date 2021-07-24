using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Domain;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_IndirectBudgetQuery : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	protected void InitPage()
	{
		string text = base.Request["id"];
		string str = string.Empty;
		try
		{
			System.Convert.ToInt32(text);
			str = OrganizationBudget.GetZZJGName(text);
			this.BindOrganGV();
		}
		catch
		{
			PrjInfoModel modelByPrjGuid = new PTPrjInfoBll().GetModelByPrjGuid(base.Request["id"]);
			if (modelByPrjGuid != null)
			{
				str = modelByPrjGuid.PrjName;
			}
			this.BindGV();
		}
		this.lblPoster.Text = str + "已上报的间接成本预算";
		this.DelClicked();
	}
	protected void BindGV()
	{
		this.gvBudget.DataSource = IndirectBudget.GetAllReport(base.Request["id"]);
		this.gvBudget.DataBind();
	}
	protected void BindOrganGV()
	{
		this.gvBudget.DataSource = OrganizationBudget.GetAllReport(base.Request["id"]);
		this.gvBudget.DataBind();
	}
	protected void DelClicked()
	{
		try
		{
			string strWhere = string.Concat(new string[]
			{
				" where I_XGID='",
				base.Request["id"],
				"' and V_YHDM='",
				base.UserCode,
				"'"
			});
			PTDbsjBll pTDbsjBll = new PTDbsjBll();
			System.Collections.Generic.List<PTDbsjModel> listArray = pTDbsjBll.GetListArray(strWhere);
			if (listArray.Count > 0)
			{
				pTDbsjBll.Delete(listArray[0].I_DBSJ_ID);
			}
		}
		catch
		{
		}
	}
}


