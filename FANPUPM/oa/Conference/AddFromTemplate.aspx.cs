using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class AddFromTemplate : BasePage, IRequiresSessionState
	{

		public string FilterField
		{
			get
			{
				object obj = this.ViewState["FilterField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FilterField"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.FilterField = base.Request["flt"];
				this.TvClass_Created();
			}
		}
		protected void TvClass_Created()
		{
			DataTable dataTable = DocumentAction.FilterDocumentClass(this.FilterField);
			TreeNode treeNode = new TreeNode();
			treeNode.Text = "会议分类";
			treeNode.NavigateUrl = "";
			treeNode.Target = "TemplateSelectList";
			this.TVClass.Nodes.Add(treeNode);
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["ClassName"].ToString();
				treeNode2.NavigateUrl = "TemplateSelect.aspx?cid=" + dataRow["ClassID"].ToString();
				treeNode2.Target = "TemplateSelectList";
				treeNode.Nodes.Add(treeNode2);
			}
		}
	}


