using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractType : NBasePage, IRequiresSessionState
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
	protected void btnDataBind_Click(object sender, System.EventArgs e)
	{
		this.DataBindContractType();
	}
	protected void gvwContractType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwContractType.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldContractTypeGuid.Value))
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			if (this.hfldContractTypeGuid.Value.Contains("["))
			{
				list = JsonHelper.GetListFromJson(this.hfldContractTypeGuid.Value);
			}
			else
			{
				list.Add(this.hfldContractTypeGuid.Value);
			}
			try
			{
				this.contractType.Delete(list.ToArray());
				this.DataBindContractType();
				base.RegisterScript("top.ui.show('删除成功')");
			}
			catch
			{
				base.RegisterScript("top.ui.alert('此合同类型已经使用，请先删除于此有关的合同！')");
			}
		}
	}
	protected void btnSelect_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.DataBindContractType();
	}
	private void DataBindContractType()
	{
		this.AspNetPager1.RecordCount = this.contractType.GetCount(this.Replace(this.txtTypeCode.Text.Trim()), this.Replace(this.txtTypeName.Text.Trim()), base.UserCode);
		this.gvwContractType.DataSource = this.contractType.GetList(this.Replace(this.txtTypeCode.Text.Trim()), this.Replace(this.txtTypeName.Text.Trim()), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, base.UserCode);
		this.gvwContractType.DataBind();
		this.gvwContractType.Columns[5].Visible = false;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindContractType();
	}
	public string Replace(string sourceStr)
	{
		sourceStr = sourceStr.Replace("'", "''");
		sourceStr = sourceStr.Replace("%", "[%]");
		return sourceStr;
	}
}


