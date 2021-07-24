using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class Annex : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		private string _strSenderCode = "";


		protected void Page_Load(object sender, EventArgs e)
		{
			this._strSenderCode = this.Session["yhdm"].ToString();
			if (!base.IsPostBack)
			{
				this.GetTempAnnex();
			}
		}
		private void GetTempAnnex()
		{
			int num = 0;
			int num2 = 2147483647;
			this.LBoxAnnex.Items.Clear();
			MailManage mailManage = new MailManage();
			DataTable tempAnnex = mailManage.GetTempAnnex(this._strSenderCode);
			foreach (DataRow dataRow in tempAnnex.Rows)
			{
				this.LBoxAnnex.Items.Add(new System.Web.UI.WebControls.ListItem(dataRow["v_AnnexName"].ToString(), dataRow["i_fj_id"].ToString()));
				num += int.Parse(dataRow["i_AnnexSize"].ToString());
			}
			if (num >= num2)
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('该邮件的附件总大小已经超过10M限制，请进行处理，否则该邮件无法发送！');</script>");
			}
		}
		protected void BtnAddAnnex_Click(object sender, EventArgs e)
		{
			string arg_10_0 = this.fileAnnex.PostedFile.FileName;
			MailManage mailManage = new MailManage();
			if (!mailManage.UpLoadAnnex(this.fileAnnex.PostedFile, this._strSenderCode))
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT languange=\"JavaScript\">alert('" + mailManage.MessageString + "')</SCRIPT>");
				return;
			}
			this.GetTempAnnex();
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			if (this.LBoxAnnex.SelectedIndex > -1 && this.LBoxAnnex.Items.Count > 0)
			{
				int iAnnexID = int.Parse(this.LBoxAnnex.SelectedValue.ToString());
				MailManage mailManage = new MailManage();
				if (!mailManage.DelTempAnnex(iAnnexID, false))
				{
					this.RegisterClientScriptBlock("warn", "<SCRIPT languange=\"JavaScript\">alert('" + mailManage.MessageString + "')</SCRIPT>");
					return;
				}
				this.LBoxAnnex.Items.RemoveAt(this.LBoxAnnex.SelectedIndex);
			}
		}
		private void RemoveFromSession(int iIndex)
		{
			string[] array = this.Session["Annex"].ToString().Split(new char[]
			{
				','
			});
			array[iIndex] = "";
			this.Session["Annex"] = "";
			if (array.Length > 2)
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
					System.Web.SessionState.HttpSessionState session;
					(session = this.Session)["Annex"] = session["Annex"] + array[i] + ",";
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
	}


