using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.sysManage.common;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class delloglist : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			new com.jwsoft.sysManage.common.Calendar(this.Page);
			this.tbDate.Attributes["onclick"] = "opencalendar(this)";
			this.tbDate.Attributes["onkeydown"] = "return false;";
			this.Label1.Text = "超级删除日志列表";
			if (!this.Page.IsPostBack)
			{
				DataTable twoPWDlog = myxml.GetTwoPWDlog();
				this.dgLogList.DataSource = twoPWDlog;
				this.dgLogList.DataBind();
				this.tbDate.Text = DateTime.Now.ToString("d");
				this.btnDel.Attributes["onclick"] = "if(!confirm('真的要删除吗？'))return false;";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgLogList.PageIndexChanged += new DataGridPageChangedEventHandler(this.dgLogList_PageIndexChanged);
		}
		private void dgLogList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgLogList.CurrentPageIndex = e.NewPageIndex;
			DataTable twoPWDlog = myxml.GetTwoPWDlog();
			try
			{
				this.dgLogList.DataSource = twoPWDlog;
				this.dgLogList.DataBind();
			}
			catch (Exception)
			{
				this.dgLogList.DataSource = twoPWDlog;
				this.dgLogList.DataBind();
				this.dgLogList.CurrentPageIndex = 0;
			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			string text = this.tbDate.Text;
			DataTable twoPWDlog = myxml.GetTwoPWDlog();
			bool flag = myxml.deladmindelInfo(text);
			if (flag)
			{
				this.dgLogList.DataSource = twoPWDlog;
				this.dgLogList.CurrentPageIndex = 0;
				this.dgLogList.DataBind();
				return;
			}
			this.js.Text = "alert('删除失败！');";
		}
	}


