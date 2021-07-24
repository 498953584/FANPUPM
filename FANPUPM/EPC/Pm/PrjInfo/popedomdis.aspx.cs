using ASP;
using cn.justwin.stockBLL;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class Popedomdis : PageBase, IRequiresSessionState
	{
		private PTPrjInfoBll BLL_n = new PTPrjInfoBll();
		protected string SubPrjGuid = "";
		protected static DeptManageDb DeptAct
		{
			get
			{
				return new DeptManageDb();
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!this.Page.IsPostBack)
			{
				PMModel pMModel = new PMModel();
				pMModel = PMAction.GetSingleInfo(base.Request.QueryString["subpc"]);
				this.lbPrjName.Text = pMModel.PrjName;
				this.FillTrees1.Nodes.Clear();
				this.TVDept_AppendNodeFill();
			}
		}
		private void TVDept_AppendNodeFill()
		{
			this.FillTrees1.Nodes.Clear();
			System.Data.DataTable allDepartment = Popedomdis.DeptAct.GetAllDepartment();
			this.FillTrees1.NodeID = "i_bmdm";
			this.FillTrees1.NodeText = "v_bmmc";
			this.FillTrees1.ParentId = "i_sjdm";
			this.FillTrees1.Target = "InfoList1";
			this.FillTrees1.Locations = "i_bmdm";
			this.FillTrees1.Url = "UserList.aspx?ItemCode=" + base.Request.QueryString["subpc"];
			this.FillTrees1.TreeDataSource = allDepartment;
			this.FillTrees1.FillTree();
			this.hdnUserList.Value = PMAction.GETYHDMS(base.Request.QueryString["subpc"]);
			if (this.hdnUserList.Value.Trim() != "")
			{
				string yhdms = this.hdnUserList.Value.Trim(new char[]
				{
					','
				});
				this.lbUseList.Text = PMAction.GETYHMCS(yhdms);
			}
			else
			{
				this.lbUseList.Text = "";
			}
			this.Label2.Text = this.BLL_n.getBusinessman(base.Request.QueryString["subpc"]);
			this.Lb_prjManage.Text = PMAction.GetprjManager(base.Request.QueryString["subpc"]);
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			if (PMAction.UpdatePodepom(this.hdnUserList.Value, base.Request.QueryString["subpc"]) == 1)
			{
				this.js.Text = "alert('项目人员设置成功!');window.close()";
				this.TVDept_AppendNodeFill();
				return;
			}
			this.js.Text = "alert('项目人员失败!');";
		}
	}


