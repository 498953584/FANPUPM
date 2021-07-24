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
public partial class StockManage_sendGoods_ViewSendNote : NBasePage, IRequiresSessionState
{
	private sendNoteBll sendNote = new sendNoteBll();
	private sendGoodsBLL sendGoods = new sendGoodsBLL();
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private accountMange am = new accountMange();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		SendNodteModel sendNoteModel = this.sendNote.GetSendNoteModel(base.Request.QueryString["ic"]);
		this.lblremark.Text = sendNoteModel.remark;
		this.lblsnCode.Text = sendNoteModel.snCode;
		this.lblsnAddUser.Text = ptYhmcBll.GetModelById(sendNoteModel.snAddUser).v_xm;
		this.lblsnAddTime.Text = sendNoteModel.snAddTime.ToString();
		PrjInfoModel modelByPrjGuid = this.pTPrjInfoBll.GetModelByPrjGuid(sendNoteModel.prjCode.ToString());
		if (modelByPrjGuid != null)
		{
			this.lblProjectName.Text = modelByPrjGuid.PrjName;
		}
		this.lblBllProducer.Text = ptYhmcBll.GetModelById(base.UserCode).v_xm;
		this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
		this.hdGuid.Value = sendNoteModel.snId;
		string userName = this.am.GetUserName(sendNoteModel.Limits.ToString());
		this.labLimit.Text = userName;
		string[] snId = new string[]
		{
			sendNoteModel.snId
		};
		this.ViewState["DataTable"] = this.sendGoods.GetResourceBypncode(snId);
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
}


