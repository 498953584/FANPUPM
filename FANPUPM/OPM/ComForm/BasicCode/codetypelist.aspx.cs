using ASP;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CodeTypeList : Page, IRequiresSessionState
	{

		protected CodingAction CodingAct
		{
			get
			{
				return new CodingAction();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TrVCodeType.Nodes.Clear();
				this.TrVCodeType.Target = "FraCodeList";
				TreeNode treeNode = new TreeNode();
				treeNode.Text = "选择类型";
				treeNode.NavigateUrl = "~/EPC/UserControl1/webNullTS.aspx";
				treeNode.Target = "FraCodeList";
				this.TrVCodeType.Nodes.Add(treeNode);
				DataTable dataTable = new DataTable();
				if (base.Request["type"] != null)
				{
					if (base.Request["type"].ToString() != "")
					{
						string str = base.Request["type"].ToString();
						dataTable = publicDbOpClass.DataTableQuary("SELECT [TypeID],[TypeName] FROM  [XPM_Basic_CodeType] where TypeID in (" + str + ") and  [IsVisible]=1 and [IsValid]=1");
					}
					else
					{
						dataTable = CodingAction.QueryCodeTypeDT();
					}
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					TreeNode treeNode2 = new TreeNode();
					treeNode2.Text = dataTable.Rows[i]["TypeName"].ToString();
					treeNode2.Value = dataTable.Rows[i]["TypeID"].ToString();
					treeNode2.NavigateUrl = "/EPC/Basic/CodeList.aspx?w=0&tid=" + treeNode2.Value;
					treeNode2.Target = "FraCodeList";
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


