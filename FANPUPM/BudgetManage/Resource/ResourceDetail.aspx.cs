using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Resource_ResourceDetail : NBasePage, IRequiresSessionState
{
	public static Resource resource = new Resource();
	private string resTypeId = string.Empty;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.resTypeId = base.Request.QueryString["id"];
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.AspNetPager1.RecordCount = this.GetCount();
			this.BindGv();
			this.BindDropPriceType();
			this.hdParentId.Value = base.Request.QueryString["nodeId"].ToString();
			this.hdPtNodeId.Value = base.Request.QueryString["parentId"].ToString();
		}
	}
	public void BindGv()
	{
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
				result = BudgetManage_Resource_ResourceDetail.resource.GetCount();
			}
			else
			{
				result = BudgetManage_Resource_ResourceDetail.resource.QueryCount(this.resTypeId, this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtTechnicalParameter.Text.Trim(), this.txtBrand.Text.Trim(), (this.dropPriceType.SelectedItem == null) ? string.Empty : this.dropPriceType.SelectedItem.Text, this.txtLowPrice.Text.Trim(), this.txtHighPrice.Text.Trim());
			}
		}
		catch (System.Exception)
		{
			result = 10000;
		}
		return result;
	}
	public string pnode(TreeNode node)
	{
		if (node == null)
		{
			return string.Empty;
		}
		if (node.Parent != null)
		{
			return this.pnode(node.Parent);
		}
		return node.Value;
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		new EquEquipmentService();
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldPurchaseChecked.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		}
		else
		{
			list.Add(this.hfldPurchaseChecked.Value);
		}
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			foreach (string id in list)
			{
				try
				{
					IQueryable<Res_Price> queryable =
						from p in pm2Entities.Res_Price
						where p.ResourceId == id
						select p;
					foreach (Res_Price current in queryable)
					{
						pm2Entities.DeleteObject(current);
					}
					Res_Resource res_Resource = (
						from r in pm2Entities.Res_Resource
						where r.ResourceId == id
						select r).FirstOrDefault<Res_Resource>();
					if (res_Resource != null)
					{
						pm2Entities.DeleteObject(res_Resource);
					}
					pm2Entities.SaveChanges();
					base.RegisterScriptRefresh();
				}
				catch
				{
					base.RegisterScript("alert('系统提示：\\n\\n该资源已经使用，不能删除！');");
				}
			}
		}
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
	protected void btnexecl_Click(object sender, System.EventArgs e)
	{
		DataTable exportData = this.GetExportData();
		new Common2().ExportToExecelAll(this.GetTitleByTable(exportData), "资源维护.xls", base.Request.Browser.Browser);
	}
	private DataTable GetExportData()
	{
		Resource resource = new Resource();
		return resource.Query(this.resTypeId, this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtTechnicalParameter.Text.Trim(), this.txtBrand.Text.Trim(), (this.dropPriceType.SelectedItem == null) ? string.Empty : this.dropPriceType.SelectedItem.Text, this.txtLowPrice.Text.Trim(), this.txtHighPrice.Text.Trim(), this.GetCount(), this.AspNetPager1.CurrentPageIndex);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		dt.Columns["ResourceCode"].ColumnName = "资源编号";
		dt.Columns["ResourceName"].ColumnName = "资源名称";
		dt.Columns["Brand"].ColumnName = "品牌";
		dt.Columns["TaxRate"].ColumnName = "税率";
		dt.Columns["Specification"].ColumnName = "规格";
		dt.Columns["TechnicalParameter"].ColumnName = "技术参数";
		dt.Columns["Name"].ColumnName = "单位";
		dt.Columns["InputDate"].ColumnName = "添加时间";
		dt.Columns["Series"].ColumnName = "系列";
		dt.Columns["ModelNumber"].ColumnName = "型号";
		dt.Columns["Note"].ColumnName = "备注";
		dt.Columns["CorpName"].ColumnName = "供应商";
		dt.Columns.Remove(dt.Columns["Number"]);
		dt.Columns.Remove(dt.Columns["ResourceId"]);
		dt.Columns.Remove(dt.Columns["ResourceType"]);
		return dt;
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.RecordCount = this.GetCount();
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


