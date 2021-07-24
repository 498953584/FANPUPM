using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using cn.justwin.stockModel;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_receiveGoods_ViewSendNote : NBasePage, IRequiresSessionState
{
	private sendNoteBll sendNote = new sendNoteBll();
	private receiveNoteBLL receiveNote = new receiveNoteBLL();
	private receiveGoodsBLL receiveGoods = new receiveGoodsBLL();
	private sendGoodsBLL sendGoods = new sendGoodsBLL();
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private accountMange am = new accountMange();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
			this.SendModel();
		}
	}
	public void InitPage()
	{
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		sm_receiveNote modelByrnId = this.receiveNote.GetModelByrnId(base.Request.QueryString["ic"]);
		this.lblsnCode.Text = modelByrnId.rnCode;
		this.lblsnAddUser.Text = ptYhmcBll.GetModelById(modelByrnId.rnUser).v_xm;
		this.lblsnAddTime.Text = modelByrnId.rnTime.ToString();
		this.txtRemark.Text = StringUtility.ReplaceTxt(modelByrnId.remark.ToString());
		this.txtExplain.Text = StringUtility.ReplaceTxt(modelByrnId.Explain.ToString());
		this.lblBllProducer.Text = ptYhmcBll.GetModelById(base.UserCode).v_xm;
		this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
		this.hdGuid.Value = modelByrnId.rnId;
		this.hdfSend.Value = modelByrnId.snId;
		string[] rnid = new string[]
		{
			modelByrnId.rnId
		};
		this.ViewState["DataTable"] = this.receiveGoods.getResourceByRnid(rnid);
		this.BindGv();
	}
	public void BindGv()
	{
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		if (dataTable.Rows.Count == 0)
		{
			dataTable.Rows.Add(dataTable.NewRow());
			this.gvNeedNote.DataSource = dataTable;
			this.gvNeedNote.DataBind();
			this.gvNeedNote.HeaderRow.Cells[0].Visible = false;
			this.gvNeedNote.Rows[0].Visible = false;
			return;
		}
		this.gvNeedNote.DataSource = dataTable;
		this.gvNeedNote.DataBind();
	}
	public void SendModel()
	{
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		SendNodteModel sendNoteModel = this.sendNote.GetSendNoteModel(this.hdfSend.Value);
		this.sendremark.Text = sendNoteModel.remark;
		this.sendCode.Text = sendNoteModel.snCode;
		this.sendAddUser.Text = ptYhmcBll.GetModelById(sendNoteModel.snAddUser).v_xm;
		this.sendAddTime.Text = sendNoteModel.snAddTime.ToString();
		this.lblProjectName.Text = this.pTPrjInfoBll.GetModelByPrjGuid(sendNoteModel.prjCode.ToString()).PrjName;
		string userName = this.am.GetUserName(sendNoteModel.Limits.ToString());
		this.sendLimit.Text = userName;
		string[] snId = new string[]
		{
			sendNoteModel.snId
		};
		this.ViewState["DataSend"] = this.sendGoods.GetResourceBypncode(snId);
		this.SendResoce();
	}
	public void SendResoce()
	{
		DataTable dataTable = (DataTable)this.ViewState["DataSend"];
		if (dataTable.Rows.Count == 0)
		{
			dataTable.Rows.Add(dataTable.NewRow());
			this.gvdSend.DataSource = dataTable;
			this.gvdSend.DataBind();
			this.gvdSend.HeaderRow.Cells[0].Visible = false;
			this.gvdSend.Rows[0].Visible = false;
			return;
		}
		this.gvdSend.DataSource = dataTable;
		this.gvdSend.DataBind();
	}
}


