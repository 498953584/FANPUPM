using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class helpEditFrm : PageBase, System.Web.SessionState.IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.TrvHelp_AppendNode();
			if (base.Request["url"] != null && base.Request["url"] != "/Help/help.aspx")
			{
				string text = base.Request.QueryString["url"].Trim();
				if (text.Length > 20)
				{
					text = text.Substring(3);
					if (text.Length > 31 && text.IndexOf("reftime=begin") > -1)
					{
						text = text.Substring(0, text.Length - 30);
					}
					text = text.Replace('@', '&');
				}
				DataTable dataTable = publicDbOpClass.DataTableQuary("SELECT  C_MKDM from  PT_MK WHERE   (V_CDLJ LIKE '%" + text + "%') and C_MKDM in (select C_MKDM from PT_Help) ");
				if (dataTable.Rows.Count > 0)
				{
					string str = dataTable.Rows[0][0].ToString();
					this.ifrMain.Attributes["src"] = "/Help/helpsel.aspx?id=" + str + "&mc=";
					return;
				}
				this.ifrMain.Attributes["src"] = "/Help/helpsel.aspx?id=00&mc=";
			}
		}
	}
	private void TrvHelp_AppendNode()
	{
		this.TrvHelp.Nodes.Clear();
		DataTable dataTable = publicDbOpClass.DataTableQuary("SELECT * FROM  PT_helpMK WHERE  len(C_MKDM) = 2 order by id");
		if (dataTable.Rows.Count > 0)
		{
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["v_MKMC"].ToString();
				treeNode.Value = dataRow["C_MKDM"].ToString();
				if (base.Request["op"] != null)
				{
					this.trtop.Style["display"] = "none";
					treeNode.NavigateUrl = "/Help/helpupd.aspx?id=" + base.Server.UrlEncode(dataRow["C_MKDM"].ToString()) + "&mc=" + base.Server.UrlEncode(dataRow["v_MKMC"].ToString());
				}
				else
				{
					treeNode.NavigateUrl = "/Help/helpSel.aspx?id=" + base.Server.UrlEncode(dataRow["C_MKDM"].ToString()) + "&mc=" + base.Server.UrlEncode(dataRow["v_MKMC"].ToString());
				}
				treeNode.Target = "ifrMain";
				this.TrvHelp.Nodes.Add(treeNode);
				this.TrvHelp_SubTree(treeNode, treeNode.Value, "PT_helpMK", " order by id");
			}
		}
		DataTable dataTable2 = publicDbOpClass.DataTableQuary("SELECT * FROM  PT_MK WHERE  len(C_MKDM) = 2 order by PT_MK.i_xh,PT_MK.c_mkdm");
		if (dataTable2.Rows.Count > 0)
		{
			foreach (DataRow dataRow2 in dataTable2.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow2["v_MKMC"].ToString();
				treeNode2.Value = dataRow2["C_MKDM"].ToString();
				if (base.Request["op"] != null)
				{
					treeNode2.NavigateUrl = "/Help/helpupd.aspx?id=" + base.Server.UrlEncode(dataRow2["C_MKDM"].ToString()) + "&mc=" + base.Server.UrlEncode(dataRow2["v_MKMC"].ToString());
				}
				else
				{
					treeNode2.NavigateUrl = "/Help/helpSel.aspx?id=" + base.Server.UrlEncode(dataRow2["C_MKDM"].ToString()) + "&mc=" + base.Server.UrlEncode(dataRow2["v_MKMC"].ToString());
				}
				treeNode2.Target = "ifrMain";
				this.TrvHelp.Nodes.Add(treeNode2);
				this.TrvHelp_SubTree(treeNode2, treeNode2.Value, "PT_MK", " and Isdisplay=1   order by PT_MK.i_xh,PT_MK.c_mkdm");
			}
		}
	}
	private void TrvHelp_SubTree(TreeNode ftn, string parentCode, string tb, string order)
	{
		DataTable dataTable = publicDbOpClass.DataTableQuary(string.Concat(new object[]
		{
			"SELECT * FROM  ",
			tb,
			" WHERE  C_MKDM like '",
			parentCode,
			"%' and len(c_mkdm)=",
			parentCode.Length,
			"+2",
			order
		}));
		if (dataTable.Rows.Count > 0)
		{
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["v_MKMC"].ToString();
				treeNode.Value = dataRow["C_MKDM"].ToString();
				if (base.Request["op"] != null)
				{
					treeNode.NavigateUrl = "/Help/helpupd.aspx?id=" + base.Server.UrlEncode(dataRow["C_MKDM"].ToString()) + "&mc=" + base.Server.UrlEncode(dataRow["v_MKMC"].ToString());
				}
				else
				{
					treeNode.NavigateUrl = "/Help/helpSel.aspx?id=" + base.Server.UrlEncode(dataRow["C_MKDM"].ToString()) + "&mc=" + base.Server.UrlEncode(dataRow["v_MKMC"].ToString());
				}
				treeNode.Target = "ifrMain";
				ftn.ChildNodes.Add(treeNode);
				this.TrvHelp_SubTree(treeNode, dataRow["C_MKDM"].ToString(), tb, order);
			}
		}
	}
}


