using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_TaskTypeEdit : NBasePage, IRequiresSessionState
{
	private string taskTypeId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.taskTypeId = base.Request["id"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		if (string.IsNullOrEmpty(this.taskTypeId))
		{
			this.hdGuid.Value = System.Guid.NewGuid().ToString();
			return;
		}
		BudTaskType byId = BudTaskType.GetById(this.taskTypeId);
		if (byId != null)
		{
			this.txtTaskTypeName.Text = byId.Name;
			return;
		}
		base.RegisterScript("alert('系统提示：\\n\\n 此类型不存在!');winclose('taskTypeEdit', 'TaskTypeList.aspx', true);");
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
	}
	public void AddType()
	{
	}
	public string SetMsg()
	{
		if (!string.IsNullOrEmpty(this.taskTypeId))
		{
			return "更新";
		}
		return "添加";
	}
}


