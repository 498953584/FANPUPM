using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Net;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SelectedDept : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack && base.Request.QueryString["sysid"] != null)
			{
				int num = Convert.ToInt32(base.Request.QueryString["sysid"].ToString());
				string text = base.Request.QueryString["sysName"].ToString();
				ListItem listItem = new ListItem(text, num.ToString());
				listItem.Selected = true;
				this.LBoxSelectedMan.Items.Add(listItem);
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
		protected void Button1_ServerClick(object sender, EventArgs e)
		{
			if (this.LBoxSelectedMan.Items.Count == 0)
			{
				return;
			}
			SysManageDb sysManageDb = new SysManageDb();
			DataTable dataTable = sysManageDb.QuerySys(Convert.ToInt32(this.LBoxSelectedMan.SelectedValue));
			WebRequest webRequest = WebRequest.Create(dataTable.Rows[0]["v_addressURL"].ToString());
			webRequest.Timeout = 1000;
			try
			{
				webRequest.GetResponse();
			}
			catch
			{
				this.RegisterClientScriptBlock("script", "<script language=javascript>alert('网络连接超时!');</script>");
				this.LBoxSelectedMan.Items.Clear();
				return;
			}
			this.RegisterClientScriptBlock("script", "<script language=javascript>window.close();</script>");
		}
	}


