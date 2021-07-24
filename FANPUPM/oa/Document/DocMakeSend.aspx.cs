using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class DocMakeSend : BasePage, IRequiresSessionState
	{
		public new string UserCode
		{
			get
			{
				object obj = this.ViewState["UserCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["UserCode"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.UserCode = Convert.ToString(this.Session["yhdm"]);
			this.TVCorpCode_Create();
		}
		protected void TVCorpCode_Create()
		{
			DataTable dataTable = DocumentAction.QueryCorpCode(this.UserCode);
			TreeNode treeNode = new TreeNode();
			treeNode.Text = "名称";
			treeNode.NavigateUrl = "";
			treeNode.Target = "SendFileList";
			this.TVCorpCode.Nodes.Add(treeNode);
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["CorpName"].ToString();
				treeNode2.NavigateUrl = "SendFileList.aspx?code=" + dataRow["CorpCode"].ToString();
				treeNode2.Target = "SendFileList";
				treeNode.Nodes.Add(treeNode2);
			}
		}
	}


