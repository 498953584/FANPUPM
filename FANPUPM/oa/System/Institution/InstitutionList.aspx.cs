using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Enterprise_InstitutionList : PageBase, IRequiresSessionState
{
	private static string dRole = "";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Bind_DataTV();
			this.Bind_GVInsList("");
			this.BtnWF_Purview.Attributes["onclick"] = "javascript:if(!confirm('确认提交审核吗？')) return false;";
			this.BtnDelete.Attributes["onclick"] = "javascript:if(!confirm('确认删除吗？')) return false;";
			this.HdnLeveCode.Value = PageHelper.QueryUser(this, base.UserCode);
		}
	}
	protected void Bind_DataTV()
	{
		string sqlString = " select i_dutyid from pt_yhmc where v_yhdm='" + this.Session["yhdm"].ToString() + "' ";
		Enterprise_InstitutionList.dRole = publicDbOpClass.ExecuteScalar(sqlString).ToString();
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
			if (dataRow["PermissionSet"].ToString().IndexOf(Enterprise_InstitutionList.dRole) >= 0 || dataRow["PermissionSet"].ToString().IndexOf(this.Session["yhdm"].ToString()) >= 0 || dataRow["PermissionClass"].ToString() == "-1")
			{
				treeNode.Text = dataRow["ClassName"].ToString();
				treeNode.Value = dataRow["LeveCode"].ToString();
				tn.ChildNodes.Add(treeNode);
			}
			this.CreateTree(dt, treeNode, dataRow["LeveCode"].ToString());
		}
	}
	protected void Bind_GVInsList(string leveCode)
	{
		if (!string.IsNullOrEmpty(leveCode))
		{
			string sqlString = " select * from Institutions where isvalid=1 and ClassCode='" + leveCode + "' order by IssueDate desc ";
			this.GVInsList.DataSource = publicDbOpClass.DataTableQuary(sqlString);
			this.GVInsList.DataBind();
			return;
		}
		string sqlString2 = string.Concat(new string[]
		{
			" select * from Institutions a inner join InstitutionClass b on a.classcode=b.levecode where isvalid=1 and (PermissionSet like '%",
			this.Session["yhdm"].ToString(),
			"%' or permissionset like'%",
			Enterprise_InstitutionList.dRole,
			"%' or PermissionClass=-1) order by writedate desc "
		});
		this.GVInsList.DataSource = publicDbOpClass.DataTableQuary(sqlString2);
		this.GVInsList.DataBind();
	}
	protected void TVInsClass_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.Bind_GVInsList(this.TVInsClass.SelectedValue);
	}
	protected void GVInsList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["id"] = this.GVInsList.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.GVInsList.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Cells[0].Text = (this.GVInsList.PageIndex * this.GVInsList.PageSize + (e.Row.RowIndex + 1)).ToString();
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);rowClick('",
				dataRowView["InsCode"].ToString(),
				"','",
				dataRowView["status"].ToString(),
				"');"
			});
			if (e.Row.Cells[5].Text == "-1")
			{
				e.Row.Cells[5].Text = "未启动流程";
			}
			else
			{
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
			}
			if (e.Row.Cells[2].Text.Trim().Length > 12)
			{
				e.Row.Cells[2].Text = e.Row.Cells[2].Text.Trim().Substring(0, 12) + "...";
				e.Row.Cells[2].ToolTip = dataRowView["InsName"].ToString();
			}
			string text = string.Format("<span onclick=\"viewInstitutions('{0}');\">{1}</span>", dataRowView["InsCode"].ToString(), e.Row.Cells[2].Text);
			e.Row.Cells[2].Text = text;
		}
	}
	protected void BtnDelete_Click(object sender, EventArgs e)
	{
		string sqlString = " select status from Institutions where InsCode='" + this.HdnInsCode.Value + "' ";
		object obj = publicDbOpClass.ExecuteScalar(sqlString);
		string a = "";
		if (obj != null)
		{
			a = obj.ToString();
		}
		string sqlString2 = " delete from Institutions where InsCode='" + this.HdnInsCode.Value + "' ";
		string sqlString3 = " update Institutions set IsValid=0 where InsCode='" + this.HdnInsCode.Value + "' ";
		if (a == "-1")
		{
			if (publicDbOpClass.ExecuteSQL(sqlString2) > 0)
			{
				this.Bind_GVInsList(this.TVInsClass.SelectedValue);
			}
			else
			{
				this.RegisterStartupScript("alert2", "<script>alert('删除失败！');</script>");
			}
		}
		if (a == "1" || a == "-2")
		{
			if (publicDbOpClass.ExecuteSQL(sqlString3) > 0)
			{
				this.Bind_GVInsList(this.TVInsClass.SelectedValue);
			}
			else
			{
				this.RegisterStartupScript("alert3", "<script>alert('删除失败！');</script>");
			}
		}
		if (a == "0")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('未审核完毕的制度不能废止!')", true);
		}
		if (a == "-3")
		{
			sqlString2 = string.Format("\r\n                BEGIN\r\n\t                --删除审核记录\r\n\t                DELETE  WF_Instance WHERE ID IN(\r\n\t\t                SELECT ID FROM WF_Instance_Main WHERE BusinessCode='069' AND BusinessClass='001'\r\n\t\t                AND InstanceCode='{0}'\r\n\t                ) \r\n\t                DELETE WF_Instance_Main WHERE BusinessCode='069'  AND BusinessClass='001'\r\n\t                AND InstanceCode='{0}'\r\n\t                --删除制度\t\r\n                    DELETE FROM Institutions WHERE InsCode='{0}'\r\n                END \r\n            ", this.HdnInsCode.Value);
			if (publicDbOpClass.ExecuteSQL(sqlString2) > 0)
			{
				this.Bind_GVInsList(this.TVInsClass.SelectedValue);
				return;
			}
			this.RegisterStartupScript("alert2", "<script>alert('删除失败！');</script>");
		}
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
		string text = string.Concat(new string[]
		{
			" select * from Institutions a inner join InstitutionClass b on a.ClassCode=b.LeveCode where (PermissionSet like '%",
			this.Session["yhdm"].ToString(),
			"%' or PermissionSet like '%",
			Enterprise_InstitutionList.dRole,
			"%' or PermissionClass=-1) and isvalid=1 and "
		});
		text += this.ddl_Search.SelectedValue;
		text = text + " like'%" + this.txtcontent.Text.Trim() + "%'";
		text += " order by IssueDate desc ";
		this.GVInsList.AllowPaging = false;
		this.GVInsList.DataSource = publicDbOpClass.DataTableQuary(text);
		this.GVInsList.DataBind();
	}
	protected void GVInsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVInsList.PageIndex = e.NewPageIndex;
		this.Bind_GVInsList(this.TVInsClass.SelectedValue);
	}
	protected void BtnWF_Purview_Click(object sender, EventArgs e)
	{
		Guid recordID = new Guid(this.HdnInsCode.Value);
		string text = FlowAuditAction.BeginFlow("069", "001", recordID, "", base.UserCode);
		if (text == "工作流程已成功启动")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!')", true);
			this.Bind_GVInsList(this.TVInsClass.SelectedValue);
		}
		if (text == "请先设置当前模块的审核流程")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
	}
}


