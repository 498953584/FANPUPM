using ASP;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
	public partial class ProjectTreelist : System.Web.UI.UserControl
	{
		public string TargetFrame
		{
			get
			{
				object obj = this.ViewState["TargetFrame"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["TargetFrame"] = value;
			}
		}
		public string PrjUrl
		{
			get
			{
				object obj = this.ViewState["PrjUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PrjUrl"] = value;
			}
		}
		public string UserCode
		{
			get
			{
				object obj = this.ViewState["UserCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["UserCode"] = value;
			}
		}
		public string SubPrjUrl
		{
			get
			{
				object obj = this.ViewState["SubPrjUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["SubPrjUrl"] = value;
			}
		}
		public string qx
		{
			get
			{
				object obj = this.ViewState["qx"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["qx"] = value;
			}
		}
		public string Audit
		{
			get
			{
				object obj = this.ViewState["Audit"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Audit"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.SubPrjUrl == "")
				{
					base.Response.Write("未指定连接地址");
					return;
				}
				if (base.Session["PmSet"] != null && base.Session["PmSet"].ToString() == "1")
				{
					this.TrvPrj_Year();
					this.TrvPrj_AppendNode();
					return;
				}
				this.BindProjectYear();
				this.BindProjectTree();
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
		private void BindProjectTree()
		{
			this.tvProject.Nodes.Clear();
			this.tvProject.Target = this.TargetFrame;
			TreeNode treeNode = new TreeNode();
			treeNode.Value = "";
			treeNode.Text = this.ddlYear.SelectedItem.Text + "所有项目";
			treeNode.NavigateUrl = "webTreeTS.aspx?fj=";
			this.tvProject.Nodes.Add(treeNode);
			DataTable prjsubTreebyUserandYear = PMAction.GetPrjsubTreebyUserandYear(this.UserCode, int.Parse(this.ddlYear.SelectedValue), "");
			DataRow[] array = prjsubTreebyUserandYear.Select("LEN(TypeCode)=5  ", "  StartDate desc");
			for (int i = 0; i < array.Length; i++)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = array[i]["PrjName"].ToString();
				if (array[i]["Permission"].ToString() != "0")
				{
					if (this.SubPrjUrl.IndexOf("?") > 0)
					{
						treeNode2.NavigateUrl = string.Concat(new string[]
						{
							this.SubPrjUrl,
							"&PrjCode=",
							array[i]["PrjCode1"].ToString(),
							"&PrjGuid=",
							array[i]["PrjCode"].ToString(),
							"&pc=",
							array[i]["PrjCode"].ToString().ToUpper(),
							"&pn=",
							base.Server.UrlEncode(array[i]["PrjName"].ToString()),
							"&qx=",
							this.qx,
							"&Audit=",
							this.Audit
						});
					}
					else
					{
						treeNode2.NavigateUrl = string.Concat(new string[]
						{
							this.SubPrjUrl,
							"?PrjCode=",
							array[i]["PrjCode1"].ToString(),
							"&PrjGuid=",
							array[i]["PrjCode"].ToString(),
							"&pc=",
							array[i]["PrjCode"].ToString().ToUpper(),
							"&pn=",
							base.Server.UrlEncode(array[i]["PrjName"].ToString()),
							"&qx=",
							this.qx,
							"&Audit=",
							this.Audit
						});
					}
				}
				else
				{
					treeNode2.SelectAction = TreeNodeSelectAction.None;
					treeNode2.ToolTip = "无权限";
					treeNode2.Value = string.Empty;
				}
				treeNode.ChildNodes.Add(treeNode2);
				DataRow[] array2 = prjsubTreebyUserandYear.Select("TypeCode LIKE '" + array[i]["TypeCode"].ToString() + "%' AND LEN(TypeCode)=10 ", "  TypeCode asc");
				for (int j = 0; j < array2.Length; j++)
				{
					TreeNode treeNode3 = new TreeNode();
					treeNode3.Text = array2[j]["PrjName"].ToString();
					if (this.SubPrjUrl.IndexOf("?") > 0)
					{
						treeNode3.NavigateUrl = string.Concat(new string[]
						{
							this.SubPrjUrl,
							"&PrjCode=",
							array2[j]["PrjCode1"].ToString(),
							"&PrjGuid=",
							array2[j]["PrjCode"].ToString(),
							"&pc=",
							array2[j]["PrjCode"].ToString().ToUpper(),
							"&pn=",
							base.Server.UrlEncode(array2[j]["PrjName"].ToString()),
							"&qx=",
							this.qx,
							"&Audit=",
							this.Audit
						});
					}
					else
					{
						treeNode3.NavigateUrl = string.Concat(new string[]
						{
							this.SubPrjUrl,
							"?PrjCode=",
							array2[j]["PrjCode1"].ToString(),
							"&PrjGuid=",
							array2[j]["PrjCode"].ToString(),
							"&pc=",
							array2[j]["PrjCode"].ToString().ToUpper(),
							"&pn=",
							base.Server.UrlEncode(array2[j]["PrjName"].ToString()),
							"&qx=",
							this.qx,
							"&Audit=",
							this.Audit
						});
					}
					treeNode2.ChildNodes.Add(treeNode3);
				}
			}
		}
		private void BindProjectYear()
		{
			DataTable projectYears = PMAction.GetProjectYears();
			if (projectYears.Rows.Count > 0)
			{
				int num = (projectYears.Rows[0]["BeginYear"] == DBNull.Value) ? DateTime.Today.Year : int.Parse(projectYears.Rows[0]["BeginYear"].ToString());
				int num2 = (projectYears.Rows[0]["EndYear"] == DBNull.Value) ? DateTime.Today.Year : int.Parse(projectYears.Rows[0]["EndYear"].ToString());
				for (int i = num; i <= num2; i++)
				{
					this.ddlYear.Items.Add(new ListItem(i.ToString() + "年度", i.ToString()));
				}
			}
			try
			{
				this.ddlYear.SelectedValue = DateTime.Today.Year.ToString();
			}
			catch
			{
			}
		}
		private void TrvPrj_AppendNode()
		{
			this.tvProject.Nodes.Clear();
			this.tvProject.Target = this.TargetFrame;
			TreeNode treeNode = new TreeNode();
			treeNode.Text = "所有项目";
			treeNode.Value = "";
			treeNode.NavigateUrl = "webTreeTS.aspx";
			this.tvProject.Nodes.Add(treeNode);
			int num = int.Parse(this.ddlYear.SelectedValue);
			string text = " SELECT distinct CASE prjstateName WHEN '其他' THEN 1000 ELSE PrjState END AS PrjState,prjstateName FROM ( ";
			text += " SELECT distinct PrjState,";
			text += " ISNULL((SELECT ItemName FROM dbo.Basic_CodeList WHERE TypeCode='ProjectState' AND ItemCode>=7 AND (ItemCode = PT_PrjInfo.PrjState)),'其他') AS prjstateName ";
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				" FROM PT_PrjInfo  WHERE i_ChildNum=0 and IsValid=1 and    ( Podepom LIKE '%",
				this.UserCode,
				"%' or  PrjManager LIKE '%",
				this.UserCode,
				"%' )  and (",
				num.ToString(),
				" between year(startDate) and year(enddate))) prjState ORDER BY PrjState  "
			});
			DataTable dataTable = publicDbOpClass.DataTableQuary(text);
			string text3 = " SELECT CASE prjstateName WHEN '其他' THEN 1000 ELSE PrjState END AS PrjState,PrjGuid, prjcode,PrjName,PrjCode1,UserCode,prjstateName FROM ( ";
			text3 = text3 + "SELECT PrjState, PrjGuid,PrjGuid as prjcode, PrjName as PrjName , PrjCode as PrjCode1, '" + this.UserCode + "' as UserCode,";
			text3 += " ISNULL((SELECT ItemName FROM dbo.Basic_CodeList WHERE TypeCode='ProjectState' AND ItemCode>=7 AND (ItemCode = PT_PrjInfo.PrjState)),'其他') AS prjstateName,StartDate ";
			string text4 = text3;
			text3 = string.Concat(new string[]
			{
				text4,
				" FROM  PT_PrjInfo WHERE  i_ChildNum=0 and IsValid=1 AND   ( Podepom LIKE '%",
				this.UserCode,
				"%' or  PrjManager LIKE '%",
				this.UserCode,
				"%' ) and (",
				num.ToString(),
				" between year(startDate) and year(enddate))) tabPrj  ORDER BY StartDate DESC"
			});
			DataTable dataTable2 = publicDbOpClass.DataTableQuary(text3);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					TreeNode treeNode2 = new TreeNode();
					treeNode2.Text = dataRow["prjstateName"].ToString();
					treeNode2.Value = dataRow["PrjState"].ToString();
					treeNode2.NavigateUrl = "webTreeTS.aspx?";
					treeNode.ChildNodes.Add(treeNode2);
					DataRow[] dr = dataTable2.Select("PrjState=" + dataRow["PrjState"].ToString());
					this.TrvPrj_SubTree(treeNode2, dr);
				}
			}
		}
		private void TrvPrj_SubTree(TreeNode ftn, DataRow[] dr)
		{
			for (int i = 0; i < dr.Length; i++)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dr[i]["PrjName"].ToString();
				treeNode.Value = dr[i]["PrjCode"].ToString();
				if (this.SubPrjUrl.IndexOf("?") > 0)
				{
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						this.SubPrjUrl,
						"&PrjCode=",
						dr[i]["PrjCode1"].ToString(),
						"&PrjGuid=",
						dr[i]["PrjCode"].ToString(),
						"&pc=",
						dr[i]["PrjCode"].ToString().ToUpper(),
						"&pn=",
						base.Server.UrlEncode(dr[i]["PrjName"].ToString()),
						"&qx=",
						this.qx,
						"&Audit=",
						this.Audit
					});
				}
				else
				{
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						this.SubPrjUrl,
						"?PrjCode=",
						dr[i]["PrjCode1"].ToString(),
						"&PrjGuid=",
						dr[i]["PrjCode"].ToString(),
						"&pc=",
						dr[i]["PrjCode"].ToString().ToUpper(),
						"&pn=",
						base.Server.UrlEncode(dr[i]["PrjName"].ToString()),
						"&qx=",
						this.qx,
						"&Audit=",
						this.Audit
					});
				}
				ftn.ChildNodes.Add(treeNode);
			}
		}
		private void TrvPrj_Year()
		{
			DataTable prjYears = PMAction.GetPrjYears(this.UserCode);
			if (prjYears.Rows.Count > 0)
			{
				int num = (prjYears.Rows[0]["BeginYear"] == DBNull.Value) ? DateTime.Today.Year : int.Parse(prjYears.Rows[0]["BeginYear"].ToString());
				int num2 = (prjYears.Rows[0]["EndYear"] == DBNull.Value) ? DateTime.Today.Year : int.Parse(prjYears.Rows[0]["EndYear"].ToString());
				for (int i = num; i <= num2; i++)
				{
					this.ddlYear.Items.Add(new ListItem(i.ToString() + "年度", i.ToString()));
				}
			}
			try
			{
				this.ddlYear.SelectedValue = DateTime.Today.Year.ToString();
			}
			catch
			{
			}
		}
		protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (base.Session["PmSet"] != null && base.Session["PmSet"].ToString() == "1")
			{
				this.TrvPrj_AppendNode();
				return;
			}
			this.BindProjectTree();
		}
	}


