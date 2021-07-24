using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_TaskResource : NBasePage, IRequiresSessionState
{
	private string consTaskId = string.Empty;
	private string prjId = string.Empty;
	private string year = string.Empty;
	private string type = string.Empty;
	private string reportId = string.Empty;
	private StringBuilder strJS = new StringBuilder("setWidthHeight();");
	private MeterialPlanStock materialStock = new MeterialPlanStock();

	protected override void OnInit(EventArgs e)
	{
		if (base.Request.QueryString["consTaskId"] != null)
		{
			this.consTaskId = base.Request.QueryString["consTaskId"];
		}
		if (base.Request.QueryString["type"] != null)
		{
			this.type = base.Request.QueryString["type"];
		}
		if (base.Request.QueryString["prjId"] != null)
		{
			this.prjId = base.Request.QueryString["prjId"];
		}
		if (base.Request.QueryString["reportId"] != null)
		{
			this.reportId = base.Request.QueryString["reportId"];
		}
		base.OnInit(e);
		this.FileUpload1.RecordCode = this.consTaskId;
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
		}
	}
	protected void BindGv()
	{
		List<string> resourceIds = ConstructResource.GetResourceIds(this.consTaskId);
		List<ConstructResource> all = ConstructResource.GetAll(this.consTaskId, resourceIds);
		this.ViewState["ResourcesList"] = all;
		this.gvResource.DataSource = all;
		this.gvResource.DataBind();
	}
	protected void btnBindResource_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldResourceId.Value))
		{
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(this.hfldResourceId.Value);
			List<string> list = new List<string>();
			if (array != null)
			{
				this.UpdateSelectResources();
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string item = array2[i];
					list.Add(item);
				}
				List<ConstructResource> all = ConstructResource.GetAll(this.consTaskId, list);
				List<ConstructResource> list2 = this.ViewState["ResourcesList"] as List<ConstructResource>;
				foreach (ConstructResource current in all)
				{
					if (!list2.Contains(current))
					{
						list2.Add(current);
					}
				}
				this.ViewState["ResourcesList"] = list2;
				this.gvResource.DataSource = list2;
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
				string value = this.gvResource.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["id"] = value;
			}
			catch
			{
			}
		}
	}
	protected void btnSave_ServerClick(object sender, EventArgs e)
	{
		try
		{
			ConstructTask byId = ConstructTask.GetById(this.consTaskId);
			IList<ConstructResource> list = new List<ConstructResource>();
			byId.ResourceList = list;
			foreach (GridViewRow gridViewRow in this.gvResource.Rows)
			{
				string resourceId = this.gvResource.DataKeys[gridViewRow.RowIndex].Value.ToString();
				decimal quantity = Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtNumber")).Text);
				decimal unitPrice = Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtPrice")).Text);
				ConstructResource item = ConstructResource.Create(Guid.NewGuid().ToString(), this.consTaskId, resourceId, quantity, unitPrice);
				list.Add(item);
			}
			byId.ResourceList = list;
			ConstructTask.AddResource(byId);
			base.RegisterScript("winclose()");
		}
		catch (Exception)
		{
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		try
		{
			this.UpdateSelectResources();
			List<ConstructResource> list = this.ViewState["ResourcesList"] as List<ConstructResource>;
			if (this.gvResource.Rows.Count > 0)
			{
				for (int i = this.gvResource.Rows.Count - 1; i >= 0; i--)
				{
					GridViewRow gridViewRow = this.gvResource.Rows[i];
					CheckBox checkBox = (CheckBox)gridViewRow.FindControl("cbBox");
					if (checkBox.Checked)
					{
						list.RemoveAt(gridViewRow.RowIndex);
					}
				}
			}
			this.ViewState["ResourcesList"] = list;
			this.gvResource.DataSource = list;
			this.gvResource.DataBind();
		}
		catch (Exception)
		{
			this.strJS.Append("$('#btnBindResource').hide(); alert('没有找到要删除的资源!');");
			base.RegisterScript(this.strJS.ToString());
		}
	}
	protected void UpdateSelectResources()
	{
		List<ConstructResource> list = this.ViewState["ResourcesList"] as List<ConstructResource>;
		if (list != null && list.Count == this.gvResource.Rows.Count)
		{
			foreach (GridViewRow gridViewRow in this.gvResource.Rows)
			{
				TextBox textBox = (TextBox)gridViewRow.FindControl("txtPrice");
				if (textBox != null)
				{
					list[gridViewRow.RowIndex].UnitPrice = Convert.ToDecimal(textBox.Text.Trim());
				}
				TextBox textBox2 = (TextBox)gridViewRow.FindControl("txtNumber");
				if (textBox2 != null)
				{
					list[gridViewRow.RowIndex].Quantity = Convert.ToDecimal(textBox2.Text.Trim());
				}
			}
		}
	}
	protected void btnClose_Click(object sender, EventArgs e)
	{
		base.RegisterScript(string.Concat(new string[]
		{
			"winclose('ResourceList', 'ConstructTask.aspx?prjId=",
			this.prjId,
			"&type=edit&conId=",
			this.reportId,
			"', true)"
		}));
	}
}


