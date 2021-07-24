using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DatumClassTree : NBasePage, IRequiresSessionState
	{
		protected string strRur = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				string text = base.Request.QueryString["type"];
				string key;
				switch (key = text)
				{
				case "1":
					this.strRur = "/EPC/QuaitySafety/CriterionList.aspx?Flage=Q&Type=Edit";
					break;
				case "2":
					this.strRur = "/EPC/QuaitySafety/CriterionList.aspx?Flage=Q&Type=List";
					break;
				case "3":
					this.strRur = "/EPC/QuaitySafety/CriterionList.aspx?Flage=S&Type=Edit";
					break;
				case "4":
					this.strRur = "/EPC/QuaitySafety/CriterionList.aspx?Flage=S&Type=List";
					break;
				case "5":
					this.strRur = "/EPC/QuaitySafety/AccidentRecord.aspx?Flage=Q&Type=Edit";
					break;
				case "6":
					this.strRur = "/EPC/QuaitySafety/AccidentRecord.aspx?Flage=Q&Type=List";
					break;
				case "7":
					this.strRur = "/EPC/QuaitySafety/AccidentRecord.aspx?Flage=S&Type=Edit";
					break;
				case "8":
					this.strRur = "/EPC/QuaitySafety/AccidentRecord.aspx?Flage=S&Type=List";
					break;
				case "11":
					this.strRur = "DatumClassList.aspx?Parent=3";
					break;
				case "12":
					this.strRur = "DatumClassList.aspx?Parent=2";
					break;
				}
				DataTable type = KnowledgeTypeAction.GetType("");
				string pid = base.Request.QueryString["Parent"];
				this.TreeBind(this.strRur, type, pid);
			}
		}
		private void TreeBind(string Url, DataTable dt, string pid)
		{
			this.tvProject.Nodes.Clear();
			this.tvProject.Target = "InfoList";
			TreeNode treeNode = new TreeNode();
			DataRow[] array = dt.Select(" parentid=1 ", "  Typeid asc");
			this.ViewState["TYPE"] = pid;
			if (pid != null)
			{
				if (!(pid == "2"))
				{
					if (pid == "3")
					{
						treeNode.Value = "3";
						treeNode.Text = "安全类别";
						treeNode.NavigateUrl = Url + "&TypeId=3";
						this.tvProject.Nodes.Add(treeNode);
						array = dt.Select(" parentid=3 ", "  Typeid asc");
					}
				}
				else
				{
					treeNode.Value = "2";
					treeNode.Text = "质量类别";
					treeNode.NavigateUrl = Url + "&TypeId=2";
					this.tvProject.Nodes.Add(treeNode);
					array = dt.Select(" parentid=2 ", "  Typeid asc");
				}
			}
			for (int i = 0; i < array.Length; i++)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = array[i]["TypeName"].ToString();
				treeNode2.NavigateUrl = Url + "&TypeId=" + array[i]["Typeid"].ToString();
				treeNode.ChildNodes.Add(treeNode2);
				this.TreesubBind(treeNode2, dt, array[i]["Typeid"].ToString(), Url);
			}
		}
		private void TreesubBind(TreeNode tr, DataTable dt, string parentid, string Url)
		{
			DataRow[] array = dt.Select("parentid='" + parentid + "' ", "  Typeid asc");
			for (int i = 0; i < array.Length; i++)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = array[i]["TypeName"].ToString();
				treeNode.NavigateUrl = "TypeList.aspx?TypeId=" + array[i]["Typeid"].ToString();
				treeNode.NavigateUrl = Url + "&TypeId=" + array[i]["Typeid"].ToString();
				tr.ChildNodes.Add(treeNode);
				this.TreesubBind(treeNode, dt, array[i]["Typeid"].ToString(), Url);
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


