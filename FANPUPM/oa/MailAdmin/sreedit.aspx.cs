using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SReEdit : BasePage, IRequiresSessionState
	{
		private int _iSysID;
		private string _strSenderCode = "";
		private string _strSenderName = "";
		private int _iMailID;
		private string _strMailBox = "";
		private string _strOperType = "";
		private string _strHeaderCode = "";
		private string _strHeaderName = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			this._strSenderCode = this.Session["yhdm"].ToString();
			userManageDb userManageDb = new userManageDb();
			this._strSenderName = userManageDb.GetUserName(this._strSenderCode);
			try
			{
				this._strHeaderCode = base.Request.QueryString["hcode"].ToString();
				this._iMailID = int.Parse(base.Request.QueryString["mailID"].ToString());
				this._strMailBox = base.Request.QueryString["mailBox"].ToString();
				this._strOperType = base.Request.QueryString["oType"].ToString();
			}
			catch
			{
				base.Response.End();
			}
			this._strHeaderName = userManageDb.GetUserName(this._strHeaderCode);
			if (!base.IsPostBack)
			{
				this.Session["System"] = "";
				this.Session["HumanCode"] = "";
				this.Session["HumanName"] = "";
				if (this._strOperType == "r")
				{
					this.BtnConsignee.Disabled = true;
				}
				if (this.DelTempAnnex(this._strSenderCode))
				{
					this.GetMail(this._iMailID);
					return;
				}
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('删除临时文件出错！');</SCRIPT>");
			}
		}
		private bool DelTempAnnex(string strSenderCode)
		{
			MailManage mailManage = new MailManage();
			DataTable tempAnnex = mailManage.GetTempAnnex(strSenderCode);
			for (int i = 0; i < tempAnnex.Rows.Count; i++)
			{
				if (!mailManage.DelTempAnnex(int.Parse(tempAnnex.Rows[i]["i_fj_id"].ToString()), true))
				{
					return false;
				}
			}
			return true;
		}
		private void GetMail(int iMailID)
		{
			MailManage mailManage = new MailManage();
			if (this._strOperType == "z" && !mailManage.ReEditAnnex(iMailID, this._strSenderCode))
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('还原邮件失败，请刷新试试！');</SCRIPT>");
				return;
			}
			DataTable oneMail = mailManage.GetOneMail(iMailID, this._strHeaderCode);
			if (oneMail.Rows.Count > 0)
			{
				if (this._strOperType == "r")
				{
					this.TBoxTitle.Text = "[回复]" + oneMail.Rows[0]["v_zt"].ToString();
				}
				else
				{
					if (this._strOperType == "z")
					{
						this.TBoxTitle.Text = "[转发]" + oneMail.Rows[0]["v_zt"].ToString();
					}
					else
					{
						this.TBoxTitle.Text = oneMail.Rows[0]["v_zt"].ToString();
					}
				}
				this.TBoxContent.Text = oneMail.Rows[0]["txt_zw"].ToString();
				if (this._strOperType == "z" && int.Parse(oneMail.Rows[0]["i_fjsl"].ToString()) > 0)
				{
					DataTable mailAnnex = mailManage.GetMailAnnex(iMailID);
					foreach (DataRow dataRow in mailAnnex.Rows)
					{
						TextBox expr_18D = this.TBoxAnnex;
						expr_18D.Text = expr_18D.Text + dataRow["v_Lmc"].ToString() + ",";
					}
				}
				if (oneMail.Rows[0]["i_MailType"].ToString() == "0")
				{
					this.RBtnMailType.Items[0].Selected = true;
				}
				else
				{
					this.RBtnMailType.Items[1].Selected = true;
				}
				this.Session["System"] = oneMail.Rows[0]["i_SysID"].ToString() + ",";
				this.Session["HumanCode"] = oneMail.Rows[0]["i_SysID"].ToString() + ":" + oneMail.Rows[0]["v_fxrdm"].ToString() + "!";
				this.Session["HumanName"] = oneMail.Rows[0]["v_fxrxm"].ToString() + ",";
				this.TBoxConsignee.Text = this.Session["HumanName"].ToString();
			}
		}
		private void BtnToDraft_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (!mailManage.isAllowSize(this._strSenderCode))
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('该邮件的附件总大小已经超过10M限制，请进行处理，否则该邮件无法保存到草稿箱！');</script>");
				return;
			}
			int iMailType = int.Parse(this.RBtnMailType.SelectedValue.ToString());
			string strTitle = this.TBoxTitle.Text.ToString();
			string strContent = this.TBoxContent.Text.ToString();
			string text = this.Session["HumanCode"].ToString();
			string text2 = this.Session["HumanName"].ToString();
			if (this._strMailBox == "c")
			{
				if (mailManage.UpdDraft(this._strSenderCode, this._iMailID, strTitle, strContent, text, text2, iMailType))
				{
					this.RegisterClientScriptBlock("warn", "<SCRIPT languange=\"JavaScript\">alert('成功更新草稿!');</SCRIPT>");
					return;
				}
				this.RegisterClientScriptBlock("warn", "<SCRIPT languange=\"JavaScript\">alert('更新草稿出错!');</SCRIPT>");
				return;
			}
			else
			{
				if (mailManage.SaveToDraft(this._iSysID, this._strSenderCode, this._strSenderName, strTitle, strContent, text, text2, iMailType))
				{
					this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('成功保存到草稿箱！')</SCRIPT>");
					return;
				}
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('保存到草稿箱出错！')</SCRIPT>");
				return;
			}
		}
		protected void BtnSend_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (!mailManage.isAllowSize(this._strSenderCode))
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('该邮件的附件总大小已经超过10M限制，请进行处理，否则该邮件无法发送！');</script>");
				return;
			}
			string text = this.Session["HumanCode"].ToString();
			if (text.Length == 0)
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('收件人不能为空！')</SCRIPT>");
				return;
			}
			string text2 = this.TBoxConsignee.Text.ToString();
			string text3 = this.TBoxTitle.Text.ToString();
			if (text3.Trim() == "")
			{
				text3 = "---";
			}
			string text4 = this.TBoxContent.Text.ToString();
			text4 = text4 + "<BR><B>本邮件是由" + this._strSenderName + "代为处理！</B>";
			int iMailType = int.Parse(this.RBtnMailType.SelectedValue.ToString());
			int iSms;
			if (this.CBoxSMS.Checked)
			{
				iSms = 1;
			}
			else
			{
				iSms = 0;
			}
			string text5 = "";
			string[] aryConsigneeCode = text.Split(new char[]
			{
				'!'
			});
			string[] aryConsigneeName = text2.Split(new char[]
			{
				','
			});
			if (!mailManage.SendMail(this._iSysID, this._strHeaderCode, this._strHeaderName, DateTime.Now, text3, text4, iSms, aryConsigneeCode, aryConsigneeName, iMailType))
			{
				text5 += "向本系统发送邮件失败\\n";
			}
			if (this.CBoxSend.Checked && !mailManage.SaveToOut(this._iSysID, this._strSenderCode, this._strSenderName, text3, text4, text, text2, iMailType))
			{
				text5 += "将邮件保存到发件箱出错！";
			}
			mailManage.SetMailState(this._iMailID, this._strHeaderCode, this._strOperType);
			if (text5 != "")
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('" + text5 + "');</SCRIPT>");
				return;
			}
			this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('邮件发送成功！');</SCRIPT>");
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void BtnBack_Click(object sender, EventArgs e)
		{
			base.Response.Redirect(string.Concat(new string[]
			{
				"SViewMail.aspx?hcode=",
				this._strHeaderCode.ToString(),
				"&mailID=",
				this._iMailID.ToString(),
				"&mailBox=",
				this._strMailBox.ToString()
			}));
		}
	}


