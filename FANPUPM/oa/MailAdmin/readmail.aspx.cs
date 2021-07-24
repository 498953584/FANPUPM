using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ReadMail : BasePage, IRequiresSessionState
	{
		private int _iSysID;
		private string _strMailBox = "";
		private string _strSenderCode = "";
		private int _iMailID;

		protected void Page_Load(object sender, EventArgs e)
		{
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			this._strSenderCode = this.Session["yhdm"].ToString();
			try
			{
				this._iMailID = int.Parse(base.Request.QueryString["mailID"].ToString());
				this._strMailBox = base.Request.QueryString["mailBox"].ToString();
			}
			catch
			{
				base.Response.End();
			}
			if (this._strMailBox == "l")
			{
				this.BtnEdit.Visible = false;
			}
			else
			{
				if (this._strMailBox == "d")
				{
					this.BtnEdit.Visible = true;
				}
				else
				{
					if (this._strMailBox == "s")
					{
						this.BtnEdit.Visible = false;
					}
				}
			}
			if (!base.IsPostBack)
			{
				this.RestoreMail(this._iMailID, this._strSenderCode);
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
					dataTable.Rows[0]["v_Sys"].ToString();
					string str = dataTable.Rows[0]["v_SysName"].ToString();
					this.LabSender.Text = oneMail.Rows[0]["v_fxrxm"].ToString() + "[来自：" + str + "]";
				}
				else
				{
					this.LabSender.Text = oneMail.Rows[0]["v_fxrxm"].ToString();
				}
				this.LabConsignee.Text = oneMail.Rows[0]["v_jsrxm"].ToString();
				this.LbCSR.Text = this.sub(oneMail.Rows[0]["V_CSR"].ToString());
				this.LabTitle.Text = oneMail.Rows[0]["v_zt"].ToString();
				this.LabDateTime.Text = oneMail.Rows[0]["dtm_sjsj"].ToString();
				string text = oneMail.Rows[0]["txt_zw"].ToString();
				text = text.Replace("\r\n", "<BR>");
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
						HtmlGenericControl expr_245 = this.annexBlock;
						string innerHtml = expr_245.InnerHtml;
						expr_245.InnerHtml = string.Concat(new string[]
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
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void BtnEdit_Click(object sender, EventArgs e)
		{
			base.Response.Redirect(string.Concat(new string[]
			{
				"ReWrite.aspx?mailID=",
				this._iMailID.ToString(),
				"&mailBox=",
				this._strMailBox,
				"&oType=e"
			}));
		}
		protected void BtnDelY_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (mailManage.DelMail(this._iMailID, this._strSenderCode))
			{
				if (this._strMailBox == "s")
				{
					base.Response.Redirect("InBox.aspx");
					return;
				}
				if (this._strMailBox == "d")
				{
					base.Response.Redirect("OutBox.aspx");
					return;
				}
				if (this._strMailBox == "l")
				{
					base.Response.Redirect("TrashBox.aspx");
				}
			}
		}
		protected void BtnDelN_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (mailManage.MoveToRecycle(this._iMailID, this._strSenderCode))
			{
				if (this._strMailBox == "s")
				{
					base.Response.Redirect("InBox.aspx");
					return;
				}
				if (this._strMailBox == "d")
				{
					base.Response.Redirect("OutBox.aspx");
					return;
				}
				if (this._strMailBox == "l")
				{
					base.Response.Redirect("TrashBox.aspx");
				}
			}
		}
		protected string sub(string strSub)
		{
			if (strSub != "")
			{
				strSub = strSub.Substring(0, strSub.Length - 1);
			}
			return strSub;
		}
	}


