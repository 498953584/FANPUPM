using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_System_Institution_InstitutionAbolish : PageBase, IRequiresSessionState
{
	private static string dRole = "";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.TreeViewData_Bind();
			this.GVInsList_Bind("");
			this.BtnDelete.Attributes.Add("onclick", "javascript:if(!confirm('确定删除该废止制度吗?彻底删除无法恢复!')) return false;");
			this.btnBackValid.Attributes.Add("onclick", "javascript:if(!confirm('确定恢复吗?')) return false;");
		}
	}
	protected void TreeViewData_Bind()
	{
		string sqlString = " select i_dutyid from pt_yhmc where v_yhdm='" + this.Session["yhdm"].ToString() + "' ";
		oa_System_Institution_InstitutionAbolish.dRole = publicDbOpClass.ExecuteScalar(sqlString).ToString();
		DataTable dt = new DataTable();
		string sqlString2 = " select * from InstitutionClass order by LeveCode ";
		dt = publicDbOpClass.DataTableQuary(sqlString2);
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "企业制度分类";
		treeNode.NavigateUrl = "javascript:location.href=location.href;";
		this.TVInsClass.Nodes.Add(treeNode);
		this.CreateTree(dt, treeNode, "");
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
			if (dataRow["PermissionSet"].ToString().IndexOf(oa_System_Institution_InstitutionAbolish.dRole) >= 0 || dataRow["PermissionSet"].ToString().IndexOf(this.Session["yhdm"].ToString()) >= 0 || dataRow["PermissionClass"].ToString() == "-1")
			{
				treeNode.Text = dataRow["ClassName"].ToString();
				treeNode.Value = dataRow["LeveCode"].ToString();
				tn.ChildNodes.Add(treeNode);
			}
			this.CreateTree(dt, treeNode, dataRow["LeveCode"].ToString());
		}
	}
	protected void GVInsList_Bind(string leveCode)
	{
		if (!string.IsNullOrEmpty(leveCode))
		{
			string sqlString = " select * from Institutions where isvalid=0 and ClassCode='" + leveCode + "' order by IssueDate desc ";
			this.GVInsList.DataSource = publicDbOpClass.DataTableQuary(sqlString);
			this.GVInsList.DataBind();
			return;
		}
		string sqlString2 = string.Concat(new string[]
		{
			" select * from Institutions a inner join InstitutionClass b on a.classcode=b.levecode where isvalid=0 and (PermissionSet like '%",
			this.Session["yhdm"].ToString(),
			"%' or permissionset like'%",
			oa_System_Institution_InstitutionAbolish.dRole,
			"%' or PermissionClass=-1) order by writedate desc "
		});
		this.GVInsList.DataSource = publicDbOpClass.DataTableQuary(sqlString2);
		this.GVInsList.DataBind();
	}
	protected void TVInsClass_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.GVInsList_Bind(this.TVInsClass.SelectedValue);
	}
	protected void btnBackValid_Click(object sender, EventArgs e)
	{
		string sqlString = "update Institutions set IsValid=1 where inscode='" + this.HdnInsCode.Value + "'";
		int num = publicDbOpClass.ExecSqlString(sqlString);
		if (num > 0)
		{
			base.ClientScript.RegisterStartupScript(base.GetType(), "", "top.ui.show( '恢复成功!')", true);
			this.GVInsList_Bind(this.TVInsClass.SelectedValue);
			return;
		}
		base.ClientScript.RegisterStartupScript(base.GetType(), "", "top.ui.alert(alert('恢复意外失败!请刷新后再试')", true);
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
		string text = string.Concat(new string[]
		{
			" select * from Institutions a inner join InstitutionClass b on a.ClassCode=b.LeveCode where (PermissionSet like '%",
			this.Session["yhdm"].ToString(),
			"%' or PermissionSet like '%",
			oa_System_Institution_InstitutionAbolish.dRole,
			"%' or PermissionClass=-1) and isvalid=0 and "
		});
		text += this.ddl_Search.SelectedValue;
		text = text + " like'%" + this.txtcontent.Text.Trim() + "%'";
		text += " order by IssueDate desc ";
		this.GVInsList.AllowPaging = false;
		this.GVInsList.DataSource = publicDbOpClass.DataTableQuary(text);
		this.GVInsList.DataBind();
	}
	protected void BtnDelete_Click(object sender, EventArgs e)
	{
		string sqlString = "delete from institutions where inscode='" + this.HdnInsCode.Value + "' and isvalid=0";
		if (publicDbOpClass.ExecSqlString(sqlString) > 0)
		{
			base.ClientScript.RegisterStartupScript(base.GetType(), "", "top.ui.show( '彻底删除成功!')", true);
			this.GVInsList_Bind(this.TVInsClass.SelectedValue);
		}
	}
	protected void GVInsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVInsList.PageIndex = e.NewPageIndex;
		this.GVInsList_Bind(this.TVInsClass.SelectedValue);
	}
	protected void GVInsList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Cells[0].Text = (this.GVInsList.PageIndex * this.GVInsList.PageSize + (e.Row.RowIndex + 1)).ToString();
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);rowClick('" + dataRowView["InsCode"].ToString() + "');";
			if (e.Row.Cells[5].Text == "0")
			{
				e.Row.Cells[5].Text = "审核中";
			}
			else
			{
				if (e.Row.Cells[5].Text == "1")
				{
					e.Row.Cells[5].Text = "已审核";
					e.Row.Cells[5].ForeColor = Color.Green;
				}
				else
				{
					if (e.Row.Cells[5].Text == "-2")
					{
						e.Row.Cells[5].Text = "驳回";
						e.Row.Cells[5].ForeColor = Color.Red;
					}
				}
			}
			if (e.Row.Cells[2].Text.Trim().Length > 12)
			{
				e.Row.Cells[2].Text = e.Row.Cells[2].Text.Trim().Substring(0, 12) + "...";
				e.Row.Cells[2].ToolTip = dataRowView["InsName"].ToString();
			}
			if (e.Row.Cells[1].Text.Trim().Length > 12)
			{
				e.Row.Cells[1].Text = e.Row.Cells[1].Text.Trim().Substring(0, 12) + "...";
				e.Row.ToolTip = dataRowView["uniquecode"].ToString();
			}
			e.Row.Cells[2].Text = string.Concat(new string[]
			{
				"<a href='InstitutionView.aspx?ic=",
				dataRowView["InsCode"].ToString(),
				"&valid=0' target='_blank'>",
				e.Row.Cells[2].Text,
				"</a>"
			});
		}
	}
}


