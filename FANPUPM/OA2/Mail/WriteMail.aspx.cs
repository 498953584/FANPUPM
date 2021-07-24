using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.SMS;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OA2_WriteMail : NBasePage, IRequiresSessionState
{
	private string mailId = string.Empty;
	private string edit = "0";
	private string Transmit = "0";
	private MailService mailService = new MailService();
	private static string folder = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["mailId"]))
		{
			this.mailId = base.Request["mailId"];
		}
		if (!string.IsNullOrEmpty(base.Request["edit"]))
		{
			this.edit = base.Request["edit"];
		}
		if (!string.IsNullOrEmpty(base.Request["Transmit"]))
		{
			this.Transmit = base.Request["Transmit"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		OA2_WriteMail.folder = ConfigurationManager.AppSettings["Mail"];
		if (!base.IsPostBack)
		{
			this.Init();
		}
	}
	protected void btnSend_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.IList<string> userList = this.GetUserList();
		System.Collections.Generic.IEnumerable<string> enumerable = userList.Distinct<string>();
		System.Collections.Generic.Dictionary<string, string> dictionary = new System.Collections.Generic.Dictionary<string, string>();
		if (enumerable.Count<string>() > 0)
		{
			System.Collections.Generic.List<Mail> list = new System.Collections.Generic.List<Mail>();
			string value = string.Empty;
			int num = 0;
			foreach (string current in enumerable)
			{
				value = System.Guid.NewGuid().ToString();
				dictionary.Add(current, value);
				string toMailId = System.Guid.NewGuid().ToString();
				list.Add(new Mail
				{
					MailId = value,
					ToMailId = toMailId,
					MailName = this.txtName.Text.Trim(),
					MailFrom = base.UserCode,
					MailTo = current,
					AllMailToCode = this.hfldTo.Value,
					AllMailTo = this.txtTo.Text,
					AllCopytoCode = this.hfldCopyto.Value,
					AllCopyto = this.txtCopyto.Text,
					MailContent = this.txtContent.Value,
					IsValid = true,
					IsReaded = false,
					MailType = "I",
					AnnexId = (string)this.ViewState["annexId"],
					InputDate = System.DateTime.Now.AddSeconds((double)num)
				});
				list.Add(new Mail
				{
					MailId = System.Guid.NewGuid().ToString(),
					ToMailId = toMailId,
					MailName = this.txtName.Text.Trim(),
					MailFrom = base.UserCode,
					MailTo = current,
					AllMailToCode = this.hfldTo.Value,
					AllMailTo = this.txtTo.Text,
					AllCopytoCode = this.hfldCopyto.Value,
					AllCopyto = this.txtCopyto.Text,
					MailContent = this.txtContent.Value,
					IsValid = true,
					IsReaded = false,
					MailType = "O",
					AnnexId = (string)this.ViewState["annexId"],
					InputDate = System.DateTime.Now.AddSeconds((double)num)
				});
				num++;
			}
			if (!string.IsNullOrEmpty(this.mailId) && this.edit == "1")
			{
				this.mailService.Delete(this.mailId);
			}
			this.mailService.AddOrUpdate(list);
			if (this.chkMobileMsg.Checked)
			{
				this.SendMobileMsg();
			}
			if (this.chkDbsj.Checked)
			{
				this.SendDbsj(dictionary);
			}
			this.hfldMailId.Value = value;
			base.RegisterScript("selectSend();");
			return;
		}
		base.RegisterScript("alert('收件人不能为空!');");
	}
	protected void btnSaveDraft_Click(object sender, System.EventArgs e)
	{
		Mail mail;
		if (!string.IsNullOrEmpty(this.mailId) && this.edit == "1")
		{
			mail = this.mailService.GetById(this.mailId);
		}
		else
		{
			if (!string.IsNullOrEmpty((string)this.ViewState["mailId"]))
			{
				mail = this.mailService.GetById(this.ViewState["mailId"].ToString());
				this.mailId = mail.MailId;
				this.edit = "1";
			}
			else
			{
				mail = new Mail();
				mail.MailId = System.Guid.NewGuid().ToString();
				this.ViewState["mailId"] = mail.MailId;
				this.mailId = mail.MailId;
				this.edit = "1";
				mail.ToMailId = System.Guid.NewGuid().ToString();
			}
		}
		mail.MailName = this.txtName.Text.Trim();
		mail.MailFrom = base.UserCode;
		mail.MailTo = base.UserCode;
		mail.AllMailToCode = this.hfldTo.Value;
		mail.AllMailTo = this.txtTo.Text;
		mail.AllCopytoCode = this.hfldCopyto.Value;
		mail.AllCopyto = this.txtCopyto.Text;
		mail.MailContent = this.txtContent.Value;
		mail.IsValid = true;
		mail.IsReaded = false;
		mail.MailType = "D";
		mail.AnnexId = (string)this.ViewState["annexId"];
		mail.InputDate = System.DateTime.Now;
		this.mailService.AddOrUpdate(mail);
		this.lblsave.Text = "保存成功！";
		this.MailDatabind();
		base.RegisterShow("系统提示", "保存成功");
	}
	private new void Init()
	{
		if (!string.IsNullOrEmpty(this.mailId))
		{
			this.MailDatabind();
		}
		else
		{
			this.ViewState["annexId"] = System.Guid.NewGuid().ToString();
			this.hfldFolder.Value = OA2_WriteMail.folder + "/" + (string)this.ViewState["annexId"];
		}
		this.lblsave.Text = string.Empty;
	}
	private System.Collections.Generic.IList<string> GetUserList()
	{
		return (
			from s in this.hfldTo.Value.Split(new char[]
			{
				','
			})
			where s.Length == 8
			select s).ToList<string>().Union(
			from s in this.hfldCopyto.Value.Split(new char[]
			{
				','
			})
			where s.Length == 8
			select s).ToList<string>();
	}
	private void SendMobileMsg()
	{
		System.Collections.Generic.IList<string> userList = this.GetUserList();
		new PTYhmcService();
		foreach (string current in userList)
		{
			SMS sMS = new SMS();
			DataTable dataTable = publicDbOpClass.DataTableQuary("select MobilePhoneCode from pt_yhmc where v_yhdm='" + current + "'");
			if (dataTable.Rows.Count > 0)
			{
				if (!(bool)sMS.Send(WebUtil.GetUserNames(current), dataTable.Rows[0][0].ToString(), "给您发送了一封邮件:" + this.txtName.Text.Trim(), "", "", "")[0])
				{
					base.RegisterShow("系统提示", "发送失败");
				}
			}
			else
			{
				base.RegisterShow("系统提示", "收件号为空");
			}
		}
	}
	private void SendDbsj(System.Collections.Generic.Dictionary<string, string> userCodeMailId)
	{
		this.GetUserList();
		PTDBSJService pTDBSJService = new PTDBSJService();
		foreach (System.Collections.Generic.KeyValuePair<string, string> current in userCodeMailId)
		{
			PTDBSJ pTDBSJ = new PTDBSJ();
			pTDBSJ.I_XGID = current.Key;
			pTDBSJ.V_LXBM = "008";
			pTDBSJ.V_YHDM = current.Key;
			pTDBSJ.DTM_DBSJ = new System.DateTime?(System.DateTime.Now);
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.txtName.Text))
			{
				text = this.txtName.Text;
			}
			else
			{
				text = "【无主题】";
			}
			if (text.Length > 93)
			{
				text = text.Substring(0, 90) + "...";
			}
			pTDBSJ.V_Content = "您有新邮件(" + text + ")";
			pTDBSJ.V_TPLJ = "new_Mail.gif";
			pTDBSJ.V_DBLJ = "OA2/Mail/MailFrame.aspx?mailId=" + current.Value;
			pTDBSJ.C_OpenFlag = "0";
			pTDBSJService.Add(pTDBSJ);
		}
	}
	private void MailDatabind()
	{
		Mail byId = this.mailService.GetById(this.mailId);
		if (this.edit == "0" && this.Transmit == "0")
		{
			this.txtTo.Text = byId.MailFromYhmc.v_xm + ",";
			this.hfldTo.Value = byId.MailFrom + ",";
			string str = string.Empty;
			if (!string.IsNullOrEmpty(byId.MailName))
			{
				str = byId.MailName;
			}
			else
			{
				str = "【无主题】";
			}
			this.txtName.Text = "回复：" + str;
			this.txtContent.Value = "<div style='height: 50px; overflow: hidden;'><table height='50px'><tr><td></td></tr></table></div>";
			HtmlTextArea expr_C5 = this.txtContent;
			object value = expr_C5.Value;
			expr_C5.Value = string.Concat(new object[]
			{
				value,
				"<div><div style=' background: #EFEFEF;'>----------原始邮件----------</br>发件人:",
				this.txtTo.Text,
				"</br>发送时间:",
				byId.InputDate,
				"</br>收件人:",
				byId.MailToYhmc.v_xm,
				"</br>主题:",
				byId.MailName,
				"</br></div>",
				byId.MailContent,
				"</br></div>"
			});
			this.ViewState["annexId"] = System.Guid.NewGuid().ToString();
			this.hfldFolder.Value = OA2_WriteMail.folder + "/" + (string)this.ViewState["annexId"];
			return;
		}
		if (this.edit == "1" && this.Transmit == "0")
		{
			this.txtTo.Text = byId.AllMailTo;
			this.hfldTo.Value = byId.AllMailToCode;
			this.txtName.Text = byId.MailName;
			this.txtCopyto.Text = byId.AllCopyto;
			this.hfldCopyto.Value = byId.AllCopytoCode;
			this.txtContent.Value = byId.MailContent;
			if (!string.IsNullOrEmpty(byId.AnnexId))
			{
				this.ViewState["annexId"] = byId.AnnexId;
				this.hfldFolder.Value = OA2_WriteMail.folder + "/" + (string)this.ViewState["annexId"];
				try
				{
					string text = ConfigHelper.Get("Mail").ToString();
					System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(base.Server.MapPath(text) + "/" + byId.AnnexId.ToString());
					System.IO.FileInfo[] files = directoryInfo.GetFiles();
					System.IO.FileInfo[] array = files;
					for (int i = 0; i < array.Length; i++)
					{
						System.IO.FileInfo fileInfo = array[i];
						base.RegisterScript(string.Concat(new object[]
						{
							"uploadbind('",
							fileInfo.Name,
							"','",
							System.Math.Round((double)fileInfo.Length / 1024.0 / 1024.0, 2, System.MidpointRounding.AwayFromZero),
							"M','",
							text,
							"/",
							byId.AnnexId.ToString(),
							"');"
						}));
					}
					return;
				}
				catch
				{
					return;
				}
			}
			this.ViewState["annexId"] = System.Guid.NewGuid().ToString();
			this.hfldFolder.Value = OA2_WriteMail.folder + "/" + (string)this.ViewState["annexId"];
			return;
		}
		if (this.edit == "0" && this.Transmit == "1")
		{
			string text2 = string.Empty;
			if (!string.IsNullOrEmpty(byId.MailName))
			{
				text2 = "转发：" + byId.MailName;
			}
			else
			{
				text2 = "转发：【无主题】";
			}
			this.txtName.Text = text2;
			this.txtContent.Value = "<div style='height: 50px; overflow: hidden;'><table height='50px'><tr><td></td></tr></table></div>";
			HtmlTextArea expr_467 = this.txtContent;
			object value2 = expr_467.Value;
			expr_467.Value = string.Concat(new object[]
			{
				value2,
				"<div><div style=' background: #EFEFEF;'>----------原始邮件----------</br>发件人:",
				byId.MailFromYhmc.v_xm,
				"</br>发送时间:",
				byId.InputDate,
				"</br>收件人:",
				byId.MailToYhmc.v_xm,
				"</br>主题:",
				byId.MailName,
				"</br></div>",
				byId.MailContent,
				"</br></div>"
			});
			if (!string.IsNullOrEmpty(byId.AnnexId))
			{
				this.ViewState["annexId"] = byId.AnnexId;
				this.hfldFolder.Value = OA2_WriteMail.folder + "/" + (string)this.ViewState["annexId"];
				try
				{
					string text3 = ConfigHelper.Get("Mail").ToString();
					System.IO.DirectoryInfo directoryInfo2 = new System.IO.DirectoryInfo(base.Server.MapPath(text3) + "/" + byId.AnnexId.ToString());
					System.IO.FileInfo[] files2 = directoryInfo2.GetFiles();
					System.IO.FileInfo[] array2 = files2;
					for (int j = 0; j < array2.Length; j++)
					{
						System.IO.FileInfo fileInfo2 = array2[j];
						base.RegisterScript(string.Concat(new object[]
						{
							"uploadbind('",
							fileInfo2.Name,
							"','",
							System.Math.Round((double)fileInfo2.Length / 1024.0 / 1024.0, 2, System.MidpointRounding.AwayFromZero),
							"M','",
							text3,
							"/",
							byId.AnnexId.ToString(),
							"');"
						}));
					}
					return;
				}
				catch
				{
					return;
				}
			}
			this.ViewState["annexId"] = System.Guid.NewGuid().ToString();
			this.hfldFolder.Value = OA2_WriteMail.folder + "/" + (string)this.ViewState["annexId"];
		}
	}
	private string FilesBind(string recordCode)
	{
		string text = ConfigHelper.Get("Mail").ToString();
		string result;
		try
		{
			string[] files = System.IO.Directory.GetFiles(base.Server.MapPath(text) + "/" + recordCode);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = text + "/" + recordCode;
				string str2 = str + "/" + text3;
				text3 = string.Concat(new string[]
				{
					"<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
					HttpUtility.UrlEncode(str2),
					"\"  >",
					text3,
					"</a>"
				});
				stringBuilder.Append(text3);
				stringBuilder.Append(", ");
			}
			string text4 = string.Empty;
			if (stringBuilder.Length > 2)
			{
				text4 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
			}
			result = text4;
		}
		catch
		{
			result = "";
		}
		return result;
	}
}


