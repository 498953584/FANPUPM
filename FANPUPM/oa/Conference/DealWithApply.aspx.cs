using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DealWithApply : BasePage, IRequiresSessionState
	{
		public int RecordId
		{
			get
			{
				object obj = this.ViewState["RecordId"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["RecordId"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!this.Page.IsPostBack)
			{
				this.RecordId = Convert.ToInt32(base.Request.QueryString["mid"]);
				this.setData(this.RecordId);
			}
		}
		protected void setData(int recordId)
		{
			DataTable dataTable = ConferenceManage.QueryApplyInfo(recordId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtSendTo.Text = "发消息至 " + dataTable.Rows[0]["UserName"].ToString();
				this.hdnMangeCode.Value = dataTable.Rows[0]["ManagerCode"].ToString();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (ConferenceManage.SetApplyState(-1, this.RecordId))
			{
				if (this.CBoxSMS.Checked)
				{
					DateTime sendTime = default(DateTime);
					sendTime = DateTime.Now;
					PublicInterface.SendSmsMsg(new SMSLog
					{
						SendUser = this.Session["yhdm"].ToString(),
						ReceiveUser = this.hdnMangeCode.Value.ToString(),
						Message = this.txtContent.Text,
						SendTime = sendTime,
						I_XGID = this.RecordId.ToString(),
						V_LXBM = "009"
					});
				}
				if (this.CBRTX.Checked)
				{
					PublicInterface.SendSysMsg(this.getPTDBSJ(this.RecordId.ToString(), "会议退回：" + this.txtContent.Text, this.Session["yhdm"].ToString()));
				}
				this.JS.Text = "alert('会议室申请已处理!');window.returnValue='" + this.RecordId.ToString() + "';window.close();";
				return;
			}
			this.JS.Text = "alert('处理会议室申请失败!');";
		}
		private PTDBSJ getPTDBSJ(string xgid, string Mes, string jsyhdm)
		{
			return new PTDBSJ
			{
				V_LXBM = "009",
				I_XGID = xgid,
				DTM_DBSJ = DateTime.Now,
				V_Content = Mes,
				V_DBLJ = "?rid=" + xgid,
				V_YHDM = jsyhdm
			};
		}
	}


