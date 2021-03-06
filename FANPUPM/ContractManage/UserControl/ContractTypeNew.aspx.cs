using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_UserControl_ContractTypeNew : NBasePage, IRequiresSessionState
{
	private ContractType contractType = new ContractType();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindContractType();
		}
	}
	private void BindContractType()
	{
		this.AspNetPager1.RecordCount = this.contractType.GetCount(this.txtTypeCode.Text.Trim(), this.txtTypeName.Text.Trim(), base.UserCode, new bool?(true));
		System.Collections.Generic.List<ContractTypeModel> list = this.contractType.GetList(this.txtTypeCode.Text.Trim(), this.txtTypeName.Text.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, base.UserCode, new bool?(true));
		this.gvwContractType.DataSource = list;
		this.gvwContractType.DataBind();
	}
	protected void SearchBt_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindContractType();
	}
	protected void gvwContractType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string value = this.gvwContractType.DataKeys[e.Row.RowIndex].Value.ToString();
			string s = this.gvwContractType.DataKeys[e.Row.RowIndex].Values[1].ToString();
			HttpUtility.HtmlEncode(s);
			e.Row.Attributes["id"] = value;
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindContractType();
	}
}


