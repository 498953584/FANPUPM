using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Web;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractType_Permission : NBasePage, IRequiresSessionState
{
	private ContractType contractType = new ContractType();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindContractType();
		}
	}
	protected void btnSelect_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.DataBindContractType();
	}
	protected void gvwContractType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwContractType.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindContractType();
	}
	protected void btnDataBind_Click(object sender, System.EventArgs e)
	{
	}
	private void DataBindContractType()
	{
		this.AspNetPager1.RecordCount = this.contractType.GetCount(this.txtTypeCode.Text.Trim(), this.txtTypeName.Text.Trim(), string.Empty);
		this.gvwContractType.DataSource = this.contractType.GetList(this.txtTypeCode.Text.Trim(), this.txtTypeName.Text.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, string.Empty);
		this.gvwContractType.DataBind();
		string a = ConfigHelper.Get("IsIncomeContractApprove");
		if (a == "1")
		{
			this.gvwContractType.Columns[5].Visible = true;
			return;
		}
		this.gvwContractType.Columns[5].Visible = false;
	}
}


