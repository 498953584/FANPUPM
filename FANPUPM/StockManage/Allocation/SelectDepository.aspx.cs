using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Allocation_SelectDepository : NBasePage, IRequiresSessionState
{
	private TreasuryPermitBll treasuryPermitBll = new TreasuryPermitBll();
	private string module = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Module"]))
		{
			this.module = base.Request["Module"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Clear();
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["tshow"] == null || base.Request.QueryString["hid"] == null || base.Request.QueryString["no"] == null)
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "alert", "<script> alert('\\n系统提示：\\n页面错误 请找技术人员！');</script>");
				return;
			}
			this.Hdnshow.Value = base.Request.QueryString["tshow"];
			this.Hdnhid.Value = base.Request.QueryString["hid"];
			this.Hdnno.Value = base.Request.QueryString["no"];
			if (base.Request.QueryString["dc"] != null)
			{
				this.HdnDCode.Value = base.Request.QueryString["dc"].ToString();
			}
			if (base.Request.QueryString["op"] != null)
			{
				this.HdnOperat.Value = base.Request.QueryString["op"].ToString();
			}
			if (base.Request.QueryString["sm"] != null)
			{
				this.HdnStockModel.Value = base.Request.QueryString["sm"];
			}
			if (base.Request.QueryString["it"] != null)
			{
				this.HdnIt.Value = base.Request.QueryString["it"];
			}
			this.BindTV();
			this.TVDepository.Attributes["ondblclick"] = "javascript:return dblTreeNode(event,'')";
		}
	}
	protected void BindTV()
	{
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		DataTable treasuryList = allocationBllAction.GetTreasuryList(-1);
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "仓库名称";
		this.TVDepository.Nodes.Add(treeNode);
		treeNode.SelectAction = TreeNodeSelectAction.None;
		this.CreateTreeNodes(treasuryList, treeNode, "");
	}
	protected void CreateTreeNodes(DataTable dt, TreeNode tn, string code)
	{
		DataRow[] array = dt.Select(string.Concat(new string[]
		{
			"tcode like '",
			code,
			"%' and len(tcode)=(len('",
			code,
			"')+3)"
		}), "tcode");
		string yhdm = this.Session["yhdm"].ToString();
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow dataRow = array2[i];
			TreeNode treeNode = new TreeNode();
			treeNode.Text = dataRow["tname"].ToString();
			treeNode.Value = dataRow["tcode"].ToString();
			if (base.Request.QueryString["disable"] != null)
			{
				if (this.HdnStockModel.Value == "0" || this.HdnStockModel.Value == "")
				{
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						"javascript:dbclick('",
						treeNode.Text,
						"','",
						treeNode.Value,
						"','",
						dataRow["tflag"].ToString(),
						"')"
					});
				}
				else
				{
					if ((this.Hdnno.Value == "1" && dataRow["tflag"].ToString() == "1") || (this.Hdnno.Value == "0" && dataRow["tflag"].ToString() == "0"))
					{
						treeNode.NavigateUrl = "javascript:setNoAleave('提示:同级别的库不能相互调拨！');";
					}
					else
					{
						treeNode.NavigateUrl = string.Concat(new string[]
						{
							"javascript:dbclick('",
							treeNode.Text,
							"','",
							treeNode.Value,
							"','",
							dataRow["tflag"].ToString(),
							"')"
						});
					}
				}
			}
			else
			{
				if (this.treasuryPermitBll.IsPermitBool(treeNode.Value, yhdm))
				{
					if (this.module == "import")
					{
						if (StockParameter.DepotType == DepotType.ParallelMode.ToString())
						{
							treeNode.NavigateUrl = string.Concat(new string[]
							{
								"javascript:dbclick('",
								treeNode.Text,
								"','",
								treeNode.Value,
								"','",
								dataRow["tflag"].ToString(),
								"')"
							});
						}
						else
						{
							if (new Treasury().GetTotalCode() != treeNode.Value)
							{
								treeNode.NavigateUrl = "javascript:setNoAleave('提示:总分模式下只有总库才有入库权')";
							}
							else
							{
								treeNode.NavigateUrl = string.Concat(new string[]
								{
									"javascript:dbclick('",
									treeNode.Text,
									"','",
									treeNode.Value,
									"','",
									dataRow["tflag"].ToString(),
									"')"
								});
							}
						}
					}
					else
					{
						if (this.HdnStockModel.Value == "0" || this.HdnStockModel.Value == "")
						{
							treeNode.NavigateUrl = string.Concat(new string[]
							{
								"javascript:dbclick('",
								treeNode.Text,
								"','",
								treeNode.Value,
								"','",
								dataRow["tflag"].ToString(),
								"')"
							});
						}
						else
						{
							if ((this.Hdnno.Value == "1" && dataRow["tflag"].ToString() == "1") || (this.Hdnno.Value == "0" && dataRow["tflag"].ToString() == "0"))
							{
								treeNode.NavigateUrl = "javascript:setNoAleave('提示:同级别的库不能相互调拨！');";
							}
							else
							{
								treeNode.NavigateUrl = string.Concat(new string[]
								{
									"javascript:dbclick('",
									treeNode.Text,
									"','",
									treeNode.Value,
									"','",
									dataRow["tflag"].ToString(),
									"')"
								});
							}
						}
					}
				}
				else
				{
					treeNode.NavigateUrl = "javascript:setNoAleave()";
					treeNode.SelectAction = TreeNodeSelectAction.None;
					treeNode.Text = "<font color='#808080'>" + treeNode.Text + "</font>";
					treeNode.ToolTip = "无权限";
				}
			}
			tn.ChildNodes.Add(treeNode);
			this.CreateTreeNodes(dt, treeNode, dataRow["tcode"].ToString());
		}
	}
}


