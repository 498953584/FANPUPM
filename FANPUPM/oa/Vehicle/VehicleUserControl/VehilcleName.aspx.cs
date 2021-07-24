using ASP;
using cn.justwin.BLL;
using cn.justwin.VehiclesBLL;
using cn.justwin.VehiclesModel;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.sysManage.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_VehicleUserControl_VehilcleName : NBasePage, IRequiresSessionState
{
	private TypeAndStateBll TASBLL = new TypeAndStateBll();
	private userManageDb umd = new userManageDb();
	protected string _DeptCode;

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (!string.IsNullOrEmpty(base.Request.QueryString["code"]))
			{
				this.DataBindContractType();
			}
			else
			{
				this.iiTypeC();
			}
			CreatDepTree creatDepTree = new CreatDepTree(this.tvDept.Nodes);
			creatDepTree.EnabledLink = true;
			creatDepTree.NavigationURL = "VehilcleName.aspx";
			userManageDb userManageDb = new userManageDb();
			string a = userManageDb.manageDept(this.Session["yhdm"].ToString()).Trim();
			if (a == "1")
			{
				creatDepTree.BuildTreeView(this.Page.Session["yhdm"].ToString(), 0);
			}
			if (this.tvDept.Nodes.Count > 0)
			{
				this.tvDept.Nodes[0].Selected = true;
			}
		}
	}
	private void iiTypeC()
	{
		if (!string.IsNullOrEmpty(base.Request.QueryString["VehicleName_Control"]) && !string.IsNullOrEmpty(base.Request.QueryString["TypeIdControl"]))
		{
			this.hfldTypeId.Value = base.Request.QueryString["TypeIdControl"].ToString();
			string value = base.Request.QueryString["VehicleName_Control"].ToString();
			this.hfldTypeName.Value = value;
		}
	}
	private void DataBindContractType()
	{
		if (!string.IsNullOrEmpty(base.Request.QueryString["code"]))
		{
			this._DeptCode = this.Page.Request.Params["code"].ToString();
			this.umd.depName(this._DeptCode);
			DataTable dataSource = this.umd.depUserList(this._DeptCode, "y");
			this.VehicleType.DataSource = dataSource;
			this.VehicleType.DataBind();
		}
	}
	private void DataBindProject()
	{
	}
	private void AddContractType(TreeNode node, string guid)
	{
		string strWhere = string.Format(" ParentGuid = '{0}'", guid);
		List<TypeAndState> modelList = this.TASBLL.GetModelList(strWhere);
		foreach (TypeAndState current in modelList)
		{
			TreeNode child = new TreeNode(current.Name, current.guid.ToString());
			node.ChildNodes.Add(child);
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
	}
	protected void LinkButton1_Click(object sender, EventArgs e)
	{
	}
}


