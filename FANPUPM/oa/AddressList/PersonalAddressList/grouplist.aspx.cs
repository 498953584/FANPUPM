using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class GroupList : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = "个人通讯录";
				this.tvGroup.Nodes.Add(treeNode);
				AddressListDb addressListDb = new AddressListDb();
				DataTable dataTable = addressListDb.pGetGroup(this.Page.Session["yhdm"].ToString());
				foreach (DataRow dataRow in dataTable.Rows)
				{
					TreeNode treeNode2 = new TreeNode();
					treeNode2.Text = dataRow["v_fzmc"].ToString();
					treeNode2.Target = "linkman";
					treeNode2.NavigateUrl = "LinkmanList.aspx?iGroup=" + dataRow["i_id"].ToString() + "&strGroupName=" + dataRow["v_fzmc"].ToString();
					treeNode.ChildNodes.Add(treeNode2);
				}
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


