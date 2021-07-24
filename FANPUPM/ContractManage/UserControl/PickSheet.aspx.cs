using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_UserControl_PickSheet : NBasePage, IRequiresSessionState
{
	private string prjGuid = string.Empty;
	private string modifyId = string.Empty;
	private string action = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ModifyId"]))
		{
			this.modifyId = base.Request["ModifyId"];
		}
		if (!string.IsNullOrEmpty(base.Request["PrjGuid"]))
		{
			this.prjGuid = base.Request["PrjGuid"];
		}
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			IncometModifyBll incometModifyBll = new IncometModifyBll();
			DataTable dataSource = new DataTable();
			if (this.action == "modify")
			{
				dataSource = incometModifyBll.PickSheetByModifyId(this.modifyId);
			}
			else
			{
				dataSource = incometModifyBll.PickSheetByPrjGuid(this.prjGuid);
			}
			this.rptSheet.DataSource = dataSource;
			this.rptSheet.DataBind();
		}
	}
}


