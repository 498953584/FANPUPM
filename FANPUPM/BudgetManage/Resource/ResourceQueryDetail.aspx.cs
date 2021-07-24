using ASP;
using cn.justwin.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Resource_ResourceQueryDetail : NBasePage, IRequiresSessionState
{
	public static Resource resource = new Resource();
	private string resTypeId = string.Empty;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindGv();
			this.BindDropPriceType();
		}
	}
	public void BindGv()
	{
		this.resTypeId = base.Request.QueryString["id"];
		this.AspNetPager1.RecordCount = this.GetCount();
		DataTable table = this.GetTable();
		System.Collections.Generic.IList<PriceType> priceTypes = PriceType.GetPriceTypes(base.UserCode);
		if (this.gvResource.Columns.Count < 14 + priceTypes.Count)
		{
			int num = 9;
			foreach (PriceType current in priceTypes)
			{
				BoundField boundField = new BoundField();
				boundField.DataField = current.PriceTypeName;
				boundField.HeaderText = current.PriceTypeName;
				this.gvResource.Columns.Insert(num++, boundField);
			}
		}
		this.gvResource.DataSource = table;
		this.gvResource.DataBind();
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.RecordCount = this.GetCount();
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			CheckBox checkBox = new CheckBox();
			checkBox.ID = "cbAllBox";
			if (e.Row.Cells[0].Controls.Count == 0)
			{
				e.Row.Cells[0].Controls.Add(checkBox);
			}
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			try
			{
				string text = this.gvResource.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["id"] = text;
				e.Row.Attributes["corpId"] = this.gvResource.DataKeys[e.Row.RowIndex].Values[1].ToString();
				CheckBox checkBox2 = new CheckBox();
				checkBox2.ID = "cbBox";
				checkBox2.ToolTip = text;
				if (e.Row.Cells[0].Controls.Count == 0)
				{
					e.Row.Cells[0].Controls.Add(checkBox2);
				}
				int num = this.gvResource.Columns.Count - 2;
				string str = this.gvResource.DataKeys[e.Row.RowIndex].Values[2].ToString();
				e.Row.Cells[num].Text = StringUtility.GetStr(str, 10);
				e.Row.Cells[num + 1].Text = ResourceImage.GetImages(text);
			}
			catch
			{
			}
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	private DataTable GetTable()
	{
		Resource resource = new Resource();
		return resource.Query(this.resTypeId, this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtTechnicalParameter.Text.Trim(), this.txtBrand.Text.Trim(), (this.dropPriceType.SelectedItem == null) ? string.Empty : this.dropPriceType.SelectedItem.Text, this.txtLowPrice.Text.Trim(), this.txtHighPrice.Text.Trim(), this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
	}
	private void BindDropPriceType()
	{
		this.dropPriceType.DataSource = PriceType.GetPriceTypes();
		this.dropPriceType.DataBind();
		this.dropPriceType.Items.Insert(0, new ListItem("", ""));
	}
	private int GetCount()
	{
		int result = 0;
		try
		{
			if (string.IsNullOrWhiteSpace(this.resTypeId) && string.IsNullOrWhiteSpace(this.txtResourceCode.Text) && string.IsNullOrWhiteSpace(this.txtResourceName.Text) && string.IsNullOrWhiteSpace(this.txtSpecification.Text) && string.IsNullOrWhiteSpace(this.txtTechnicalParameter.Text) && string.IsNullOrWhiteSpace(this.txtBrand.Text) && this.dropPriceType.SelectedItem == null && string.IsNullOrWhiteSpace(this.txtLowPrice.Text) && string.IsNullOrWhiteSpace(this.txtHighPrice.Text))
			{
				result = BudgetManage_Resource_ResourceQueryDetail.resource.GetCount();
			}
			else
			{
				result = BudgetManage_Resource_ResourceQueryDetail.resource.QueryCount(this.resTypeId, this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtTechnicalParameter.Text.Trim(), this.txtBrand.Text.Trim(), (this.dropPriceType.SelectedItem == null) ? string.Empty : this.dropPriceType.SelectedItem.Text, this.txtLowPrice.Text.Trim(), this.txtHighPrice.Text.Trim());
			}
		}
		catch (System.Exception)
		{
			result = 10000;
		}
		return result;
	}
}


