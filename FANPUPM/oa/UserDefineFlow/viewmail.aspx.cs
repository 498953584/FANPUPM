using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ViewMail : BasePage, IRequiresSessionState
	{
		private int _iSysID;
		private string _strMailBox = "";
		private string _strSenderCode = "";
		private int _iMailID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			this._strSenderCode = this.Session["yhdm"].ToString();
			string str = "";
			string a = "";
			try
			{
				str = base.Request.QueryString.ToString();
				if (base.Request["rid"] != null && base.Request.QueryString["rid"].ToString() != null)
				{
					this._iMailID = int.Parse(base.Request.QueryString["rid"].ToString());
					this._strMailBox = "s";
					a = "s";
				}
				else
				{
					a = base.Request.QueryString["mailtype"].ToString();
					this._iMailID = int.Parse(base.Request.QueryString["mailID"].ToString());
					this._strMailBox = base.Request.QueryString["mailBox"].ToString();
				}
			}
			catch
			{
				base.Response.End();
			}
			if (this._strMailBox == "l")
			{
				this.BtnEdit.Visible = false;
				this.BtnReply.Visible = false;
				this.BtnTrans.Visible = false;
				this.BtnDelN.Visible = false;
				this.BtnDelY.Text = "删除";
			}
			else
			{
				if (this._strMailBox == "d")
				{
					this.BtnEdit.Visible = true;
					this.BtnReply.Visible = false;
					this.BtnTrans.Visible = false;
				}
				else
				{
					if (this._strMailBox == "s")
					{
						this.BtnEdit.Visible = false;
						this.BtnReply.Visible = false;
						this.BtnTrans.Visible = false;
					}
				}
			}
			if (!base.IsPostBack)
			{
				this.RestoreMail(this._iMailID, this._strSenderCode);
				PTDBSJAction pTDBSJAction = new PTDBSJAction();
				pTDBSJAction.Delete(string.Concat(new object[]
				{
					" v_lxbm = '008' and v_YHDM = '",
					this.Session["yhdm"].ToString(),
					"' and i_XGID = '",
					this._iMailID,
					"'"
				}), 1);
				this.BtnDelY.Attributes["onclick"] = "return confirm('该操作不可恢复，你确认要删除?',1,0);";
				if (a == "n")
				{
					this.HLnkBack.NavigateUrl = "newmail.aspx?" + str;
					return;
				}
				if (a == "s")
				{
					this.HLnkBack.NavigateUrl = "inbox.aspx?" + str;
					return;
				}
				if (a == "l")
				{
					this.HLnkBack.NavigateUrl = "Trashbox.aspx?" + str;
				}
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
				this.LabConsignee.Text = this.sub(oneMail.Rows[0]["v_SJR"].ToString());
				this.LbCSR.Text = this.sub(oneMail.Rows[0]["V_CSR"].ToString());
				this.LabTitle.Text = oneMail.Rows[0]["v_zt"].ToString();
				this.LabDateTime.Text = oneMail.Rows[0]["dtm_sjsj"].ToString();
				this.LblCon.Text = oneMail.Rows[0]["txt_zw"].ToString();
				DataTable mailAnnex = mailManage.GetMailAnnex(iMailID);
				int num2 = 20 * mailAnnex.Rows.Count;
				this.tr_fj.Attributes["height"] = num2.ToString() + "px";
				if (mailAnnex.Rows.Count > 0)
				{
					for (int i = 0; i < mailAnnex.Rows.Count; i++)
					{
						string[] array = mailAnnex.Rows[i]["v_Lmc"].ToString().Split(new char[]
						{
							'-'
						});
						HtmlGenericControl expr_25A = this.annexBlock;
						string innerHtml = expr_25A.InnerHtml;
						expr_25A.InnerHtml = string.Concat(new string[]
						{
							innerHtml,
							"<a href='",
							mailAnnex.Rows[i]["v_fjlj"].ToString(),
							mailAnnex.Rows[i]["v_Lmc"].ToString(),
							"' target=_blank>",
							array[1].ToString(),
							"</a> "
						});
					}
				}
				mailManage.ReadMail(iMailID, strSenderCode);
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void BtnEdit_Click(object sender, System.EventArgs e)
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
		protected void BtnDelN_Click(object sender, System.EventArgs e)
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
		protected void BtnDelY_Click(object sender, System.EventArgs e)
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
		protected void BtnReply_Click(object sender, System.EventArgs e)
		{
			base.Response.Redirect(string.Concat(new string[]
			{
				"ReEdit.aspx?mailID=",
				this._iMailID.ToString(),
				"&mailBox=",
				this._strMailBox,
				"&oType=r"
			}));
		}
		protected void BtnTrans_Click(object sender, System.EventArgs e)
		{
			base.Response.Redirect(string.Concat(new string[]
			{
				"ReEdit.aspx?mailID=",
				this._iMailID.ToString(),
				"&mailBox=",
				this._strMailBox,
				"&oType=z"
			}));
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


