using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.EasyUI;
using cn.justwin.Model;
using cn.justwin.Serialize;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HumanResource_Organization_OrgManage : NBasePage, IRequiresSessionState
{
	private PTDUTYAction hrAction = new PTDUTYAction();
	private string url = string.Empty;
	public DeptManageDb dmd
	{
		get
		{
			return new DeptManageDb();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		this.url = this.hfldTargetSrc.Value;
		if (!base.IsPostBack)
		{
			cn.justwin.Domain.EasyUI.Department department = new cn.justwin.Domain.EasyUI.Department();
			JsonSerializer jsonSerializer = new JsonSerializer();
			this.hfldDepartJson.Value = jsonSerializer.Serialize<cn.justwin.Domain.EasyUI.Department[]>(department.GetDepartment(10).ToArray<cn.justwin.Domain.EasyUI.Department>());
		}
	}
	protected void LinkButton1_Click(object sender, EventArgs e)
	{
		cn.justwin.BLL.Department department = new cn.justwin.BLL.Department();
		DepartmentInfo department2 = department.GetDepartment();
		XmlSerializer xmlSerializer = new XmlSerializer();
		string text = xmlSerializer.Serialize<DepartmentInfo>(department2);
		text = text.Replace("utf-16", "utf-8");
		base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("组织机构.xml", Encoding.UTF8));
		base.Response.BinaryWrite(Encoding.UTF8.GetBytes(text));
		base.Response.End();
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
				treeNode.ToolTip = dataRow["i_bmdm"].ToString();
				ftn.ChildNodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
}


