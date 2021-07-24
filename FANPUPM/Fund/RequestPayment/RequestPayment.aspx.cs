using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Fund.RequestPayment.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_RequestPayment_RequestPayment : NBasePage, IRequiresSessionState
{
	private RequestPayDetail detatilBLL = new RequestPayDetail();
	private RequestPayMain mailBLL = new RequestPayMain();
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();

	protected Guid PrjGuid
	{
		get
		{
			object obj = this.ViewState["PRJGUID"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["PRJGUID"] = value;
		}
	}
	public string plantype
	{
		get
		{
			object obj = this.ViewState["plantype"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["plantype"] = value;
		}
	}
	public string prjName
	{
		get
		{
			object obj = this.ViewState["prjName"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["prjName"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.gvBudget.PageSize = NBasePage.pagesize;
			this.setTitle();
			this.InitPage();
		}
	}
	public void setTitle()
	{
		this.hfPrjName.Value = this.prjName;
		this.lblTitle.Text = this.prjName + "  请款计划";
	}
	public void InitPage()
	{
		this.BindDrop();
		this.BindTree();
		this.BindGv();
	}
	public void BindGv()
	{
		this.hfldPrjId.Value = this.tvBudget.SelectedValue;
		this.hfldPurchaseChecked.Value = string.Empty;
		if (this.hfldPrjId.Value.ToString() != "")
		{
			this.prjName = this.tvBudget.SelectedNode.Text.ToString();
			this.setTitle();
			DataTable list = this.mailBLL.GetList(" PrjGuid ='" + this.hfldPrjId.Value.ToString() + "' ", " RPayDate desc");
			this.gvBudget.DataSource = list;
		}
		this.gvBudget.DataBind();
	}
	public void BindDrop()
	{
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindDlistYears(this.ddlYear, this.Session["PmSet"], base.UserCode, base.Request["year"]);
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.ddlYear.SelectedValue = base.Request["year"];
		}
	}
	protected void BindTree()
	{
		if (this.ddlYear.SelectedValue == "zzjg")
		{
			this.BindZZJGTree();
			return;
		}
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindTreeNodes(this.tvBudget, this.Session["PmSet"], base.UserCode, base.Request["prjGuid"], this.ddlYear.SelectedItem.Text, this.ddlYear.SelectedValue);
		TreeNode treeNode = this.tvBudget.Nodes[0];
		this.DisabledBtnAdd(treeNode.ChildNodes.Count);
	}
	private void DisabledBtnAdd(int dataCount)
	{
		if (dataCount >= 1)
		{
			this.btnAdd.Disabled = false;
			return;
		}
		this.btnAdd.Disabled = true;
	}
	protected void BindZZJGTree()
	{
		this.tvBudget.Nodes.Clear();
		DataTable departmentTree = this.hrAction.GetDepartmentTree("0");
		this.DisabledBtnAdd(departmentTree.Rows.Count);
		if (departmentTree.Rows.Count > 0)
		{
			for (int i = 0; i < departmentTree.Rows.Count; i++)
			{
				DataRow dataRow = departmentTree.Rows[i];
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				this.tvBudget.Nodes.Add(treeNode);
				if (i == 0)
				{
					treeNode.Select();
				}
				if (!base.IsPostBack && base.Request["prjId"] != null && treeNode.Value == base.Request["prjId"])
				{
					treeNode.Select();
				}
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
			this.tvBudget.ExpandAll();
		}
	}
	private void Bind_SubTree(TreeNode ftn, string strBMDM)
	{
		DataTable departmentTree = this.hrAction.GetDepartmentTree(strBMDM);
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				if (!base.IsPostBack && base.Request["prjId"] != null && treeNode.Value == base.Request["prjId"])
				{
					treeNode.Select();
				}
				ftn.ChildNodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
	protected void TreeView_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.BindTree();
		this.BindGv();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			if (e.Row.Cells[7].Text.ToString().Length > 23)
			{
				e.Row.Cells[7].ToolTip = e.Row.Cells[7].Text.ToString();
				e.Row.Cells[7].Text = e.Row.Cells[7].Text.Substring(0, 22) + "...";
			}
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> list = new List<string>();
		if (this.hfldPurchaseChecked.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		}
		else
		{
			list.Add(this.hfldPurchaseChecked.Value);
		}
		try
		{
			foreach (string current in list)
			{
				if (this.mailBLL.Delete(new Guid(current)))
				{
					this.detatilBLL.Delete(new Guid(current));
				}
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
		}
		this.BindGv();
	}
	protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvBudget.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
}


