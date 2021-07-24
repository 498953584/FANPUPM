using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SViewMail : BasePage, IRequiresSessionState
	{
		private int _iSysID;
		private string _strMailBox = "";
		private string _strSenderCode = "";
		private int _iMailID;
		private string _strHeaderCode = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			this._strSenderCode = this.Session["yhdm"].ToString();
			try
			{
				this._strHeaderCode = base.Request.QueryString["hcode"].ToString();
				this._iMailID = int.Parse(base.Request.QueryString["mailID"].ToString());
				this._strMailBox = base.Request.QueryString["mailBox"].ToString();
			}
			catch
			{
				base.Response.End();
			}
			if (!base.IsPostBack)
			{
				this.HLnkBack.NavigateUrl = "MailSecretary.aspx?hcode=" + this._strHeaderCode;
				this.RestoreMail(this._iMailID, this._strHeaderCode);
				this.BtnDelY.Attributes["onclick"] = "return confirm('该操作不可恢复，你确认要删除?',1,0);";
			}
		}
		private void RestoreMail(int iMailID, string strSenderCode)
		{
			MailManage mailManage = new MailManage();
			DataTable oneMail = mailManage.GetOneMail(iMailID, strSenderCode);
			if (oneMail.Rows.Count > 0)
			{
				int num = int.Parse(oneMail.Rows[0]["i_SysID"].ToString());
				if (num != this._iSysID)
				{
					SysManageDb sysManageDb = new SysManageDb();
					DataTable dataTable = sysManageDb.QuerySys(num);
					string str = dataTable.Rows[0]["v_SysName"].ToString();
					dataTable.Rows[0]["v_Sys"].ToString();
					this.LabSender.Text = oneMail.Rows[0]["v_fxrxm"].ToString() + "[来自：" + str + "]";
				}
				else
				{
					this.LabSender.Text = oneMail.Rows[0]["v_fxrxm"].ToString();
				}
				this.LabConsignee.Text = oneMail.Rows[0]["v_jsrxm"].ToString();
				string strUserCode = oneMail.Rows[0]["v_jsrdm"].ToString();
				this.LabTitle.Text = oneMail.Rows[0]["v_zt"].ToString();
				this.LabDateTime.Text = oneMail.Rows[0]["dtm_sjsj"].ToString();
				string text = oneMail.Rows[0]["txt_zw"].ToString();
				text = text.Replace("\r\n", "<BR>");
				text = text.Replace(" ", "&nbsp;");
				text = "&nbsp;&nbsp;&nbsp;&nbsp;" + text;
				this.contentBlock.InnerHtml = text;
				DataTable mailAnnex = mailManage.GetMailAnnex(iMailID);
				if (mailAnnex.Rows.Count > 0)
				{
					for (int i = 0; i < mailAnnex.Rows.Count; i++)
					{
						string[] array = mailAnnex.Rows[i]["v_Lmc"].ToString().Split(new char[]
						{
							'-'
						});
						HtmlGenericControl expr_24F = this.annexBlock;
						string innerHtml = expr_24F.InnerHtml;
						expr_24F.InnerHtml = string.Concat(new string[]
						{
							innerHtml,
							"<LI><A href=\"#\" onclick=\"javascript:download('",
							mailAnnex.Rows[i]["v_fjlj"].ToString(),
							mailAnnex.Rows[i]["v_Lmc"].ToString(),
							"','",
							array[1].ToString(),
							"');\">",
							array[1].ToString(),
							"</A>"
						});
					}
				}
				mailManage.ReadMail(iMailID, strUserCode);
			}
		}
		protected void BtnDelN_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (mailManage.MoveToRecycle(this._iMailID, this._strHeaderCode))
			{
				base.Response.Redirect("MailSecretary.aspx?hcode" + this._strHeaderCode);
				return;
			}
			this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('将邮件移到垃圾箱失败！')</SCRIPT>");
		}
		protected void BtnDelY_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (mailManage.DelMail(this._iMailID, this._strHeaderCode))
			{
				base.Response.Redirect("MailSecretary.aspx?hcode" + this._strHeaderCode);
				return;
			}
			this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('删除邮件失败！');</SCRIPT>");
		}
		protected void BtnReply_Click(object sender, EventArgs e)
		{
			base.Response.Redirect(string.Concat(new string[]
			{
				"SReEdit.aspx?hcode=",
				this._strHeaderCode,
				"&mailID=",
				this._iMailID.ToString(),
				"&mailBox=",
				this._strMailBox,
				"&oType=r"
			}));
		}
		protected void BtnTrans_Click(object sender, EventArgs e)
		{
			base.Response.Redirect(string.Concat(new string[]
			{
				"SReEdit.aspx?hcode=",
				this._strHeaderCode,
				"&mailID=",
				this._iMailID.ToString(),
				"&mailBox=",
				this._strMailBox,
				"&oType=z"
			}));
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


