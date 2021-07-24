using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_basicset_ShowView : NBasePage, IRequiresSessionState
{
	private PTDbsjBll pTDbsjBll = new PTDbsjBll();
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["gid"]))
		{
			this.id = base.Request["gid"];
		}
		if (!string.IsNullOrEmpty(base.Request["i"]))
		{
			this.id = base.Request["i"];
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
		PTDbsjModel modelByGID = this.pTDbsjBll.GetModelByGID(this.id);
		this.lbRecordDate.Text = modelByGID.DTM_DBSJ.ToString();
		this.lbContent.Text = modelByGID.V_Content;
	}
}


