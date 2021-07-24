using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using FreeTextBoxControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ReEdit : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		
		private int _iSysID;
		private string _strSenderCode = "";
		private string _strSenderName = "";
		private int _iMailID;
		private string _strMailBox = "";
		private string _strOperType = "";


		protected void Page_Load(object sender, EventArgs e)
		{
			this.TBoxConsignee.Attributes["onclick"] = "getWhereForc(1);return false;";
			this.txtCopy.Attributes["onclick"] = "getWhereForc(2);return false;";
			this.txtSecret.Attributes["onclick"] = "getWhereForc(3);return false;";
			this.lbzs.Attributes["onclick"] = "showAnnex();return false;";
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			this._strSenderCode = this.Session["yhdm"].ToString();
			userManageDb userManageDb = new userManageDb();
			this._strSenderName = userManageDb.GetUserName(this._strSenderCode);
			try
			{
				this._iMailID = int.Parse(base.Request.QueryString["mailID"].ToString());
				this._strMailBox = base.Request.QueryString["mailBox"].ToString();
				this._strOperType = base.Request.QueryString["oType"].ToString();
			}
			catch
			{
				base.Response.End();
			}
			if (!base.IsPostBack)
			{
				this.Session["System"] = "";
				this.Session["HumanCode"] = "";
				this.Session["HumanName"] = "";
				this.Session["HumanCode2"] = "";
				this.Session["HumanName2"] = "";
				this.Session["HumanCode3"] = "";
				this.Session["HumanName3"] = "";
				if (this.DelTempAnnex(this._strSenderCode))
				{
					this.GetMail(this._iMailID);
					return;
				}
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('删除暂存区临时文件出错，刷新一次看看！');</SCRIPT>");
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
			DataTable oneMail = mailManage.GetOneMail(iMailID, this._strSenderCode);
			if (oneMail.Rows.Count > 0)
			{
				this.Session["System"] = oneMail.Rows[0]["i_SysID"].ToString() + ",";
				this.Session["HumanCode"] = oneMail.Rows[0]["i_SysID"].ToString() + ":" + oneMail.Rows[0]["v_fxrdm"].ToString() + "!";
				this.Session["HumanName"] = oneMail.Rows[0]["v_fxrxm"].ToString() + ",";
				this.TBoxConsignee.Text = this.Session["HumanName"].ToString();
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
						System.Web.UI.WebControls.TextBox expr_26C = this.TBoxAnnex;
						expr_26C.Text = expr_26C.Text + dataRow["v_Lmc"].ToString() + ",";
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
		protected void BtnToDraft_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (!mailManage.isAllowSize(this._strSenderCode))
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('该邮件的附件总大小已经超过10M限制，请进行处理，否则该邮件无法保存到草稿箱！');</script>");
				return;
			}
			int iMailType = 0;
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
			string text = "";
			string text2 = this.Session["HumanCode"].ToString() + this.Session["HumanCode2"].ToString() + this.Session["HumanCode3"].ToString();
			if (this.Session["HumanCode"].ToString().Length == 0)
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('收件人不能为空！')</SCRIPT>");
				return;
			}
			string text3 = this.TBoxConsignee.Text.ToString();
			string strTitle = this.TBoxTitle.Text.ToString();
			string strContent = this.TBoxContent.Text.ToString();
			int iMailType = 0;
			int iSms;
			if (this.CBoxSMS.Checked)
			{
				iSms = 1;
			}
			else
			{
				iSms = 0;
			}
			text3 = this.Session["HumanName"].ToString() + this.Session["HumanName2"].ToString() + this.Session["HumanName3"].ToString();
			string[] aryConsigneeCode = text2.Split(new char[]
			{
				'!'
			});
			string[] aryConsigneeName = text3.Split(new char[]
			{
				','
			});
			if (!mailManage.SendMail(this._iSysID, this._strSenderCode, this._strSenderName, DateTime.Now, strTitle, strContent, iSms, aryConsigneeCode, aryConsigneeName, iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString(), 0))
			{
				text += "向本系统发送邮件失败\\n";
			}
			if (this.CBoxSend.Checked)
			{
				text3 = this.Session["HumanName"].ToString();
				text2 = this.Session["HumanCode"].ToString();
				if (!mailManage.SaveToOut(this._iSysID, this._strSenderCode, this._strSenderName, strTitle, strContent, text2, text3, iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString()))
				{
					text += "将邮件保存到发件箱出错！";
				}
			}
			mailManage.SetMailState(this._iMailID, this._strSenderCode, this._strOperType);
			if (text != "")
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('" + text + "');</SCRIPT>");
				return;
			}
			base.Server.Transfer("viewmail_2.aspx");
		}
	}


