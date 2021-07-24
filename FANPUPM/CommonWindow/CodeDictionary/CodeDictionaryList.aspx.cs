using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class CommonWindow_CodeDictionary_CodeDictionaryList : BasePage, IRequiresSessionState
{

	protected PTDictionaryItemAction pia
	{
		get
		{
			return new PTDictionaryItemAction();
		}
	}
	protected string Type
	{
		get
		{
			return this.ViewState["TYPE"].ToString();
		}
		set
		{
			this.ViewState["TYPE"] = value;
		}
	}
	protected string code
	{
		get
		{
			return this.ViewState["CODE"].ToString();
		}
		set
		{
			this.ViewState["CODE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && (base.Request["t"] != null | base.Request["sc"] != null))
		{
			this.Type = base.Request["t"].ToString();
			this.code = base.Request["c"].ToString();
			this.btnAdd.Attributes["onclick"] = "return openEdit('Add');";
			this.btnEdit.Attributes["onclick"] = "return openEdit('Edit');";
			this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前请假记录数据吗？');";
			this.Tree_Bind();
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.SqlDictionaryItem.SelectCommand = "SELECT [ClassID], [KeyValue], [DisplayValue], [IsValid] FROM [PT_DictionaryItem]  where ClassID = " + this.HdnClassCode.Value;
		this.GVPTDictionaryItem.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.SqlDictionaryItem.SelectCommand = "SELECT [ClassID], [KeyValue], [DisplayValue], [IsValid] FROM [PT_DictionaryItem]  where ClassID = " + this.HdnClassCode.Value;
		this.GVPTDictionaryItem.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.pia.Delete(Convert.ToInt32(this.HdnRecoreId.Value)) == 1)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('删除成功');", true);
			this.GVPTDictionaryItem.DataBind();
		}
		else
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('删除失败');", true);
		}
		this.GVPTDictionaryItem.DataBind();
	}
	protected void GVPTDictionaryItem_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["KeyValue"].ToString() + "');";
		}
	}
	private void Tree_Bind()
	{
		this.TVDictionaryClass.Nodes.Clear();
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "名称";
		treeNode.Value = "";
		this.TVDictionaryClass.Nodes.Add(treeNode);
		DataTable dataTable = new DataTable();
		if (this.Type == "s")
		{
			dataTable = this.pia.GetClassList(" SubSystemCode ='" + this.code + "'");
		}
		else
		{
			if (this.Type == "b")
			{
				dataTable = this.pia.GetClassList(" BussinessCode ='" + this.code + "'");
			}
		}
		if (dataTable.Rows.Count > 0)
		{
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["ClassName"].ToString();
				treeNode2.Value = dataRow["ClassID"].ToString();
				treeNode.ChildNodes.Add(treeNode2);
			}
		}
	}
	protected void TVDictionaryClass_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.HdnClassCode.Value = this.TVDictionaryClass.SelectedNode.Value;
		this.SqlDictionaryItem.SelectCommand = "SELECT [ClassID], [KeyValue], [DisplayValue], [IsValid] FROM [PT_DictionaryItem]  where ClassID = " + this.HdnClassCode.Value;
		this.GVPTDictionaryItem.DataBind();
	}
}


