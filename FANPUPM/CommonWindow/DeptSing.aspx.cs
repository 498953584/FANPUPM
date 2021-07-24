using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.EasyUI;
using cn.justwin.Serialize;
using System;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class CommonWindow_DeptSing : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			cn.justwin.Domain.EasyUI.Department department = new cn.justwin.Domain.EasyUI.Department();
			JsonSerializer jsonSerializer = new JsonSerializer();
			this.hfldDepartJson.Value = jsonSerializer.Serialize<cn.justwin.Domain.EasyUI.Department[]>(department.GetDepartment(10).ToArray<cn.justwin.Domain.EasyUI.Department>());
		}
	}
}


