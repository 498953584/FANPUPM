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
	public partial class ReWrite : BasePage, IRequiresSessionState
	{
		
		private int _iSysID;
		private string _strSenderCode = "";
		private string _strSenderName = "";
		private int _iMailID;
		private string _strMailBox = "";
		private string _strOperType = "";

		protected void Page_Load(object sender, EventArgs e)
		{
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
					this.GetDraft(this._iMailID);
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
		private void GetDraft(int iMailID)
		{
			MailManage mailManage = new MailManage();
			if (mailManage.ReEditAnnex(iMailID, this._strSenderCode))
			{
				DataTable oneMail = mailManage.GetOneMail(iMailID, this._strSenderCode);
				if (oneMail.Rows.Count > 0)
				{
					this.TBoxTitle.Text = oneMail.Rows[0]["v_zt"].ToString();
					this.TBoxContent.Text = oneMail.Rows[0]["txt_zw"].ToString();
					if (int.Parse(oneMail.Rows[0]["i_fjsl"].ToString()) > 0)
					{
						DataTable mailAnnex = mailManage.GetMailAnnex(iMailID);
						foreach (DataRow dataRow in mailAnnex.Rows)
						{
							TextBox expr_D0 = this.TBoxAnnex;
							expr_D0.Text = expr_D0.Text + dataRow["v_Lmc"].ToString() + ",";
						}
					}
					DataTable consignee = mailManage.GetConsignee(iMailID);
					if (consignee.Rows.Count > 0)
					{
						this.Session["HumanCode"] = consignee.Rows[0]["v_jsrdm"].ToString();
						this.Session["HumanName"] = consignee.Rows[0]["v_jsrxm"].ToString();
						this.TBoxConsignee.Text = this.Session["HumanName"].ToString();
						string text = this.Session["HumanCode"].ToString();
						string[] array = text.Split(new char[]
						{
							'!'
						});
						string text2 = "";
						for (int i = 0; i < array.Length - 1; i++)
						{
							text2 = text2 + array[i].Split(new char[]
							{
								':'
							})[0].ToString() + ":";
						}
						string[] array2 = text2.Split(new char[]
						{
							':'
						});
						for (int j = 0; j < array2.Length; j++)
						{
							int num = j + 1;
							while (num < array2.Length && !(array2[j].ToString() == ""))
							{
								if (array2[j].ToString() == array2[num].ToString())
								{
									array2[num] = "";
								}
								num++;
							}
						}
						for (int k = 0; k < array2.Length; k++)
						{
							if (array2[k].ToString() != "")
							{
								HttpSessionState session;
								(session = this.Session)["System"] = session["System"] + array2[k] + ",";
							}
						}
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
			string strTitle = this.TBoxTitle.Text.ToString();
			int iMailType = 0;
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
				if (mailManage.SaveToDraft(this._iSysID, this._strSenderCode, this._strSenderName, strTitle, strContent, text, text2, iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString()))
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
			string text = this.Session["HumanCode"].ToString() + this.Session["HumanCode2"].ToString() + this.Session["HumanCode3"].ToString();
			if (text.Length == 0)
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('收件人不能为空！')</SCRIPT>");
				return;
			}
			string text2 = this.Session["HumanName"].ToString() + this.Session["HumanName2"].ToString() + this.Session["HumanName3"].ToString();
			string text3 = this.TBoxTitle.Text.ToString();
			if (text3.Trim() == "")
			{
				text3 = "---";
			}
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
			int rtx;
			if (this.CBRTX.Checked)
			{
				rtx = 1;
			}
			else
			{
				rtx = 0;
			}
			string text4 = "";
			string[] aryConsigneeCode = text.Split(new char[]
			{
				'!'
			});
			string[] aryConsigneeName = text2.Split(new char[]
			{
				','
			});
			if (!mailManage.SendMail(this._iSysID, this._strSenderCode, this._strSenderName, DateTime.Now, text3, strContent, iSms, aryConsigneeCode, aryConsigneeName, iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString(), rtx))
			{
				text4 += "向本系统发送邮件失败\\n";
			}
			if (this.CBoxSend.Checked)
			{
				text2 = this.Session["HumanName"].ToString();
				text = this.Session["HumanCode"].ToString();
				if (!mailManage.SaveToOut(this._iSysID, this._strSenderCode, this._strSenderName, text3, strContent, text, text2, iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString()))
				{
					text4 += "将邮件保存到发件箱出错！";
				}
			}
			if (text4 != "")
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('" + text4 + "');</SCRIPT>");
				return;
			}
			this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('邮件发送成功！');</SCRIPT>");
		}
	}


