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
	public partial class TypeTree : NBasePage, IRequiresSessionState
	{
		protected DataTable dtPrj;

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (base.Request.QueryString["PrjCode"] == Guid.Empty.ToString())
			{
				base.Response.Write("<font style='font-size:9pt'>请先选择分配项目</font>");
				base.Response.End();
			}
			this.ViewState["TYPE"] = base.Request.QueryString["type"];
			this.ViewState["PrjGuid"] = ((base.Request.QueryString["PrjCode"] == null) ? "" : base.Request.QueryString["PrjCode"].ToString());
			string url = "";
			if (!this.Page.IsPostBack)
			{
				this.ViewState["DT"] = KnowledgeTypeAction.GetType("");
				string a;
				if ((a = this.ViewState["TYPE"].ToString()) != null)
				{
					if (!(a == "1"))
					{
						if (!(a == "2"))
						{
							if (!(a == "3"))
							{
								if (a == "4")
								{
									url = "DatumSeach.aspx?PrjCode=" + this.ViewState["PrjGuid"].ToString();
								}
							}
							else
							{
								url = "DatumAffirmList.aspx?PrjCode=" + this.ViewState["PrjGuid"].ToString();
							}
						}
						else
						{
							url = "DatumList.aspx?PrjCode=" + this.ViewState["PrjGuid"].ToString();
						}
					}
					else
					{
						url = "TypeList.aspx";
					}
				}
				DataTable type = KnowledgeTypeAction.GetType("");
				this.TreeBind(url, type);
			}
		}
		private void TreeBind(string Url, DataTable dt)
		{
			this.tvProject.Nodes.Clear();
			this.tvProject.Target = "InfoList";
			TreeNode treeNode = new TreeNode();
			DataRow[] array = dt.Select(" parentid=1 ", "  Typeid asc");
			if (base.Request.QueryString["tag"] == "s")
			{
				this.ViewState["TYPE"] = "3";
			}
			else
			{
				this.ViewState["TYPE"] = "2";
			}
			string a;
			if ((a = this.ViewState["TYPE"].ToString()) != null)
			{
				if (!(a == "1"))
				{
					if (!(a == "2"))
					{
						if (a == "3")
						{
							treeNode.Value = "3";
							treeNode.Text = "安全类别";
							treeNode.NavigateUrl = "TypeList.aspx?TypeId=3";
							this.tvProject.Nodes.Add(treeNode);
							array = dt.Select(" parentid=3 ", "  Typeid asc");
						}
					}
					else
					{
						treeNode.Value = "2";
						treeNode.Text = "质量类别";
						treeNode.NavigateUrl = "TypeList.aspx?TypeId=2";
						this.tvProject.Nodes.Add(treeNode);
						array = dt.Select(" parentid=2 ", "  Typeid asc");
					}
				}
				else
				{
					treeNode.Value = "1";
					treeNode.Text = "资料类别";
					treeNode.NavigateUrl = "TypeList.aspx?TypeId=1";
					this.tvProject.Nodes.Add(treeNode);
				}
			}
			for (int i = 0; i < array.Length; i++)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = array[i]["TypeName"].ToString();
				treeNode2.NavigateUrl = "TypeList.aspx?TypeId=" + array[i]["Typeid"].ToString();
				treeNode.ChildNodes.Add(treeNode2);
				this.TreesubBind(treeNode2, dt, array[i]["Typeid"].ToString());
			}
		}
		private void TreesubBind(TreeNode tr, DataTable dt, string parentid)
		{
			DataRow[] array = dt.Select("parentid='" + parentid + "' ", "  Typeid asc");
			for (int i = 0; i < array.Length; i++)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = array[i]["TypeName"].ToString();
				treeNode.NavigateUrl = "TypeList.aspx?TypeId=" + array[i]["Typeid"].ToString();
				tr.ChildNodes.Add(treeNode);
				this.TreesubBind(treeNode, dt, array[i]["Typeid"].ToString());
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


