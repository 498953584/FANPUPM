using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class FlowSet : BasePage, IRequiresSessionState
	{
		public string IsGeneral
		{
			get
			{
				object obj = this.ViewState["IsGeneral"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["IsGeneral"] = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.IsGeneral = base.Request["flag"];
				this.MKMCBind();
				this.TVRole_Create(this.DDLMKMC.SelectedValue.ToString());
			}
		}
		private void TVRole_Create(string MKDM)
		{
			this.TVRole.Nodes.Clear();
			string text = "";
			DataTable dataTable = FlowTemplateAction.QueryBusinessCode(MKDM);
			TreeNode treeNode = new TreeNode();
			treeNode.Text = "业务编码";
			treeNode.NavigateUrl = "";
			treeNode.Target = "fraTemplate";
			this.TVRole.Nodes.Add(treeNode);
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["BusinessName"].ToString();
				string businessCode = dataRow["BusinessCode"].ToString();
				if (this.IsGeneral == "1")
				{
					treeNode2.NavigateUrl = string.Concat(new string[]
					{
						"TemplateList.aspx?ti=",
						dataRow["BusinessCode"].ToString(),
						"&class=",
						text.ToString(),
						"&flag=",
						this.IsGeneral.ToString()
					});
				}
				else
				{
					treeNode2.NavigateUrl = string.Concat(new string[]
					{
						"CorpTemplateList.aspx?ti=",
						dataRow["BusinessCode"].ToString(),
						"&class=",
						text.ToString(),
						"&flag=",
						this.IsGeneral.ToString()
					});
				}
				treeNode2.Target = "fraTemplate";
				treeNode.ChildNodes.Add(treeNode2);
				DataTable dataTable2 = FlowTemplateAction.QueryBusinessClass(businessCode);
				foreach (DataRow dataRow2 in dataTable2.Rows)
				{
					TreeNode treeNode3 = new TreeNode();
					treeNode3.Text = dataRow2["BusinessClassName"].ToString();
					if (this.IsGeneral == "1")
					{
						treeNode3.NavigateUrl = string.Concat(new string[]
						{
							"TemplateList.aspx?ti=",
							dataRow2["BusinessCode"].ToString(),
							"&class=",
							dataRow2["BusinessClass"].ToString(),
							"&flag=",
							this.IsGeneral.ToString()
						});
					}
					else
					{
						treeNode3.NavigateUrl = string.Concat(new string[]
						{
							"CorpTemplateList.aspx?ti=",
							dataRow2["BusinessCode"].ToString(),
							"&class=",
							dataRow2["BusinessClass"].ToString(),
							"&flag=",
							this.IsGeneral.ToString()
						});
					}
					treeNode3.Target = "fraTemplate";
					treeNode2.ChildNodes.Add(treeNode3);
					treeNode2.Expanded = new bool?(true);
				}
				this.TVRole.ExpandAll();
			}
		}
		private void MKMCBind()
		{
			DataTable mKMC = FlowTemplateAction.GetMKMC();
			DataView defaultView = mKMC.DefaultView;
			defaultView.RowFilter = "V_MKMC IS NOT NULL";
			this.DDLMKMC.DataTextField = "V_MKMC";
			this.DDLMKMC.DataValueField = "C_MKDM";
			this.DDLMKMC.DataSource = defaultView.ToTable();
			this.DDLMKMC.DataBind();
		}
		protected void DDLMKMC_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.TVRole_Create(this.DDLMKMC.SelectedValue.ToString());
		}
	}


