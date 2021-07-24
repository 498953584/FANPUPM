using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometPayment_ViewPaymentList : NBasePage, IRequiresSessionState
{
	private IncometContractBll incometContractBll = new IncometContractBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hldfIsExamineApprove.Value = ConfigHelper.Get("IsIncomeContractApprove");
			this.BindGv();
		}
	}
	public void BindGv()
	{
		string text = "";
		if (this.hldfIsExamineApprove.Value == "1")
		{
			text = " AND FlowState=1 ";
		}
		if (base.Request.QueryString["g"] != null)
		{
			text += " and p1.IsArchived='true'";
		}
		if (base.Request.QueryString["Archived"] != null)
		{
			text += " and IsArchived='false' and conState in (4,5) ";
		}
		this.BindGv(this.incometContractBll.GetInfoByParam(base.Request.QueryString["contractId"], "Con_Incomet_Payment", base.UserCode, text));
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvConract.DataSource = storageDataSource;
			this.gvConract.DataBind();
			this.gvConract.HeaderRow.Cells[0].Visible = false;
			this.gvConract.Rows[0].Visible = false;
			return;
		}
		this.gvConract.DataSource = storageDataSource;
		this.gvConract.DataBind();
	}
}


