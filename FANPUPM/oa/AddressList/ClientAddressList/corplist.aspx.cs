using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CorpList : NBasePage, IRequiresSessionState
	{
		private string strUrl = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = "外部通讯录";
				treeNode.Value = "0";
				this.tvClient.Nodes.Add(treeNode);
				AddressListDb addressListDb = new AddressListDb();
				DataTable dataTable = addressListDb.eGetCorp();
				foreach (DataRow dataRow in dataTable.Rows)
				{
					TreeNode treeNode2 = new TreeNode();
					treeNode2.Text = dataRow["v_mc"].ToString();
					treeNode2.Target = "LinkmanList";
					treeNode2.Value = "?iClient=" + dataRow["i_id"].ToString() + "&strClientName=" + dataRow["v_mc"].ToString();
					treeNode.ChildNodes.Add(treeNode2);
				}
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
			if (base.Request.QueryString["flag"].ToString() == "Manage")
			{
				this.strUrl = "LinkmanList.aspx";
				return;
			}
			if (base.Request.QueryString["flag"].ToString() == "Search")
			{
				this.strUrl = "LinkmanSearch.aspx";
			}
		}
		private void InitializeComponent()
		{
		}
		public void tvClient_SelectedNodeChanged(object sender, EventArgs e)
		{
			if (this.tvClient.SelectedValue != "0")
			{
				base.RegisterScript("setVal('" + this.strUrl + this.tvClient.SelectedValue + "')");
			}
		}
	}


