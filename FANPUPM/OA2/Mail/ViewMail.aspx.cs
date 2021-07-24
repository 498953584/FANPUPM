using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OA2_ViewMail : NBasePage, IRequiresSessionState
{
	private string mailId = string.Empty;
	private string reply = "0";
	private MailService mailService = new MailService();
	private string mailTY = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["mailId"]))
		{
			this.mailId = base.Request["mailId"];
			this.hfldMailId.Value = this.mailId;
		}
		if (!string.IsNullOrEmpty(base.Request["reply"]))
		{
			this.reply = base.Request["reply"];
		}
		if (!string.IsNullOrEmpty(base.Request["mailTY"]))
		{
			this.mailTY = base.Request["mailTY"];
			this.hfldMailTY.Value = base.Request["mailTY"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty((string)this.ViewState["mailId"]))
		{
			this.mailId = (string)this.ViewState["mailId"];
		}
		if (!base.IsPostBack)
		{
			this.Init();
		}
	}
	private new void Init()
	{
		if (this.reply == "1")
		{
			this.btnEdit.Visible = true;
			this.btnEdit1.Visible = true;
			this.btnTransmit.Visible = true;
			this.btnTransmit1.Visible = true;
		}
		else
		{
			this.btnEdit.Visible = false;
			this.btnEdit1.Visible = false;
			this.btnTransmit.Visible = false;
			this.btnTransmit1.Visible = false;
		}
		this.BindDrop();
		this.Visiable();
		Mail byId = this.mailService.GetById(this.mailId);
		if (!string.IsNullOrEmpty(byId.AnnexId))
		{
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
						"uploadbind('<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
						HttpUtility.UrlEncode(string.Concat(new string[]
						{
							text,
							"/",
							byId.AnnexId.ToString(),
							"/",
							fileInfo.Name
						})),
						"\"  >",
						fileInfo.Name,
						"</a>','",
						System.Math.Round((double)fileInfo.Length / 1024.0 / 1024.0, 2, System.MidpointRounding.AwayFromZero),
						"M');"
					}));
				}
				goto IL_1D9;
			}
			catch
			{
				this.TRUpload.Visible = false;
				goto IL_1D9;
			}
		}
		this.TRUpload.Visible = false;
		IL_1D9:
		if (byId != null)
		{
			string value = byId.MailContent.ToString();
			this.txtContent.Value = value;
			if (!string.IsNullOrEmpty(byId.MailName))
			{
				this.lblTitle.Text = byId.MailName;
			}
			else
			{
				this.lblTitle.Text = "【无主题】";
			}
			this.lblFrom.Text = byId.MailFromYhmc.v_xm;
			this.lblTo.Text = byId.AllMailTo.TrimEnd(new char[]
			{
				','
			});
			if (string.IsNullOrEmpty(byId.AllCopyto))
			{
				this.TRMailTo.Visible = false;
			}
			else
			{
				this.TRMailTo.Visible = true;
			}
			this.lblMailTo.Text = byId.AllCopyto.TrimEnd(new char[]
			{
				','
			});
			this.lblDate.Text = byId.InputDate.ToString();
			if (this.reply == "1")
			{
				if (!byId.IsReaded)
				{
					byId.IsReaded = true;
					byId.ReadTime = new System.DateTime?(System.DateTime.Now);
					this.mailService.Update(byId);
				}
				string strToMailId = byId.ToMailId;
				System.Collections.Generic.IEnumerable<Mail> source = this.mailService.AsEnumerable<Mail>();
				if (!string.IsNullOrEmpty(strToMailId))
				{
					source =
						from m in source
						where m.ToMailId.Contains(strToMailId) && m.MailType == "O"
						select m;
				}
				System.Collections.Generic.List<Mail> list = source.ToList<Mail>();
				if (list.Count > 0)
				{
					list[0].IsReaded = true;
					list[0].ReadTime = new System.DateTime?(System.DateTime.Now);
					this.mailService.Update(list);
				}
			}
		}
	}
	public string FilesBind(string recordCode)
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
				string text4 = System.Math.Round((double)text2.Length / 1024.0, 2, System.MidpointRounding.AwayFromZero) + "kb";
				text3 = string.Concat(new string[]
				{
					"<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
					HttpUtility.UrlEncode(str2),
					"\"  >",
					text3,
					"</a>;&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp",
					text4
				});
				stringBuilder.Append(text3);
				stringBuilder.Append(", ");
			}
			string text5 = string.Empty;
			if (stringBuilder.Length > 2)
			{
				text5 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
				this.TRUpload.Visible = true;
			}
			else
			{
				this.TRUpload.Visible = false;
			}
			result = text5;
		}
		catch
		{
			result = "";
		}
		return result;
	}
	private void BindDrop()
	{
		this.DDLtBox.Items.Clear();
		this.DDLtBox.Items.Add(new ListItem("草稿箱", "D"));
		if (this.mailTY != "L")
		{
			this.DDLtBox.Items.Add(new ListItem("已删除邮件", "L"));
		}
		if (this.mailTY == "L" || this.mailTY == "O")
		{
			this.DDLtBox.Items.Add(new ListItem("收件箱", "I"));
		}
		this.DDLtBox1.Items.Clear();
		this.DDLtBox1.Items.Add(new ListItem("草稿箱", "D"));
		if (this.mailTY != "L")
		{
			this.DDLtBox1.Items.Add(new ListItem("已删除邮件", "L"));
		}
		if (this.mailTY == "L" || this.mailTY == "O")
		{
			this.DDLtBox1.Items.Add(new ListItem("收件箱", "I"));
		}
	}
	public void Visiable()
	{
		Mail byId = this.mailService.GetById(this.mailId);
		this.ViewState["startDate"] = byId.InputDate.AddSeconds(1.0);
		this.ViewState["endDate"] = byId.InputDate.AddSeconds(-1.0);
		if (this.reply == "1")
		{
			this.ViewState["count"] = this.mailService.GetCount("", "", "", WebUtil.GetUserNames(base.UserCode), new System.DateTime?((System.DateTime)this.ViewState["startDate"]), null, "I", new bool?(true), null);
		}
		if (this.mailTY == "O")
		{
			this.ViewState["count"] = this.mailService.GetCount("", "", WebUtil.GetUserNames(base.UserCode), "", new System.DateTime?((System.DateTime)this.ViewState["startDate"]), null, "O", new bool?(true), null);
		}
		if (this.mailTY == "L")
		{
			IQueryable<Mail> source = this.mailService.AsQueryable<Mail>();
			source =
				from m in source
				where m.IsValid == false && ((m.MailType == "I" && m.MailTo == this.UserCode) || (m.MailType == "O" && m.MailFrom == this.UserCode))
				select m;
			source =
				from m in source
				where m.InputDate >= (System.DateTime)this.ViewState["startDate"]
				select m;
			source =
				from m in source
				orderby m.InputDate descending
				select m;
			this.ViewState["count"] = source.Count<Mail>();
		}
		if ((int)this.ViewState["count"] > 0)
		{
			this.btnFront.Enabled = true;
			this.btnFront1.Enabled = true;
		}
		else
		{
			this.btnFront.Enabled = false;
			this.btnFront1.Enabled = false;
		}
		if (this.reply == "1")
		{
			this.ViewState["countN"] = this.mailService.GetCount("", "", "", WebUtil.GetUserNames(base.UserCode), null, new System.DateTime?((System.DateTime)this.ViewState["endDate"]), "I", new bool?(true), null);
		}
		if (this.mailTY == "O")
		{
			this.ViewState["countN"] = this.mailService.GetCount("", "", WebUtil.GetUserNames(base.UserCode), "", null, new System.DateTime?((System.DateTime)this.ViewState["endDate"]), "O", new bool?(true), null);
		}
		if (this.mailTY == "L")
		{
			IQueryable<Mail> source2 = this.mailService.AsQueryable<Mail>();
			source2 =
				from m in source2
				where m.IsValid == false && ((m.MailType == "I" && m.MailTo == this.UserCode) || (m.MailType == "O" && m.MailFrom == this.UserCode))
				select m;
			source2 =
				from m in source2
				where m.InputDate <= (System.DateTime)this.ViewState["endDate"]
				select m;
			source2 =
				from m in source2
				orderby m.InputDate descending
				select m;
			this.ViewState["countN"] = source2.Count<Mail>();
		}
		if ((int)this.ViewState["countN"] > 0)
		{
			this.btnbehind.Enabled = true;
			this.btnbehind1.Enabled = true;
			return;
		}
		this.btnbehind.Enabled = false;
		this.btnbehind1.Enabled = false;
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		try
		{
			if (this.mailTY == "L")
			{
				string empty = string.Empty;
				IQueryable<Mail> source = this.mailService.AsQueryable<Mail>();
				source =
					from m in source
					where m.IsValid == false && ((m.MailType == "I" && m.MailTo == this.UserCode) || (m.MailType == "O" && m.MailFrom == this.UserCode))
					select m;
				source =
					from m in source
					where m.InputDate <= (System.DateTime)this.ViewState["endDate"]
					select m;
				source =
					from m in source
					orderby m.InputDate descending
					select m;
				int? num = new int?((int)this.ViewState["countN"]);
				int? num2 = new int?(1);
				if (num.HasValue && num2.HasValue)
				{
					source = source.Skip((num2.Value - 1) * num.Value).Take(num.Value);
				}
				System.Collections.Generic.IList<Mail> list = source.ToList<Mail>();
				if (list.Count > 0)
				{
					empty = list[0].MailId;
				}
				this.mailService.Delete(this.mailId);
				this.ViewState["mailId"] = empty;
				this.mailId = empty;
			}
			else
			{
				string empty2 = string.Empty;
				if (this.reply == "1")
				{
					System.Collections.Generic.IList<Mail> list2 = this.mailService.Query("", "", "", WebUtil.GetUserNames(base.UserCode), null, new System.DateTime?((System.DateTime)this.ViewState["endDate"]), "I", new bool?(true), null, new int?((int)this.ViewState["countN"]), new int?(1));
					if (list2.Count > 0)
					{
						empty2 = list2[0].MailId;
					}
				}
				if (this.mailTY == "O")
				{
					System.Collections.Generic.IList<Mail> list3 = this.mailService.Query("", "", WebUtil.GetUserNames(base.UserCode), "", null, new System.DateTime?((System.DateTime)this.ViewState["endDate"]), "O", new bool?(true), null, new int?((int)this.ViewState["countN"]), new int?(1));
					if (list3.Count > 0)
					{
						empty2 = list3[0].MailId;
					}
				}
				Mail byId = this.mailService.GetById(this.mailId);
				byId.IsValid = false;
				byId.InputDate = System.DateTime.Now;
				this.mailService.Update(byId);
				this.ViewState["mailId"] = empty2;
				this.mailId = empty2;
			}
			if (!string.IsNullOrEmpty(this.mailId))
			{
				this.Init();
			}
			else
			{
				base.RegisterScript("selectSend();");
			}
			base.RegisterShow("系统提示", "删除成功");
		}
		catch
		{
			base.RegisterShow("系统提示", "删除失败");
		}
	}
	protected void btnFront_Click(object sender, System.EventArgs e)
	{
		if (this.reply == "1")
		{
			System.Collections.Generic.IList<Mail> list = this.mailService.Query("", "", "", WebUtil.GetUserNames(base.UserCode), new System.DateTime?((System.DateTime)this.ViewState["startDate"]), null, "I", new bool?(true), null, new int?((int)this.ViewState["count"]), new int?(1));
			this.ViewState["mailId"] = list[(int)this.ViewState["count"] - 1].MailId;
			this.mailId = list[(int)this.ViewState["count"] - 1].MailId;
		}
		if (this.mailTY == "O")
		{
			System.Collections.Generic.IList<Mail> list2 = this.mailService.Query("", "", WebUtil.GetUserNames(base.UserCode), "", new System.DateTime?((System.DateTime)this.ViewState["startDate"]), null, "O", new bool?(true), null, new int?((int)this.ViewState["count"]), new int?(1));
			this.ViewState["mailId"] = list2[(int)this.ViewState["count"] - 1].MailId;
			this.mailId = list2[(int)this.ViewState["count"] - 1].MailId;
		}
		if (this.mailTY == "L")
		{
			IQueryable<Mail> source = this.mailService.AsQueryable<Mail>();
			source =
				from m in source
				where m.IsValid == false && ((m.MailType == "I" && m.MailTo == this.UserCode) || (m.MailType == "O" && m.MailFrom == this.UserCode))
				select m;
			source =
				from m in source
				where m.InputDate >= (System.DateTime)this.ViewState["startDate"]
				select m;
			source =
				from m in source
				orderby m.InputDate descending
				select m;
			int? num = new int?((int)this.ViewState["count"]);
			int? num2 = new int?(1);
			if (num.HasValue && num2.HasValue)
			{
				source = source.Skip((num2.Value - 1) * num.Value).Take(num.Value);
			}
			System.Collections.Generic.IList<Mail> list3 = source.ToList<Mail>();
			this.ViewState["mailId"] = list3[(int)this.ViewState["count"] - 1].MailId;
			this.mailId = list3[(int)this.ViewState["count"] - 1].MailId;
		}
		this.Init();
	}
	protected void btnbehind_Click(object sender, System.EventArgs e)
	{
		if (this.reply == "1")
		{
			System.Collections.Generic.IList<Mail> list = this.mailService.Query("", "", "", WebUtil.GetUserNames(base.UserCode), null, new System.DateTime?((System.DateTime)this.ViewState["endDate"]), "I", new bool?(true), null, new int?((int)this.ViewState["countN"]), new int?(1));
			this.ViewState["mailId"] = list[0].MailId;
			this.mailId = list[0].MailId;
		}
		if (this.mailTY == "O")
		{
			System.Collections.Generic.IList<Mail> list2 = this.mailService.Query("", "", WebUtil.GetUserNames(base.UserCode), "", null, new System.DateTime?((System.DateTime)this.ViewState["endDate"]), "O", new bool?(true), null, new int?((int)this.ViewState["countN"]), new int?(1));
			this.ViewState["mailId"] = list2[0].MailId;
			this.mailId = list2[0].MailId;
		}
		if (this.mailTY == "L")
		{
			IQueryable<Mail> source = this.mailService.AsQueryable<Mail>();
			source =
				from m in source
				where m.IsValid == false && ((m.MailType == "I" && m.MailTo == this.UserCode) || (m.MailType == "O" && m.MailFrom == this.UserCode))
				select m;
			source =
				from m in source
				where m.InputDate <= (System.DateTime)this.ViewState["endDate"]
				select m;
			source =
				from m in source
				orderby m.InputDate descending
				select m;
			int? num = new int?((int)this.ViewState["countN"]);
			int? num2 = new int?(1);
			if (num.HasValue && num2.HasValue)
			{
				source = source.Skip((num2.Value - 1) * num.Value).Take(num.Value);
			}
			System.Collections.Generic.IList<Mail> list3 = source.ToList<Mail>();
			this.ViewState["mailId"] = list3[0].MailId;
			this.mailId = list3[0].MailId;
		}
		this.Init();
	}
	protected void btnMove_Click(object sender, System.EventArgs e)
	{
		try
		{
			Mail byId = this.mailService.GetById(this.mailId);
			if (this.DDLtBox.SelectedValue == "L")
			{
				byId.IsValid = false;
				byId.InputDate = System.DateTime.Now;
			}
			else
			{
				if (this.DDLtBox.SelectedValue == "D")
				{
					byId.MailType = "D";
					byId.IsValid = true;
					byId.MailFrom = base.UserCode;
					byId.InputDate = System.DateTime.Now;
				}
				else
				{
					if (this.DDLtBox.SelectedValue == "I")
					{
						byId.ToMailId = System.Guid.NewGuid().ToString().Trim();
						byId.MailType = "I";
						byId.IsValid = true;
						byId.IsReaded = false;
						byId.MailTo = base.UserCode;
						byId.MailFrom = base.UserCode;
						byId.AllMailTo = WebUtil.GetUserNames(base.UserCode);
						byId.AllMailToCode = base.UserCode;
						byId.AllCopyto = string.Empty;
						byId.AllCopytoCode = string.Empty;
						byId.InputDate = System.DateTime.Now;
						byId.ReadTime = null;
						Mail mail = new Mail();
						mail.MailId = System.Guid.NewGuid().ToString().Trim();
						mail.ToMailId = byId.ToMailId;
						mail.MailName = byId.MailName;
						mail.MailFrom = base.UserCode;
						mail.MailTo = base.UserCode;
						mail.AllCopyto = string.Empty;
						mail.AllCopytoCode = string.Empty;
						mail.AllMailTo = WebUtil.GetUserNames(base.UserCode);
						mail.AllMailToCode = base.UserCode;
						mail.AnnexId = byId.AnnexId;
						mail.InputDate = System.DateTime.Now.AddSeconds(1.0);
						mail.IsReaded = false;
						mail.IsValid = true;
						mail.MailContent = byId.MailContent;
						mail.MailType = "O";
						this.mailService.Add(mail);
					}
				}
			}
			this.mailService.Update(byId);
			base.RegisterShow("系统提示", "转移成功");
			base.RegisterScript("selectSend()");
		}
		catch
		{
			base.RegisterShow("系统提示", "转移失败");
		}
	}
}


