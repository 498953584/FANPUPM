using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_TaskResource : NBasePage, IRequiresSessionState
{
	private string taskId = string.Empty;
	private string prjId = string.Empty;
	private string year = string.Empty;
	private string version = string.Empty;
	private string isAllowEditRes = string.Empty;
	private string pageURl = string.Empty;
	private System.Text.StringBuilder strJS = new System.Text.StringBuilder("setWidthHeight();");
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private MeterialPlanStock materialStock = new MeterialPlanStock();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request.QueryString["taskId"]))
		{
			this.taskId = base.Request.QueryString["taskId"];
		}
		if (!string.IsNullOrEmpty(base.Request.QueryString["prjId"]))
		{
			this.prjId = base.Request.QueryString["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request.QueryString["version"]))
		{
			this.version = base.Request.QueryString["version"];
		}
		if (!string.IsNullOrEmpty(base.Request.QueryString["year"]))
		{
			this.year = base.Request.QueryString["year"];
		}
		if (!string.IsNullOrEmpty(base.Request.QueryString["isAllowEditRes"]))
		{
			this.isAllowEditRes = base.Request.QueryString["isAllowEditRes"];
		}
		if (!string.IsNullOrEmpty(base.Request.QueryString["pageURl"]))
		{
			this.pageURl = base.Request.QueryString["pageURl"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.hfldIsAllowEditRes.Value = this.isAllowEditRes;
			this.hfldVersion.Value = this.version;
			this.BindTaskInfo();
			this.BindGv();
		}
	}
	protected void BindTaskInfo()
	{
		if (this.hfldIsWBSRelevance.Value == "1")
		{
			BudTaskService budTaskService = new BudTaskService();
			cn.justwin.Domain.Entities.BudTask byId = budTaskService.GetById(this.taskId);
			this.lblTaskCode.Text = byId.TaskCode;
			this.lblTaskName.Text = byId.TaskName;
			this.lblDesc.Text = byId.FeatureDescription;
			this.lblQu.Text = byId.Quantity.Value.ToString();
		}
	}
	protected void BindGv()
	{
		DataTable dataTable = new DataTable();
		if (this.hfldIsWBSRelevance.Value == "1")
		{
			dataTable = cn.justwin.Domain.BudTask.GetResourcesByTask(this.taskId);
		}
		else
		{
			dataTable = cn.justwin.Domain.BudTask.GetResourcesByPrjId(this.prjId);
		}
		this.ViewState["ResourcesTable"] = dataTable;
		this.gvResource.DataSource = dataTable;
		this.gvResource.DataBind();
	}
	protected void btnBindResource_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldResourceId.Value))
		{
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(this.hfldResourceId.Value.Trim());
			if (array != null)
			{
				DataTable dataTable = new DataTable();
				if (this.hfldIsWBSRelevance.Value == "1")
				{
					dataTable = cn.justwin.Domain.BudTask.showMaterialListForAdd(DBHelper.GetInParameterSql(array), this.taskId);
				}
				else
				{
					dataTable = cn.justwin.Domain.BudTask.showMaterialListByPrjId(DBHelper.GetInParameterSql(array), this.prjId);
				}
				this.UpdataViewState();
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
				if (this.hfldIsAllowEditRes.Value == "0")
				{
					TextBox textBox = (TextBox)e.Row.FindControl("txtPrice");
					textBox.Enabled = false;
					TextBox textBox2 = (TextBox)e.Row.FindControl("txtNumber");
					textBox2.Enabled = false;
				}
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
			System.Collections.Generic.List<TaskResource> list = new System.Collections.Generic.List<TaskResource>();
			cn.justwin.Domain.BudTask budTask = null;
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				budTask = cn.justwin.Domain.BudTask.GetById(this.taskId);
				budTask.Resources = list;
			}
			for (int i = 0; i < this.gvResource.Rows.Count; i++)
			{
				cn.justwin.Domain.Resource byId = cn.justwin.Domain.Resource.GetById(this.gvResource.DataKeys[i].Values[0].ToString());
				decimal quantity = System.Convert.ToDecimal(((TextBox)this.gvResource.Rows[i].FindControl("txtNumber")).Text);
				decimal price = System.Convert.ToDecimal(((TextBox)this.gvResource.Rows[i].FindControl("txtPrice")).Text);
				decimal num = System.Convert.ToDecimal(((TextBox)this.gvResource.Rows[i].FindControl("txtLoss")).Text);
				if (this.hfldIsWBSRelevance.Value == "1")
				{
					budTask.AddResource(byId, quantity, price, num, "save");
				}
				else
				{
					list.Add(new TaskResource
					{
						Resource = byId,
						Quantity = quantity,
						Price = price,
						PrjGuid = this.prjId,
						Versions = System.Convert.ToInt32(this.hfldVersion.Value.Trim()),
						InputDate = new System.DateTime?(System.DateTime.Now),
						InputUser = PageHelper.QueryUser(this, base.UserCode),
						LossCoefficient = new decimal?(num)
					});
				}
			}
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				budTask.InputUser = PageHelper.QueryUser(this, base.UserCode);
				budTask.InputDate = System.DateTime.Now;
				cn.justwin.Domain.BudTask.AddResource(budTask);
				BudTaskService budTaskService = new BudTaskService();
				budTaskService.UpdateTotal2(this.taskId);
			}
			else
			{
				TaskResource.AddResource(list, this.prjId, System.Convert.ToInt32(this.hfldVersion.Value));
			}
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_ResourceDeploy' });");
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
			this.strJS.Append("$('#btnBindResource').hide(); alert('没有找到要删除的资源!');");
			base.RegisterScript(this.strJS.ToString());
		}
	}
	protected void btnClose_Click(object sender, System.EventArgs e)
	{
		base.RegisterScript("top.ui.closeTab();");
	}
	private void UpdataViewState()
	{
		DataTable dataTable = this.ViewState["ResourcesTable"] as DataTable;
		if (dataTable != null && dataTable.Rows.Count == this.gvResource.Rows.Count)
		{
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				GridViewRow gridViewRow = this.gvResource.Rows[i];
				TextBox textBox = (TextBox)gridViewRow.FindControl("txtPrice");
				if (textBox != null)
				{
					dataRow["price"] = textBox.Text.Trim();
				}
				TextBox textBox2 = (TextBox)gridViewRow.FindControl("txtNumber");
				if (textBox2 != null)
				{
					dataRow["number"] = textBox2.Text.Trim();
				}
				TextBox textBox3 = (TextBox)gridViewRow.FindControl("txtLoss");
				if (textBox3 != null)
				{
					dataRow["LossCoefficient"] = textBox3.Text.Trim();
				}
			}
			this.ViewState["ResourcesTable"] = dataTable;
		}
	}
}


