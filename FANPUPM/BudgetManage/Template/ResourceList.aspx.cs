using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Template_BudTemplateResource : NBasePage, IRequiresSessionState
{
	private string itemId = string.Empty;
	private string tempType = string.Empty;
	private string tempId = string.Empty;
	private MeterialPlanStock materialStock = new MeterialPlanStock();

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request.QueryString["itemId"] != null)
		{
			this.itemId = base.Request.QueryString["itemId"];
		}
		if (base.Request.QueryString["tempType"] != null)
		{
			this.tempType = base.Request.QueryString["tempType"];
		}
		if (base.Request.QueryString["tempId"] != null)
		{
			this.tempId = base.Request.QueryString["tempId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	protected void BindGv()
	{
		DataTable resourcesByItemId = BudTemplateItem.GetResourcesByItemId(this.itemId);
		this.ViewState["ResourcesTable"] = resourcesByItemId;
		this.gvResource.DataSource = resourcesByItemId;
		this.gvResource.DataBind();
	}
	protected void btnBindResource_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldResourceId.Value))
		{
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(this.hfldResourceId.Value);
			if (array != null)
			{
				DataTable dataTable = BudTemplateItem.showMaterialListForAdd(DBHelper.GetInParameterSql(array), this.itemId);
				DataTable dataTable2 = this.ViewState["ResourcesTable"] as DataTable;
				if (dataTable2 != null)
				{
					dataTable2.PrimaryKey = new DataColumn[]
					{
						dataTable2.Columns["ResourceCode"]
					};
					dataTable.PrimaryKey = new DataColumn[]
					{
						dataTable.Columns["ResourceCode"]
					};
					dataTable2.Merge(dataTable, true);
					dataTable = dataTable2;
				}
				this.ViewState["ResourcesTable"] = dataTable;
				this.gvResource.DataSource = dataTable;
				this.gvResource.DataBind();
			}
		}
		this.hfldResourceId.Value = string.Empty;
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				string value = this.gvResource.DataKeys[e.Row.RowIndex].Values[0].ToString();
				e.Row.Attributes["id"] = value;
				string value2 = this.gvResource.DataKeys[e.Row.RowIndex].Values[1].ToString();
				e.Row.Attributes["resourceId"] = value2;
			}
			catch
			{
			}
		}
	}
	protected void btnSave_ServerClick(object sender, System.EventArgs e)
	{
		try
		{
			BudTemplateItem byId = BudTemplateItem.GetById(this.itemId, this.tempId);
			System.Collections.Generic.List<TaskResource> resources = new System.Collections.Generic.List<TaskResource>();
			byId.Resources = resources;
			for (int i = 0; i < this.gvResource.Rows.Count; i++)
			{
				cn.justwin.Domain.Resource byId2 = cn.justwin.Domain.Resource.GetById(this.gvResource.DataKeys[i].Values[0].ToString());
				decimal quantity = System.Convert.ToDecimal(((TextBox)this.gvResource.Rows[i].FindControl("txtNumber")).Text);
				decimal price = System.Convert.ToDecimal(((TextBox)this.gvResource.Rows[i].FindControl("txtPrice")).Text);
				decimal lossCoefficient = System.Convert.ToDecimal(((TextBox)this.gvResource.Rows[i].FindControl("txtLossCoefficient")).Text);
				byId.AddResource(byId2, quantity, price, PageHelper.QueryUser(this, base.UserCode), System.DateTime.Now, "save", lossCoefficient);
			}
			BudTemplateItem.AddResource(byId);
			base.RegisterScript("$('#btnBindResource').hide(); top.ui.show('保存成功！')");
			base.RegisterScript(string.Concat(new string[]
			{
				"winclose('ResourceList', 'BudTemplateList.aspx?tempId=",
				this.tempId,
				"&tempType=",
				this.tempType,
				"', true)"
			}));
			this.BindGv();
		}
		catch (System.Exception)
		{
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		try
		{
			DataTable dataTable = (DataTable)this.ViewState["ResourcesTable"];
			if (this.gvResource.Rows.Count > 0)
			{
				for (int i = this.gvResource.Rows.Count - 1; i >= 0; i--)
				{
					GridViewRow gridViewRow = this.gvResource.Rows[i];
					CheckBox checkBox = (CheckBox)gridViewRow.FindControl("cbBox");
					if (checkBox.Checked)
					{
						dataTable.Rows.RemoveAt(gridViewRow.RowIndex);
					}
				}
			}
			this.ViewState["ResourcesTable"] = dataTable;
			this.gvResource.DataSource = (DataTable)this.ViewState["ResourcesTable"];
			this.gvResource.DataBind();
		}
		catch (System.Exception)
		{
			base.RegisterScript("$('#btnBindResource').hide(); alert('没有找到要删除的资源!'))");
		}
	}
	protected void btnClose_Click(object sender, System.EventArgs e)
	{
		base.RegisterScript(string.Concat(new string[]
		{
			"winclose('ResourceList', 'BudTemplateList.aspx?tempId=",
			this.tempId,
			"&tempType=",
			this.tempType,
			"', true)"
		}));
	}
}


