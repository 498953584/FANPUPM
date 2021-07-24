using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.action.ContractManage;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Enterprise_InstitutionClass : PageBase, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Bind_DataTV();
			this.Bind_DataGV();
			this.BtnDelete.Attributes["onclick"] = "javascript:if(!confirm('确认删除吗？')) return false;";
		}
	}
	protected void Bind_DataTV()
	{
		DataTable dt = new DataTable();
		string sqlString = " select * from InstitutionClass order by LeveCode ";
		dt = publicDbOpClass.DataTableQuary(sqlString);
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "企业制度分类";
		treeNode.NavigateUrl = "javascript:location.href=location.href;";
		this.TVInsClass.Nodes.Add(treeNode);
		this.CreateTree(dt, treeNode, "");
	}
	protected void Bind_DataGV()
	{
		string sqlString = string.Concat(new string[]
		{
			" select * from InstitutionClass where LeveCode like'",
			this.HdnLeveCode.Value,
			"%' and len(LeveCode)='",
			(this.HdnLeveCode.Value.Length + 3).ToString(),
			"' "
		});
		this.GVPermissionList.DataSource = publicDbOpClass.DataTableQuary(sqlString);
		this.GVPermissionList.DataBind();
	}
	protected void CreateTree(DataTable dt, TreeNode tn, string leveCode)
	{
		DataRow[] array = dt.Select(string.Concat(new object[]
		{
			"LeveCode like '",
			leveCode,
			"%' and len(LeveCode)='",
			leveCode.Length + 3,
			"'"
		}), " levecode asc ");
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow dataRow = array2[i];
			TreeNode treeNode = new TreeNode();
			treeNode.Text = dataRow["ClassName"].ToString();
			treeNode.Value = dataRow["LeveCode"].ToString();
			tn.ChildNodes.Add(treeNode);
			this.CreateTree(dt, treeNode, dataRow["LeveCode"].ToString());
		}
	}
	protected void TVInsClass_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.HdnLeveCode.Value = this.TVInsClass.SelectedValue;
		this.Bind_DataGV();
	}
	protected void GVPermissionList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Cells[0].Text = (this.GVPermissionList.PageIndex * this.GVPermissionList.PageSize + (e.Row.RowIndex + 1)).ToString();
			TextBox textBox = (TextBox)e.Row.Cells[4].FindControl("txtPersSet");
			textBox.Attributes["readonly"] = "readonly";
			if (dataRowView["PermissionClass"].ToString() == "-1")
			{
				e.Row.Cells[3].Text = "默认";
			}
			if (dataRowView["PermissionClass"].ToString() == "0")
			{
				e.Row.Cells[3].Text = "以岗位设定权限";
				ContractClass contractClass = new ContractClass();
				textBox.Text = contractClass.GetDutyName((dataRowView["PermissionSet"].ToString().Trim() == "") ? "0" : dataRowView["PermissionSet"].ToString().Trim());
			}
			if (dataRowView["PermissionClass"].ToString() == "1")
			{
				e.Row.Cells[3].Text = "以个人设定权限";
				textBox.Text = new DepositoryInfo().GetUserName(dataRowView["PermissionSet"].ToString().Trim());
			}
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);rowClick('",
				this.GVPermissionList.DataKeys[e.Row.RowIndex].Value,
				"','",
				dataRowView["PermissionClass"].ToString(),
				"','",
				dataRowView["PermissionSet"].ToString(),
				"','",
				textBox.Text,
				"');"
			});
		}
	}
	protected void BtnDelete_Click(object sender, EventArgs e)
	{
		string sqlString = string.Concat(new object[]
		{
			" select count(InsId) from InstitutionClass where LeveCode like'",
			this.HdnLeveCode.Value,
			"%' and len(LeveCode)='",
			this.HdnLeveCode.Value.Length + 3,
			"' "
		});
		string sqlString2 = " select count(InsCode) from Institutions where ClassCode='" + this.HdnLeveCode.Value + "' and IsValid=1 ";
		if (!(publicDbOpClass.ExecuteScalar(sqlString).ToString() == "0") || !(publicDbOpClass.ExecuteScalar(sqlString2).ToString() == "0"))
		{
			this.RegisterStartupScript("alert3", "<script>alert('注意：\\n\\n此类别下，已增添了相应内容。\\n\\n请首先在“制度管理”模块清除此类别下相应内容，方可删除当前类别。');</script>");
			return;
		}
		string sqlString3 = " delete from InstitutionClass where LeveCode='" + this.HdnLeveCode.Value + "' ";
		if (publicDbOpClass.ExecuteSQL(sqlString3) > 0)
		{
			this.RegisterStartupScript("alert1", "<script>location.href=location.href;</script>");
			this.Bind_DataGV();
			return;
		}
		this.RegisterStartupScript("alert2", "<script>alert('删除失败！');</script>");
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
	}
	protected void GVPermissionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVPermissionList.PageIndex = e.NewPageIndex;
		this.Bind_DataGV();
	}
}


