using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.AllocUser;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using cn.justwin.stockModel;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_AllocUser : NBasePage, IRequiresSessionState
{
	private IAllocUser allocUser;
	private PtYhmcService ptYhmcBll = new PtYhmcService();
	private PTbdmService ptdbmBll = new PTbdmService();
	private string type = string.Empty;
	private string tableName = string.Empty;
	private System.Collections.Generic.List<string> idList = new System.Collections.Generic.List<string>();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["idJson"]))
		{
			this.idList = JsonHelper.GetListFromJson(base.Request["idJson"]);
		}
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"];
		}
		if (!string.IsNullOrEmpty(base.Request["tableName"]))
		{
			this.tableName = base.Request["tableName"];
		}
		this.allocUser = this.CreateAllocUser(this.type);
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTree();
			if (this.idList.Count == 1)
			{
				System.Collections.Generic.IList<string> existsUser = this.GetExistsUser();
				this.BindExistsUser(existsUser);
			}
		}
	}
	protected void BindTree()
	{
		this.TvBranch.Nodes.Clear();
		System.Collections.Generic.IList<PTbdm> pTbdmByWhere = this.ptdbmBll.GetPTbdmByWhere(" where i_sjdm=0 ");
		foreach (PTbdm current in pTbdmByWhere)
		{
			TreeNode treeNode = new TreeNode(current.V_BMMC, current.i_bmdm.ToString());
			treeNode.ToolTip = string.Concat(current.i_bmdm);
			treeNode.Selected = true;
			this.AddChildNodes(treeNode);
			this.TvBranch.Nodes.Add(treeNode);
		}
	}
	protected void AddChildNodes(TreeNode node)
	{
		System.Collections.Generic.IList<PTbdm> pTbdmByWhere = this.ptdbmBll.GetPTbdmByWhere("where i_sjdm='" + node.Value + "'");
		foreach (PTbdm current in pTbdmByWhere)
		{
			TreeNode treeNode = new TreeNode(current.V_BMMC, current.i_bmdm.ToString());
			treeNode.ToolTip = string.Concat(current.i_bmdm);
			node.ChildNodes.Add(treeNode);
			this.AddChildNodes(treeNode);
		}
	}
	protected void trvwDepartment_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		string selectedValue = this.TvBranch.SelectedValue;
		this.lbSelect.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where i_bmdm = {0} and  State!= 2", selectedValue));
		this.lbSelect.DataBind();
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.lbSelect.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where v_xm like '%{0}%'", this.txtQuery.Text.Trim()));
		this.lbSelect.DataBind();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (this.idList.Count == 1)
		{
			this.DeleteExistsUser();
		}
		this.AddUser();
		base.RegisterScript("succeed();");
	}
	private IAllocUser CreateAllocUser(string type)
	{
		string[] configurationLocations = new string[]
		{
			"assembly://PmBusinessLogic/cn.justwin.BLL.AllocUser/AllocUser.xml"
		};
		IApplicationContext applicationContext = new XmlApplicationContext(configurationLocations);
		IObjectFactory objectFactory = applicationContext;
		return objectFactory.GetObject(type) as IAllocUser;
	}
	private void BindExistsUser(System.Collections.Generic.IList<string> userCodes)
	{
		if (userCodes != null)
		{
			PtYhmcBll ptYhmcBll = new PtYhmcBll();
			ptYhmcBll.GetNames(userCodes);
			this.hfldCodeJson.Value = JsonHelper.JsonSerializer<string[]>(userCodes.ToArray<string>());
			PTYhmcService pTYhmcService = new PTYhmcService();
			System.Collections.Generic.IList<string> nameList = pTYhmcService.GetNameList(userCodes);
			this.hfldNameJson.Value = JsonHelper.JsonSerializer<string[]>(nameList.ToArray<string>());
		}
	}
	private System.Collections.Generic.IList<string> GetExistsUser()
	{
		System.Collections.Generic.IList<string> result = new System.Collections.Generic.List<string>();
		if (this.allocUser != null)
		{
			result = this.allocUser.GetExistsUser(this.idList[0], this.tableName);
		}
		return result;
	}
	private void DeleteExistsUser()
	{
		if (this.allocUser != null)
		{
			this.allocUser.DeleteExistsUser(this.idList[0], this.tableName);
		}
	}
	private void AddUser()
	{
		if (this.allocUser != null)
		{
			System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldCodeJson.Value);
			this.allocUser.AddUser(this.idList, listFromJson, this.tableName);
		}
	}
}


