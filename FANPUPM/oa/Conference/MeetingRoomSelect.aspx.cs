using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class MeetingRoomSelect : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TvCorp_Created();
			}
		}
		protected void TvCorp_Created()
		{
			DataTable dataTable = ConferenceManage.QueryCorpCode();
			TreeNode treeNode = new TreeNode();
			treeNode.Text = "集团公司";
			treeNode.NavigateUrl = "";
			treeNode.Target = "BoardroomList";
			this.TVCorp.Nodes.Add(treeNode);
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["corpName"].ToString();
				treeNode2.NavigateUrl = "SelectRoomList.aspx?code=" + dataRow["corpCode"].ToString();
				treeNode2.Target = "BoardroomList";
				treeNode.Nodes.Add(treeNode2);
			}
		}
	}


