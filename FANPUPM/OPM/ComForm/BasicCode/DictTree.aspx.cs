using ASP;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
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
		private string prjId = string.Empty;
		private string prjCode = string.Empty;
		private string opType = string.Empty;

		protected CodingAction CodingAct
		{
			get
			{
				return new CodingAction();
			}
		}
		public string DictTypeID
		{
			get
			{
				object obj = this.ViewState["DictTypeID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DictTypeID"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!string.IsNullOrEmpty(base.Request["pc"]))
			{
				this.prjId = base.Request["pc"];
				PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
				PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
				this.prjCode = byId.PrjCode;
			}
			if (!string.IsNullOrEmpty(base.Request["opType"]))
			{
				this.opType = base.Request["opType"].Trim();
			}
			if (base.Request["codetype"] != "")
			{
				this.DictTypeID = base.Request["codetype"];
			}
			else
			{
				base.Response.End();
			}
			if (!this.Page.IsPostBack)
			{
				this.TvDict.Nodes.Clear();
				this.TvDict.Target = "FraCodeList";
				TreeNode treeNode = new TreeNode();
				treeNode.Text = "选择类型";
				treeNode.Value = "0";
				treeNode.NavigateUrl = "~/EPC/UserControl1/webNullTS.aspx";
				string dictTypeID;
				if ((dictTypeID = this.DictTypeID) != null)
				{
					if (dictTypeID == "Img")
					{
						treeNode.Text = "图纸类型";
						treeNode.NavigateUrl = string.Concat(new string[]
						{
							"/OPM/Business_Data/Business_Data_Manage.aspx?w=0&codeid=0&codetype=",
							base.Request["codetype"].ToString(),
							"&pc=",
							base.Request["pc"].ToString(),
							"&isshowall=false&opType=",
							this.opType,
							"&PrjCode=",
							this.prjCode
						});
						goto IL_271;
					}
					if (dictTypeID == "211")
					{
						treeNode.NavigateUrl = string.Concat(new string[]
						{
							"/OPM/FireManage/FireManageList.aspx?w=0&codeid=0&codetype=",
							base.Request["codetype"].ToString(),
							"&pc=",
							base.Request["pc"].ToString(),
							"&opType=",
							this.opType,
							"&PrjCode=",
							this.prjCode
						});
						goto IL_271;
					}
				}
				treeNode.NavigateUrl = "~/EPC/UserControl1/webNullTS.aspx";
				IL_271:
				if (this.DictTypeID == "Img")
				{
					if (this.opType == "img")
					{
						treeNode.Text = "图纸类型";
						treeNode.NavigateUrl = string.Concat(new string[]
						{
							"/OPM/Business_Data/Business_Data_Main.aspx?w=0&codeid=0&codetype=",
							base.Request["codetype"].ToString(),
							"&pc=",
							base.Request["pc"].ToString(),
							"&isshowall=false&opType=",
							this.opType,
							"&PrjCode=",
							this.prjCode
						});
					}
					if (this.opType == "view")
					{
						treeNode.Text = "图纸类型";
						treeNode.NavigateUrl = string.Concat(new string[]
						{
							"/OPM/Business_Data/Business_Data_Main.aspx?w=0&codeid=0&codetype=",
							base.Request["codetype"].ToString(),
							"&pc=",
							base.Request["pc"].ToString(),
							"&isshowall=false&opType=",
							this.opType,
							"&PrjCode=",
							this.prjCode
						});
					}
				}
				treeNode.Target = "FraCodeList";
				treeNode.Selected = true;
				this.TvDict.Nodes.Add(treeNode);
				DataTable dataTable = CodingAction.CodeInfoListType(this.DictTypeID);
				DataTable dt = CodingAction.CodeInfoList(dataTable.Rows[0]["TypeID"].ToString());
				this.CreateDictTree(0, dt, treeNode);
			}
		}
		private void CreateDictTree(int ParentCodeID, DataTable dt, TreeNode RootNode)
		{
			DataRow[] array = dt.Select("ParentCodeID=" + ParentCodeID, " IsDefault DESC,I_xh asc");
			int i = 0;
			while (i < array.Length)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = array[i]["CodeName"].ToString();
				treeNode.Value = array[i]["CodeID"].ToString();
				array[i]["parentcodelist"].ToString();
				string dictTypeID;
				if ((dictTypeID = this.DictTypeID) == null)
				{
					goto IL_1CD;
				}
				if (!(dictTypeID == "Img"))
				{
					if (!(dictTypeID == "211"))
					{
						goto IL_1CD;
					}
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						"/OPM/FireManage/FireManageList.aspx?w=0&codeid=",
						treeNode.Value,
						"&codetype=",
						base.Request["codetype"].ToString(),
						"&opType=",
						this.opType,
						"&pc=",
						base.Request["pc"].ToString(),
						"&PrjCode=",
						this.prjCode
					});
				}
				else
				{
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						"/OPM/Business_Data/Business_Data_Manage.aspx?w=0&codeid=",
						treeNode.Value,
						"&codetype=",
						base.Request["codetype"].ToString(),
						"&opType=",
						this.opType,
						"&pc=",
						base.Request["pc"].ToString(),
						"&isshowall=true&PrjCode=",
						this.prjCode
					});
				}
				IL_1D8:
				if (this.DictTypeID == "Img" && this.opType == "img")
				{
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						"/OPM/Business_Data/Business_Data_Main.aspx?w=0&codeid=",
						treeNode.Value,
						"&codetype=",
						base.Request["codetype"].ToString(),
						"&opType=",
						this.opType,
						"&pc=",
						base.Request["pc"].ToString(),
						"&isshowall=true&PrjCode=",
						this.prjCode
					});
				}
				treeNode.Target = "FraCodeList";
				RootNode.ChildNodes.Add(treeNode);
				int parentCodeID = Convert.ToInt32(treeNode.Value);
				this.CreateDictTree(parentCodeID, dt, treeNode);
				i++;
				continue;
				IL_1CD:
				treeNode.NavigateUrl = "~/EPC/UserControl1/webNullTS.aspx";
				goto IL_1D8;
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


